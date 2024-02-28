// <copyright file="AzureQueryExecutor.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bellatrix.KeyVault;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;

namespace Bellatrix.DynamicTestCases.AzureDevOps;

public static class AzureQueryExecutor
{
    private static readonly string _uri;
    private static readonly string _personalAccessToken;

    static AzureQueryExecutor()
    {
        _uri = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<AzureDevOpsDynamicTestCasesSettings>().Url);
        _personalAccessToken = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<AzureDevOpsDynamicTestCasesSettings>().Token);
    }

    public static List<WorkItem> GetWorkItems(string workItemId)
    {
        var credentials = new VssBasicCredential(string.Empty, _personalAccessToken);

        try
        {
            // create instance of work item tracking http client
            using var httpClient = new WorkItemTrackingHttpClient(new Uri(_uri), credentials);

            // execute the query to get the list of work items in the results
            var result = httpClient.GetWorkItemsAsync(new int[] { workItemId.ToInt() }).Result;

            return httpClient.GetWorkItemsAsync(new int[] { workItemId.ToInt() }).Result;
        }
        catch
        {
            return new List<WorkItem>();
        }
    }
}