// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicIssueType.cs" company="David Bevin">
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
    /// Basic information about an issue type.
    /// </summary>
    public class BasicIssueType
    {
        /// <summary>
        /// Initializes the basic issue type information.
        /// </summary>
        /// <param name="self">The URI resource for us.</param>
        /// <param name="id">The issue type unique ID.</param>
        /// <param name="name">The name of the issue type.</param>
        /// <param name="isSubtask">Whether or not this is a subtask.</param>
        internal BasicIssueType(Uri self, long? id, string name, bool isSubtask)
        {
            Self = self;
            Id = id;
            Name = name;
            Subtask = isSubtask;
        }

        /// <summary>
        /// Gets our URI resource.
        /// </summary>
        public Uri Self { get; private set; }

        /// <summary>
        /// Gets the unique ID for the issue type.
        /// </summary>
        public long? Id { get; private set; }

        /// <summary>
        /// Gets the name of the issue type.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this is a subtask.
        /// </summary>
        public bool Subtask { get; private set; }
    }
}
