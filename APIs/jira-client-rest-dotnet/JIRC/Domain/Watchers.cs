// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Watchers.cs" company="David Bevin">
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
    /// Detailed information about the watchers of a given issue.
    /// </summary>
    public class Watchers : BasicWatchers
    {
        /// <summary>
        /// Initializes the watchers.
        /// </summary>
        /// <param name="self">The URI for the watchers resource.</param>
        /// <param name="isWatching">Whether or not anyone is watching the issue.</param>
        /// <param name="count">The number of users watching the issue.</param>
        /// <param name="users">The users who are watching the issue.</param>
        internal Watchers(Uri self, bool isWatching, int count, IEnumerable<BasicUser> users)
            : base(self, isWatching, count)
        {
            Users = users;
        }

        /// <summary>
        /// Gets a collection of users who are watching the issue.
        /// </summary>
        public IEnumerable<BasicUser> Users { get; private set; }
    }
}
