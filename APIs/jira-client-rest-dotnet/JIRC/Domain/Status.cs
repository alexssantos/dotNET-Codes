// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Status.cs" company="David Bevin">
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
    public class Status : BasicStatus
    {
        public Status(Uri self, string name, string description, Uri iconUrl)
            : base(self, name)
        {
            Description = description;
            IconUrl = iconUrl;
        }

        public string Description { get; private set; }

        public Uri IconUrl { get; private set; }
    }
}
