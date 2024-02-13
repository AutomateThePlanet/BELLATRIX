// <copyright file="AzureDevOpsBugReportingService.cs" company="Automate The Planet Ltd.">
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
using System.Text;
using Bellatrix.BugReporting;
using Bellatrix.BugReporting.AzureDevOps;
using Bellatrix.BugReporting.Contracts;

namespace Bellatrix.BugReporting.AzureDevOps;

public class AzureDevOpsBugReportingService : IBugReportingService
{
    public void LogBug(BugReportingContext testCasesContext, string exceptionMessage, List<string> filePathsToBeAttached = null)
    {
        string stepsToReproduce = GenerateStepsToReproduce(testCasesContext.TestSteps);
        AzureQueryExecutor.CreateBug($"TEST FAILED: {testCasesContext.TestFullName}", stepsToReproduce, exceptionMessage, filePathsToBeAttached);
    }

    // TODO: Generate Better HTML. Do it in editor and then see the format.
    private string GenerateStepsToReproduce(List<TestStep> testSteps)
    {
        var stepsToReproduceBuilder = new StringBuilder();
        foreach (var step in testSteps)
        {
            stepsToReproduceBuilder.AppendLine($"<div>{step.Description}");
            if (!string.IsNullOrEmpty(step.Expected))
            {
                stepsToReproduceBuilder.Append($" {step.Expected}");
            }

            stepsToReproduceBuilder.AppendLine("</div>");
        }

        return stepsToReproduceBuilder.ToString();
    }
}