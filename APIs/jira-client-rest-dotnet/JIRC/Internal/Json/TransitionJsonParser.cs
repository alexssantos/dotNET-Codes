// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransitionJsonParser.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using JIRC.Domain;

using ServiceStack.Text;

namespace JIRC.Internal.Json
{
    internal static class TransitionJsonParser
    {
        public static IEnumerable<Transition> Parse(JsonObject json)
        {
            return
                json.ArrayObjects("transitions")
                    .ConvertAll(
                        x =>
                            new Transition
                            {
                                Id = x.Get<int>("id"),
                                Name = x.Get("name"),
                                To = x.Get<Transition.TransitionTo>("to"),
                                Fields = ParseFields(x.Get<Dictionary<string, JsonObject>>("fields"))
                            });
        }

        private static IEnumerable<Transition.TransitionField> ParseFields(Dictionary<string, JsonObject> json)
        {
            return
                json.Select(
                    kvp => new Transition.TransitionField { Id = kvp.Key, Required = kvp.Value.Get<bool>("required"), Type = kvp.Value.Get<JsonObject>("schema").Get("type") })
                    .ToList();
        }
    }
}
