// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearchRestClient.cs" company="David Bevin">
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
    /// An interface for handling Searches in JIRA.
    /// </summary>
    public interface ISearchRestClient
    {
        /// <summary>
        /// Performs a JQL search and returns issues matching the query.
        /// </summary>
        /// <param name="jql">A valid JQL query. Restricted JQL characters (like '/') must be properly escaped.</param>
        /// <returns>The issues that match a given JQL query.</returns>
        /// <exception cref="WebServiceException">There was a problem parsing the JQL query..</exception>
        SearchResult SearchJql(string jql);

        /// <summary>
        /// Performs a JQL search and returns issues matching the query.
        /// </summary>
        /// <param name="jql">A valid JQL query. Restricted JQL characters (like '/') must be properly escaped.</param>
        /// <param name="maxResults">The maximum results for the search. If null, the default number configured in JIRA is used (generally 50).</param>
        /// <param name="startAt">Starting index (0-based) defining how many issues should be skipped in the result set.</param>
        /// <param name="fields">A set of fields which should be retrieved.You can specify *all for all fields\or *navigable (which is the default value, used when null is given) which will cause to include only
        /// navigable fields in the result. To ignore the specific field you can use "-" before the field's name.
        /// Note that the following fields: summary, issuetype, created, updated, project and status are
        /// required. These fields are included in *all and *navigable.</param>
        /// <returns>The issues that match a given JQL query.</returns>
        /// <exception cref="WebServiceException">There was a problem parsing the JQL query..</exception>
        SearchResult SearchJql(string jql, int? maxResults, int? startAt, params string[] fields);

        /// <summary>
        /// Retrieves a list of the callers favorite filters.
        /// </summary>
        /// <returns>List of filters.</returns>
        IEnumerable<Filter> GetFavouriteFilters();

        /// <summary>
        /// Retrieves a filter for the given URI.
        /// </summary>
        /// <param name="filterUri">The URI of the filter resource to retrieve.</param>
        /// <returns>The filter requested.</returns>
        /// <exception cref="WebServiceException">The URI does not exist, or if the calling user does not have permission to access the filter.</exception>
        Filter GetFilter(Uri filterUri);

        /// <summary>
        /// Retrieves a filter with the given Id.
        /// </summary>
        /// <param name="id">The ID of the filter to retrieve.</param>
        /// <returns>The filter requested.</returns>
        /// <exception cref="WebServiceException">A filter with the specified ID does not exist, or if the calling user does not have permission to access the filter.</exception>
        Filter GetFilter(int id);
    }
}
