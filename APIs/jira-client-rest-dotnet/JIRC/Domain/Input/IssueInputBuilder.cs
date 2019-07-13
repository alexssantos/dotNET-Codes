// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueInputBuilder.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using JIRC.Extensions;

using ServiceStack.Common.Extensions;

namespace JIRC.Domain.Input
{
    public class IssueInputBuilder
    {
        private readonly Dictionary<string, FieldInput> fields = new Dictionary<string, FieldInput>();

        public IssueInputBuilder(string projectKey, long issueTypeId)
        {
            SetProjectKey(projectKey);
            SetIssueTypeId(issueTypeId);
        }

        public IssueInputBuilder(BasicProject project, BasicIssueType issueType)
        {
            SetProject(project);
            SetIssueType(issueType);
        }

        public IssueInputBuilder(string projectKey, long issueTypeId, string summary)
            : this(projectKey, issueTypeId)
        {
            SetSummary(summary);
        }

        public IssueInputBuilder(BasicProject project, BasicIssueType issueType, string summary)
            : this(project, issueType)
        {
            SetSummary(summary);
        }

        public IssueInputBuilder SetSummary(string summary)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.Summary, summary));
        }

        public IssueInputBuilder SetProjectKey(string projectKey)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.Project, ComplexIssueInputFieldValue.With("key", projectKey)));
        }

        public IssueInputBuilder SetProject(BasicProject project)
        {
            return SetProjectKey(project.Key);
        }

        public IssueInputBuilder SetIssueTypeId(long issueTypeId)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.IssueType, ComplexIssueInputFieldValue.With("id", issueTypeId.ToString())));
        }

        public IssueInputBuilder SetIssueType(BasicIssueType issueType)
        {
            if (issueType == null)
            {
                throw new ArgumentNullException("issueType");
            }

            if (issueType.Id == null)
            {
                throw new ArgumentException("BasicIssueType cannot have a null Id field", "issueType");
            }

            return SetIssueTypeId((long)issueType.Id);
        }

        public IssueInputBuilder SetFieldInput(FieldInput fieldInput)
        {
            fields.Add(fieldInput.Id, fieldInput);
            return this;
        }

        public IssueInputBuilder SetFieldValue(string id, ComplexIssueInputFieldValue value)
        {
            return SetFieldInput(new FieldInput(id, value));
        }

        public IssueInputBuilder SetFieldValue(string id, object value)
        {
            throw new NotImplementedException();
        }

        public IssueInputBuilder SetDescription(string description)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.Description, description));
        }

        public IssueInputBuilder SetAssignee(BasicUser assignee)
        {
            return SetAssigneeName(assignee.Name);
        }

        public IssueInputBuilder SetAssigneeName(string assignee)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.Assignee, ComplexIssueInputFieldValue.With("name", assignee)));
        }

        public IssueInput Build()
        {
            return new IssueInput(fields);
        }

        public IssueInputBuilder SetAffectedVersions(IEnumerable<JiraVersion> versions)
        {
            return SetAffectedVersionNames(versions.ConvertAll(a => a.Name));
        }

        public IssueInputBuilder SetAffectedVersionNames(IEnumerable<string> names)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.AffectsVersion, names.ConvertAll(a => ComplexIssueInputFieldValue.With("name", a))));
        }

        public IssueInputBuilder SetComponentNames(IEnumerable<string> names)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.Components, names.ConvertAll(a => ComplexIssueInputFieldValue.With("name", a))));
        }

        public IssueInputBuilder SetComponents(IEnumerable<BasicComponent> components)
        {
            return SetComponentNames(components.ConvertAll(a => a.Name));
        }

        public IssueInputBuilder SetComponents(params BasicComponent[] components)
        {
            return SetComponents(components.ToList());
        }

        public IssueInputBuilder SetDueDate(DateTimeOffset date)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.DueDate, date.ToRestString()));
        }

        public IssueInputBuilder SetFixVersions(IEnumerable<JiraVersion> versions)
        {
            return SetFixVersionNames(versions.ConvertAll(a => a.Name));
        }

        public IssueInputBuilder SetFixVersionNames(IEnumerable<string> names)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.FixVersions, names.ConvertAll(a => ComplexIssueInputFieldValue.With("name", a))));
        }

        public IssueInputBuilder SetPriority(BasicPriority priority)
        {
            if (priority == null)
            {
                throw new ArgumentNullException("priority");
            }

            if (priority.Id == null)
            {
                throw new ArgumentException("Priority Id cannot be null", "priority");
            }

            return SetPriorityId((long)priority.Id);
        }

        public IssueInputBuilder SetPriorityId(long id)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.Priority, ComplexIssueInputFieldValue.With("id", id.ToString())));
        }

        public IssueInputBuilder SetReporter(BasicUser reporter)
        {
            return SetReporterName(reporter.Name);
        }

        public IssueInputBuilder SetReporterName(string reporterName)
        {
            return SetFieldInput(new FieldInput(IssueFieldId.Reporter, ComplexIssueInputFieldValue.With("name", reporterName)));
        }
    }
}
