// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicResolution.cs" company="David Bevin">
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
    public class BasicResolution : AddressableNamedEntity
    {
        public BasicResolution(Uri self, string name)
            : base(self, name)
        {
        }
    }
}
