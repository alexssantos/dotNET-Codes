// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraRestClientFactory.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JIRC.Clients;

using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

namespace JIRC
{
    /// <summary>
    /// A factory for creating JIRA clients.
    /// </summary>
    public static class JiraRestClientFactory
    {
        private const string LatestRestUri = "/rest/api/latest";

        /// <summary>
        /// Creates a new JIRA client using Basic Authentication. You really should use HTTPS when you do this.
        /// </summary>
        /// <param name="serverUri">The base URI of the JIRA instance.</param>
        /// <param name="username">The username to use.</param>
        /// <param name="password">The password to use.</param>
        /// <returns>A new client for accessing JIRA.</returns>
        public static IJiraRestClient CreateWithBasicHttpAuth(Uri serverUri, string username, string password)
        {
            var client = new JsonServiceClient(GetFullBaseUri(serverUri))
            {
                UserName = username,
                Password = password,
                AlwaysSendBasicAuthHeader = true
            };

            return new JiraRestClient(serverUri, client);
        }

        /// <summary>
        /// Creates a new JIRA client using anonymous access.
        /// </summary>
        /// <param name="serverUri">The base URI of the JIRA instance.</param>
        /// <returns>A new client for accessing JIRA.</returns>
        public static IJiraRestClient CreateWithAnonymous(Uri serverUri)
        {
            return new JiraRestClient(serverUri, new JsonServiceClient(GetFullBaseUri(serverUri)));
        }

        /// <summary>
        /// Creates a new JIRA client with session based access.
        /// </summary>
        /// <param name="serverUri">The base URI of the JIRA instance.</param>
        /// <param name="username">The username to use.</param>
        /// <param name="password">The password to use.</param>
        /// <returns>A new client for accessing JIRA.</returns>
        public static IJiraRestClient CreateWithSessionAuth(Uri serverUri, string username, string password)
        {
            var client = new JsonServiceClient(GetFullBaseUri(serverUri)) { UserName = username, Password = password };

            client.OnAuthenticationRequired = request =>
                {
                    var obj = new JsonObject { { "username", username }, { "password", password } };
                    client.Post<JsonObject>(BuildUrl(serverUri, "/rest/auth/1/session"), obj);
                };

            return new JiraRestClient(serverUri, client);
        }

        private static string GetFullBaseUri(Uri serverUri)
        {
            return BuildUrl(serverUri, LatestRestUri);
        }

        private static string BuildUrl(Uri serverUri, string path)
        {
            if (serverUri == null)
            {
                throw new ArgumentNullException("serverUri");
            }

            if (serverUri.LocalPath.Length > 1)
            {
                // LocalPath is '/' usually
                path = serverUri.LocalPath + path;
            }

            return new Uri(serverUri, path).ToString();
        }
    }
}
