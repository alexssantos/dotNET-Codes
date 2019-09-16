// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransitionInputJsonGenerator.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

using JIRC.Domain;
using JIRC.Domain.Input;

using ServiceStack.Text;

namespace JIRC.Internal.Json.Gen
{
    internal static class TransitionInputJsonGenerator
    {
        internal static JsonObject Generate(TransitionInput transitionInput, ServerInfo serverInfo)
        {
            var jsonObject = new JsonObject();
            if (serverInfo.BuildNumber >= ServerVersionConstants.BuildNumberJira5)
            {
                var id = new JsonObject { { "id", transitionInput.Id.ToString() } };
                jsonObject.Add("transition", id.ToJson());
            }
            else
            {
                jsonObject.Add("transition", transitionInput.Id.ToString());
            }

            if (transitionInput.Comment != null)
            {
                var comment = CommentJsonGenerator.Generate(transitionInput.Comment, serverInfo).ToJson();
                if (serverInfo.BuildNumber >= ServerVersionConstants.BuildNumberJira5)
                {
                    var jsonComment = new JsonArrayObjects { new JsonObject { { "add", comment } } };
                    var jsonUpdate = new JsonObject { { "comment", jsonComment.ToJson() } };
                    jsonObject.Add("update", jsonUpdate.ToJson());
                }
                else
                {
                    jsonObject.Add("comment", comment);
                }
            }

            if (transitionInput.Fields != null && transitionInput.Fields.Any())
            {
                var list = transitionInput.Fields.Where(f => f.Value != null).ToDictionary(f => f.Id, f => ComplexIssueInputFieldValueJsonGenerator.GenerateFieldValueForJson(f.Value));
                jsonObject.Add("fields", list.ToJson());
            }

            return jsonObject;
        }
    }
}
