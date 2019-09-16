// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentInputWithProjectKeyJsonGenerator.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JIRC.Domain;
using JIRC.Domain.Input;

using ServiceStack.Text;

namespace JIRC.Internal.Json.Gen
{
    internal static class ComponentInputWithProjectKeyJsonGenerator
    {
        internal static JsonObject Generate(ComponentInputWithProjectKey componentInput)
        {
            if (componentInput == null)
            {
                throw new ArgumentNullException("componentInput");
            }

            var res = new JsonObject();

            if (componentInput.ProjectKey != null)
            {
                res.Add("project", componentInput.ProjectKey);
            }

            if (componentInput.Name != null)
            {
                res.Add("name", componentInput.Name);
            }

            if (componentInput.Description != null)
            {
                res.Add("description", componentInput.Description);
            }

            if (componentInput.LeadUserName != null)
            {
                res.Add("leadUserName", componentInput.LeadUserName);
            }

            switch (componentInput.AssigneeType)
            {
                case AssigneeType.ProjectDefault:
                    res.Add("assigneeType", "PROJECT_DEFAULT");
                    break;

                case AssigneeType.ComponentLead:
                    res.Add("assigneeType", "COMPONENT_LEAD");
                    break;

                case AssigneeType.ProjectLead:
                    res.Add("assigneeType", "PROJECT_LEAD");
                    break;

                case AssigneeType.Unassigned:
                    res.Add("assigneeType", "UNASSIGNED");
                    break;

                default:
                    throw new ArgumentException("Assignee type is invalid", "componentInput");
            }

            return res;
        }
    }
}
