// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Session.cs" company="David Bevin">
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
    /// Information about the "session" of the currently logged-in user.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Creates the session information.
        /// </summary>
        /// <param name="userUri">The URI for the user resource.</param>
        /// <param name="username">The login name for the user.</param>
        /// <param name="loginInfo">Information about the session.</param>
        internal Session(Uri userUri, string username, LoginInfo loginInfo)
        {
            UserUri = userUri;
            UserName = username;
            LoginInfo = loginInfo;
        }

        /// <summary>
        /// Gets the URI for the user resource.
        /// </summary>
        public Uri UserUri { get; private set; }

        /// <summary>
        /// Gets the username for the user.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets statistics about the user's login behavior.
        /// </summary>
        public LoginInfo LoginInfo { get; private set; }
    }
}
