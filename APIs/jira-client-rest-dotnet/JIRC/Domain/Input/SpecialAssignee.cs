// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecialAssignee.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain.Input
{
    /// <summary>
    /// An special assignment operation.
    /// </summary>
    public enum SpecialAssignee
    {
        /// <summary>
        /// Assign automatically based on the project configuration.
        /// </summary>
        Automatic,

        /// <summary>
        /// Unassigned from the current assignee.
        /// </summary>
        Unassigned
    }
}
