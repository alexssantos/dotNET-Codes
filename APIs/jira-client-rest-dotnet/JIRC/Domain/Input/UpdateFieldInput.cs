// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateFieldInput.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace JIRC.Domain.Input
{
    /// <summary>
    /// Handles updates to fields.
    /// </summary>
    internal class UpdateFieldInput
    {
        /// <summary>
        /// Initializes a new instance of the field updater.
        /// </summary>
        /// <param name="field">The standard field to update.</param>
        internal UpdateFieldInput(IssueFieldId field)
            : this(field, new List<FieldUpdateElement>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the field updater.
        /// </summary>
        /// <param name="fieldName">The custom field to update.</param>
        internal UpdateFieldInput(string fieldName)
            : this(fieldName, new List<FieldUpdateElement>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the field updater.
        /// </summary>
        /// <param name="field">The standard field to update.</param>
        /// <param name="elements">The changes to make.</param>
        internal UpdateFieldInput(IssueFieldId field, IEnumerable<FieldUpdateElement> elements)
        {
            // Convert field enum to field name sooner and store as a string.
            FieldName = field.ToRestString();
            Elements = elements.ToList();
        }

        /// <summary>
        /// Initializes a new instance of the field updater.
        /// </summary>
        /// <param name="fieldName">The custom field to update.</param>
        /// <param name="elements">The changes to make.</param>
        internal UpdateFieldInput(string fieldName, IEnumerable<FieldUpdateElement> elements)
        {
            // Store field name directly as a string.
            FieldName = fieldName;
            Elements = elements.ToList();
        }

        /// <summary>
        /// Gets the name of the field being modified.
        /// </summary>
        public string FieldName { get; private set; }

        /// <summary>
        /// Gets the elements to update.
        /// </summary>
        public IList<FieldUpdateElement> Elements { get; private set; }

        /// <summary>
        /// Adds an operation to the update field.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        /// <param name="value">The value.</param>
        public void AddOperation(StandardOperation operation, object value)
        {
            Elements.Add(new FieldUpdateElement(operation, value));
        }
    }
}