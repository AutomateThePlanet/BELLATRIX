// <copyright file="TestCasesContext.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;

namespace Bellatrix.DynamicTestCases;

/// <summary>
/// A class used for access to the Test Case Info across the framework insfrastructure. Not to be used in the tests.
/// </summary>
public sealed class TestCasesContext
{
    public TestCasesContext()
    {
        AdditionalProperties = new Dictionary<string, string>();
        TestSteps = new List<TestStep>();
    }

        public string TestCaseName { get; set; }
        public string Precondition { get; set; }
        public string TestCaseDescription { get; set; }
        public string TestCaseId { get; set; }
        public string TestFullName { get; set; }
        public string TestProjectName { get; set; }
        public string RequirementId { get; set; }
        public string SuiteId { get; set; }
        public long ProjectId { get; set; }
        public TestCase TestCase { get; set; }
        public List<TestStep> TestSteps { get; set; }
        public Dictionary<string, string> AdditionalProperties { get; set; }

    public string GetAdditionalPropertyByKey(string key)
    {
        if (AdditionalProperties.ContainsKey(key))
        {
            return AdditionalProperties[key];
        }
        else
        {
            return null;
        }
    }
}