// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Attachment.cs" company="David Bevin">
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
    public class Attachment
    {
        internal Attachment(Uri self, string filename, BasicUser author, DateTimeOffset creationDate, int size, string mimeType, Uri contentUri, Uri thumbnailUri)
        {
            this.Self = self;
            this.Filename = filename;
            this.Author = author;
            this.CreationDate = creationDate;
            this.Size = size;
            this.MimeType = mimeType;
            this.ContentUri = contentUri;
            this.ThumbnailUri = thumbnailUri;
        }

        public Uri Self { get; private set; }

        public string Filename { get; private set; }

        public BasicUser Author { get; private set; }

        public DateTimeOffset CreationDate { get; private set; }

        public int Size { get; private set; }

        public string MimeType { get; private set; }

        public Uri ContentUri { get; private set; }

        public bool HasThumbnail
        {
            get
            {
                return ThumbnailUri != null;
            }
        }

        public Uri ThumbnailUri { get; private set; }
    }
}
