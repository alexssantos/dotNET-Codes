// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMetadataRestClient.cs" company="David Bevin">
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
    /// An interface for handling Metadata in JIRA.
    /// </summary>
    public interface IMetadataRestClient
    {
        /// <summary>
        /// Retrieves detailed information about the selected issue type.
        /// </summary>
        /// <param name="uri">The URI of the issue type resource.</param>
        /// <returns>Detailed information about an issue type.</returns>
        /// <exception cref="WebServiceException">The issue type was not found, or the calling user does not have permission to view it.</exception>
        IssueType GetIssueType(Uri uri);

        /// <summary>
        /// Retrieves a complete list of available issue types.
        /// </summary>
        /// <returns>Detailed information about all available issue types.</returns>
        IEnumerable<IssueType> GetIssueTypes();

        /// <summary>
        /// Retrieves a complete list of all available issue link types.
        /// </summary>
        /// <returns>Detailed information about all available issue link types.</returns>
        IEnumerable<IssueLinksType> GetIssueLinkTypes();

        /// <summary>
        /// Retrieves detailed information about a selected status.
        /// </summary>
        /// <param name="uri">The URI of the status resource.</param>
        /// <returns>Detailed information about a status.</returns>
        /// <exception cref="WebServiceException">The status resource was not found, or the calling user does not have permission to view it.</exception>
        Status GetStatus(Uri uri);

        /// <summary>
        /// Retrieves detailed information about a selected priority.
        /// </summary>
        /// <param name="uri">The URI of the priority resource.</param>
        /// <returns>Detailed information about a priority.</returns>
        /// <exception cref="WebServiceException">The priority resource was not found, or the calling user does not have permission to view it.</exception>
        Priority GetPriority(Uri uri);

        /// <summary>
        /// Retrieves a complete list of all available priorities.
        /// </summary>
        /// <returns>Detailed information about the priorities.</returns>
        /// <exception cref="WebServiceException">There was a problem communicating with the JIRA instance.</exception>
        IEnumerable<Priority> GetPriorities();

        /// <summary>
        /// Retrieves detailed information about a selected resolution.
        /// </summary>
        /// <param name="uri">The URI of the resolution resource.</param>
        /// <returns>Detailed information about the resolution.</returns>
        /// <exception cref="WebServiceException">The resolution resource was not found, or the calling user does not have permission to view it.</exception>
        Resolution GetResolution(Uri uri);

        /// <summary>
        /// Retrieves a complete list of all available resolutions.
        /// </summary>
        /// <returns>Detailed information about the resolutions.</returns>
        /// <exception cref="WebServiceException">There was a problem communicating with the JIRA instance.</exception>
        IEnumerable<Resolution> GetResolutions();

        /// <summary>
        /// Retrieves information about the JIRA instance.
        /// </summary>
        /// <returns>Information about the JIRA instance.</returns>
        /// <exception cref="WebServiceException">There was a problem communicating with the JIRA instance.</exception>
        ServerInfo GetServerInfo();

        /// <summary>
        /// Retrieves information about JIRA custom and system fields.
        /// </summary>
        /// <returns>Information about fields in JIRA.</returns>
        /// <exception cref="WebServiceException">There was a problem communicating with the JIRA instance.</exception>
        IEnumerable<Field> GetFields();
    }
}
