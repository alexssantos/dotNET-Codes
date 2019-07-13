// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomFieldTypes.cs" company="David Bevin">
//   Copyright (c) 2015 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;

using ServiceStack.Text;

namespace JIRC.Domain
{
    /// <summary>
    /// A read-only dictionary of custom fields with support for registering converters.
    /// </summary>
    /// <remarks>
    /// Use a specially designated class such as <see cref="JiraIssueRestClient"/> for editing
    /// custom fields depending on the object that owns this instance of <see cref="CustomFieldTypes"/>.
    /// Enumeration or counting of custom fields is not supported.
    /// Indexer returns null for null fields and fields that don't exist.
    /// </remarks>
    public class CustomFieldTypes
    {
        /// <summary>
        /// All custom fields are expected to start with this string.
        /// </summary>
        private const string CustomFieldPrefix = "customfield";

        /// <summary>
        /// Converters for setting or retrieving custom field values transmitted as Json.
        /// </summary>
        private static Dictionary<string, Tuple<Func<string, object>, Func<object, string>>> converters =
            new Dictionary<string, Tuple<Func<string, object>, Func<object, string>>>();

        /// <summary>
        /// Custom field values retrieved from Json.
        /// </summary>
        private Dictionary<string, object> values;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFieldTypes"/> class.
        /// </summary>
        /// <remarks>Do not use this constructor except for unit tests.</remarks>
        internal CustomFieldTypes()
        {
            this.values = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFieldTypes"/> class.
        /// </summary>
        /// <param name="container">Serialized representation of a Jira class that can contain custom fields, such as <see cref="Issue"/>.</param>
        internal CustomFieldTypes(JsonObject container)
        {
            this.values = new Dictionary<string, object>();
            this.Parse(container);
        }

        /// <summary>
        /// Register a converter for setting and retrieving any custom field with specified Id.
        /// </summary>
        /// <param name="customFieldName">Id of an Issue field to set and retrieve using the supplied converter.</param>
        /// <param name="parser">Function used to parse the custom field value from serialized Json string to an object.</param>
        /// <param name="serializer">Function used to serialize the custom field value from object to a Json string.</param>
        public static void Register(string customFieldName, Func<string, object> parser, Func<object, string> serializer)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(customFieldName) || !customFieldName.StartsWith(CustomFieldPrefix))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "customFieldName must be a non-empty string that starts with '{0}'.", CustomFieldPrefix), "customFieldName");
            }

            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }

            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }

            // Register the new converter replacing a previous one for the same field Id if already registered.
            converters[customFieldName] = new Tuple<Func<string, object>, Func<object, string>>(parser, serializer);
        }

        /// <summary>
        /// Checks whether a custom field exists.
        /// </summary>
        /// <param name="customFieldName">Name of the custom field.</param>
        /// <returns>Whether the specified custom field exists.</returns>
        public bool Exists(string customFieldName)
        {
            return this.values.ContainsKey(customFieldName);
        }

        /// <summary>
        /// Gets a custom field.
        /// </summary>
        /// <param name="customFieldName">Name of the custom field to get.</param>
        /// <returns>Custom field value, or null if it doesn't exist.</returns>
        public object this[string customFieldName]
        {
            get
            {
                object result;

                if (this.values == null || !this.values.TryGetValue(customFieldName, out result))
                {
                    return null;
                }

                return result;
            }

            internal set
            {
                // Used for unit tests.
                if (this.values == null)
                {
                    this.values = new Dictionary<string, object>();
                }

                this.values[customFieldName] = value;
            }
        }

        /// <summary>
        /// Serialize a single custom field using registered converters.
        /// </summary>
        /// <param name="customFieldName">The name of a custom field to serialize.</param>
        /// <param name="value">Original field value.</param>
        /// <returns>Field value serialized as a literal string or a Json object as a string.</returns>
        internal static string Serialize(string customFieldName, object value)
        {
            // Attempt to find a serializer for this field.
            Tuple<Func<string, object>, Func<object, string>> converter;

            if (customFieldName.StartsWith(CustomFieldPrefix) &&
                converters.TryGetValue(customFieldName, out converter))
            {
                // Convert field value to a Json string using a registered serializer.
                return converter.Item2(value);
            }
            else
            {
                // Perform default conversion.
                if (value == null)
                {
                    // Null custom field.
                    return null;
                }
                else
                {
                    // String literal custom field or object type dumped to a string for any other type.
                    return value.ToString();
                }
            }
        }

        /// <summary>
        /// Parse custom fields from a JSON container using registered converters.
        /// </summary>
        /// <param name="container">Serialized representation of a Jira class that can contain custom fields, such as <see cref="Issue"/>.</param>
        internal void Parse(JsonObject container)
        {
            foreach (var field in container)
            {
                if (field.Key.StartsWithIgnoreCase(CustomFieldPrefix))
                {
                    // Attempt to find a converter for this field.
                    Tuple<Func<string, object>, Func<object, string>> converter;

                    if (converters.TryGetValue(field.Key, out converter))
                    {
                        // Convert field value to object using a registered converter.
                        this.values[field.Key] = converter.Item1(field.Value);
                    }
                    else
                    {
                        // Perform default conversion.
                        if (field.Value == null)
                        {
                            // Null custom field.
                            this.values[field.Key] = null;
                        }
                        else if (field.Value.StartsWith("{"))
                        {
                            // Simple custom field with main 'value' property that contains the value.
                            this.values[field.Key] = container.Get<JsonObject>(field.Key).Get<string>("value");
                        }
                        else
                        {
                            // String literal custom field.
                            this.values[field.Key] = field.Value;
                        }
                    }
                }
            }
        }
    }
}