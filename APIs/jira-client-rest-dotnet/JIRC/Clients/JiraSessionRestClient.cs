// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraSessionRestClient.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JIRC.Domain;

using ServiceStack.ServiceClient.Web;

namespace JIRC.Clients
{
    /// <summary>
    /// The REST client for obtaining Session information from JIRA.
    /// </summary>
    internal class JiraSessionRestClient : ISessionRestClient
    {
        private readonly JsonServiceClient client;

        private readonly Uri serverUri;

        /// <summary>
        /// Initializes a new instance of the Session client.
        /// </summary>
        /// <param name="client">The JSON client that has been set up for a specific JIRA instance.</param>
        /// <param name="serverUri">The base URI for the server.</param>
        public JiraSessionRestClient(JsonServiceClient client, Uri serverUri)
        {
            this.client = client;
            this.serverUri = serverUri;
        }

        /// <summary>
        /// Gets information about the current session.
        /// </summary>
        /// <returns>Information about the session.</returns>
        /// <exception cref="WebServiceException">There was a problem accessing the server.</exception>
        public Session GetCurrentSession()
        {
            var uri = new Uri(serverUri, "/rest/auth/latest/session");
            return client.Get<Session>(uri.ToString());
        }
    }
}
