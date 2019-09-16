using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiraRestClientTest
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// create a connection to JIRA using the Rest client
			var jira = Jira.CreateRestClient("https://intranet.mindsatwork.com.br/jira/", "alex.santos", "swoordfish");


			Issue issue = await GetComentarioAsync(jira);


			/*
			 var issuesQuery = from i in issuesQuery
						  where i.Assignee == "admin" && i.Priority == "Major"
						  orderby i.Created
						  select i;
			*/
			Console.WriteLine();
		}

		public async static Task<Issue> GetComentarioAsync(Jira jira)
		{
			// use LINQ syntax to retrieve issues
			int rangeStart = 1634;
			int rangeEnd = 1646;
			List<int> range = Enumerable.Range(rangeStart, rangeEnd).ToList();
			string[] issuesRange = range.Select(x => $"BDMS-{x}").ToArray();
			IDictionary<string, Issue> issuesDict = await jira.Issues.GetIssuesAsync(issueKeys: issuesRange);

			foreach (var iKey in issuesDict.Keys)
			{
				Console.WriteLine("------------------");
				Console.WriteLine($"Issue: {iKey}");
				Console.WriteLine(issuesDict[iKey]);
				Console.WriteLine("------------------");
			}


			// get comments
			Issue issue = await jira.Issues.GetIssueAsync("BDMS-1079");
			var comments = await issue.GetCommentsAsync();

			Console.WriteLine(comments.First().Body);
			Console.WriteLine(comments.First().CreatedDate);
			Console.WriteLine(comments);

			// add comment
			await issue.AddCommentAsync("new test comment");

			return issue;
		}
	}
}
