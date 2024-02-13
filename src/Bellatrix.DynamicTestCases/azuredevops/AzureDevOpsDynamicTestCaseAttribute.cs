// <copyright file="AzureDevOpsDynamicTestCaseAttribute.cs" company="Automate The Planet Ltd.">
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
using System.Text;

namespace Bellatrix.DynamicTestCases.AzureDevOps;

[AttributeUsage(AttributeTargets.All)]
public class AzureDevOpsDynamicTestCaseAttribute : DynamicTestCaseAttribute
{
    public AzureDevOpsDynamicTestCaseAttribute()
    {
    }

    public AzureDevOpsDynamicTestCaseAttribute(string testCaseId, string testCaseDescription)
        : base(testCaseId, testCaseDescription)
    {
    }

    public AzureDevOpsDynamicTestCaseAttribute(string testCaseId, string testCaseDescription, string testCaseName)
        : base(testCaseId, testCaseDescription, testCaseName)
    {
    }

    public AzureDevOpsDynamicTestCaseAttribute(
        string testCaseId,
        string testCaseDescription,
        string testCaseName,
        string requirementId,
        string suiteId)
       : base(testCaseId, testCaseDescription, testCaseName, requirementId, suiteId)
    {
    }

    public int Priority { get; set; } = 1;
    public string AreaPath { get; set; }
    public string IterationPath { get; set; }

    public override void SetCustomProperties()
    {
        var dynamicTestCasesService = ServicesCollection.Current.Resolve<DynamicTestCasesService>();
        dynamicTestCasesService.AddAdditionalProperty(nameof(Priority), Priority.ToString());
        dynamicTestCasesService.AddAdditionalProperty(nameof(AreaPath), AreaPath);
        dynamicTestCasesService.AddAdditionalProperty(nameof(IterationPath), IterationPath);
    }
}
