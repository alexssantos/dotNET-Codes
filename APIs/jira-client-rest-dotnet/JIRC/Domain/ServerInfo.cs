// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerInfo.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace JIRC.Domain
{
    public class ServerInfo
    {
        public Uri BaseUrl { get; set; }

        public string Version { get; set; }

        public int BuildNumber { get; set; }

        public DateTimeOffset BuildDate { get; set; }

        public DateTimeOffset? ServerTime { get; set; }

        public string ScmInfo { get; set; }

        public string ServerTitle { get; set; }
    }
}
