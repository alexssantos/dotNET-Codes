// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraIssueRestClient.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

namespace JIRC.Internal.Tests.Json
{
    /// <summary>
    /// Mock JSON client so that we can send requests to edit Issue fields without actually doing it.
    /// </summary>
    public class MockJsonServiceClient: JsonServiceClient
    {
        /// <summary>
        /// Gets the last Put request submitted.
        /// </summary>
        public JsonObject PutRequest { get; private set; }

        /// <summary>
        /// Override put requests used for editing fields.
        /// </summary>
        /// <typeparam name="TResponse">Response type to expect.</typeparam>
        /// <param name="relativeOrAbsoluteUrl">Action Uri.</param>
        /// <param name="request">Request to make.</param>
        /// <returns>Action response.</returns>
        public override TResponse Put<TResponse>(string relativeOrAbsoluteUrl, object request)
        {
            // Capture the request and do nothing otherwise.
            this.PutRequest = request as JsonObject;
            return default(TResponse);
        }
    }
}
