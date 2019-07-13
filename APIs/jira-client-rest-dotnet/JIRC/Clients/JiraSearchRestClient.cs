// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraSearchRestClient.cs" company="David Bevin">
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
    /// The REST client for accessing Search and Filter information from JIRA.
    /// </summary>
    internal class JiraSearchRestClient : ISearchRestClient
    {
        private const string FilterUriPrefix = "filter";

        private const string SearchUriPrefix = "search";

        private const string FavouruteUriPostfix = "favourite";

        private const int MaxJqlLengthForGet = 500;

        private readonly JsonServiceClient client;

        private readonly Uri searchUri;

        /// <summary>
        /// Initializes a new instance of the filter/search REST client.
        /// </summary>
        /// <param name="client">The JSON client that has been set up for a specific JIRA instance.</param>
        public JiraSearchRestClient(JsonServiceClient client)
        {
            this.client = client;

            searchUri = new Uri(client.BaseUri).Append(SearchUriPrefix);
        }

        /// <summary>
        /// Performs a JQL search and returns issues matching the query.
        /// </summary>
        /// <param name="jql">A valid JQL query. Restricted JQL characters (like '/') must be properly escaped.</param>
        /// <returns>The issues that match a given JQL query.</returns>
        /// <exception cref="WebServiceException">There was a problem parsing the JQL query..</exception>
        public SearchResult SearchJql(string jql)
        {
            return SearchJql(jql, null, null);
        }

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
        public SearchResult SearchJql(string jql, int? maxResults, int? startAt, params string[] fields)
        {
            var qb = new UriBuilder(searchUri);
            qb.AppendQuery("jql", jql);

            if (maxResults != null)
            {
                qb.AppendQuery("maxResults", maxResults.ToString());
            }

            if (startAt != null)
            {
                qb.AppendQuery("startAt", startAt.ToString());
            }

            if (fields != null && fields.Length > 0)
            {
                qb.AppendQuery("fields", fields.Join(","));
            }

            var query = qb.Uri.ToString();

            return query.Length > MaxJqlLengthForGet ? client.Post<SearchResult>(query, string.Empty) : client.Get<SearchResult>(query);
        }

        /// <summary>
        /// Retrieves a list of the callers favorite filters.
        /// </summary>
        /// <returns>List of filters.</returns>
        public IEnumerable<Filter> GetFavouriteFilters()
        {
            return client.Get<IEnumerable<Filter>>("/{0}/{1}".Fmt(FilterUriPrefix, FavouruteUriPostfix));
        }

        /// <summary>
        /// Retrieves a filter for the given URI.
        /// </summary>
        /// <param name="filterUri">The URI of the filter resource to retrieve.</param>
        /// <returns>The filter requested.</returns>
        /// <exception cref="WebServiceException">The URI does not exist, or if the calling user does not have permission to access the filter.</exception>
        public Filter GetFilter(Uri filterUri)
        {
            return client.Get<Filter>(filterUri.ToString());
        }

        /// <summary>
        /// Retrieves a filter with the given Id.
        /// </summary>
        /// <param name="id">The ID of the filter to retrieve.</param>
        /// <returns>The filter requested.</returns>
        /// <exception cref="WebServiceException">A filter with the specified ID does not exist, or if the calling user does not have permission to access the filter.</exception>
        public Filter GetFilter(int id)
        {
            return client.Get<Filter>("/{0}/{1}".Fmt(FilterUriPrefix, id));
        }
    }
}
