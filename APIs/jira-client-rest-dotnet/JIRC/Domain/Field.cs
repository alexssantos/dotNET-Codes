// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Field.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain
{
    /// <summary>
    /// A class that describes a field in JIRA.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Initializes a new field.
        /// </summary>
        /// <param name="id">The ID of the field.</param>
        /// <param name="name">The name.</param>
        /// <param name="fieldType">The type of field; whether custom or JIRA.</param>
        /// <param name="orderable">Whether or not the field is orderable.</param>
        /// <param name="navigable">Whether or not the field is navigable.</param>
        /// <param name="searchable">Whether or not the field is searchable.</param>
        /// <param name="schema">The schema.</param>
        internal Field(string id, string name, Type fieldType, bool orderable, bool navigable, bool searchable, FieldSchema schema)
        {
            Id = id;
            Name = name;
            FieldType = fieldType;
            Orderable = orderable;
            Navigable = navigable;
            Searchable = searchable;
            Schema = schema;
        }

        /// <summary>
        /// The type of field (e.g. standard or custom).
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// This is a standard JIRA field.
            /// </summary>
            Jira,

            /// <summary>
            /// This is a custom field.
            /// </summary>
            Custom
        }

        /// <summary>
        /// Gets the field ID.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the field name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of field (either custom or standard).
        /// </summary>
        public Type FieldType { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the field is orderable.
        /// </summary>
        public bool Orderable { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the field is navigable.
        /// </summary>
        public bool Navigable { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the field is searchable.
        /// </summary>
        public bool Searchable { get; private set; }

        /// <summary>
        /// Gets the schema of the field.
        /// </summary>
        public FieldSchema Schema { get; private set; }
    }
}
