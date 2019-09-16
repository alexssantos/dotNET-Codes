// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionInput.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace JIRC.Domain.Input
{
    public class VersionInput
    {
        public string ProjectKey { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? ReleaseDate { get; set; }

        public bool Archived { get; set; }

        public bool Released { get; set; }
    }
}
