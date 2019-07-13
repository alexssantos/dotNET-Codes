// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueUpdateJsonGenerator.cs" company="David Bevin">
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
    internal static class IssueUpdateJsonGenerator
    {
        internal static JsonObject Generate(IEnumerable<FieldInput> fields)
        {
            var jsonObject = new JsonObject();
            var list = new Dictionary<string, object>();

            if (fields != null)
            {
                foreach (var f in fields.Where(f => f.Value != null))
                {
                    list.Add(f.Id, ComplexIssueInputFieldValueJsonGenerator.GenerateFieldValueForJson(f.Value));
                }
            }

            jsonObject.Add("fields", list.ToJson());
            return jsonObject;
        }
    }
}
