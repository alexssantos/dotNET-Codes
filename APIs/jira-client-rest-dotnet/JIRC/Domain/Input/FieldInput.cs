// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldInput.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Domain.Input
{
    public class FieldInput
    {
        public FieldInput(string id, object value)
        {
            Id = id;
            Value = value;
        }

        public FieldInput(IssueFieldId field, object value)
        {
            Id = field.ToRestString();
            Value = value;
        }

        public string Id { get; set; }

        public object Value { get; set; }
    }
}
