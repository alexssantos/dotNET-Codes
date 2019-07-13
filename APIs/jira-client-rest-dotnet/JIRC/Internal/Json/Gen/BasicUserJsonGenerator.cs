// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicUserJsonGenerator.cs" company="David Bevin">
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
    internal static class BasicUserJsonGenerator
    {
        internal static JsonObject Generate(BasicUser user)
        {
            return new JsonObject { { "self", user.Self.ToString() }, { "name", user.Name }, { "displayName", user.DisplayName } };
        }
    }
}
