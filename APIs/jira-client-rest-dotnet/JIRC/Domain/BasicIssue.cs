// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicIssue.cs" company="David Bevin">
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
    public class BasicIssue
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public Uri Self { get; set; }
    }
}
