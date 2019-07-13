// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CimFieldsInfoMapJsonParser.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using JIRC.Domain;

using ServiceStack.Text;

namespace JIRC.Internal.Json
{
    /// <summary>
    /// Parses custom meta-data fields used in <see cref="CimIssue"/> class.
    /// </summary>
    /// <remarks>
    /// See <see cref="CustomFieldTypes"/> for parsing custom fields in <see cref="Issue"/> class that does not represent meta-data.
    /// </remarks>
    internal static class CimFieldsInfoMapJsonParser
    {
        private static readonly Dictionary<string, Func<JsonObject, object>> RegisteredAllowedValueParsers = new Dictionary<string, Func<JsonObject, object>>
        {
            { "project", CustomJsonSerializer.BasicProjectJsonParser },
            { "version", CustomJsonSerializer.JiraVersionJsonParser },
            { "issuetype", CustomJsonSerializer.BasicIssueTypeJsonParser },
            { "priority", CustomJsonSerializer.BasicPriorityJsonParser },
            // Insert custom allowed value parsers here. Example: { "customMetaDataFieldOption", CustomMetaDataFieldOptionJsonParser.Parse },
            { "component", CustomJsonSerializer.ComponentJsonParser },
            { "resolution", CustomJsonSerializer.BasicResolutionJsonParser },
            { "securitylevel", CustomJsonSerializer.SecurityLevelJsonParser }
        };

        internal static Dictionary<string, CimFieldInfo> Parse(Dictionary<string, JsonObject> json)
        {
            var dictionary = new Dictionary<string, CimFieldInfo>();

            if (json != null)
            {
                foreach (var kvp in json)
                {
                    dictionary.Add(kvp.Key, ParseIssueFieldInfo(kvp.Value, kvp.Key));
                }
            }

            return dictionary;
        }

        private static CimFieldInfo ParseIssueFieldInfo(JsonObject json, string id)
        {
            var schema = json.Get<FieldSchema>("schema");

            return new CimFieldInfo
            {
                Id = id,
                Name = json.Get("name"),
                Required = json.Get<bool>("required"),
                Schema = schema,
                Operations = json.Get<List<StandardOperation>>("operations"),
                AllowedValues = ParseAllowedValues(json.ArrayObjects("allowedValues"), schema),
                AutoCompleteUri = json.Get<Uri>("autoCompleteUrl")
            };
        }

        private static IEnumerable<object> ParseAllowedValues(JsonArrayObjects json, FieldSchema schema)
        {
            if (json == null)
            {
                return null;
            }

            if (json.Count == 0)
            {
                return new object[0];
            }

            var parser = GetParserFor(schema);
            if (parser != null)
            {
                return json.ConvertAll(parser.Invoke);
            }

            // Fallback to returning an array of JsonObjects
            return json;
        }

        private static Func<JsonObject, object> GetParserFor(FieldSchema schema)
        {
            var customFieldTypesWithFieldOption = new List<string>
            {
                "com.atlassian.jira.plugin.system.customfieldtypes:multicheckboxes",
                "com.atlassian.jira.plugin.system.customfieldtypes:radiobuttons",
                "com.atlassian.jira.plugin.system.customfieldtypes:select",
                "com.atlassian.jira.plugin.system.customfieldtypes:cascadingselect",
                "com.atlassian.jira.plugin.system.customfieldtypes:multiselect"
            };

            var type = schema.Type == "array" ? schema.Items : schema.Type;
            if (schema.Custom != null && customFieldTypesWithFieldOption.Contains(schema.Custom))
            {
                // TODO: Implement custom meta-data fields option parser!
                type = "customFieldOption";
            }

            Func<JsonObject, object> parser;
            RegisteredAllowedValueParsers.TryGetValue(type, out parser);
            return parser;
        }
    }
}
