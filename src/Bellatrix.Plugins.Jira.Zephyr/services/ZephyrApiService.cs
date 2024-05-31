// <copyright file="ZephyrApiService.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
using Newtonsoft.Json;
using RestSharp;
using Bellatrix.Plugins.Jira.Zephyr.Data;

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

    internal static ZephyrTestCycleResponse CreateTestCycle(ZephyrPlugin.ZephyrLocalData data)
    {
        var request = new RestRequest("/testcycles", Method.Post);
        request.AddJsonBody(new { name = data.TestCycle.Name, projectKey = data.TestCycle.ProjectKey, statusName = data.TestCycle.StatusName });

        var response = _client.Value.Execute(request);
        var obj =  JsonConvert.DeserializeObject<ZephyrTestCycleResponse>(response.Content);
        return obj;
    }

    internal static RestResponse ExecuteTestCase(ZephyrTestCase testCase)
    {
        var body = new Dictionary<string, object>
        {
            { "projectKey", testCase.ProjectId },
            { "testCycleKey", testCase.TestCycleId },
            { "testCaseKey", testCase.TestCaseId },
            { "statusName", testCase.Status },
            { "executionTime", (int)testCase.Duration }
        };

        var request = new RestRequest("/testexecutions", Method.Post);
        request.AddJsonBody(body);

        return _client.Value.Execute(request);
    }

    internal static RestResponse MarkTestCycleDone(ZephyrPlugin.ZephyrLocalData data)
    {
        var body = new Dictionary<string, object>
        {
            { "id", data.TestCycleResponse.id },
            { "key", data.TestCycleResponse.key },
            { "name", data.TestCycle.Name },
            { "project", new { id = GetProjectId(data.TestCycleResponse.key) } },
            { "status", new { id = GetStatusId(Settings.CycleFinalStatus, data.TestCycle.ProjectKey) } },
            { "plannedEndDate", data.TestCycle.PlannedEndDate }
        };

        var request = new RestRequest($"/testcycles/{Settings.DefaultProjectKey}", Method.Put);
        request.AddJsonBody(body);

        return _client.Value.Execute(request);
    }

    private static string GetProjectId(string testCycleIdOrKey)
    {
        var request = new RestRequest($"/testcycles/{testCycleIdOrKey}", Method.Get);

        var response = _client.Value.Execute<TestCycle>(request);
        return response.Data.Project.Id;
    }

    private static string GetStatusId(string statusName, string projectKey)
    {
        try
        {
            var statuses = GetStatuses(projectKey);
            var status = statuses.Find(s => s.Name.Equals(statusName, StringComparison.OrdinalIgnoreCase));

            if (status == null)
            {
                throw new ArgumentException($"Could not find a status in project {projectKey} with the provided status name: {statusName}");
            }

            return status.Id;
        }
        catch (ArgumentException ex)
        {
            Logger.LogInformation(ex.Message);
            return string.Empty;
        }
    }

    private static List<Status> GetStatuses(string projectKey)
    {
        var request = new RestRequest("/statuses", Method.Get);
        request.AddQueryParameter("maxResults", "100");
        request.AddQueryParameter("projectKey", projectKey);
        request.AddQueryParameter("statusType", "TEST_CYCLE");

        var response = _client.Value.Execute<Statuses>(request);
        return response.Data.Values;
    }

    private class Status
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    private class Statuses
    {
        [JsonProperty("values")]
        public List<Status> Values { get; set; }
    }

    private class Project
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    private class TestCycle
    {
        [JsonProperty("project")]
        public Project Project { get; set; }
    }
}
