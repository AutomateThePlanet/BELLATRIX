// <copyright file="AzureTestCase.cs" company="Automate The Planet Ltd.">
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

public class AzureTestCase
{
    public AzureTestCase()
    {
        TestSteps = new List<TestStep>();
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public string AreaPath { get; set; }
    public string TeamProject { get; set; }
    public string IterationPath { get; set; }
    public string Priority { get; set; }
    public string AutomatedTestName { get; set; }
    public string AutomatedTestStorage { get; set; }
    public string Steps { get; set; }
    public string RequirementUrl { get; set; }
    public int Id { get; set; }
    public List<TestStep> TestSteps { get; set; }
    public string TestStepsHtml { get; set; }
}
