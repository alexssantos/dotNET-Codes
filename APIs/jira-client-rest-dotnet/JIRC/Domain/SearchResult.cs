// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchResult.cs" company="David Bevin">
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
    /// Represents the results of a JQL query with some basic paging information.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Creates search results.
        /// </summary>
        /// <param name="startIndex">The start index of the results.</param>
        /// <param name="maxResults">The maximum number of results in a "page".</param>
        /// <param name="total">The total number of results in the query.</param>
        /// <param name="issues">The issues in the result set.</param>
        internal SearchResult(int startIndex, int maxResults, int total, IEnumerable<Issue> issues)
        {
            StartIndex = startIndex;
            MaxResults = maxResults;
            Total = total;
            Issues = issues;
        }

        /// <summary>
        /// Gets the 0-based start index of the returned issues.
        /// </summary>
        public int StartIndex { get; private set; }

        /// <summary>
        /// Gets the maximum number of results in the "page".
        /// </summary>
        public int MaxResults { get; private set; }

        /// <summary>
        /// Gets the total number of issues returned by the query.
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// Gets the issues in the current page of the result set.
        /// </summary>
        public IEnumerable<Issue> Issues { get; private set; }
    }
}
