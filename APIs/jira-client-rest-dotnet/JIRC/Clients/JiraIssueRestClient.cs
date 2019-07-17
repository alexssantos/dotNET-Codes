// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraIssueRestClient.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JIRC.Domain;
using JIRC.Domain.Input;
using JIRC.Extensions;
using JIRC.Internal.Json;
using JIRC.Internal.Json.Gen;
using ServiceStack.Common.Web;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JIRC.Clients
{
	/// <summary>
	/// The REST client for issues in JIRA.
	/// </summary>
	internal class JiraIssueRestClient : IIssueRestClient
	{
		private const string IssuesUriPrefix = "issue";

		private const string CommentUriPostfix = "comment";

		private const string EditMetaUriPostfix = "editmeta";

		private const string AssigneeUriPostfix = "assignee";

		private readonly JsonServiceClient client;

		private readonly IMetadataRestClient metadataClient;

		private readonly ISessionRestClient sessionClient;

		private ServerInfo serverInfo;

		/// <summary>
		/// Initializes a new instance of the issue REST client.
		/// </summary>
		/// <param name="client">The JSON client that has been set up for a specific JIRA instance.</param>
		/// <param name="metadataClient">The metadata REST client for the same JIRA instance.</param>
		/// <param name="sessionClient">The session REST client for the same JIRA instance.</param>
		public JiraIssueRestClient(JsonServiceClient client, IMetadataRestClient metadataClient, ISessionRestClient sessionClient)
		{
			this.client = client;
			this.metadataClient = metadataClient;
			this.sessionClient = sessionClient;
		}

		/// <summary>
		/// Creates an issue or a subtask.
		/// </summary>
		/// <param name="issue">Information about the issue to create. The fields that can be set on issue creation can be determined using the <see cref="IIssueRestClient.GetCreateIssueMetadata()"/> or <see cref="IIssueRestClient.GetCreateIssueMetadata(JIRC.Domain.Input.GetCreateIssueMetadataOptions)"/> methods.
		/// The <see cref="IssueInputBuilder"/> class can be used to help generate the appropriate fields.</param>
		/// <seealso cref="IssueInputBuilder"/>
		/// <returns>Details of the created issue.</returns>
		/// <exception cref="WebServiceException">The input is invalid (e.g. missing required fields, invalid field values, and so forth), or if the calling user does not have permission to create the issue.</exception>
		public BasicIssue CreateIssue(IssueInput issue)
		{
			var json = IssueInputJsonGenerator.Generate(issue);
			return client.Post<BasicIssue>("issue", json);
		}

		/// <summary>
		/// Returns the meta data for creating issues.
		/// This includes the available projects, issue types and fields, including field types and whether or not those fields are required. Projects will not be returned if the user does not have permission to create issues in that project.</summary>
		/// <returns>A collection of fields for each project that corresponds with fields in the create screen for each project/issue type.</returns>
		/// <exception cref="WebServiceException">The caller is not logged in, or does not have permission to view any projects.</exception>
		public IEnumerable<CimProject> GetCreateIssueMetadata()
		{
			return GetCreateIssueMetadata(null);
		}

		/// <summary>
		/// Returns the meta data for creating issues.
		/// This includes the available projects, issue types and fields, including field types and whether or not those fields are required. Projects will not be returned if the user does not have permission to create issues in that project.</summary>
		/// <param name="options">A set of options that allow the project/field/issue types to be constrained. The <see cref="GetCreateIssueMetadataOptionsBuilder"/> class can be used to help generate the appropriate request.</param>
		/// <seealso cref="GetCreateIssueMetadataOptionsBuilder"/>
		/// <returns>A collection of fields for each project that corresponds with fields in the create screen for each project/issue type.</returns>
		/// <exception cref="WebServiceException">The caller is not logged in, or does not have permission to view any of the requested projects.</exception>
		public IEnumerable<CimProject> GetCreateIssueMetadata(GetCreateIssueMetadataOptions options)
		{
			var qb = new UriBuilder(client.BaseUri);
			qb.Path = qb.Path.AppendPath("issue", "createmeta");

			if (options != null)
			{
				if (options.ProjectIds != null && options.ProjectIds.Any())
				{
					qb.AppendQuery("projectIds", options.ProjectIds.Join(","));
				}

				if (options.ProjectKeys != null && options.ProjectKeys.Any())
				{
					qb.AppendQuery("projectKeys", options.ProjectKeys.Join(","));
				}

				if (options.IssueTypeIds != null && options.IssueTypeIds.Any())
				{
					qb.AppendQuery("issuetypeIds", options.IssueTypeIds.Join(","));
				}

				if (options.IssueTypeName != null)
				{
					foreach (var i in options.IssueTypeName)
					{
						qb.AppendQuery("issuetypeNames", i);
					}
				}

				if (options.Expandos != null && options.Expandos.Any())
				{
					qb.AppendQuery("expand", options.Expandos.Join(","));
				}
			}

			var json = client.Get<JsonObject>(qb.Uri.ToString());
			return json.Get<IEnumerable<CimProject>>("projects");
		}

		/// <summary>
		/// Creates many issue or subtasks in one bulk operation.
		/// </summary>
		/// <param name="issues">The collection of issues or subtasks to create.</param>
		/// <returns>Information about the created issue (including URIs), or error information.</returns>
		/// <exception cref="WebServiceException">The caller is not logged in, or does not have permission to create issues in the specified projects.</exception>
		public BulkOperationResult<BasicIssue> CreateIssues(IList<IssueInput> issues)
		{
			var json = IssuesInputJsonGenerator.Generate(issues);
			return client.Post<BulkOperationResult<BasicIssue>>("issue/bulk", json);
		}

		/// <summary>
		/// Gets the issue specified by the unique key (e.g. "AA-123").
		/// </summary>
		/// <param name="key">The unique key for the issue.</param>
		/// <returns>Information about the issue.</returns>
		/// <exception cref="WebServiceException">The requested issue is not found, or the user does not have permission to view it.</exception>
		public Issue GetIssue(string key)
		{
			return client.Get<Issue>("/{0}/{1}".Fmt(IssuesUriPrefix, key));
		}

		/// <summary>
		/// Delete an issue.
		/// If the issue has subtasks you must set the parameter <see cref="deleteSubtasks"/> to delete the issue. You cannot delete an issue without its subtasks also being deleted.
		/// </summary>
		/// <param name="issueKey">The unique key of the issue to delete.</param>
		/// <param name="deleteSubtasks">Must be set to true if the issue has subtasks.</param>
		/// <exception cref="WebServiceException">The requested issue is not found, or the user does not have permission to delete it.</exception>
		public void DeleteIssue(string issueKey, bool deleteSubtasks)
		{
			var qb = new UriBuilder(client.BaseUri);
			qb.Path = qb.Path.AppendPath("issue", issueKey);
			qb.AppendQuery("deleteSubtasks", deleteSubtasks.ToString().ToLower());
			client.Delete<JsonObject>(qb.Uri.ToString());
		}

		/// <summary>
		/// Gets details about the users who are watching the specified issue.
		/// </summary>
		/// <param name="watchersUri">URI of the watchers resource for the selected issue. Usually obtained by getting the <see cref="BasicWatchers.Self"/> property on the <see cref="Issue"/>.</param>
		/// <returns>The list of watchers for the issue with the given URI.</returns>
		/// <exception cref="WebServiceException">The requested watcher URI is not found, or the user does not have permission to view it.</exception>
		public Watchers GetWatchers(Uri watchersUri)
		{
			return client.Get<Watchers>(watchersUri.ToString());
		}

		/// <summary>
		/// Gets details about the users who have voted for the specified issue.
		/// </summary>
		/// <param name="votesUri">URI of the voters resource for the selected issue. Usually obtained by getting the <see cref="BasicVotes.Self"/> property on the <see cref="Issue"/>.</param>
		/// <returns>The list of voters for the issue with the given URI.</returns>
		/// <exception cref="WebServiceException">The requested voter URI is not found, or the user does not have permission to view it.</exception>
		public Votes GetVotes(Uri votesUri)
		{
			return client.Get<Votes>(votesUri.ToString());
		}

		/// <summary>
		/// Get a list of the transitions possible for this issue by the current user, along with fields that are required and their types.
		/// </summary>
		/// <param name="transitionsUri">URI of transitions resource of selected issue. Usually obtained by getting the <see cref="Issue.TransitionsUri"/> property.</param>
		/// <returns>Transition information about the transitions available for the selected issue in its current state.</returns>
		/// <exception cref="WebServiceException">The requested transition URI is not found, or the user does not have permission to view it.</exception>
		public IEnumerable<Transition> GetTransitions(Uri transitionsUri)
		{
			var qb = new UriBuilder(transitionsUri);
			qb.AppendQuery("expand", "transitions.fields");

			var json = client.Get<JsonObject>(qb.Uri.ToString());
			return TransitionJsonParser.Parse(json);
		}

		/// <summary>
		/// Get a list of the transitions possible for this issue by the current user, along with fields that are required and their types.
		/// </summary>
		/// <param name="issue">The issue on which to obtain the available transitions for.</param>
		/// <returns>Transition information about the transitions available for the selected issue in its current state.</returns>
		/// <exception cref="WebServiceException">The requested issue is not found, or the user does not have permission to view it.</exception>
		public IEnumerable<Transition> GetTransitions(Issue issue)
		{
			if (issue.TransitionsUri != null)
			{
				return GetTransitions(issue.TransitionsUri);
			}

			var uri = issue.Self.Append("transitions");
			return GetTransitions(uri);
		}

		/// <summary>
		/// Perform a transition on an issue. When performing the transition you can update or set other issue fields.
		/// </summary>
		/// <param name="transitionsUri">URI of transitions resource of selected issue. Usually obtained by getting the <see cref="Issue.TransitionsUri"/> property.</param>
		/// <param name="transitionInput">Information about the transition to perform.</param>
		/// <exception cref="WebServiceException">There is no transition specified, or the requested issue is not found, or the user does not have permission to view it.</exception>
		public void Transition(Uri transitionsUri, TransitionInput transitionInput)
		{
			var json = TransitionInputJsonGenerator.Generate(transitionInput, GetServerInfo());
			client.Post<JsonObject>(transitionsUri.ToString(), json);
		}

		/// <summary>
		/// Perform a transition on an issue. When performing the transition you can update or set other issue fields.
		/// </summary>
		/// <param name="issue">The issue on which to obtain the available transitions for.</param>
		/// <param name="transitionInput">Information about the transition to perform.</param>
		/// <exception cref="WebServiceException">There is no transition specified, or the requested issue is not found, or the user does not have permission to view it.</exception>
		public void Transition(Issue issue, TransitionInput transitionInput)
		{
			Transition(issue.TransitionsUri ?? issue.Self.Append("transitions"), transitionInput);
		}

		/// <summary>
		/// Retrieves a list of users who may be used as assignee when editing an issue. For a list of users when creating an issue, see <see cref="IProjectRestClient.GetAssignableUsers(string)"/>.
		/// </summary>
		/// <param name="issueKey">The unique key for the issue (e.g. "AA-123").</param>
		/// <returns>Returns a list of users who may be assigned to an issue during an edit.</returns>
		/// <exception cref="WebServiceException">The project is not found, or the calling user does not have permission to view it.</exception>
		public IEnumerable<User> GetAssignableUsers(string issueKey)
		{
			return GetAssignableUsers(issueKey, null, null);
		}

		/// <summary>
		/// Retrieves a list of users who may be used as assignee when editing an issue. For a list of users when creating an issue, see <see cref="IProjectRestClient.GetAssignableUsers(string, int?, int?)"/>.
		/// </summary>
		/// <param name="issueKey">The unique key of issue (e.g. AA-123).</param>
		/// <param name="startAt">The index of the first user to return (0-based).</param>
		/// <param name="maxResults">The maximum number of users to return (defaults to 50). The maximum allowed value is 1000. If you specify a value that is higher than this number, your search results will be truncated.</param>
		/// <returns>Returns a list of users who may be assigned to an issue during creation.</returns>
		public IEnumerable<User> GetAssignableUsers(string issueKey, int? startAt, int? maxResults)
		{
			var qb = new UriBuilder(client.BaseUri.AppendPath(JiraUserRestClient.UserAssignableSearchUriPrefix));
			qb.AppendQuery("issueKey", issueKey);

			if (maxResults != null)
			{
				qb.AppendQuery("maxResults", maxResults.ToString());
			}

			if (startAt != null)
			{
				qb.AppendQuery("startAt", startAt.ToString());
			}

			return client.Get<IEnumerable<User>>(qb.Uri.ToString());
		}

		/// <summary>
		/// Assigns an issue to a user.
		/// </summary>
		/// <param name="issue">The issue to make the assignment for.</param>
		/// <param name="user">The username of the person to assign the issue to.</param>
		public void AssignTo(Issue issue, string user)
		{
			if (issue == null)
			{
				throw new ArgumentNullException("issue");
			}

			var request = new JsonObject { { "name", user } };
			client.Put<JsonObject>(issue.Self.Append(AssigneeUriPostfix).ToString(), request);
		}

		/// <summary>
		/// Assigns an issue to the automatic assignee or removes the assignee entirely.
		/// </summary>
		/// <param name="issue">The issue to make the assignment for.</param>
		/// <param name="assignee">The special assignee type.</param>
		public void AssignTo(Issue issue, SpecialAssignee assignee)
		{
			var user = assignee == SpecialAssignee.Automatic ? "-1" : null;
			AssignTo(issue, user);
		}

		/// <summary>
		/// Casts your vote on the selected issue. Casting a vote on already votes issue by the caller, causes the exception.
		/// </summary>
		/// <param name="votesUri">URI of the voters resource for the selected issue. Usually obtained by getting the <see cref="BasicVotes.Self"/> property on the <see cref="Issue"/>.</param>
		/// <exception cref="WebServiceException">If the user cannot vote for any reason. (The user is the reporter, the user does not have permission to vote, voting is disabled in the instance, the issue does not exist, etc.).</exception>
		public void Vote(Uri votesUri)
		{
			client.Post<JsonObject>(votesUri.ToString(), null);
		}

		/// <summary>
		///  Removes your vote from the selected issue. Removing a vote from the issue without your vote causes the exception.
		/// </summary>
		/// <param name="votesUri">URI of the voters resource for the selected issue. Usually obtained by getting the <see cref="BasicVotes.Self"/> property on the <see cref="Issue"/>.</param>
		/// <exception cref="WebServiceException">If the user cannot remove a vote for any reason. (The user did not vote on the issue, the user is the reporter, voting is disabled, the issue does not exist, etc.).</exception>
		public void Unvote(Uri votesUri)
		{
			client.Delete<JsonObject>(votesUri.ToString());
		}

		/// <summary>
		/// Starts watching selected issue.
		/// </summary>
		/// <param name="watchersUri">URI of the watchers resource for the selected issue. Usually obtained by getting the <see cref="BasicWatchers.Self"/> property on the <see cref="Issue"/>.</param>
		/// <exception cref="WebServiceException">If the issue does not exist, or the the calling user does not have permission to watch the issue.</exception>
		public void Watch(Uri watchersUri)
		{
			client.Post<JsonObject>(watchersUri.ToString(), null);
		}

		/// <summary>
		/// Stops watching selected issue.
		/// </summary>
		/// <param name="watchersUri">URI of the watchers resource for the selected issue. Usually obtained by getting the <see cref="BasicWatchers.Self"/> property on the <see cref="Issue"/>.</param>
		/// <exception cref="WebServiceException">If the issue does not exist, or the the calling user does not have permission to watch the issue.</exception>
		public void Unwatch(Uri watchersUri)
		{
			RemoveWatcher(watchersUri, GetLoggedUsername());
		}

		/// <summary>
		/// Adds selected person as a watcher for selected issue.
		/// </summary>
		/// <param name="watchersUri">URI of the watchers resource for the selected issue. Usually obtained by getting the <see cref="BasicWatchers.Self"/> property on the <see cref="Issue"/>.</param>
		/// <param name="username">The user to add as a watcher.</param>
		/// <exception cref="WebServiceException">If the issue does not exist, or the the calling user does not have permission to modifier watchers of the issue.</exception>
		public void AddWatcher(Uri watchersUri, string username)
		{
			var json = new JsonValue(username);
			client.Post<JsonObject>(watchersUri.ToString(), json);
		}

		/// <summary>
		/// Removes selected person as a watcher for selected issue.
		/// </summary>
		/// <param name="watchersUri">URI of the watchers resource for the selected issue. Usually obtained by getting the <see cref="BasicWatchers.Self"/> property on the <see cref="Issue"/>.</param>
		/// <param name="username">The user to remove as a watcher.</param>
		/// <exception cref="WebServiceException">If the issue does not exist, or the the calling user does not have permission to modifier watchers of the issue.</exception>
		public void RemoveWatcher(Uri watchersUri, string username)
		{
			var qb = new UriBuilder(watchersUri);
			if (GetServerInfo().BuildNumber >= ServerVersionConstants.BuildNumberJira44)
			{
				qb.AppendQuery("username", username);
			}
			else
			{
				qb.Path.AppendPath(username);
			}

			client.Delete<JsonObject>(qb.Uri.ToString());
		}

		/// <summary>
		/// Creates link between two issues and adds a comment (optional) to the source issues.
		/// </summary>
		/// <param name="linkIssuesInput">Details for the link and the comment (optional) to be created.</param>
		/// <exception cref="WebServiceException">If there was a problem linking the issues, or the the calling user does not have permission to link issues.</exception>
		public void LinkIssue(LinkIssuesInput linkIssuesInput)
		{
			client.Post<JsonObject>("issueLink", LinkIssuesInputJsonGenerator.Generate(linkIssuesInput, GetServerInfo()));
		}

		/// <summary>
		/// Adds an attachment to an issue.
		/// </summary>
		/// <param name="attachmentsUri">The URI of the attachment resource for a given issue.</param>
		/// <param name="filename">The name of the file to attach.</param>
		public void AddAttachment(Uri attachmentsUri, string filename)
		{
			var fileInfo = new FileInfo(filename);

			client.Headers.Add("X-Atlassian-Token", "nocheck");
			client.PostFile<JsonObject>(attachmentsUri.ToString(), fileInfo, MimeTypes.GetMimeType(fileInfo.Name));
			client.Headers.Remove("X-Atlassian-Token");
		}

		/// <summary>
		/// Adds a comment to the specified issue.
		/// </summary>
		/// <param name="issue">The  issue to add the comment to.</param>
		/// <param name="comment">The comment to add to the issue.</param>
		/// <exception cref="WebServiceException">If the issue does not exist, or the the calling user does not have permission to comment on the issue.</exception>
		public void AddComment(BasicIssue issue, Comment comment)
		{
			var uri = issue.Self.Append(CommentUriPostfix);
			var request = CommentJsonGenerator.Generate(comment, GetServerInfo());
			client.Post<Comment>(uri.ToString(), request);
		}

		/// <summary>
		/// Adds a comment to the specified issue.
		/// </summary>
		/// <param name="commentsUri">The URI for the issue to add the comment to.</param>
		/// <param name="comment">The comment to add to the issue.</param>
		/// <exception cref="WebServiceException">If the issue does not exist, or the the calling user does not have permission to comment on the issue.</exception>
		public void AddComment(Uri commentsUri, Comment comment)
		{
			var request = CommentJsonGenerator.Generate(comment, GetServerInfo());
			client.Post<Comment>(commentsUri.ToString(), request);
		}

		/// <summary>
		/// Adds a new work log entry to issue.
		/// </summary>
		/// <param name="worklogUri">The URI for the work log resource for the selected issue.</param>
		/// <param name="worklogInput">The work log information to add to the issue.</param>
		/// <exception cref="WebServiceException">If the issue does not exist, or the the calling user does not have permission to add work log information to the issue.</exception>
		public void AddWorklog(Uri worklogUri, WorklogInput worklogInput)
		{
			var qb = new UriBuilder(worklogUri);
			qb.AppendQuery("adjustEstimate", worklogInput.AdjustEstimate.ToString().ToLower());

			switch (worklogInput.AdjustEstimate)
			{
				case WorklogInput.AdjustmentEstimate.New:
					qb.AppendQuery("newEstimate", worklogInput.AdjustEstimateValue);
					break;

				case WorklogInput.AdjustmentEstimate.Manual:
					qb.AppendQuery("reduceBy", worklogInput.AdjustEstimateValue);
					break;
			}

			client.Post<JsonObject>(qb.Uri.ToString(), WorklogInputJsonGenerator.Generate(worklogInput));
		}

		/// <summary>
		/// Adds a label to an issue.
		/// </summary>
		/// <param name="issue">The issue to add the label to.</param>
		/// <param name="label">The label to add.</param>
		public void AddLabel(Issue issue, string label)
		{
			if (issue == null)
			{
				throw new ArgumentNullException("issue");
			}

			if (string.IsNullOrWhiteSpace(label))
			{
				throw new ArgumentException("Label cannot be empty", "label");
			}

			var update = new UpdateFieldInput(IssueFieldId.Labels);
			update.AddOperation(StandardOperation.Add, label);

			var json = IssueEditMetaJsonGenerator.Generate(new List<UpdateFieldInput> { update });
			client.Put<JsonObject>(issue.Self.ToString(), json);
		}

		/// <summary>
		/// Removes a label from an issue.
		/// </summary>
		/// <param name="issue">The issue to remove the label from.</param>
		/// <param name="label">The label to remove.</param>
		public void RemoveLabel(Issue issue, string label)
		{
			if (issue == null)
			{
				throw new ArgumentNullException("issue");
			}

			if (string.IsNullOrWhiteSpace(label))
			{
				throw new ArgumentException("Label cannot be empty", "label");
			}

			var update = new UpdateFieldInput(IssueFieldId.Labels);
			update.AddOperation(StandardOperation.Remove, label);

			var json = IssueEditMetaJsonGenerator.Generate(new List<UpdateFieldInput> { update });
			client.Put<JsonObject>(issue.Self.ToString(), json);
		}

		/// <summary>
		/// Edits a standard field on an issue.
		/// </summary>
		/// <param name="issue">The issue to edit the field on.</param>
		/// <param name="id">The id of the standard Issue field to edit.</param>
		/// <param name="value">The new value of the field.</param>
		public void EditField(Issue issue, IssueFieldId id, object value)
		{
			if (issue == null)
			{
				throw new ArgumentNullException("issue");
			}

			var update = new UpdateFieldInput(id);
			update.AddOperation(StandardOperation.Set, value);

			var json = IssueEditMetaJsonGenerator.Generate(new List<UpdateFieldInput> { update });
			client.Put<JsonObject>(issue.Self.ToString(), json);
		}

		/// <summary>
		/// Edits a custom field on an issue.
		/// </summary>
		/// <param name="issue">The issue to edit the field on.</param>
		/// <param name="name">The name of the custom Issue field to edit.</param>
		/// <param name="value">The new value of the field.</param>
		public void EditField(Issue issue, string name, object value)
		{
			if (issue == null)
			{
				throw new ArgumentNullException("issue");
			}

			var update = new UpdateFieldInput(name);
			update.AddOperation(StandardOperation.Set, value);

			var json = IssueEditMetaJsonGenerator.Generate(new List<UpdateFieldInput> { update });
			client.Put<JsonObject>(issue.Self.ToString(), json);
		}

		private ServerInfo GetServerInfo()
		{
			return serverInfo ?? (serverInfo = metadataClient.GetServerInfo());
		}

		private string GetLoggedUsername()
		{
			return sessionClient.GetCurrentSession().UserName;
		}
	}
}
