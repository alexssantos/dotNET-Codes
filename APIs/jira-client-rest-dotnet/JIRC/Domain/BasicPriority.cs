﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicPriority.cs" company="David Bevin">
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
    public class BasicPriority : AddressableNamedEntity
    {
        public BasicPriority(Uri self, long? id, string name)
            : base(self, name)
        {
            Id = id;
        }

        public long? Id { get; private set; }
    }
}
