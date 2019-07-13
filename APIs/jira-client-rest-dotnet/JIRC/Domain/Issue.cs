// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Issue.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using JIRC.Extensions;

namespace JIRC.Domain
{
    /// <summary>
    /// A detailed representation of an Issue.
    /// </summary>
    public class Issue : BasicIssue
    {
        /// <summary>
        /// Initializes a new instance of an issue.
        /// </summary>
        /// <param name="id">The id of the issue.</param>
        /// <param name="key">The key (e.g. DEMO-8).</param>
        /// <param name="self">The URI for the issue itself.</param>
        internal Issue(int id, string key, Uri self)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            Attachments = new Attachment[0];
            CustomFields = new CustomFieldTypes();
            Id = id;
            Key = key;
            Self = self;
        }

        /// <summary>
        /// Gets the URI for the comments resource.
        /// </summary>
        public Uri CommentsUri
        {
            get
            {
                return Self != null ? Self.Append("comment") : null;
            }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Gets the Fix Versions.
        /// </summary>
        public IEnumerable<JiraVersion> FixVersions { get; internal set; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        public string Parent { get; internal set; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        public BasicProject Project { get; internal set; }

        /// <summary>
        /// Gets the reporter of the issue.
        /// </summary>
        public User Reporter { get; internal set; }

        /// <summary>
        /// Gets the issue summary.
        /// </summary>
        public string Summary { get; internal set; }

        /// <summary>
        /// Gets the URI for the transitions resource.
        /// </summary>
        public Uri TransitionsUri { get; internal set; }

        /// <summary>
        /// Gets the URI for the transitions resource.
        /// </summary>
        public Uri AttachmentsUri
        {
            get
            {
                return Self.Append("attachments");
            }
        }

        /// <summary>
        /// Gets the URI for the transitions resource.
        /// </summary>
        public Uri WorklogUri
        {
            get
            {
                return Self.Append("worklog");
            }
        }

        /// <summary>
        /// Gets the watchers of the issue.
        /// </summary>
        public BasicWatchers Watchers { get; internal set; }

        /// <summary>
        /// Gets the voters of the issue.
        /// </summary>
        public BasicVotes Votes { get; internal set; }

        /// <summary>
        /// Gets the Affects Version for the issue.
        /// </summary>
        public IEnumerable<JiraVersion> AffectedVersions { get; internal set; }

        /// <summary>
        /// Gets the assignee of the issue.
        /// </summary>
        public User Assignee { get; internal set; }

        /// <summary>
        /// Gets the attachments for the issue.
        /// </summary>
        public IEnumerable<Attachment> Attachments { get; internal set; }

        /// <summary>
        /// Gets the labels for the issue.
        /// </summary>
        public IEnumerable<string> Labels { get; internal set; }

        /// <summary>
        /// Gets the status of the issue.
        /// </summary>
        public string Status { get; internal set; }

        /// <summary>
        /// Gets the resolution of the issue.
        /// </summary>
        public string Resolution { get; internal set; }

        /// <summary>
        /// Gets all the sub-tasks for this issue.
        /// </summary>
        public IEnumerable<Issue> SubTasks { get; internal set; }

        /// <summary>
        /// Gets the type of this issue.
        /// </summary>
        public IssueType IssueType { get; internal set; }

        /// <summary>
        /// Gets all the custom fields for the issue.
        /// </summary>
        public CustomFieldTypes CustomFields { get; internal set; }
    }
}
