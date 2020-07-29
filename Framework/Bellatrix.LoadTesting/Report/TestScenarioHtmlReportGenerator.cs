// <copyright file="TestScenarioHtmlReportGenerator.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using System.Text;
using Bellatrix.LoadTesting.Model.Results;

namespace Bellatrix.LoadTesting.Report
{
    public class TestScenarioHtmlReportGenerator
    {
        public string GenerateHtml(LoadTestRunResults loadTestRunResults)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<br><span class=\"titleLabel\">Total Execution Time:</span>  <span class=\"titleLabelValue\">{loadTestRunResults.TotalExecutionTime}</span><br><br>");
            sb.AppendLine("<span class=\"titleLabel\">Test Scenarios Execution Times</span> <br>");
            sb.AppendLine("<div class=\"chart-container\" style=\"overflow: hidden; height: 500px; width: 800px\">");
            sb.AppendLine("<canvas id=\"testScenariosExecutionTimesChart\"></canvas>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div id=\"allTests\">");
            int testScenarioCount = 1;
            foreach (var currentTestScenarioResults in loadTestRunResults.TestScenarioResults.Values)
            {
                sb.AppendLine($"<div id=\"test{testScenarioCount}\">");

                sb.AppendLine($"<span class=\"titleLabel\">Test Scenario:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.TestName}</span><br>");
                sb.AppendLine($"<span class=\"titleLabel\">Average Execution Time:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.AverageExecutionTimeSeconds} seconds</span><br>");
                sb.AppendLine($"<span class=\"titleLabel\">Max Execution Time:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.MaxExecutionTimeSeconds} seconds</span><br>");
                sb.AppendLine($"<span class=\"titleLabel\">Min Execution Time:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.MinExecutionTimeSeconds} seconds</span><br>");
                sb.AppendLine($"<span class=\"titleLabel\">Times Executed:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.TimesExecuted}</span><br>");
                sb.AppendLine($"<span class=\"titleLabel\">Times Passed:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.TimesPassed}</span><br>");
                sb.AppendLine($"<span class=\"titleLabel\">Times Failed:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.TimesFailed}</span><br>");
                sb.AppendLine($"<span class=\"titleLabel\">Weight:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.Weight}</span><br><br>");
                sb.AppendLine($"<div id=\"jsGridRequests{testScenarioCount}\"></div><br><br>");

                sb.AppendLine($"<span class=\"titleLabel\">Failed Assertions:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.FailedAssertionsCount}</span><br><br>");
                sb.AppendLine($"<div id=\"jsGridFailedAssertions{testScenarioCount}\"></div><br><br>");
                sb.AppendLine($"<span class=\"titleLabel\">All Assertions:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.TotalAssertionsCount}</span><br><br>");
                sb.AppendLine($"<span class=\"titleLabel\">Failed Assertions:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.FailedAssertionsCount}</span><br><br>");
                sb.AppendLine($"<span class=\"titleLabel\">Passed Assertions:</span>  <span class=\"titleLabelValue\">{currentTestScenarioResults.PassedAssertionsCount}</span><br><br>");
                sb.AppendLine($"<div id=\"jsGridAllAssertions{testScenarioCount}\"></div><br><br>");
                sb.AppendLine("</div>");

                testScenarioCount++;
            }

            sb.AppendLine("</div>");
            return sb.ToString();
        }
    }
}