// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraComponentRestClient.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JIRC.Domain;
using JIRC.Domain.Input;
using JIRC.Extensions;
using JIRC.Internal.Json.Gen;

using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

namespace JIRC.Clients
{
    /// <summary>
    /// The REST client for components in JIRA.
    /// </summary>
    internal class JiraComponentRestClient : IComponentRestClient
    {
        private readonly JsonServiceClient client;

        private readonly Uri baseComponentUri;

        /// <summary>
        /// Initializes a new instance of the component REST client.
        /// </summary>
        /// <param name="client">The JSON client that has been set up for a specific JIRA instance.</param>
        public JiraComponentRestClient(JsonServiceClient client)
        {
            this.client = client;
            baseComponentUri = new Uri(client.BaseUri).Append("component");
        }

        /// <summary>
        /// Gets details of a component.
        /// </summary>
        /// <param name="componentUri">The URI for the selected component resource.</param>
        /// <returns>Returns detailed information about the component.</returns>
        /// <exception cref="WebServiceException">There is no component with the given key, or if the calling user does not have permission to view the component.</exception>
        public Component GetComponent(Uri componentUri)
        {
            return client.Get<Component>(componentUri.ToString());
        }

        /// <summary>
        /// Creates a new component in a project.
        /// </summary>
        /// <param name="projectKey">The key for the project in which to create the new component.</param>
        /// <param name="componentInput">A class containing the essential information about the component.</param>
        /// <returns>The newly created component.</returns>
        /// <exception cref="WebServiceException">The caller is not logged in and does not have permission to create components in the project.</exception>
        public Component CreateComponent(string projectKey, ComponentInput componentInput)
        {
            var helper = new ComponentInputWithProjectKey(projectKey, componentInput);
            var json = ComponentInputWithProjectKeyJsonGenerator.Generate(helper);
            return client.Post<Component>(baseComponentUri.ToString(), json);
        }

        /// <summary>
        /// Creates a new component in a project.
        /// </summary>
        /// <param name="componentUri">The URI for the selected component.</param>
        /// <param name="componentInput">A class containing the essential information about the component.</param>
        /// <returns>The updated component.</returns>
        /// <exception cref="WebServiceException">The caller is not logged in and does not have permission to edit components.</exception>
        public Component UpdateComponent(Uri componentUri, ComponentInput componentInput)
        {
            var helper = new ComponentInputWithProjectKey(null, componentInput);
            var json = ComponentInputWithProjectKeyJsonGenerator.Generate(helper);
            return client.Put<Component>(componentUri.ToString(), json);
        }

        /// <summary>
        /// Deletes a project component.
        /// </summary>
        /// <param name="componentUri">The URI of the component to delete.</param>
        /// <exception cref="WebServiceException">The caller is not logged in and does not have permission to delete the component.</exception>
        public void RemoveComponent(Uri componentUri)
        {
            RemoveComponent(componentUri, null);
        }

        /// <summary>
        /// Deletes a project component.
        /// </summary>
        /// <param name="componentUri">The URI of the component to delete.</param>
        /// <param name="moveIssueToComponentUri">Any issues assigned to the component being deleted will be moved to the specified component.</param>
        /// <exception cref="WebServiceException">The caller is not logged in and does not have permission to delete the component.</exception>
        public void RemoveComponent(Uri componentUri, Uri moveIssueToComponentUri)
        {
            var qb = new UriBuilder(componentUri);
            if (moveIssueToComponentUri != null)
            {
                qb.AppendQuery("moveIssuesTo", moveIssueToComponentUri.ToString());
            }

            client.Delete<JsonObject>(qb.Uri.ToString());
        }

        /// <summary>
        /// Returns count of issues related to this component.
        /// </summary>
        /// <param name="componentUri">The URI for the selected component.</param>
        /// <returns>The number of issues associated with the component.</returns>
        /// <exception cref="WebServiceException">The caller is not logged in and does not have permission to view the component.</exception>
        public int GetComponentRelatedIssuesCount(Uri componentUri)
        {
            var uri = componentUri.Append("relatedIssueCounts");
            var json = client.Get<JsonObject>(uri.ToString());
            return json.Get<int>("issueCount");
        }
    }
}
