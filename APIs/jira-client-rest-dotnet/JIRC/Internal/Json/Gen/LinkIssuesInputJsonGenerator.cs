// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkIssuesInputJsonGenerator.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JIRC.Domain;
using JIRC.Domain.Input;

using ServiceStack.Text;

namespace JIRC.Internal.Json.Gen
{
    internal static class LinkIssuesInputJsonGenerator
    {
        internal static JsonObject Generate(LinkIssuesInput linkIssuesInput, ServerInfo serverInfo)
        {
            var json = new JsonObject();

            if (serverInfo.BuildNumber >= ServerVersionConstants.BuildNumberJira5)
            {
                var jsonType = new JsonObject { { "name", linkIssuesInput.LinkType } };
                var jsonInward = new JsonObject { { "key", linkIssuesInput.FromIssueKey } };
                var jsonOutward = new JsonObject { { "key", linkIssuesInput.ToIssueKey } };

                json.Add("type", jsonType.ToJson());
                json.Add("inwardIssue", jsonInward.ToJson());
                json.Add("outwardIssue", jsonOutward.ToJson());
            }
            else
            {
                json.Add("linkType", linkIssuesInput.LinkType);
                json.Add("fromIssueKey", linkIssuesInput.FromIssueKey);
                json.Add("toIssueKey", linkIssuesInput.ToIssueKey);
            }

            if (linkIssuesInput.Comment != null)
            {
                json.Add("comment", CommentJsonGenerator.Generate(linkIssuesInput.Comment, serverInfo).ToJson());
            }

            return json;
        }
    }
}
