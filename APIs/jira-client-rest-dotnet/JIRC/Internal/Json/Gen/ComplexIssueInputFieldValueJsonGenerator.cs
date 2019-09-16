// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComplexIssueInputFieldValueJsonGenerator.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using JIRC.Domain.Input;

namespace JIRC.Internal.Json.Gen
{
    internal static class ComplexIssueInputFieldValueJsonGenerator
    {
        public static object GenerateFieldValueForJson(object rawValue)
        {
            if (rawValue == null)
            {
                return null;
            }

            if (rawValue is ComplexIssueInputFieldValue)
            {
                var complex = rawValue as ComplexIssueInputFieldValue;
                return complex.ValuesMap.ToDictionary(val => val.Key, val => GenerateFieldValueForJson(val.Value));
            }

            if (rawValue is List<ComplexIssueInputFieldValue>)
            {
                var inputArray = rawValue as List<ComplexIssueInputFieldValue>;
                return inputArray.ConvertAll(GenerateFieldValueForJson);
            }

            return rawValue;
        }
    }
}
