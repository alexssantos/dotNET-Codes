// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueFieldId.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace JIRC.Domain
{
    public enum IssueFieldId
    {
        AffectsVersion,
        Assignee,
        Attachment,
        Comment,
        Components,
        Created,
        Description,
        DueDate,
        FixVersions,
        IssueType,
        Labels,
        Links,
        LinksPre5,
        Priority,
        Project,
        Reporter,
        Resolution,
        Status,
        Subtasks,
        Summary,
        TimeTracking,
        Transitions,
        Updated,
        Votes,
        Watcher,
        WatcherPre5,
        Worklog,
        Worklogs,
    }

    public static class IssueFieldIdHelper
    {
        private static readonly Dictionary<IssueFieldId, string> FieldNames = new Dictionary<IssueFieldId, string>
        {
            { IssueFieldId.AffectsVersion, "versions" },
            { IssueFieldId.Assignee, "assignee" },
            { IssueFieldId.Attachment, "attachment" },
            { IssueFieldId.Comment, "comment" },
            { IssueFieldId.Components, "components" },
            { IssueFieldId.Created, "created" },
            { IssueFieldId.Description, "description" },
            { IssueFieldId.DueDate, "duedate" },
            { IssueFieldId.FixVersions, "fixVersions" },
            { IssueFieldId.IssueType, "issuetype" },
            { IssueFieldId.Labels, "labels" },
            { IssueFieldId.Links, "issuelinks" },
            { IssueFieldId.LinksPre5, "links" },
            { IssueFieldId.Priority, "priority" },
            { IssueFieldId.Project, "project" },
            { IssueFieldId.Reporter, "reporter" },
            { IssueFieldId.Resolution, "resolution" },
            { IssueFieldId.Status, "status" },
            { IssueFieldId.Subtasks, "subtasks" },
            { IssueFieldId.Summary, "summary" },
            { IssueFieldId.TimeTracking, "timetracking" },
            { IssueFieldId.Transitions, "transitions" },
            { IssueFieldId.Updated, "updated" },
            { IssueFieldId.Votes, "votes" },
            { IssueFieldId.Watcher, "watches" },
            { IssueFieldId.WatcherPre5, "watcher" },
            { IssueFieldId.Worklog, "worklog" },
            { IssueFieldId.Worklogs, "worklogs" },
        };

        public static string ToRestString(this IssueFieldId fieldId)
        {
            return FieldNames[fieldId];
        }
    }
}
