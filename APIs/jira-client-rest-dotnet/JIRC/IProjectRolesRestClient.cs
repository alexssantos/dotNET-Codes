// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProjectRolesRestClient.cs" company="David Bevin">
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
    /// An interface for handling Project Roles in JIRA.
    /// </summary>
    public interface IProjectRolesRestClient
    {
        /// <summary>
        /// Retrieves detailed information about the selected role.
        /// </summary>
        /// <param name="uri">The URI for the role resource.</param>
        /// <returns>Detailed information about the selected role.</returns>
        /// <exception cref="WebServiceException">The project role was not found, or the calling user does not have permission to view it.</exception>
        ProjectRole GetRole(Uri uri);

        /// <summary>
        /// Retrieves detailed information about the selected role in the specified project.
        /// </summary>
        /// <param name="projectUri">The URI of the project resource.</param>
        /// <param name="roleId">The Id of the role.</param>
        /// <returns>Detailed information about the selected role.</returns>
        /// <exception cref="WebServiceException">The project role or role ID was not found, or the calling user does not have permission to view it.</exception>
        ProjectRole GetRole(Uri projectUri, int roleId);

        /// <summary>
        /// Retrieves detailed information for all roles for the specified project.
        /// </summary>
        /// <param name="projectUri">The URI of the project resource.</param>
        /// <returns>A collection of all roles for the project.</returns>
        /// <exception cref="WebServiceException">The project was not found, or the calling user does not have permission to view it.</exception>
        IEnumerable<ProjectRole> GetRoles(Uri projectUri);

        /// <summary>
        /// Adds a user to a specific role in the project.
        /// </summary>
        /// <param name="projectUri">The URI of the project resource.</param>
        /// <param name="roleId">The ID of the project role.</param>
        /// <param name="userName">The name of the user to add.</param>
        /// <returns>Detailed information about the selected role.</returns>
        ProjectRole AddUserToRole(Uri projectUri, int roleId, string userName);

        /// <summary>
        /// Adds a group to a specific role in the project.
        /// </summary>
        /// <param name="projectUri">The URI of the project resource.</param>
        /// <param name="roleId">The ID of the project role.</param>
        /// <param name="groupName">The name of the group to add.</param>
        /// <returns>Detailed information about the selected role.</returns>
        ProjectRole AddGroupToRole(Uri projectUri, int roleId, string groupName);
    }
}
