// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulkOperationErrorResult.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JIRC.Domain.Util;

namespace JIRC.Domain
{
    public class BulkOperationErrorResult
    {
        public BulkOperationErrorResult(ErrorCollection errors, int failedElementNumber)
        {
            Errors = errors;
            FailedElementNumber = failedElementNumber;
        }

        public ErrorCollection Errors { get; set; }

        public int FailedElementNumber { get; set; }
    }
}
