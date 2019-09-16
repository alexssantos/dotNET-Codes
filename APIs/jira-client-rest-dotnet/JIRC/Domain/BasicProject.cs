// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicProject.cs" company="David Bevin">
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
    /// Basic information about a project.
    /// </summary>
    public class BasicProject
    {
        /// <summary>
        /// Initializes a new instance of basic information for a project.
        /// </summary>
        /// <param name="self">The URI for the project resource.</param>
        /// <param name="key">The project key.</param>
        /// <param name="name">The name of the project.</param>
        internal BasicProject(Uri self, string key, string name)
        {
            Self = self;
            Key = key;
            Name = name;
        }

        /// <summary>
        /// Gets the key for the project.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the URI of the project resource.
        /// </summary>
        public Uri Self { get; private set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// True if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="other">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((BasicProject)other);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Key != null ? this.Key.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Self != null ? this.Self.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(BasicProject other)
        {
            return string.Equals(this.Key, other.Key) && string.Equals(this.Name, other.Name) && Equals(this.Self, other.Self);
        }
    }
}
