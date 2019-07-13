// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomJsonSerializer.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using JIRC.Domain;
using JIRC.Extensions;

using ServiceStack.Common.Extensions;
using ServiceStack.Text;

namespace JIRC.Internal.Json
{
    internal class CustomJsonSerializer
    {
        internal static void RegisterAllClasses()
        {
            // TODO: Generate this at initialisation time from methods!
            JsConfig<BasicResolution>.RawDeserializeFn = a => BasicResolutionJsonParser(JsonObject.Parse(a));
            JsConfig<BasicUser>.RawDeserializeFn = a => BasicUserJsonParser(JsonObject.Parse(a));
            JsConfig<User>.RawDeserializeFn = a => UserJsonParser(JsonObject.Parse(a));
            JsConfig<SecurityLevel>.RawDeserializeFn = a => SecurityLevelJsonParser(JsonObject.Parse(a));
            JsConfig<IEnumerable<IssueLinksType>>.RawDeserializeFn = a => IssueLinkTypesJsonParser(JsonObject.Parse(a));
            JsConfig<BasicPriority>.RawDeserializeFn = a => BasicPriorityJsonParser(JsonObject.Parse(a));
            JsConfig<Component>.RawDeserializeFn = a => ComponentJsonParser(JsonObject.Parse(a));
            JsConfig<BasicProject>.RawDeserializeFn = a => BasicProjectJsonParser(JsonObject.Parse(a));
            JsConfig<Project>.RawDeserializeFn = a => ProjectJsonParser(JsonObject.Parse(a));
            JsConfig<CimProject>.RawDeserializeFn = a => CimProjectJsonParser(JsonObject.Parse(a));
            JsConfig<JiraVersion>.RawDeserializeFn = a => JiraVersionJsonParser(JsonObject.Parse(a));
            JsConfig<BasicVotes>.RawDeserializeFn = a => BasicVotesJsonParser(JsonObject.Parse(a));
            JsConfig<Watchers>.RawDeserializeFn = a => WatchersJsonParser(JsonObject.Parse(a));
            JsConfig<SearchResult>.RawDeserializeFn = a => SearchResultJsonParser(JsonObject.Parse(a));
            JsConfig<Session>.RawDeserializeFn = a => SessionJsonParser(JsonObject.Parse(a));
            JsConfig<LoginInfo>.RawDeserializeFn = a => LoginInfoJsonParser(JsonObject.Parse(a));
            JsConfig<Field>.RawDeserializeFn = a => FieldJsonParser(JsonObject.Parse(a));
            JsConfig<BasicIssueType>.RawDeserializeFn = a => BasicIssueTypeJsonParser(JsonObject.Parse(a));
            JsConfig<IssueType>.RawDeserializeFn = a => IssueTypeJsonParser(JsonObject.Parse(a));
            JsConfig<CimIssueType>.RawDeserializeFn = a => CimIssueTypeJsonParser(JsonObject.Parse(a));
            JsConfig<ProjectRole>.RawDeserializeFn = a => ProjectRoleJsonParser(JsonObject.Parse(a));
            JsConfig<VersionRelatedIssuesCount>.RawDeserializeFn = a => VersionRelatedIssuesCountJsonParser(JsonObject.Parse(a));
            JsConfig<Issue>.RawDeserializeFn = a => IssueJsonParser(JsonObject.Parse(a));
            JsConfig<Attachment>.RawDeserializeFn = a => AttachmentJsonParser(JsonObject.Parse(a));
            JsConfig<IssueLinksType>.RawDeserializeFn = a => IssueLinksTypesJsonParser(JsonObject.Parse(a));
        }

        internal static BasicResolution BasicResolutionJsonParser(JsonObject json)
        {
            return new BasicResolution(json.Get<Uri>("self"), json.Get("name"));
        }

        internal static SecurityLevel SecurityLevelJsonParser(JsonObject json)
        {
            return new SecurityLevel(json.Get<Uri>("self"), json.Get<long>("id"), json.Get("name"), json.Get("description"));
        }

        internal static JiraVersion JiraVersionJsonParser(JsonObject json)
        {
            return new JiraVersion(
                json.Get<Uri>("self"),
                json.Get<long>("id"),
                json.Get("name"),
                json.Get("description"),
                json.Get<bool>("archived"),
                json.Get<bool>("released"),
                json.Get<DateTimeOffset>("releaseDate"));
        }

        internal static BasicUser BasicUserJsonParser(JsonObject json)
        {
            return new BasicUser(json.Get<Uri>("self"), json.Get("name"), json.Get("displayName"));
        }

        internal static User UserJsonParser(JsonObject json)
        {
            var avatars = json.Get<Dictionary<string, Uri>>("avatarUrls");
            var basic = BasicUserJsonParser(json);
            var active = !json.ContainsKey("active") || json.Get<bool>("active");
            var timezone = json.Get("timeZone");
            return new User(basic, json.Get("emailAddress"), null, avatars, active, timezone);
        }

        internal static Field FieldJsonParser(JsonObject json)
        {
            var isCustom = json.Get<bool>("custom");
            return new Field(
                json.Get("id"),
                json.Get("name"),
                isCustom ? Field.Type.Custom : Field.Type.Jira,
                json.Get<bool>("orderable"),
                json.Get<bool>("navigable"),
                json.Get<bool>("searchable"),
                json.Get<FieldSchema>("schema"));
        }

        internal static BasicIssueType BasicIssueTypeJsonParser(JsonObject json)
        {
            return new BasicIssueType(json.Get<Uri>("self"), json.Get<long>("id"), json.Get("name"), json.Get<bool>("subtask"));
        }

        internal static IssueType IssueTypeJsonParser(JsonObject json)
        {
            var basic = BasicIssueTypeJsonParser(json);
            var iconUrl = json.Get<Uri>("iconUrl");
            var description = json.Get("description");
            return new IssueType(basic.Self, basic.Id, basic.Name, basic.Subtask, description, iconUrl);
        }

        internal static IEnumerable<IssueLinksType> IssueLinkTypesJsonParser(JsonObject json)
        {
            var allTypes = json.Get<JsonArrayObjects>("issueLinkTypes");
            return allTypes.ConvertAll(IssueLinksTypesJsonParser);
        }

        internal static IssueLinksType IssueLinksTypesJsonParser(JsonObject json)
        {
            return new IssueLinksType(json.Get<Uri>("self"), json.Get("id"), json.Get("name"), json.Get("inward"), json.Get("outward"));
        }

        internal static BasicPriority BasicPriorityJsonParser(JsonObject json)
        {
            return new BasicPriority(json.Get<Uri>("self"), json.Get<long>("id"), json.Get("name"));
        }

        internal static CimIssueType CimIssueTypeJsonParser(JsonObject json)
        {
            var issueType = IssueTypeJsonParser(json);
            var fields = json.Get<Dictionary<string, JsonObject>>("fields");
            return new CimIssueType(issueType.Self, issueType.Id, issueType.Name, issueType.Subtask, issueType.Description, issueType.IconUrl, CimFieldsInfoMapJsonParser.Parse(fields));
        }

        internal static CimProject CimProjectJsonParser(JsonObject json)
        {
            var basicProject = BasicProjectJsonParser(json);
            var issueTypes = json.Get<IEnumerable<CimIssueType>>("issuetypes");
            var avatarUris = json.Get<Dictionary<string, Uri>>("avatarUrls");
            return new CimProject(basicProject.Self, basicProject.Key, basicProject.Name, avatarUris, issueTypes);
        }

        internal static BasicProject BasicProjectJsonParser(JsonObject json)
        {
            return new BasicProject(json.Get<Uri>("self"), json.Get("key"), json.Get("name"));
        }

        internal static BasicVotes BasicVotesJsonParser(JsonObject json)
        {
            return new BasicVotes(json.Get<Uri>("self"), json.Get<int>("votes"), json.Get<bool>("hasVoted"));
        }

        internal static Project ProjectJsonParser(JsonObject json)
        {
            return new Project(
                json.Get<Uri>("self"),
                json.Get("key"),
                json.Get("name"),
                json.Get("description"),
                json.Get<BasicUser>("lead"),
                json.Get<Uri>("url"),
                json.Get<IEnumerable<JiraVersion>>("versions"),
                json.Get<IEnumerable<Component>>("components"),
                null,
                json.Get<Dictionary<string, Uri>>("roles").ConvertAll(x => new BasicProjectRole(x.Value, x.Key)));
        }

        internal static ProjectRole ProjectRoleJsonParser(JsonObject json)
        {
            return new ProjectRole(json.Get<long>("id"), json.Get<Uri>("self"), json.Get("name"), json.Get("description"), json.Get<IEnumerable<RoleActor>>("actors"));
        }

        internal static Watchers WatchersJsonParser(JsonObject json)
        {
            return new Watchers(json.Get<Uri>("self"), json.Get<bool>("isWatching"), json.Get<int>("watchCount"), json.Get<IEnumerable<BasicUser>>("watchers"));
        }

        internal static SearchResult SearchResultJsonParser(JsonObject json)
        {
            return new SearchResult(
                json.Get<int>("startAt"),
                json.Get<int>("maxResults"),
                json.Get<int>("total"),
                json.Get<IEnumerable<Issue>>("issues"));
        }

        internal static Session SessionJsonParser(JsonObject json)
        {
            return new Session(json.Get<Uri>("self"), json.Get("name"), json.Get<LoginInfo>("loginInfo"));
        }

        internal static LoginInfo LoginInfoJsonParser(JsonObject json)
        {
            return new LoginInfo(
                json.Get<int>("failedLoginCount"),
                json.Get<int>("loginCount"),
                json.Get<DateTimeOffset>("lastFailedLoginTime"),
                json.Get<DateTimeOffset>("previousLoginTime"));
        }

        internal static Component ComponentJsonParser(JsonObject json)
        {
            var assigneeTypeStr = json.Get("assigneeType");
            AssigneeInfo assignee = null;
            if (assigneeTypeStr != null)
            {
                assignee = new AssigneeInfo
                {
                    AssigneeType = ParseAssigneeType(json.Get("assigneeType")),
                    Assignee = json.Get<BasicUser>("assignee"),
                    RealAssigneeType = ParseAssigneeType(json.Get("realAssigneeType")),
                    RealAssignee = json.Get<BasicUser>("realAssignee"),
                    AssigneeTypeValid = json.Get<bool>("isAssigneeTypeValid")
                };
            }

            return new Component(json.Get<Uri>("self"), json.Get<long>("id"), json.Get("name"), json.Get("description"), json.Get<BasicUser>("lead"), assignee);
        }

        internal static VersionRelatedIssuesCount VersionRelatedIssuesCountJsonParser(JsonObject json)
        {
            return new VersionRelatedIssuesCount(json.Get<Uri>("self"), json.Get<int>("issuesFixedCount"), json.Get<int>("issuesAffectedCount"));
        }

        internal static Issue IssueJsonParser(JsonObject json)
        {
            var issue = new Issue(json.Get<int>("id"), json.Get<string>("key"), json.Get<Uri>("self"));
            var fields = json.Get<JsonObject>("fields");
            if (fields != null)
            {
                issue.Summary = fields.Get<string>("summary");
                issue.Reporter = fields.Get<User>("reporter");
                issue.Assignee = fields.Get<User>("assignee");
                issue.Description = fields.Get<string>("description");
                issue.Project = fields.Get<BasicProject>("project");

                if (fields.ContainsKey("transitions"))
                {
                    issue.TransitionsUri = fields.Get<Uri>("transitions");
                }
                else if (issue.Self != null)
                {
                    issue.TransitionsUri = issue.Self.Append("transitions");
                }

                issue.FixVersions = fields.Get<IEnumerable<JiraVersion>>("fixVersions");
                issue.AffectedVersions = fields.Get<IEnumerable<JiraVersion>>("versions");
                issue.Watchers = fields.Get<BasicWatchers>("watches");
                issue.Votes = fields.Get<BasicVotes>("votes");
                issue.Attachments = fields.Get<IEnumerable<Attachment>>("attachment");
                issue.Labels = fields.Get<IEnumerable<string>>("labels");
                issue.Status = fields.Get<JsonObject>("status").Get<string>("name");

                if (fields.ContainsKey("parent"))
                {
                    issue.Parent = fields.Get<string>("parent");
                }

                if (fields.ContainsKey("subtasks"))
                {
                    issue.SubTasks = fields.Get<IEnumerable<Issue>>("subtasks");
                }

                if (fields.ContainsKey("issuetype"))
                {
                    issue.IssueType = fields.Get<IssueType>("issuetype");
                }

                if (fields["resolution"] != null)
                {
                    issue.Resolution = fields.Get<string>("name");
                }

                issue.CustomFields = new CustomFieldTypes(fields);
            }

            return issue;
        }

        internal static Attachment AttachmentJsonParser(JsonObject json)
        {
            return new Attachment(
                json.Get<Uri>("self"),
                json.Get("filename"),
                json.Get<BasicUser>("author"),
                json.Get<DateTimeOffset>("created"),
                json.Get<int>("size"),
                json.Get("mimeType"),
                json.Get<Uri>("content"),
                json.Get<Uri>("thumbnail"));
        }

        private static AssigneeType ParseAssigneeType(string str)
        {
            if (str == "COMPONENT_LEAD")
            {
                return AssigneeType.ComponentLead;
            }

            if (str == "PROJECT_DEFAULT")
            {
                return AssigneeType.ProjectDefault;
            }

            if (str == "PROJECT_LEAD")
            {
                return AssigneeType.ProjectLead;
            }

            if (str == "UNASSIGNED")
            {
                return AssigneeType.Unassigned;
            }

            throw new ArgumentException("Unexpected value of assignee type [{0}]".Fmt(str), "str");
        }
    }
}
