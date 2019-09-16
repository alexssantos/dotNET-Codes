// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CimIssueType.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace JIRC.Domain
{
    /// <summary>
    /// Describes issue type fields with fields info map.
    /// The CIM prefix stands for CreateIssueMetadata, as this class is used in output of <see cref="IIssueRestClient.GetCreateIssueMetadata()"/>.
    /// </summary>
    public class CimIssueType : IssueType
    {
        private readonly Dictionary<string, CimFieldInfo> fields;

        /// <summary>
        /// Initializes the Create Issue Metadata issue type.
        /// </summary>
        /// <param name="self">The URI resource for us.</param>
        /// <param name="id">The issue type unique ID.</param>
        /// <param name="name">The name of the issue type.</param>
        /// <param name="isSubtask">Whether or not this is a subtask.</param>
        /// <param name="description">The description.</param>
        /// <param name="iconUrl">The URI for the icon resource associated with this issue type.</param>
        /// <param name="fields">The fields for the issue type.</param>
        internal CimIssueType(Uri self, long? id, string name, bool isSubtask, string description, Uri iconUrl, Dictionary<string, CimFieldInfo> fields)
            : base(self, id, name, isSubtask, description, iconUrl)
        {
            this.fields = fields ?? new Dictionary<string, CimFieldInfo>();
        }

        /// <summary>
        /// Gets the available fields for this issue type.
        /// </summary>
        public IEnumerable<CimFieldInfo> Fields
        {
            get
            {
                return fields.Values;
            }
        }
    }
}
