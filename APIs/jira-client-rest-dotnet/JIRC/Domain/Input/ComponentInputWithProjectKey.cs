// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentInputWithProjectKey.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain.Input
{
    public class ComponentInputWithProjectKey : ComponentInput
    {
        public ComponentInputWithProjectKey(string projectKey, ComponentInput componentInput)
            : base(componentInput)
        {
            ProjectKey = projectKey;
        }

        public string ProjectKey { get; set; }
    }
}
