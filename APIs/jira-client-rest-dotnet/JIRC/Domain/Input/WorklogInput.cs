// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorklogInput.cs" company="David Bevin">
//   Copyright (c) 2013 David Bevin.
// </copyright>
// // <summary>
//   https://bitbucket.org/dpbevin/jira-rest-client-dot-net
//   Licensed under the BSD 2-Clause License.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace JIRC.Domain.Input
{
    public class WorklogInput
    {
        public WorklogInput(Uri self, Uri issueUri, BasicUser author, BasicUser updateAuthor, string comment, DateTimeOffset startDate, int minutesSpent, Visibility visibility)
            : this(self, issueUri, author, updateAuthor, comment, startDate, minutesSpent, visibility, AdjustmentEstimate.Auto, null)
        {
        }

        public WorklogInput(
            Uri self,
            Uri issueUri,
            BasicUser author,
            BasicUser updateAuthor,
            string comment,
            DateTimeOffset startDate,
            int minutesSpent,
            Visibility visibility,
            AdjustmentEstimate adjustEstimate,
            string adjustEstimateValue)
        {
            Self = self;
            IssueUri = issueUri;
            Author = author;
            UpdateAuthor = updateAuthor;
            Comment = comment;
            StartDate = startDate;
            MinutesSpent = minutesSpent;
            Visibility = visibility;
            AdjustEstimate = adjustEstimate;
            AdjustEstimateValue = adjustEstimateValue;
        }

        public Uri Self { get; set; }

        public Uri IssueUri { get; set; }

        public BasicUser Author { get; set; }

        public BasicUser UpdateAuthor { get; set; }

        public string Comment { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public int MinutesSpent { get; set; }

        public Visibility Visibility { get; set; }

        public AdjustmentEstimate AdjustEstimate { get; set; }

        public string AdjustEstimateValue { get; set; }

        public enum AdjustmentEstimate
        {
            New,
            Leave,
            Manual,
            Auto
        }
    }
}
