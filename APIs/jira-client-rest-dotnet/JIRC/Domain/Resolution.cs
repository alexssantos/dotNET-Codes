// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Resolution.cs" company="David Bevin">
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
    public class Resolution : BasicResolution
    {
        public Resolution(Uri self, string name, string descritpion)
            : base(self, name)
        {
            Description = descritpion;
        }

        public string Description { get; private set; }
    }
}
