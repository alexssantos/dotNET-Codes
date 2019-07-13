// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvatarSizes.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain
{
    /// <summary>
    /// A class containing some constants about avatars.
    /// </summary>
    public static class AvatarSizes
    {
        /// <summary>
        /// Key used to represent a REALLY SMALL avatar when stored in a dictionary.
        /// </summary>
        public const string ExtraSmall = "16x16";

        /// <summary>
        /// Key used to represent a SMALL avatar when stored in a dictionary.
        /// </summary>
        public const string Small = "24x24";

        /// <summary>
        /// Key used to represent a MEDIUM avatar when stored in a dictionary.
        /// </summary>
        public const string Medium = "32x32";

        /// <summary>
        /// Key used to represent a STANDARD avatar when stored in a dictionary.
        /// </summary>
        public const string Standard = "48x48";
    }
}
