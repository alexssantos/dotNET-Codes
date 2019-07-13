// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldSchema.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain
{
    public class FieldSchema
    {
        public string Type { get; set; }

        public string Items { get; set; }

        public string System { get; set; }

        public string Custom { get; set; }

        public int CustomId { get; set; }
    }
}
