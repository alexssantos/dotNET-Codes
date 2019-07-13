// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Comment.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace JIRC.Domain
{
    public class Comment
    {
        public Comment(Uri self, string body, BasicUser author, BasicUser updateAuthor, DateTimeOffset? creationDate, DateTimeOffset? updateDate, Visibility visibility, long? id)
        {
            Author = author;
            UpdateAuthor = updateAuthor;
            CreationDate = creationDate;
            UpdateDate = updateDate;
            Body = body;
            Self = self;
            Visibility = visibility;
            Id = id;
        }

        public BasicUser Author { get; private set; }

        public string Body { get; private set; }

        public DateTimeOffset? CreationDate { get; private set; }

        public long? Id { get; private set; }

        public Uri Self { get; private set; }

        public BasicUser UpdateAuthor { get; private set; }

        public DateTimeOffset? UpdateDate { get; private set; }

        public Visibility Visibility { get; private set; }

        public static Comment Create(string body)
        {
            return new Comment(null, body, null, null, null, null, null, null);
        }

        public static Comment CreateWithRoleLevel(string body, string roleLevel)
        {
            return new Comment(null, body, null, null, null, null, Visibility.Role(roleLevel), null);
        }

        public static Comment CreateWithGroupLevel(string body, string groupLevel)
        {
            return new Comment(null, body, null, null, null, null, Visibility.Group(groupLevel), null);
        }
    }
}
