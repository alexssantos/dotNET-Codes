// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionRelatedIssuesCount.cs" company="David Bevin">
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
    /// Represents the number of issue which use the given version in the Fix Version(s) and Affects Version(s).
    /// </summary>
    public class VersionRelatedIssuesCount
    {
        /// <summary>
        /// Initializes the statistics.
        /// </summary>
        /// <param name="versionUri">The URI of the version resource.</param>
        /// <param name="numFixedIssues">The number of issues with this version in the Fix Version(s).</param>
        /// <param name="numAffectsIssues">The number of issues with this version in the Affects Version(s).</param>
        internal VersionRelatedIssuesCount(Uri versionUri, int numFixedIssues, int numAffectsIssues)
        {
            VersionUrl = versionUri;
            NumFixedIssues = numFixedIssues;
            NumAffectedIssues = numAffectsIssues;
        }

        /// <summary>
        /// Gets the URI of the version resource.
        /// </summary>
        public Uri VersionUrl { get; private set; }

        /// <summary>
        /// Gets the number of issues that include this Fix Version.
        /// </summary>
        public int NumFixedIssues { get; private set; }

        /// <summary>
        /// Gets the number of issues that include this Affects Version.
        /// </summary>
        public int NumAffectedIssues { get; private set; }
    }
}
