// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Filter.cs" company="David Bevin">
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
    public class Filter : AddressableNamedEntity
    {
        public Filter(Uri self, long id, string name, string description, string jql, Uri viewUrl, Uri searchUrl, BasicUser owner, bool favourite)
            : base(self, name)
        {
            Id = id;
            Description = description;
            Jql = jql;
            ViewUrl = viewUrl;
            SearchUrl = searchUrl;
            Owner = owner;
            Favourite = favourite;
        }

        public long Id { get; private set; }

        public string Description { get; private set; }

        public BasicUser Owner { get; private set; }

        public Uri ViewUrl { get; private set; }

        public Uri SearchUrl { get; private set; }

        public string Jql { get; private set; }

        public bool Favourite { get; private set; }
    }
}
