// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UriExtensions.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Web;

namespace JIRC.Extensions
{
    internal static class UriExtensions
    {
        internal static Uri Append(this Uri uri, string param)
        {
            var relative = new Uri(param, UriKind.Relative);
            return new Uri(Path.Combine(uri.ToString(), relative.ToString()));
        }

        internal static void AppendQuery(this UriBuilder uri, string name, string value)
        {
            var queryToAppend = HttpUtility.UrlEncode(name) + "=" + HttpUtility.UrlEncode(value);

            if (uri.Query.Length > 1)
                uri.Query = uri.Query.Substring(1) + "&" + queryToAppend;
            else
                uri.Query = queryToAppend;
        }
    }
}
