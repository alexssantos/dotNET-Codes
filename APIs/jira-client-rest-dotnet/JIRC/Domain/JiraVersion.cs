// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraVersion.cs" company="David Bevin">
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
    /// <summary>
    /// Detailed information about a version defined for a project.
    /// </summary>
    public class JiraVersion
    {
        /// <summary>
        /// Creates the version information.
        /// </summary>
        /// <param name="self">The URI for the version resource.</param>
        /// <param name="id">The ID of the version.</param>
        /// <param name="name">The name of the version.</param>
        /// <param name="description">A description for the version.</param>
        /// <param name="archived">Whether or not the version has been archived.</param>
        /// <param name="released">Whether or not the version has been released.</param>
        /// <param name="releaseDate">The release date of the version.</param>
        internal JiraVersion(Uri self, long? id, string name, string description, bool archived, bool released, DateTimeOffset releaseDate)
        {
            Self = self;
            Id = id;
            Name = name;
            Description = description;
            Archived = archived;
            Released = released;
            ReleaseDate = releaseDate;
        }

        /// <summary>
        /// Gets a value indicating whether the version has been archived.
        /// </summary>
        public bool Archived { get; private set; }

        /// <summary>
        /// Gets the description for the version.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the ID of the version.
        /// </summary>
        public long? Id { get; private set; }

        /// <summary>
        /// Gets the name of the version.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the release date of the version.
        /// </summary>
        public DateTimeOffset? ReleaseDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the version has been released.
        /// </summary>
        public bool Released { get; private set; }

        /// <summary>
        /// Gets the URI for the version resource.
        /// </summary>
        public Uri Self { get; private set; }
    }
}
