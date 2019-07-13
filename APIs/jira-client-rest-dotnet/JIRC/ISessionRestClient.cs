// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISessionRestClient.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JIRC.Domain;

using ServiceStack.ServiceClient.Web;

namespace JIRC
{
    /// <summary>
    /// An interface for obtaining information about login sessions.
    /// </summary>
    public interface ISessionRestClient
    {
        /// <summary>
        /// Gets information about the current session.
        /// </summary>
        /// <returns>Information about the session.</returns>
        /// <exception cref="WebServiceException">There was a problem accessing the server.</exception>
        Session GetCurrentSession();
    }
}
