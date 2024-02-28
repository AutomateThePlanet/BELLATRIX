// <copyright file="DynamicTestCaseAttribute.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix;

[AttributeUsage(AttributeTargets.All)]
public class DynamicTestCaseAttribute : Attribute
{
    public string SuiteId { get; set; }
    public string RequirementId { get; set; }
    public string TestName { get; set; }
    public string Description { get; set; }
    public string TestCaseId { get; set; }

    public DynamicTestCaseAttribute()
    {
    }

    public DynamicTestCaseAttribute(string testCaseId, string testCaseDescription)
    {
        TestCaseId = testCaseId;
        Description = testCaseDescription;
    }

    public DynamicTestCaseAttribute(string testCaseId, string testCaseDescription, string testCaseName)
        : this(testCaseId, testCaseDescription)
    {
        TestName = testCaseName;
    }

    public DynamicTestCaseAttribute(
        string testCaseId,
        string testCaseDescription,
        string testCaseName,
        string requirementId,
        string suiteId)
       : this(testCaseId, testCaseDescription, testCaseName)
    {
        RequirementId = requirementId;
        SuiteId = suiteId;
    }

    public virtual void SetCustomProperties()
    {
    }
}
