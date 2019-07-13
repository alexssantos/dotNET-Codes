// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueLinksType.cs" company="David Bevin">
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
    public class IssueLinksType : AddressableNamedEntity
    {
        internal IssueLinksType(Uri self, string id, string name, string inward, string outward)
            : base(self, name)
        {
            Id = id;
            Inward = inward;
            Outward = outward;
        }

        public string Id { get; private set; }

        public string Inward { get; private set; }

        public string Outward { get; private set; }
    }
}
