// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorklogInputJsonGenerator.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JIRC.Domain.Input;
using JIRC.Extensions;

using ServiceStack.Text;

namespace JIRC.Internal.Json.Gen
{
    internal static class WorklogInputJsonGenerator
    {
        internal static JsonObject Generate(WorklogInput worklogInput)
        {
            var json = new JsonObject
            {
                { "self", worklogInput.Self.ToString() },
                { "comment", worklogInput.Comment },
                { "started", worklogInput.StartDate.ToRestString() },
                { "timeSpent", worklogInput.MinutesSpent.ToString() }
            };

            if (worklogInput.Visibility != null)
            {
                json.Add("visibility", VisibilityJsonGenerator.Generate(worklogInput.Visibility).ToJson());
            }

            if (worklogInput.Author != null)
            {
                json.Add("author", BasicUserJsonGenerator.Generate(worklogInput.Author).ToJson());
            }

            if (worklogInput.UpdateAuthor != null)
            {
                //json.Add("updateAuthor", BasicUserJsonGenerator.Generate(worklogInput.UpdateAuthor).ToString());
               json.Add("updateAuthor",  worklogInput.UpdateAuthor.ToJson());
            }

            return json;
        }
    }
}
