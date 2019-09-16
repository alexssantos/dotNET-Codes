// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorCollection.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace JIRC.Domain.Util
{
    public class ErrorCollection
    {
        public ErrorCollection(int status, IEnumerable<string> errorMessages, IDictionary<string, string> errors)
        {
            Status = status;
            ErrorMessages = errorMessages;
            Errors = errors;
        }

        public int Status { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }

        public IDictionary<string, string> Errors { get; set; }
    }
}
