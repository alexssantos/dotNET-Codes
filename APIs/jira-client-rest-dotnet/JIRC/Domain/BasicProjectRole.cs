// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicProjectRole.cs" company="David Bevin">
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
    /// <summary>
    /// A simple representation of a way to group users.
    /// </summary>
    public class BasicProjectRole
    {
        /// <summary>
        /// Initializes a project role.
        /// </summary>
        /// <param name="self">The URI for us.</param>
        /// <param name="name">The name of the project role.</param>
        internal BasicProjectRole(Uri self, string name)
        {
            Name = name;
            Self = self;
        }

        /// <summary>
        /// Gets the name of the project role.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the URI for the project role resource.
        /// </summary>
        public Uri Self { get; private set; }
    }
}
