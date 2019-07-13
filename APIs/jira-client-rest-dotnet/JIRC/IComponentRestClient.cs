// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IComponentRestClient.cs" company="David Bevin">
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

using ServiceStack.ServiceClient.Web;

namespace JIRC
{
    /// <summary>
    /// An interface for handling Components in JIRA.
    /// </summary>
    public interface IComponentRestClient
    {
        /// <summary>
        /// Gets details of a component.
        /// </summary>
        /// <param name="componentUri">The URI for the selected component resource.</param>
        /// <returns>Returns detailed information about the component.</returns>
        /// <exception cref="WebServiceException">There is no component with the given key, or if the calling user does not have permission to view the component.</exception>
        Component GetComponent(Uri componentUri);

        /// <summary>
        /// Creates a new component in a project.
        /// </summary>
        /// <param name="projectKey">The key for the project in which to create the new component.</param>
        /// <param name="componentInput">A class containing the essential information about the component.</param>
        /// <returns>The newly created component.</returns>
        /// <exception cref="WebServiceException">The caller is not logged in and does not have permission to create components in the project.</exception>
        Component CreateComponent(string projectKey, ComponentInput componentInput);

        /// <summary>
        /// Creates a new component in a project.
        /// </summary>
        /// <param name="componentUri">The URI for the selected component.</param>
        /// <param name="componentInput">A class containing the essential information about the component.</param>
        /// <returns>The updated component.</returns>
        /// <exception cref="WebServiceException">The caller is not logged in and does not have permission to edit components.</exception>
        Component UpdateComponent(Uri componentUri, ComponentInput componentInput);

        /// <summary>
        /// Deletes a project component.
        /// </summary>
        /// <param name="componentUri">The URI of the component to delete.</param>
        /// <exception cref="WebServiceException">The caller is not logged in and does not have permission to delete the component.</exception>
        void RemoveComponent(Uri componentUri);

        /// <summary>
        /// Deletes a project component.
        /// </summary>
        /// <param name="componentUri">The URI of the component to delete.</param>
        /// <param name="moveIssueToComponentUri">Any issues assigned to the component being deleted will be moved to the specified component.</param>
        /// <exception cref="WebServiceException">The caller is not logged in and does not have permission to delete the component.</exception>
        void RemoveComponent(Uri componentUri, Uri moveIssueToComponentUri);

        /// <summary>
        /// Returns count of issues related to this component.
        /// </summary>
        /// <param name="componentUri">The URI for the selected component.</param>
        /// <returns>The number of issues associated with the component.</returns>
        /// <exception cref="WebServiceException">The caller is not logged in and does not have permission to view the component.</exception>
        int GetComponentRelatedIssuesCount(Uri componentUri);
    }
}
