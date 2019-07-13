// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraUserRestClient.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using JIRC.Domain;
using JIRC.Extensions;

using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

namespace JIRC.Clients
{
    /// <summary>
    /// The REST client for Users in JIRA.
    /// </summary>
    internal class JiraUserRestClient : IUserRestClient
    {
        /// <summary>
        /// The URI resource used when searching for users.
        /// </summary>
        internal const string UserAssignableSearchUriPrefix = "user/assignable/search";

        private const string UserUriPrefix = "user";

        private const string GroupPickerUriPrefix = "groups/picker";

        private readonly JsonServiceClient client;

        /// <summary>
        /// Initializes a new instance of the User REST client.
        /// </summary>
        /// <param name="client">The JSON client that has been set up for a specific JIRA instance.</param>
        public JiraUserRestClient(JsonServiceClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Gets detailed information about the user.
        /// </summary>
        /// <param name="username">The login username for the user.</param>
        /// <returns>Detailed information about the user.</returns>
        /// <exception cref="WebServiceException">The specified username does not exist, or the caller does not have permission to view the users.</exception>
        public User GetUser(string username)
        {
            var qb = new UriBuilder(client.BaseUri.AppendPath(UserUriPrefix));
            qb.AppendQuery("username", username);
            qb.AppendQuery("expand", "groups");

            return GetUser(qb.Uri);
        }

        /// <summary>
        /// Gets detailed information about the user.
        /// </summary>
        /// <param name="userUri">The URI for the user resource.</param>
        /// <returns>Detailed information about the user.</returns>
        /// <exception cref="WebServiceException">The specified username does not exist, or the caller does not have permission to view the users.</exception>
        public User GetUser(Uri userUri)
        {
            return client.Get<User>(userUri.ToString());
        }

        /// <summary>
        /// Gets a list of all groups.
        /// </summary>
        /// <returns>A list of groups.</returns>
        public IEnumerable<string> GetGroups()
        {
            var qb = new UriBuilder(client.BaseUri.AppendPath(GroupPickerUriPrefix));
            qb.AppendQuery("maxResults", "500");

            var response = client.Get<JsonObject>(qb.Uri.ToString());

            var groups = response.Get<IEnumerable<JsonObject>>("groups");

            return groups.Select(a => a.Get<string>("name"));
        }
    }
}
