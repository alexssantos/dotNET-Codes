// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibilityJsonGenerator.cs" company="David Bevin">
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
    internal static class VisibilityJsonGenerator
    {
        internal static JsonObject Generate(Visibility visibility)
        {
            return new JsonObject { { "type", visibility.Type.ToString().ToLower() }, { "value", visibility.Value }, };
        }
    }
}
