// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraMetadataRestClient.cs" company="David Bevin">
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

namespace JIRC.Clients
{
    /// <summary>
    /// The REST client for accessing Metadata in a JIRA instance.
    /// </summary>
    internal class JiraMetadataRestClient : IMetadataRestClient
    {
        private const string ServerInfoResource = "/serverInfo";

        private const string IssueTypeResource = "/issuetype";

        private const string IssueLinkTypeResource = "/issueLinkType";

        private const string PriorityResource = "/priority";

        private const string ResolutionResource = "/resolution";

        private const string FieldResource = "/field";

        private readonly JsonServiceClient client;

        /// <summary>
        /// Initializes a new instance of the Projects Roles REST client.
        /// </summary>
        /// <param name="client">The JSON client that has been set up for a specific JIRA instance.</param>
        public JiraMetadataRestClient(JsonServiceClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Retrieves detailed information about the selected issue type.
        /// </summary>
        /// <param name="uri">The URI of the issue type resource.</param>
        /// <returns>Detailed information about an issue type.</returns>
        /// <exception cref="WebServiceException">The issue type was not found, or the calling user does not have permission to view it.</exception>
        public IssueType GetIssueType(Uri uri)
        {
            return client.Get<IssueType>(uri.ToString());
        }

        /// <summary>
        /// Retrieves a complete list of available issue types.
        /// </summary>
        /// <returns>Detailed information about all available issue types.</returns>
        public IEnumerable<IssueType> GetIssueTypes()
        {
            return client.Get<IEnumerable<IssueType>>(IssueTypeResource);
        }

        /// <summary>
        /// Retrieves a complete list of all available issue link types.
        /// </summary>
        /// <returns>Detailed information about all available issue link types.</returns>
        public IEnumerable<IssueLinksType> GetIssueLinkTypes()
        {
            return client.Get<IEnumerable<IssueLinksType>>(IssueLinkTypeResource);
        }

        /// <summary>
        /// Retrieves detailed information about a selected status.
        /// </summary>
        /// <param name="uri">The URI of the status resource.</param>
        /// <returns>Detailed information about a status.</returns>
        /// <exception cref="WebServiceException">The status resource was not found, or the calling user does not have permission to view it.</exception>
        public Status GetStatus(Uri uri)
        {
            return client.Get<Status>(uri.ToString());
        }

        /// <summary>
        /// Retrieves detailed information about a selected priority.
        /// </summary>
        /// <param name="uri">The URI of the priority resource.</param>
        /// <returns>Detailed information about a priority.</returns>
        /// <exception cref="WebServiceException">The priority resource was not found, or the calling user does not have permission to view it.</exception>
        public Priority GetPriority(Uri uri)
        {
            return client.Get<Priority>(uri.ToString());
        }

        /// <summary>
        /// Retrieves a complete list of all available priorities.
        /// </summary>
        /// <returns>Detailed information about the priorities.</returns>
        /// <exception cref="WebServiceException">There was a problem communicating with the JIRA instance.</exception>
        public IEnumerable<Priority> GetPriorities()
        {
            return client.Get<IEnumerable<Priority>>(PriorityResource);
        }

        /// <summary>
        /// Retrieves detailed information about a selected resolution.
        /// </summary>
        /// <param name="uri">The URI of the resolution resource.</param>
        /// <returns>Detailed information about the resolution.</returns>
        /// <exception cref="WebServiceException">The resolution resource was not found, or the calling user does not have permission to view it.</exception>
        public Resolution GetResolution(Uri uri)
        {
            return client.Get<Resolution>(uri.ToString());
        }

        /// <summary>
        /// Retrieves a complete list of all available resolutions.
        /// </summary>
        /// <returns>Detailed information about the resolutions.</returns>
        /// <exception cref="WebServiceException">There was a problem communicating with the JIRA instance.</exception>
        public IEnumerable<Resolution> GetResolutions()
        {
            return client.Get<IEnumerable<Resolution>>(ResolutionResource);
        }

        /// <summary>
        /// Retrieves information about the JIRA instance.
        /// </summary>
        /// <returns>Information about the JIRA instance.</returns>
        /// <exception cref="WebServiceException">There was a problem communicating with the JIRA instance.</exception>
        public ServerInfo GetServerInfo()
        {
            return client.Get<ServerInfo>(ServerInfoResource);
        }

        /// <summary>
        /// Retrieves information about JIRA custom and system fields.
        /// </summary>
        /// <returns>Information about fields in JIRA.</returns>
        /// <exception cref="WebServiceException">There was a problem communicating with the JIRA instance.</exception>
        public IEnumerable<Field> GetFields()
        {
            return client.Get<IEnumerable<Field>>(FieldResource);
        }
    }
}
