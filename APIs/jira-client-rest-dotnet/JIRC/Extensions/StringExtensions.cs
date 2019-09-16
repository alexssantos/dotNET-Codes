// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Extensions
{
    public static class StringExtensions
    {
        public static string NullToEmpty(this string value)
        {
            return value ?? string.Empty;
        }
    }
}
