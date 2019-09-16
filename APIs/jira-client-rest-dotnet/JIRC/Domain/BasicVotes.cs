// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicVotes.cs" company="David Bevin">
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
    /// A class that holds some basic information about votes.
    /// </summary>
    public class BasicVotes
    {
        /// <summary>
        /// Initializes a new instance of basic voting information.
        /// </summary>
        /// <param name="self">A URI resource for votes on the selected issue.</param>
        /// <param name="votes">The number of votes for the issue.</param>
        /// <param name="hasVoted">Whether or not a user has voted for the issue.</param>
        internal BasicVotes(Uri self, int votes, bool hasVoted)
        {
            Self = self;
            Votes = votes;
            HasVoted = hasVoted;
        }

        /// <summary>
        /// Gets the URI for the votes resource on the selected issue.
        /// </summary>
        public Uri Self { get; private set; }

        /// <summary>
        /// Gets the number of votes for the issue.
        /// </summary>
        public int Votes { get; private set; }

        /// <summary>
        /// Gets a value indicating whether a user has voted for the issue.
        /// </summary>
        public bool HasVoted { get; private set; }
    }
}
