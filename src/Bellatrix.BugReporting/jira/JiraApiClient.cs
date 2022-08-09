// <copyright file="JiraApiClient.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bellatrix.BugReporting.Jira;
using Bellatrix.KeyVault;
using RestSharp;

namespace Bellatrix.BugReporting.Jira
{
    public static class JiraApiClient
    {
        private static readonly IRestClient _restClient;
        private static readonly string _token;
        private static readonly string _projectName;

        static JiraApiClient()
        {
            string baseUrl = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<JiraBugReportingSettings>().Url);
            _token = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<JiraBugReportingSettings>().Token);
            _projectName = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<JiraBugReportingSettings>().ProjectName);
            _restClient = new RestClient(baseUrl).UseSerializer<JsonNetSerializer>();
        }

        public static IssueResponse GetIssue(string id)
        {
            var request = new RestRequest($"rest/api/3/issue/{id}", Method.GET);
            request.AddHeader("Authorization", $"Basic {_token}");
            request.AddQueryParameter("fields", "summary");
            var response = _restClient.Execute<IssueResponse>(request);
            var content = response.Content;

            return response.Data;
        }

        public static List<Issue> GetAllBugs()
        {
            var issueSearchDto = new IssueSearchDto();
            issueSearchDto.jql = $"project = '{_projectName}' AND issuetype = Bug ORDER BY created DESC";
            issueSearchDto.fields = new List<string>()
            {
                "id",
                "summary",
                "status",
                "severity",
                "priority",
                "created",
                "description",
                "attachment",
            };
            issueSearchDto.expand = new List<string>() { "changelog" };
            issueSearchDto.maxResults = 100;
            var searchRequest = new RestRequest("rest/api/3/search", Method.POST);
            searchRequest.AddHeader("Authorization", $"Basic {_token}");
            searchRequest.RequestFormat = DataFormat.Json;
            searchRequest.AddJsonBody(issueSearchDto);
            var searchResponse = _restClient.Execute<IssueSearchResult>(searchRequest);

            var issuesWithSetDates = PopulateIssuesDateInfo(searchResponse.Data.issues);

            return issuesWithSetDates;
        }

        public static IssueCreateResponse CreateBug(string title, List<string> summaryLines, List<string> filesToUpload = null)
        {
            string defaultPriority = ConfigurationService.GetSection<JiraBugReportingSettings>().DefaultPriority;

            var issueCreateDto = IssueCreateDto.CreateDto(_projectName, defaultPriority, title, summaryLines);

            var createRequest = new RestRequest("rest/api/3/issue", Method.POST);
            createRequest.AddHeader("Authorization", $"Basic {_token}");
            createRequest.RequestFormat = DataFormat.Json;
            createRequest.AddJsonBody(issueCreateDto);

            IssueCreateResponse result = default;
            try
            {
                result = _restClient.Execute<IssueCreateResponse>(createRequest)?.Data;

                if (filesToUpload != null && filesToUpload.Any())
                {
                    foreach (var fileToUpload in filesToUpload)
                    {
                        var attachFileRequest = new RestRequest($"rest/api/2/issue/{result.key}/attachments", Method.POST);
                        attachFileRequest.AddHeader("Authorization", $"Basic {_token}");
                        attachFileRequest.AddHeader("X-Atlassian-Token", "no-check");
                        attachFileRequest.RequestFormat = DataFormat.Json;
                        attachFileRequest.AddFile("file", fileToUpload);

                        var attachResult = _restClient.Execute(attachFileRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        private static List<Issue> PopulateIssuesDateInfo(List<Issue> issues)
        {
            foreach (var currentIssue in issues)
            {
                currentIssue.IssueDateInfo = new IssueDateInfo(currentIssue.fields.created, currentIssue.fields.created.Year, currentIssue.fields.created.Month);
                currentIssue.IssueDateInfo.WeekOfYear = DateUtilities.GetIso8601WeekOfYear(currentIssue.IssueDateInfo.CreationDate);
            }

            return issues;
        }
    }
}
