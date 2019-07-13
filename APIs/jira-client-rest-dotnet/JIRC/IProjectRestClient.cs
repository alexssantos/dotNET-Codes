// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProjectRestClient.cs" company="David Bevin">
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
    /// An interface for handling projects in JIRA.
    /// </summary>
    public interface IProjectRestClient
    {
        /// <summary>
        /// Returns all projects, which are visible for the calling user. If no user is logged in, it returns the list of projects that are visible when using anonymous access.
        /// </summary>
        /// <returns>Projects that the caller user can see.</returns>
        /// <exception cref="WebServiceException">An error occurred when retrieving the list of projects.</exception>
        IEnumerable<BasicProject> GetAllProjects();

        /// <summary>
        /// Retrieves detailed information about a project.
        /// </summary>
        /// <param name="key">The unique key for the project (e.g. "AA").</param>
        /// <returns>Detailed information about the project.</returns>
        /// <exception cref="WebServiceException">The project is not found, or the calling user does not have permission to view it.</exception>
        Project GetProject(string key);

        /// <summary>
        /// Retrieves complete information about given project.
        /// Use this method in preference to <see cref="GetProject(string)"/> wherever you can, as this method will safeguard against URI scheme changes.
        /// </summary>
        /// <param name="projectUri">The URI for the requested project. Usually retrieved by getting the property <see cref="BasicProject.Self"/>.</param>
        /// <returns>Detailed information about the project.</returns>
        /// <exception cref="WebServiceException">The project is not found, or the calling user does not have permission to view it.</exception>
        Project GetProject(Uri projectUri);

        /// <summary>
        /// Retrieves a list of users who may be used as assignee when creating an issue. For a list of users when editing an issue, see <see cref="IIssueRestClient.GetAssignableUsers(string)"/>.
        /// </summary>
        /// <param name="projectKey">The unique key for the project (e.g. "AA").</param>
        /// <returns>Returns a list of users who may be assigned to an issue during creation.</returns>
        /// <exception cref="WebServiceException">The project is not found, or the calling user does not have permission to view it.</exception>
        IEnumerable<User> GetAssignableUsers(string projectKey);

        /// <summary>
        /// Retrieves a list of users who may be used as assignee when creating an issue. For a list of users when editing an issue, see <see cref="IIssueRestClient.GetAssignableUsers(string, int?, int?)"/>.
        /// </summary>
        /// <param name="projectKey">The unique key for the project (e.g. "AA").</param>
        /// <param name="startAt">The index of the first user to return (0-based).</param>
        /// <param name="maxResults">The maximum number of users to return (defaults to 50). The maximum allowed value is 1000. If you specify a value that is higher than this number, your search results will be truncated.</param>
        /// <returns>Returns a list of users who may be assigned to an issue during creation.</returns>
        IEnumerable<User> GetAssignableUsers(string projectKey, int? startAt, int? maxResults);
    }
}
