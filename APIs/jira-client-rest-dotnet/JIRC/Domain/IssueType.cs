// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueType.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace JIRC.Domain
{
    /// <summary>
    /// Detailed information about an issue type defined in JIRA.
    /// </summary>
    public class IssueType : BasicIssueType
    {
        /// <summary>
        /// Initializes an issue type.
        /// </summary>
        /// <param name="self">The URI resource for us.</param>
        /// <param name="id">The issue type unique ID.</param>
        /// <param name="name">The name of the issue type.</param>
        /// <param name="isSubtask">Whether or not this is a subtask.</param>
        /// <param name="description">The description.</param>
        /// <param name="iconUrl">The URI for the icon resource associated with this issue type.</param>
        internal IssueType(Uri self, long? id, string name, bool isSubtask, string description, Uri iconUrl)
            : base(self, id, name, isSubtask)
        {
            Description = description;
            IconUrl = iconUrl;
        }

        /// <summary>
        /// Gets the description of the issue type.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the URI of the icon resource associated with this issue type.
        /// </summary>
        public Uri IconUrl { get; private set; }
    }
}
