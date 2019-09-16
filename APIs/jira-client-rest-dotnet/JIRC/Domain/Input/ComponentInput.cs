// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentInput.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain.Input
{
    public class ComponentInput
    {
        public ComponentInput()
        {
        }

        protected ComponentInput(ComponentInput componentInput)
        {
            Name = componentInput.Name;
            Description = componentInput.Description;
            LeadUserName = componentInput.LeadUserName;
            AssigneeType = componentInput.AssigneeType;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string LeadUserName { get; set; }

        public AssigneeType AssigneeType { get; set; }
    }
}
