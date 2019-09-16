// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransitionInput.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace JIRC.Domain.Input
{
    public class TransitionInput
    {
        public TransitionInput(int id, IEnumerable<FieldInput> fields)
            : this(id, fields, null)
        {
        }

        public TransitionInput(int id, IEnumerable<FieldInput> fields, Comment comment)
        {
            Id = id;
            Fields = fields;
            Comment = comment;
        }

        public TransitionInput(int id)
            : this(id, new FieldInput[0], null)
        {
        }

        public int Id { get; private set; }

        public Comment Comment { get; private set; }

        public IEnumerable<FieldInput> Fields { get; private set; }
    }
}
