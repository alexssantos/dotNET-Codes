// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicUser.cs" company="David Bevin">
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
    public class BasicUser : AddressableNamedEntity
    {
        public BasicUser(Uri self, string name, string displayName)
            : base(self, name)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; private set; }
    }
}
