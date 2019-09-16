// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCreateIssueMetadataOptionsBuilder.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace JIRC.Domain.Input
{
    public class GetCreateIssueMetadataOptionsBuilder
    {
        private readonly List<string> expandos = new List<string>();
        private readonly List<string> issueTypeNames = new List<string>();
        private readonly List<long> issueTypeIds = new List<long>();
        private readonly List<string> projectKeys = new List<string>();
        private readonly List<long> projectIds = new List<long>();

        public GetCreateIssueMetadataOptionsBuilder WithExpandedIssueTypesFields()
        {
            return this.WithExpandos(GetCreateIssueMetadataOptions.ExpandProjectsIssueTypesFields);
        }

        public GetCreateIssueMetadataOptionsBuilder WithExpandos(params string[] values)
        {
            this.expandos.AddRange(values);
            return this;
        }

        public GetCreateIssueMetadataOptionsBuilder WithIssueTypeNames(params string[] values)
        {
            this.issueTypeNames.AddRange(values);
            return this;
        }

        public GetCreateIssueMetadataOptionsBuilder WithIssueTypeIds(params long[] values)
        {
            this.issueTypeIds.AddRange(values);
            return this;
        }

        public GetCreateIssueMetadataOptionsBuilder WithProjectKeys(params string[] values)
        {
            this.projectKeys.AddRange(values);
            return this;
        }

        public GetCreateIssueMetadataOptionsBuilder WithProjectIds(params long[] values)
        {
            this.projectIds.AddRange(values);
            return this;
        }

        public GetCreateIssueMetadataOptions Build()
        {
            return new GetCreateIssueMetadataOptions(this.expandos, this.issueTypeNames, this.issueTypeIds, this.projectKeys, this.projectIds);
        }
    }
}
