// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectRole.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace JIRC.Domain
{
    /// <summary>
    /// A way to group users <see cref="RoleActor"/> with projects.
    /// </summary>
    public class ProjectRole : BasicProjectRole
    {
        /// <summary>
        /// Initializes a new Project Role.
        /// </summary>
        /// <param name="id">The unique Id.</param>
        /// <param name="self">The URI of ourself.</param>
        /// <param name="name">The name of the project role.</param>
        /// <param name="description">The description of the project role.</param>
        /// <param name="actors">Actors associated with the project role.</param>
        internal ProjectRole(long id, Uri self, string name, string description, IEnumerable<RoleActor> actors)
            : base(self, name)
        {
            Id = id;
            Description = description;
            Actors = actors;
        }

        public long Id { get; private set; }

        public string Description { get; private set; }

        public IEnumerable<RoleActor> Actors { get; private set; }
    }
}
