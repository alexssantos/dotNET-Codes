// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CimProject.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace JIRC.Domain
{
    public class CimProject : BasicProject
    {
        public CimProject(Uri self, string key, string name, Dictionary<string, Uri> avatarUris, IEnumerable<CimIssueType> issueTypes)
            : base(self, key, name)
        {
            if (avatarUris == null)
            {
                throw new ArgumentNullException("avatarUris");
            }

            if (!avatarUris.ContainsKey(AvatarSizes.Standard))
            {
                throw new ArgumentException("At least one avatar Url is expected - for 48x48", "avatarUris");
            }

            AvatarUri = avatarUris.Where(a => a.Key == AvatarSizes.Standard).Select(a => a.Value).FirstOrDefault();
            MediumAvatarUri = avatarUris.Where(a => a.Key == AvatarSizes.Medium).Select(a => a.Value).FirstOrDefault();
            SmallAvatarUri = avatarUris.Where(a => a.Key == AvatarSizes.Small).Select(a => a.Value).FirstOrDefault();
            ExtraSmallAvatarUri = avatarUris.Where(a => a.Key == AvatarSizes.ExtraSmall).Select(a => a.Value).FirstOrDefault();

            IssueTypes = issueTypes;
        }

        public Uri AvatarUri { get; private set; }

        public Uri MediumAvatarUri { get; private set; }

        public Uri SmallAvatarUri { get; private set; }

        public Uri ExtraSmallAvatarUri { get; private set; }

        public IEnumerable<CimIssueType> IssueTypes { get; private set; }
    }
}
