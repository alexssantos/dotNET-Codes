// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssigneeInfo.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain
{
    public class AssigneeInfo
    {
        public BasicUser Assignee { get; set; }
        public AssigneeType AssigneeType { get; set; }
        public BasicUser RealAssignee { get; set; }
        public AssigneeType RealAssigneeType { get; set; }
        public bool AssigneeTypeValid { get; set; }
    }
}
