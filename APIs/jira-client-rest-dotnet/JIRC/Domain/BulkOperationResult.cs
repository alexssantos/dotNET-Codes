// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulkOperationResult.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace JIRC.Domain
{
    public class BulkOperationResult<T>
    {
        public BulkOperationResult(IEnumerable<T> issues, IEnumerable<BulkOperationErrorResult> errors)
        {
            Issues = issues;
            Errors = errors;
        }

        public IEnumerable<T> Issues { get; set; }

        public IEnumerable<BulkOperationErrorResult> Errors { get; set; }
    }
}
