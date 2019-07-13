// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueLinkType.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain
{
    public class IssueLinkType
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public LinkDirection Direction { get; set; }

        public enum LinkDirection
        {
            Outbound,
            Inbound
        }
    }
}
