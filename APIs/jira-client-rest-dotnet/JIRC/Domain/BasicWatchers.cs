// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicWatchers.cs" company="David Bevin">
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
    /// Basic information about watchers of an issue.
    /// </summary>
    public class BasicWatchers
    {
        /// <summary>
        /// Creates a new instance of the watcher information.
        /// </summary>
        /// <param name="self">The URI for the watcher resource.</param>
        /// <param name="isWatching">Whether or not anyone is watching the issue.</param>
        /// <param name="count">The number of users watching the issue.</param>
        protected BasicWatchers(Uri self, bool isWatching, int count)
        {
            Self = self;
            IsWatching = isWatching;
            Count = count;
        }

        /// <summary>
        /// Gets the URI of the watcher resource.
        /// </summary>
        public Uri Self { get; private set; }

        /// <summary>
        /// Gets a value indicating whether anyone is watching the issue.
        /// </summary>
        public bool IsWatching { get; private set; }

        /// <summary>
        /// Gets the number of users watching the issue.
        /// </summary>
        public int Count { get; private set; }
    }
}
