// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueInputJsonGenerator.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using JIRC.Domain.Input;

using ServiceStack.Text;

namespace JIRC.Internal.Json.Gen
{
    internal class IssueInputJsonGenerator
    {
        internal static JsonObject Generate(IssueInput issue)
        {
            var jsonObject = new JsonObject();
            var list = new Dictionary<string, object>();

            if (issue != null && issue.Fields != null)
            {
                foreach (var f in issue.Fields.Values.Where(f => f.Value != null))
                {
                    list.Add(f.Id, ComplexIssueInputFieldValueJsonGenerator.GenerateFieldValueForJson(f.Value));
                }
            }

            jsonObject.Add("fields", list.ToJson());
            return jsonObject;
        }
    }
}
