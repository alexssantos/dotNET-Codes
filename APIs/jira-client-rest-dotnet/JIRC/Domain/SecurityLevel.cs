// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityLevel.cs" company="David Bevin">
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
    public class SecurityLevel : AddressableNamedEntity
    {
        public SecurityLevel(Uri self, long id, string name, string description)
            : base(self, name)
        {
            Id = id;
            Description = description;
        }

        public long Id { get; private set; }

        public string Description { get; private set; }
    }
}
