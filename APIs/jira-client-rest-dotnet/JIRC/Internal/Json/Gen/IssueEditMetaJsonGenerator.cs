// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueEditMetaJsonGenerator.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using JIRC.Domain;
using JIRC.Domain.Input;

using ServiceStack.Common.Extensions;
using ServiceStack.Text;

namespace JIRC.Internal.Json.Gen
{
    /// <summary>
    /// Handles generation of a request to update fields on an issue..
    /// </summary>
    internal static class IssueEditMetaJsonGenerator
    {
        /// <summary>
        /// Generates the JSON.
        /// </summary>
        /// <param name="fieldChanges">The changes to make to the issue.</param>
        /// <returns>The JSON.</returns>
        internal static JsonObject Generate(IList<UpdateFieldInput> fieldChanges)
        {
            var jsonObject = new JsonObject();
            var list = new JsonObject();

            if (fieldChanges != null)
            {
                foreach (var change in fieldChanges)
                {
                    // Serialize any custom fields using their custom converters if they are defined.
                    // Default conversion calls .ToString() otherwise on non-null values, and returns null on null values.
                    list.Add(change.FieldName, change.Elements.ConvertAll(a => new JsonObject { { a.Operation.ToRestString(), CustomFieldTypes.Serialize(change.FieldName, a.Value) } }).ToJson());
                }
            }

            jsonObject.Add("update", list.ToJson());
            return jsonObject;
        }
    }
}
