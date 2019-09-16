// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraProjectRestClient.cs" company="David Bevin">
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
using JIRC.Extensions;

using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

namespace JIRC.Clients
{
    /// <summary>
    /// The REST client for Projects in JIRA.
    /// </summary>
    internal class JiraProjectRestClient : IProjectRestClient
    {
        private const string ProjectUriPrefix = "project";

        private readonly JsonServiceClient client;

        /// <summary>
        /// Initializes a new instance of the Projects REST client.
        /// </summary>
        /// <param name="client">The JSON client that has been set up for a specific JIRA instance.</param>
        public JiraProjectRestClient(JsonServiceClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Returns all projects, which are visible for the calling user. If no user is logged in, it returns the list of projects that are visible when using anonymous access.
        /// </summary>
        /// <returns>Projects that the caller user can see.</returns>
        /// <exception cref="WebServiceException">An error occurred when retrieving the list of projects.</exception>
        public IEnumerable<BasicProject> GetAllProjects()
        {
            return client.Get<IEnumerable<BasicProject>>("/{0}".Fmt(ProjectUriPrefix));
        }

        /// <summary>
        /// Retrieves detailed information about a project.
        /// </summary>
        /// <param name="key">The unique key for the project (e.g. "AA").</param>
        /// <returns>Detailed information about the project.</returns>
        /// <exception cref="WebServiceException">The project is not found, or the calling user does not have permission to view it.</exception>
        public Project GetProject(string key)
        {
            return client.Get<Project>("/{0}/{1}".Fmt(ProjectUriPrefix, key));
        }

        /// <summary>
        /// Retrieves complete information about given project.
        /// Use this method in preference to <see cref="IProjectRestClient.GetProject(string)"/> wherever you can, as this method will safeguard against URI scheme changes.
        /// </summary>
        /// <param name="projectUri">The URI for the requested project. Usually retrieved by getting the property <see cref="BasicProject.Self"/>.</param>
        /// <returns>Detailed information about the project.</returns>
        /// <exception cref="WebServiceException">The project is not found, or the calling user does not have permission to view it.</exception>
        public Project GetProject(Uri projectUri)
        {
            return client.Get<Project>(projectUri.ToString());
        }

        /// <summary>
        /// Retrieves a list of users who may be used as assignee when creating an issue. For a list of users when editing an issue, see <see cref="IIssueRestClient.GetAssignableUsers(string)"/>.
        /// </summary>
        /// <param name="projectKey">The unique key for the project (e.g. "AA").</param>
        /// <returns>Returns a list of users who may be assigned to an issue during creation.</returns>
        /// <exception cref="WebServiceException">The project is not found, or the calling user does not have permission to view it.</exception>
        public IEnumerable<User> GetAssignableUsers(string projectKey)
        {
            return GetAssignableUsers(projectKey, null, null);
        }

        /// <summary>
        /// Retrieves a list of users who may be used as assignee when creating an issue. For a list of users when editing an issue, see <see cref="IIssueRestClient.GetAssignableUsers(string, int?, int?)"/>.
        /// </summary>
        /// <param name="projectKey">The unique key for the project (e.g. "AA").</param>
        /// <param name="startAt">The index of the first user to return (0-based).</param>
        /// <param name="maxResults">The maximum number of users to return (defaults to 50). The maximum allowed value is 1000. If you specify a value that is higher than this number, your search results will be truncated.</param>
        /// <returns>Returns a list of users who may be assigned to an issue during creation.</returns>
        public IEnumerable<User> GetAssignableUsers(string projectKey, int? startAt, int? maxResults)
        {
            var qb = new UriBuilder(client.BaseUri.AppendPath(JiraUserRestClient.UserAssignableSearchUriPrefix));
            qb.AppendQuery("project", projectKey);

            if (maxResults != null)
            {
                qb.AppendQuery("maxResults", maxResults.ToString());
            }

            if (startAt != null)
            {
                qb.AppendQuery("startAt", startAt.ToString());
            }

            return client.Get<IEnumerable<User>>(qb.Uri.ToString());
        }
    }
}
