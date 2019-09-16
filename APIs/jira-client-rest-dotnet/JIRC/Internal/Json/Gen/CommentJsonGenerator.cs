// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentJsonGenerator.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JIRC.Domain;

using ServiceStack.Text;

namespace JIRC.Internal.Json.Gen
{
    internal static class CommentJsonGenerator
    {
        internal static JsonObject Generate(Comment comment, ServerInfo serverInfo)
        {
            var json = new JsonObject();

            if (comment.Body != null)
            {
                json.Add("body", comment.Body);
            }

            if (comment.Visibility != null)
            {
                if (serverInfo.BuildNumber >= ServerVersionConstants.BuildNumberJira43)
                {
                    var visibilityJson = new JsonObject();
                    var commentVisibilityType = comment.Visibility.Type == Visibility.VisibilityType.Group ? "group" : "role";

                    if (serverInfo.BuildNumber < ServerVersionConstants.BuildNumberJira5)
                    {
                        commentVisibilityType = commentVisibilityType.ToUpper();
                    }

                    visibilityJson.Add("type", commentVisibilityType);
                    visibilityJson.Add("value", comment.Visibility.Value);
                    json.Add("visibility", visibilityJson.ToJson());
                }
                else
                {
                    json.Add(comment.Visibility.Type == Visibility.VisibilityType.Role ? "role" : "group", comment.Visibility.Value);
                }
            }

            return json;
        }
    }
}
