// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVersionRestClient.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JIRC.Domain;
using JIRC.Domain.Input;

using ServiceStack.ServiceClient.Web;

namespace JIRC
{
    /// <summary>
    /// An interface for handling Versions in Projects in JIRA.
    /// </summary>
    public interface IVersionRestClient
    {
        /// <summary>
        /// Retrieves detailed information about a selected project.
        /// </summary>
        /// <param name="versionUri">The URI of the version to retrieve.</param>
        /// <returns>Detailed information about the version.</returns>
        /// <exception cref="WebServiceException">There is no version with the given URI, or if the calling user does not have permission to browse the project.</exception>
        JiraVersion GetVersion(Uri versionUri);

        /// <summary>
        /// Creates a new version in a project.
        /// </summary>
        /// <param name="versionInput">Details about the version to create.</param>
        /// <returns>Detailed information about the version that has just been created.</returns>
        /// <exception cref="WebServiceException">If the calling user does not have permission to create versions in the project.</exception>
        JiraVersion CreateVersion(VersionInput versionInput);

        /// <summary>
        /// Updates (Edits) a version with new details.
        /// </summary>
        /// <param name="versionUri">The URI of the version to edit.</param>
        /// <param name="versionInput">Details about the changes to make to the version.</param>
        /// <returns>Detailed information about the version that has just been updated.</returns>
        /// <exception cref="WebServiceException">If the calling user does not have permission to create versions in the project.</exception>
        JiraVersion UpdateVersion(Uri versionUri, VersionInput versionInput);

        /// <summary>
        /// Removes selected version.
        /// </summary>
        /// <param name="versionUri">The URI of the version to edit.</param>
        /// <exception cref="WebServiceException">If the calling user does not have permission to delete versions in the project.</exception>
        void RemoveVersion(Uri versionUri);

        /// <summary>
        /// Removes selected version, changing the Fix Version(s) and/or Affects Version(s).
        /// </summary>
        /// <param name="versionUri">The URI of the version to edit.</param>
        /// <param name="moveFixIssuesToVersionUri">URI of the version to which issues should have now set their Fix Version(s) field instead of the just removed version.</param>
        /// <param name="moveAffectedIssuesToVersionUri">The URI of the version to which issues should have now set their Affects Version(s) field instead of the just removed version.</param>
        /// <exception cref="WebServiceException">If the calling user does not have permission to delete versions in the project.</exception>
        void RemoveVersion(Uri versionUri, Uri moveFixIssuesToVersionUri, Uri moveAffectedIssuesToVersionUri);

        /// <summary>
        /// Retrieves basic statistics about issues which have their Fix Version(s) or Affects Version(s) pointing to the given version.
        /// </summary>
        /// <param name="versionUri">The URI of the version to retrieve statistics for.</param>
        /// <returns>Basic statistics about issues relating to the specified version.</returns>
        /// <exception cref="WebServiceException">If the calling user does not have permission to browse the project.</exception>
        VersionRelatedIssuesCount GetVersionRelatedIssuesCount(Uri versionUri);

        /// <summary>
        /// Retrieves the number of unresolved issue which have their Fix Version(s) field pointing to a given version.
        /// </summary>
        /// <param name="versionUri">The URI of the version to retrieve statistics for.</param>
        /// <returns>The number of unresolved issues of the requested version.</returns>
        /// <exception cref="WebServiceException">If the version does not exist, or the calling user does not have permission to browse the project.</exception>
        int GetNumUnresolvedIssues(Uri versionUri);

        /// <summary>
        /// Moves selected version after another version. Ordering of versions is important on various reports and whenever input version fields are rendered by JIRA.
        /// </summary>
        /// <param name="versionUri">The URI of the version to move.</param>
        /// <param name="afterVersionUri">The URI of the version to move selected version after.</param>
        /// <returns>Detailed information of the version that has just been moved.</returns>
        /// <exception cref="WebServiceException">If either version does not exist, or the calling user does not have permission to modify versions in the project.</exception>
        JiraVersion MoveVersionAfter(Uri versionUri, Uri afterVersionUri);

        /// <summary>
        /// Moves selected version to another position.
        /// If version already occupies given position (e.g. is the last version and we want to move to a later position or to the last position) then such call does not change anything.
        /// </summary>
        /// <param name="versionUri">The URI of the version to move.</param>
        /// <param name="versionPosition">Defines a new position of the selected version.</param>
        /// <returns>Detailed information of the version that has just been moved.</returns>
        /// <exception cref="WebServiceException">If the version does not exist, or the calling user does not have permission to modify versions in the project.</exception>
        JiraVersion MoveVersion(Uri versionUri, VersionPosition versionPosition);
    }
}
