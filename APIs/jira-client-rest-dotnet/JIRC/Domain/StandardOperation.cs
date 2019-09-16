// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardOperation.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace JIRC.Domain
{
    /// <summary>
    /// Standard operations that can be performed on issue fields.
    /// </summary>
    public enum StandardOperation
    {
        /// <summary>
        /// Sets a field value.
        /// </summary>
        Set,

        /// <summary>
        /// Adds something to the field value.
        /// </summary>
        Add,

        /// <summary>
        /// Removes a value from the field.
        /// </summary>
        Remove,

        /// <summary>
        /// Edits the field value.
        /// </summary>
        Edit
    }

    internal static class StandardOperationHelper
    {
        private static readonly Dictionary<StandardOperation, string> OperationNames = new Dictionary<StandardOperation, string>
        {
            { StandardOperation.Set, "set" },
            { StandardOperation.Edit, "edit" },
            { StandardOperation.Remove, "remove" },
            { StandardOperation.Add, "add" }
        };

        internal static string ToRestString(this StandardOperation operation)
        {
            return OperationNames[operation];
        }
    }
}
