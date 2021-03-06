﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressableNamedEntity.cs" company="David Bevin">
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
    public class AddressableNamedEntity
    {
        protected AddressableNamedEntity(Uri self, string name)
        {
            Self = self;
            Name = name;
        }

        public string Name { get; protected set; }
        public Uri Self { get; protected set; }
    }
}
