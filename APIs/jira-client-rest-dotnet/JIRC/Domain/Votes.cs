// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Votes.cs" company="David Bevin">
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
    /// Complete information about the voters for given issue.
    /// </summary>
    public class Votes : BasicVotes
    {
        /// <summary>
        /// Initializes a new instance of voting information.
        /// </summary>
        /// <param name="self">A URI resource for votes on the selected issue.</param>
        /// <param name="votes">The number of votes for the issue.</param>
        /// <param name="hasVoted">Whether or not a user has voted for the issue.</param>
        /// <param name="voters">A collection of users who have voted for the issue.</param>
        public Votes(Uri self, int votes, bool hasVoted, IEnumerable<BasicUser> voters)
            : base(self, votes, hasVoted)
        {
            Voters = voters;
        }

        /// <summary>
        /// Gets a collection of users who have voted for the issue.
        /// </summary>
        public IEnumerable<BasicUser> Voters { get; private set; }
    }
}
