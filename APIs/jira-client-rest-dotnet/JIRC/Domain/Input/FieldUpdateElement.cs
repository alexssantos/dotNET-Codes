// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldUpdateElement.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain.Input
{
    /// <summary>
    /// Allows updates of fields.
    /// Eventually, this will be used by a more generic issue update mechanism.
    /// </summary>
    internal class FieldUpdateElement
    {
        /// <summary>
        /// Initializes a new field update element.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        /// <param name="value">The value.</param>
        internal FieldUpdateElement(StandardOperation operation, object value)
        {
            this.Operation = operation;
            this.Value = value;
        }

        /// <summary>
        /// The operation to perform.
        /// </summary>
        public StandardOperation Operation { get; private set; }

        /// <summary>
        /// The value to use.
        /// </summary>
        public object Value { get; private set; }
    }
}
