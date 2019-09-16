// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Priority.cs" company="David Bevin">
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
    public class Priority : BasicPriority
    {
        public Priority(Uri self, long? id, string name, string statusColor, string description, Uri iconUrl)
            : base(self, id, name)
        {
            StatusColor = statusColor;
            Description = description;
            IconUrl = iconUrl;
        }

        // TODO: Convert to a colour object?
        public string StatusColor { get; private set; }

        public string Description { get; private set; }

        public Uri IconUrl { get; private set; }
    }
}
