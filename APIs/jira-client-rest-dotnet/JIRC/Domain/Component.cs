// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Component.cs" company="David Bevin">
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
    public class Component : BasicComponent
    {
        public Component(Uri self, long? id, string name, string description, BasicUser lead)
            :this(self, id, name, description, lead, null)
        {
        }

        public Component(Uri self, long? id, string name, string description, BasicUser lead, AssigneeInfo assigneeInfo)
            : base(self, id, name, description)
        {
            Lead = lead;
            AssigneeInfo = assigneeInfo;
        }

        public BasicUser Lead { get; private set; }

        public AssigneeInfo AssigneeInfo { get; private set; }
    }
}
