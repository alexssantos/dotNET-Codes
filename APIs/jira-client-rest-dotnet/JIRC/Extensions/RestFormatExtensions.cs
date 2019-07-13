// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RestFormatExtensions.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;

namespace JIRC.Extensions
{
    public static class RestFormatExtensions
    {
        public static string ToRestString(this DateTimeOffset dt)
        {
            return dt.ToString("o", CultureInfo.InvariantCulture);
        }
    }
}
