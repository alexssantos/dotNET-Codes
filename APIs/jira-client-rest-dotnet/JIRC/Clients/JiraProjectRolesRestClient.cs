// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraProjectRolesRestClient.cs" company="David Bevin">
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
    /// The REST client for accessing information about Project Roles.
    /// </summary>
    internal class JiraProjectRolesRestClient : IProjectRolesRestClient
    {
        private const string ProjectRoleUriPostfix = "role";

        private readonly JsonServiceClient client;

        /// <summary>
        /// Initializes a new instance of the Projects Roles REST client.
        /// </summary>
        /// <param name="client">The JSON client that has been set up for a specific JIRA instance.</param>
        public JiraProjectRolesRestClient(JsonServiceClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Retrieves detailed information about the selected role.
        /// </summary>
        /// <param name="uri">The URI for the role resource.</param>
        /// <returns>Detailed information about the selected role.</returns>
        /// <exception cref="WebServiceException">The project role was not found, or the calling user does not have permission to view it.</exception>
        public ProjectRole GetRole(Uri uri)
        {
            return client.Get<ProjectRole>(uri.ToString());
        }

        /// <summary>
        /// Retrieves detailed information about the selected role in the specified project.
        /// </summary>
        /// <param name="projectUri">The URI of the project resource.</param>
        /// <param name="roleId">The Id of the role.</param>
        /// <returns>Detailed information about the selected role.</returns>
        /// <exception cref="WebServiceException">The project role or role ID was not found, or the calling user does not have permission to view it.</exception>
        public ProjectRole GetRole(Uri projectUri, int roleId)
        {
            var uri = projectUri.Append(ProjectRoleUriPostfix).Append(roleId.ToString());
            return client.Get<ProjectRole>(uri.ToString());
        }

        /// <summary>
        /// Retrieves detailed information for all roles for the specified project.
        /// </summary>
        /// <param name="projectUri">The URI of the project resource.</param>
        /// <returns>A collection of all roles for the project.</returns>
        /// <exception cref="WebServiceException">The project was not found, or the calling user does not have permission to view it.</exception>
        public IEnumerable<ProjectRole> GetRoles(Uri projectUri)
        {
            var uri = projectUri.Append(ProjectRoleUriPostfix);
            var json = client.Get<Dictionary<string, Uri>>(uri.ToString());
            return json.Values.Select(b => GetRole(b)).ToList();
        }

        /// <summary>
        /// Adds a group to a specific role in the project.
        /// </summary>
        /// <param name="projectUri">The URI of the project resource.</param>
        /// <param name="roleId">The ID of the project role.</param>
        /// <param name="userName">The name of the user to add.</param>
        /// <returns>Detailed information about the selected role.</returns>
        public ProjectRole AddUserToRole(Uri projectUri, int roleId, string userName)
        {
            return this.AddActorToRole(projectUri, roleId, "user", userName);
        }

        /// <summary>
        /// Adds a group to a specific role in the project.
        /// </summary>
        /// <param name="projectUri">The URI of the project resource.</param>
        /// <param name="roleId">The ID of the project role.</param>
        /// <param name="groupName">The name of the group to add.</param>
        /// <returns>Detailed information about the selected role.</returns>
        public ProjectRole AddGroupToRole(Uri projectUri, int roleId, string groupName)
        {
            return this.AddActorToRole(projectUri, roleId, "group", groupName);
        }

        private ProjectRole AddActorToRole(Uri projectUri, int roleId, string actorType, string actorName)
        {
            var uri = projectUri.Append(ProjectRoleUriPostfix).Append(roleId.ToString());
            var actors = new List<string> { actorName };
            var request = new JsonObject { { actorType, actors.ToJson() } };
            return client.Post<ProjectRole>(uri.ToString(), request);
        }
    }
}
