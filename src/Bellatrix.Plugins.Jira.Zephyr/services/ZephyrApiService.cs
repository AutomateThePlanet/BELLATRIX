// <copyright file="ZephyrApiService.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Plugins.Jira.Zephyr;
using Bellatrix;
using RestSharp;
using Bellatrix.Plugins.Jira.Zephyr.Data;
using System.Text.Json;
using System.Web;

namespace Plugins.Jira.Zephyr.Services;

public static class ZephyrApiService
{
    private static ZephyrSettings Settings => ConfigurationService.GetSection<ZephyrSettings>();

    private static ThreadLocal<RestClient> _client = new ThreadLocal<RestClient>();

    static ZephyrApiService()
    {
        _client.Value = new RestClient(Settings.ApiUrl);
        _client.Value.AddDefaultHeader("Authorization", $"Bearer {Settings.Token}");
        _client.Value.AddDefaultHeader("Content-Type", "application/json");
    }

    internal static bool TryCreateTestCycle(ref ZephyrTestCycle testCycle)
    {
        var request = new RestRequest("/testcycles", Method.Post);
        request.AddJsonBody(new { name = testCycle.Name, projectKey = testCycle.ProjectKey, statusName = testCycle.StatusName });

        var response = _client.Value.Execute(request);

        var root = JsonDocument.Parse(response.Content).RootElement;

        if (root.TryGetProperty("id", out JsonElement idElement)) 
        {
            testCycle.Id = idElement.GetInt64().ToString();
        }

        if (root.TryGetProperty("key", out JsonElement keyElement))
        {
            testCycle.Key = keyElement.GetString();
        }

        return response.IsSuccessStatusCode;
    }

    internal static bool TryExecuteTestCase(ZephyrTestCase testCase)
    {
        var body = new Dictionary<string, object>
        {
            { "projectKey", testCase.ProjectKey },
            { "testCycleKey", testCase.CycleKey },
            { "testCaseKey", testCase.Id },
            { "statusName", testCase.Status },
            { "executionTime", (int)testCase.Duration }
        };

        if (!testCase.Status.Equals(TestExecutionStatus.Pass.GetValue()) && testCase.Exception != null) body.Add("comment", FormatError(testCase.Exception));

        var request = new RestRequest("/testexecutions", Method.Post).AddJsonBody(body);

        return _client.Value.Execute(request).IsSuccessStatusCode;
    }

    internal static bool TryMarkTestCycleDone(ZephyrTestCycle testCycle)
    {
        if (TryGetProjectId(testCycle.Key, out string? projectId) && TryGetStatusId(Settings.CycleFinalStatus, projectId, out string? statusId))
        {
            var body = new Dictionary<string, object>
        {
            { "id", testCycle.Id },
            { "key", testCycle.Key },
            { "name", testCycle.Name },
            { "project", new { projectId } },
            { "status", new { statusId } },
            { "plannedEndDate", testCycle.PlannedEndDate }
        };

            var request = new RestRequest($"/testcycles/{Settings.DefaultProjectKey}", Method.Put);
            request.AddJsonBody(body);

            return _client.Value.Execute(request).IsSuccessStatusCode;
        }

        return false;
    }

    private static bool TryGetProjectId(string testCycleIdOrKey, out string? projectId)
    {
        var request = new RestRequest($"/testcycles/{testCycleIdOrKey}", Method.Get);

        var response = _client.Value.Execute(request);
        var root = JsonDocument.Parse(response.Content).RootElement;

        if (root.TryGetProperty("project", out JsonElement project) && project.TryGetProperty("id", out JsonElement id))
        {
            projectId = id.GetString();
        }
        else projectId = null;

        return response.IsSuccessStatusCode;
    }

    private static bool TryGetStatusId(string statusName, string projectKey, out string? statusId)
    {
        if (TryGetStatuses(projectKey, out List<Dictionary<string, string>> statuses))
        {
            var status = statuses.Find(s => s.ContainsKey("name") && s["name"].Equals(statusName, StringComparison.OrdinalIgnoreCase));

            if (status == null)
            {
                Logger.LogInformation($"Could not find a status in project {projectKey} with the provided status name: {statusName}");
            }
            else
            {
                statusId = status["id"];
                return true;
            }
        }

        statusId = null;
        return false;
    }

    private static bool TryGetStatuses(string projectKey, out List<Dictionary<string, string>> statuses)
    {
        var request = new RestRequest("/statuses", Method.Get);
        request.AddQueryParameter("maxResults", "100");
        request.AddQueryParameter("projectKey", projectKey);
        request.AddQueryParameter("statusType", "TEST_CYCLE");

        var response = _client.Value.Execute<Dictionary<string, List<Dictionary<string, string>>>>(request);
        statuses = response.Data["values"];

        return response.IsSuccessStatusCode;
    }

    private static string FormatError(Exception exception)
    {
        return $"<strong>Failure details:</strong>\n\nError message:\n\n{HttpUtility.HtmlEncode(exception.Message)}\n\nStack Trace:<pre>{exception.StackTrace}</pre>".Replace("\n", "<br>");
    }
}
