// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleActor.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain
{
    public class RoleActor
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string AvatarUrl { get; set; }
    }
}
