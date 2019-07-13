// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginInfo.cs" company="David Bevin">
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
    /// Statistics about logins for a user.
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// Creates the new statistics.
        /// </summary>
        /// <param name="failedLoginCount">The number of failed logins.</param>
        /// <param name="loginCount">The number of successful logins.</param>
        /// <param name="lastFailedLoginDate">The date/time of last failed login.</param>
        /// <param name="previousLoginDate">The date/time of the last successful login before the current login.</param>
        internal LoginInfo(int failedLoginCount, int loginCount, DateTimeOffset? lastFailedLoginDate, DateTimeOffset? previousLoginDate)
        {
            FailedLoginCount = failedLoginCount;
            LoginCount = loginCount;
            LastFailedLoginDate = lastFailedLoginDate;
            PreviousLoginDate = previousLoginDate;
        }

        /// <summary>
        /// Gets the number of failed login attempts.
        /// </summary>
        public int FailedLoginCount { get; private set; }

        /// <summary>
        /// Gets the number of successful logins.
        /// </summary>
        public int LoginCount { get; private set; }

        /// <summary>
        /// Gets the date/time of the last failed login attempt.
        /// </summary>
        public DateTimeOffset? LastFailedLoginDate { get; private set; }

        /// <summary>
        /// Gets the date/time of the previous login.
        /// </summary>
        public DateTimeOffset? PreviousLoginDate { get; private set; }
    }
}
