// <copyright file="AzureTestCasesService.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bellatrix.KeyVault;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace Bellatrix.DynamicTestCases.AzureDevOps;

public class AzureTestCasesService
{
    private readonly string _uri;
    private readonly string _personalAccessToken;
    private readonly string _project;

    public AzureTestCasesService()
    {
        _uri = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<AzureDevOpsDynamicTestCasesSettings>().Url);
        _personalAccessToken = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<AzureDevOpsDynamicTestCasesSettings>().Token);
        _project = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<AzureDevOpsDynamicTestCasesSettings>().ProjectName);
    }

    public AzureTestCase CreatTestCase(AzureTestCase testCase)
    {
        var uri = new Uri(_uri);

        var credentials = new VssBasicCredential(string.Empty, _personalAccessToken);
        var patchDocument = new JsonPatchDocument();

        patchDocument.Add(new JsonPatchOperation()
        {
            Operation = Operation.Add,
            Path = "/fields/System.Title",
            Value = testCase.Title,
        });

        patchDocument.Add(new JsonPatchOperation()
        {
            Operation = Operation.Add,
            Path = "/fields/Microsoft.VSTS.Common.Priority",
            Value = testCase.Priority,
        });

        if (!string.IsNullOrEmpty(testCase.AreaPath))
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/System.AreaPath",
                Value = testCase.AreaPath,
            });
        }

        if (!string.IsNullOrEmpty(testCase.IterationPath))
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/System.IterationPath",
                Value = testCase.IterationPath,
            });
        }

        if (!string.IsNullOrEmpty(testCase.Description))
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/System.Description",
                Value = testCase.Description,
            });
        }

        if (!string.IsNullOrEmpty(TestStepsService.GenerateTestStepsHtml(testCase.TestSteps)) || !string.IsNullOrEmpty(testCase.TestStepsHtml))
        {
            if (testCase.TestSteps.Any())
            {
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/Microsoft.VSTS.TCM.Steps",
                    Value = TestStepsService.GenerateTestStepsHtml(testCase.TestSteps),
                });
            }
            else
            {
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/Microsoft.VSTS.TCM.Steps",
                    Value = testCase.TestStepsHtml,
                });
            }
        }

        patchDocument.Add(new JsonPatchOperation()
        {
            Operation = Operation.Add,
            Path = "/fields/Microsoft.VSTS.TCM.AutomatedTestName",
            Value = testCase.AutomatedTestName,
        });

        patchDocument.Add(new JsonPatchOperation()
        {
            Operation = Operation.Add,
            Path = "/fields/Microsoft.VSTS.TCM.AutomatedTestStorage",
            Value = testCase.AutomatedTestStorage + ".dll",
        });

        if (!string.IsNullOrEmpty(testCase.RequirementUrl))
        {
            patchDocument.Add(
          new JsonPatchOperation()
          {
              Operation = Operation.Add,
              Path = "/relations/-",
              Value = new
              {
                  rel = "System.LinkTypes.Hierarchy-Reverse",
                  url = testCase.RequirementUrl,
                  attributes = new Dictionary<string, object>()
                  {
                     { "isLocked", false },
                     { "name", "Parent" },
                     { "comment", "added automatically" },
                  },
              },
          });
        }

        VssConnection connection = new VssConnection(uri, credentials);
        WorkItemTrackingHttpClient workItemTrackingHttpClient = connection.GetClient<WorkItemTrackingHttpClient>();

        try
        {
            WorkItem result = workItemTrackingHttpClient.CreateWorkItemAsync(patchDocument, _project, "Test Case").Result;
            return ConvertWorkItemToAzureTestCase(result);
        }
        catch (AggregateException ex)
        {
            Debug.WriteLine("Error creating test case: {0}", ex.InnerException.Message);
            return null;
        }
    }

    public AzureTestCase UpdateTestCase(AzureTestCase testCase)
    {
        var uri = new Uri(_uri);
        string project = _project;

        var credentials = new VssBasicCredential(string.Empty, _personalAccessToken);
        VssConnection connection = new VssConnection(uri, credentials);
        WorkItemTrackingHttpClient workItemTrackingHttpClient = connection.GetClient<WorkItemTrackingHttpClient>();
        var existingItem = workItemTrackingHttpClient.GetWorkItemAsync(testCase.Id, expand: WorkItemExpand.Relations).Result;

        var patchDocument = new JsonPatchDocument();

        patchDocument.Add(new JsonPatchOperation()
        {
            Operation = Operation.Replace,
            Path = "/fields/System.Title",
            Value = testCase.Title,
        });

        patchDocument.Add(new JsonPatchOperation()
        {
            Operation = Operation.Replace,
            Path = "/fields/Microsoft.VSTS.Common.Priority",
            Value = testCase.Priority,
        });

        if (!string.IsNullOrEmpty(testCase.AreaPath))
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Replace,
                Path = "/fields/System.AreaPath",
                Value = testCase.AreaPath,
            });
        }

        if (!string.IsNullOrEmpty(testCase.IterationPath))
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Replace,
                Path = "/fields/System.IterationPath",
                Value = testCase.IterationPath,
            });
        }

        if (!string.IsNullOrEmpty(testCase.Description))
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Replace,
                Path = "/fields/System.Description",
                Value = testCase.Description,
            });
        }

        if (!string.IsNullOrEmpty(testCase.TestStepsHtml))
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Replace,
                Path = "/fields/Microsoft.VSTS.TCM.Steps",
                Value = testCase.TestStepsHtml,
            });
        }

        patchDocument.Add(new JsonPatchOperation()
        {
            Operation = Operation.Replace,
            Path = "/fields/Microsoft.VSTS.TCM.AutomatedTestName",
            Value = testCase.AutomatedTestName,
        });

        patchDocument.Add(new JsonPatchOperation()
        {
            Operation = Operation.Replace,
            Path = "/fields/Microsoft.VSTS.TCM.AutomatedTestStorage",
            Value = testCase.AutomatedTestStorage,
        });

        if (!string.IsNullOrEmpty(testCase.RequirementUrl))
        {
            if (existingItem.Relations == null)
            {
                patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/relations/-",
                    Value = new
                    {
                        rel = "System.LinkTypes.Hierarchy-Reverse",
                        url = testCase.RequirementUrl,
                        attributes = new Dictionary<string, object>()
                        {
                         { "isLocked", false },
                         { "name", "Parent" },
                         { "comment", "added automatically" },
                        },
                    },
                });
            }
            else if (!existingItem.Relations.Any(x => x.Url.Equals(testCase.RequirementUrl)))
            {
                patchDocument.Add(
                 new JsonPatchOperation()
                 {
                     Operation = Operation.Add,
                     Path = "/relations/-",
                     Value = new
                     {
                         rel = "System.LinkTypes.Hierarchy-Reverse",
                         url = testCase.RequirementUrl,
                         attributes = new Dictionary<string, object>()
                         {
                             { "isLocked", false },
                             { "name", "Parent" },
                             { "comment", "added automatically" },
                         },
                     },
                 });
            }
        }

        try
        {
            WorkItem result = workItemTrackingHttpClient.UpdateWorkItemAsync(patchDocument, testCase.Id).Result;

            return ConvertWorkItemToAzureTestCase(result);
        }
        catch (AggregateException ex)
        {
            Debug.WriteLine("Error updating test case: {0}", ex.InnerException.Message);
            return null;
        }
    }

    public AzureTestCase AssociateAutomationToExistingTestCase(string workItemId, string testName, string testProjectName, string requirementUrl)
    {
        var uri = new Uri(_uri);

        var credentials = new VssBasicCredential(string.Empty, _personalAccessToken);
        VssConnection connection = new VssConnection(uri, credentials);
        WorkItemTrackingHttpClient workItemTrackingHttpClient = connection.GetClient<WorkItemTrackingHttpClient>();

        var existingItem = workItemTrackingHttpClient.GetWorkItemAsync(workItemId.ToInt(), expand: WorkItemExpand.Relations).Result;
        var patchDocument = new JsonPatchDocument();

        if (existingItem.Fields.ContainsKey("Microsoft.VSTS.TCM.AutomatedTestName"))
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Replace,
                Path = "/fields/Microsoft.VSTS.TCM.AutomatedTestName",
                Value = testName,
            });
        }
        else
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/Microsoft.VSTS.TCM.AutomatedTestName",
                Value = testName,
            });
        }

        if (existingItem.Fields.ContainsKey("Microsoft.VSTS.TCM.AutomatedTestStorage"))
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Replace,
                Path = "/fields/Microsoft.VSTS.TCM.AutomatedTestStorage",
                Value = testProjectName,
            });
        }
        else
        {
            patchDocument.Add(new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/Microsoft.VSTS.TCM.AutomatedTestStorage",
                Value = testProjectName,
            });
        }

        if (!string.IsNullOrEmpty(requirementUrl))
        {
            if (existingItem.Relations == null)
            {
                patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/relations/-",
                    Value = new
                    {
                        rel = "System.LinkTypes.Hierarchy-Reverse",
                        url = requirementUrl,
                        attributes = new Dictionary<string, object>()
                        {
                         { "isLocked", false },
                         { "name", "Parent" },
                         { "comment", "added automatically" },
                        },
                    },
                });
            }
            else if (!existingItem.Relations.Any(x => x.Url.Equals(requirementUrl)))
            {
                patchDocument.Add(
                 new JsonPatchOperation()
                 {
                     Operation = Operation.Add,
                     Path = "/relations/-",
                     Value = new
                     {
                         rel = "System.LinkTypes.Hierarchy-Reverse",
                         url = requirementUrl,
                         attributes = new Dictionary<string, object>()
                         {
                             { "isLocked", false },
                             { "name", "Parent" },
                             { "comment", "added automatically" },
                         },
                     },
                 });
            }
        }

        try
        {
            WorkItem result = workItemTrackingHttpClient.UpdateWorkItemAsync(patchDocument, workItemId.ToInt()).Result;

            return ConvertWorkItemToAzureTestCase(result);
        }
        catch (AggregateException ex)
        {
            Debug.WriteLine("Error updating test case: {0}", ex.InnerException.Message);
            return null;
        }
    }

    public List<AzureTestCase> FindTestCasesByAssociatedAutomation(string fullTestName, string testStorage)
    {
        // Create a wiql object and build our query
        var wiql = new Wiql()
        {
            // NOTE: Even if other columns are specified, only the ID & URL will be available in the WorkItemReference
            Query = "Select [Id] " +
                    "From WorkItems " +
                    "Where [Work Item Type] = 'Test Case' " +
                    "And [System.TeamProject] = '" + _project + "' " +
                    $"And [Microsoft.VSTS.TCM.AutomatedTestName] = '{fullTestName}'" +
                    $"And [Microsoft.VSTS.TCM.AutomatedTestStorage] = '{testStorage}'",
        };

        var credentials = new VssBasicCredential(string.Empty, _personalAccessToken);

        try
        {
            // create instance of work item tracking http client
            using var httpClient = new WorkItemTrackingHttpClient(new Uri(_uri), credentials);

            // execute the query to get the list of work items in the results
            var result = httpClient.QueryByWiqlAsync(wiql).Result;
            var ids = result.WorkItems.Select(item => item.Id).ToArray();

            // some error handling
            if (ids.Length == 0)
            {
                return new List<AzureTestCase>();
            }

            var resultTestCases = new List<AzureTestCase>();

            // build a list of the fields we want to see
            var fields = new[] { "System.Id", "System.Title", "System.State" };

            foreach (var item in httpClient.GetWorkItemsAsync(ids, expand: WorkItemExpand.Relations).Result)
            {
                resultTestCases.Add(ConvertWorkItemToAzureTestCase(item));
            }

            return resultTestCases;
        }
        catch
        {
            return new List<AzureTestCase>();
        }
    }

    private AzureTestCase ConvertWorkItemToAzureTestCase(WorkItem workItem)
    {
        var result = new AzureTestCase();

        if (workItem.Fields.ContainsKey("System.AreaPath"))
        {
            result.AreaPath = workItem.Fields["System.AreaPath"].ToString();
        }

        if (workItem.Fields.ContainsKey("System.IterationPath"))
        {
            result.IterationPath = workItem.Fields["System.IterationPath"].ToString();
        }

        if (workItem.Fields.ContainsKey("System.Title"))
        {
            result.Title = workItem.Fields["System.Title"].ToString();
        }

        if (workItem.Fields.ContainsKey("System.Description"))
        {
            result.Description = workItem.Fields["System.Description"].ToString();
        }

        if (workItem.Fields.ContainsKey("Microsoft.VSTS.Common.Priority"))
        {
            result.Priority = workItem.Fields["Microsoft.VSTS.Common.Priority"].ToString();
        }

        if (workItem.Fields.ContainsKey("Microsoft.VSTS.TCM.AutomatedTestName"))
        {
            result.AutomatedTestName = workItem.Fields["Microsoft.VSTS.TCM.AutomatedTestName"].ToString();
        }

        if (workItem.Fields.ContainsKey("Microsoft.VSTS.TCM.AutomatedTestStorage"))
        {
            result.AutomatedTestStorage = workItem.Fields["Microsoft.VSTS.TCM.AutomatedTestStorage"].ToString();
        }

        if (workItem.Fields.ContainsKey("Microsoft.VSTS.TCM.Steps"))
        {
            result.TestStepsHtml = workItem.Fields["Microsoft.VSTS.TCM.Steps"].ToString();
        }

        if (workItem.Id != null)
        {
            result.Id = (int)workItem.Id;
        }

        if (workItem.Relations != null && workItem.Relations.Any())
        {
            result.RequirementUrl = workItem.Relations.FirstOrDefault()?.Url;
        }

        return result;
    }
}