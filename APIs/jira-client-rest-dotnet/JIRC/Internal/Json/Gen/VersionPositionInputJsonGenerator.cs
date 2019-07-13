// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionPositionInputJsonGenerator.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JIRC.Domain.Input;

using ServiceStack.Text;

namespace JIRC.Internal.Json.Gen
{
    internal static class VersionPositionInputJsonGenerator
    {
        internal static JsonObject Generate(VersionPosition versionPosition)
        {
            string posValue;

            switch (versionPosition)
            {
                case VersionPosition.First:
                    posValue = "First";
                    break;

                case VersionPosition.Last:
                    posValue = "Last";
                    break;

                case VersionPosition.Earlier:
                    posValue = "Earlier";
                    break;

                case VersionPosition.Later:
                    posValue = "Later";
                    break;

                default:
                    throw new ArgumentException("Invalid version position value", "versionPosition");
            }

            return new JsonObject { { "position", posValue } };
        }
    }
}
