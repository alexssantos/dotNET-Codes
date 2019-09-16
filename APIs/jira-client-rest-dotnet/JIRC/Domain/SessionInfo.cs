// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionInfo.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain
{
    public class SessionInfo
    {
        public SessionToken Session { get; set; }

        public class SessionToken
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
