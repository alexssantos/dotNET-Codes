// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="David Bevin">
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
    public class User : BasicUser
    {
        internal User(Uri self, string name, string displayName, string emailAddress, IEnumerable<string> groups, IDictionary<string, Uri> avatarUris, bool active, string timezone)
            : base(self, name, displayName)
        {
            if (avatarUris == null)
            {
                throw new ArgumentNullException("avatarUris");
            }

            if (!avatarUris.ContainsKey(AvatarSizes.Standard))
            {
                throw new ArgumentException("At least one avatar Url is expected - for 48x48", "avatarUris");
            }

            Timezone = timezone;
            Active = active;
            EmailAddress = emailAddress;
            Groups = groups;
            AvatarUri = avatarUris.Where(a => a.Key == AvatarSizes.Standard).Select(a => a.Value).FirstOrDefault();
            MediumAvatarUri = avatarUris.Where(a => a.Key == AvatarSizes.Medium).Select(a => a.Value).FirstOrDefault();
            SmallAvatarUri = avatarUris.Where(a => a.Key == AvatarSizes.Small).Select(a => a.Value).FirstOrDefault();
            ExtraSmallAvatarUri = avatarUris.Where(a => a.Key == AvatarSizes.ExtraSmall).Select(a => a.Value).FirstOrDefault();
        }

        internal User(BasicUser basic, string emaiAddress, IEnumerable<string> groups, IDictionary<string, Uri> avatarUris, bool active, string timezone)
            : this(basic.Self, basic.Name, basic.DisplayName, emaiAddress, groups, avatarUris, active, timezone)
        {
        }

        public string Timezone { get; private set; }

        public bool Active { get; private set; }

        public string EmailAddress { get; private set; }

        public IEnumerable<string> Groups { get; private set; }

        public Uri AvatarUri { get; private set; }

        public Uri MediumAvatarUri { get; private set; }

        public Uri SmallAvatarUri { get; private set; }

        public Uri ExtraSmallAvatarUri { get; private set; }
    }
}
