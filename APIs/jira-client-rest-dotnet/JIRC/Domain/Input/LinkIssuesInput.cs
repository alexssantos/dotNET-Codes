// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkIssuesInput.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain.Input
{
    public class LinkIssuesInput
    {
        public LinkIssuesInput(string fromIssueKey, string toIssueKey, string linkType)
            : this(fromIssueKey, toIssueKey, linkType, null)
        {
        }

        public LinkIssuesInput(string fromIssueKey, string toIssueKey, string linkType, Comment comment)
        {
            FromIssueKey = fromIssueKey;
            ToIssueKey = toIssueKey;
            LinkType = linkType;
            Comment = comment;
        }

        public string FromIssueKey { get; set; }

        public string ToIssueKey { get; set; }

        public string LinkType { get; set; }

        public Comment Comment { get; set; }
    }
}
