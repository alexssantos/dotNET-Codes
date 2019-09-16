// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRestClient.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using JIRC.Domain;

using ServiceStack.ServiceClient.Web;

namespace JIRC
{
    /// <summary>
    /// An interface for handling Users within JIRA.
    /// </summary>
    public interface IUserRestClient
    {
        /// <summary>
        /// Gets detailed information about the user.
        /// </summary>
        /// <param name="username">The login username for the user.</param>
        /// <returns>Detailed information about the user.</returns>
        /// <exception cref="WebServiceException">The specified username does not exist, or the caller does not have permission to view the users.</exception>
        User GetUser(string username);

        /// <summary>
        /// Gets detailed information about the user.
        /// </summary>
        /// <param name="userUri">The URI for the user resource.</param>
        /// <returns>Detailed information about the user.</returns>
        /// <exception cref="WebServiceException">The specified username does not exist, or the caller does not have permission to view the users.</exception>
        User GetUser(Uri userUri);

        /// <summary>
        /// Gets a list of all groups.
        /// </summary>
        /// <returns>A list of groups.</returns>
        IEnumerable<string> GetGroups();
    }
}
