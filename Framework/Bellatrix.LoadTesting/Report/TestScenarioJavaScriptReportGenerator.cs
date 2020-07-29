// <copyright file="TestScenarioJavaScriptReportGenerator.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Text;
using System.Web;
using Bellatrix.LoadTesting.Model.Results;
using Yahoo.Yui.Compressor;

namespace Bellatrix.LoadTesting.Report
{
    public class TestScenarioJavaScriptReportGenerator
    {
        private readonly List<string> _hexChartColors = new List<string>
        {
            "213, 62, 79",
            "191, 129, 45",
            "53, 151, 143",
            "188, 128, 189",
            "255, 0, 0",
            "51, 0, 51",
            "0, 77, 0",
            "0, 179, 0",
            "128, 220, 255",
            "255, 197, 128",
            "128, 177, 211",
            "51, 160, 44",
            "215, 48, 39",
            "224, 130, 20",
            "26, 0, 26",
            "26, 0, 26",
            "26, 0, 255",
            "26, 255, 255",
            "213, 62, 79",
            "191, 129, 45",
            "53, 151, 143",
            "188, 128, 189",
            "255, 0, 0",
            "51, 0, 51",
            "0, 77, 0",
            "0, 179, 0",
            "128, 220, 255",
            "255, 197, 128",
            "128, 177, 211",
            "51, 160, 44",
            "215, 48, 39",
            "224, 130, 20",
            "26, 0, 26",
            "26, 0, 26",
            "26, 0, 255",
            "26, 255, 255",
            "213, 62, 79",
            "191, 129, 45",
            "53, 151, 143",
            "188, 128, 189",
            "255, 0, 0",
            "51, 0, 51",
            "0, 77, 0",
            "0, 179, 0",
            "128, 220, 255",
            "255, 197, 128",
            "128, 177, 211",
            "51, 160, 44",
            "215, 48, 39",
            "224, 130, 20",
            "26, 0, 26",
            "26, 0, 26",
            "26, 0, 255",
            "26, 255, 255",
            "213, 62, 79",
            "191, 129, 45",
            "53, 151, 143",
            "188, 128, 189",
            "255, 0, 0",
            "51, 0, 51",
            "0, 77, 0",
            "0, 179, 0",
            "128, 220, 255",
            "255, 197, 128",
            "128, 177, 211",
            "51, 160, 44",
            "215, 48, 39",
            "224, 130, 20",
            "26, 0, 26",
            "26, 0, 26",
            "26, 0, 255",
            "26, 255, 255",
        };

        public string GenerateJavaScript(LoadTestRunResults loadTestRunResults)
        {
            var sb = new StringBuilder();
            GenerateAllRequestsGridJS(loadTestRunResults, sb);
            GenerateFailedAssertionsGridJS(loadTestRunResults, sb);
            GenerateAllAssertionsGridJS(loadTestRunResults, sb);
            GenerateTestScenariosChartJS(loadTestRunResults, sb);
            var javaScriptCompressor = new JavaScriptCompressor();
            var minifiedString = javaScriptCompressor.Compress(sb.ToString());
            return minifiedString;
        }

        private void GenerateAllRequestsGridJS(LoadTestRunResults loadTestRunResults, StringBuilder sb)
        {
            int testScenarioCount = 1;
            foreach (var currentTestScenarioResults in loadTestRunResults.TestScenarioResults.Values)
            {
                sb.AppendLine($"var dbRequests{testScenarioCount} = {{");
                sb.AppendLine("loadData: function (filter) {");
                sb.AppendLine($"return $.grep(this.requestResults{testScenarioCount}, function (requestResults) {{");
                sb.AppendLine("return (!filter.URL || requestResults.URL.toLowerCase().indexOf(filter.URL.toLowerCase()) > -1)");
                sb.AppendLine("&& (!filter.IsSuccessful || requestResults.IsSuccessful === filter.IsSuccessful)");
                sb.AppendLine("&& (!filter.ExecutionTime || requestResults.ExecutionTime.indexOf(filter.ExecutionTime) > -1)");
                sb.AppendLine("&& (!filter.StatusCode || requestResults.StatusCode === filter.StatusCode)");
                sb.AppendLine("&& (!filter.Exception || requestResults.Exception.toLowerCase().indexOf(filter.Exception.toLowerCase()) > -1);");
                sb.AppendLine("});");
                sb.AppendLine("}");
                sb.AppendLine("};");

                sb.AppendLine($"dbRequests{testScenarioCount}.requestResults{testScenarioCount} = [");
                foreach (var currentTestScenarioRunResult in currentTestScenarioResults.TestScenarioRunResults.Values)
                {
                    foreach (var requestResults in currentTestScenarioRunResult.RequestResults)
                    {
                        sb.AppendLine("{");
                        sb.AppendLine($" \"URL\": \"{HttpUtility.JavaScriptStringEncode(requestResults.RequestUrl)}\",");
                        sb.AppendLine($" \"IsSuccessful\": \"{requestResults.IsSuccessful}\",");
                        sb.AppendLine($" \"ExecutionTime\": \"{requestResults.ExecutionTime}\",");
                        sb.AppendLine($" \"StatusCode\": \"{requestResults.StatusCode}\",");
                        sb.AppendLine($" \"Exception\": \"{HttpUtility.JavaScriptStringEncode(requestResults?.ResponseContent)}\",");
                        sb.AppendLine("},");
                    }
                }

                sb.AppendLine("];");
                sb.AppendLine($"$(\"#jsGridRequests{testScenarioCount}\").jsGrid({{");
                sb.AppendLine("width: \"100 % \",");
                sb.AppendLine("height: \"800px\",");
                sb.AppendLine("filtering: true,");
                sb.AppendLine("sorting: true,");
                sb.AppendLine("paging: true,");
                sb.AppendLine("autoload: true,");
                sb.AppendLine("pageSize: 10,");
                sb.AppendLine("pageButtonCount: 5,");
                sb.AppendLine($"controller: dbRequests{testScenarioCount},");
                sb.AppendLine("fields: [{");
                sb.AppendLine(" name: \"URL\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" width: 250");
                sb.AppendLine("},");
                sb.AppendLine("{");
                sb.AppendLine(" name: \"IsSuccessful\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" width: 50");
                sb.AppendLine("},");
                sb.AppendLine("{");
                sb.AppendLine(" name: \"ExecutionTime\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" width: 60");
                sb.AppendLine("},");
                sb.AppendLine("{");
                sb.AppendLine(" name: \"StatusCode\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" width: 50");
                sb.AppendLine("},");
                sb.AppendLine("{");
                sb.AppendLine(" name: \"Exception\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" title: \"Exception Message\",");
                sb.AppendLine(" width: 300");
                sb.AppendLine("}");
                sb.AppendLine("]");
                sb.AppendLine("});");

                sb.AppendLine($"var dbfailedAssertions{testScenarioCount} = {{");
                sb.AppendLine("loadData: function (filter) {");
                sb.AppendLine($"return $.grep(this.failedAssertions{testScenarioCount}, function (failedAssertion) {{");
                sb.AppendLine("return (!filter.URL || failedAssertion.URL.toLowerCase().indexOf(filter.URL.toLowerCase()) > -1)");
                sb.AppendLine("&& (!filter.IsSuccessful || failedAssertion.IsSuccessful === filter.IsSuccessful)");
                sb.AppendLine("&& (!filter.AssertionType || failedAssertion.AssertionType.toLowerCase().indexOf(filter.AssertionType.toLowerCase()) > -1)");
                sb.AppendLine("&& (!filter.Exception || failedAssertion.Exception.toLowerCase().indexOf(filter.Exception.toLowerCase()) > -1);");
                sb.AppendLine("});");
                sb.AppendLine("}");
                sb.AppendLine("};");

                testScenarioCount++;
            }
        }

        private void GenerateFailedAssertionsGridJS(LoadTestRunResults loadTestRunResults, StringBuilder sb)
        {
            int testScenarioCount = 1;
            foreach (var currentTestScenarioResults in loadTestRunResults.TestScenarioResults.Values)
            {
                sb.AppendLine($"var dbfailedAssertions{testScenarioCount} = {{");
                sb.AppendLine("loadData: function (filter) {");
                sb.AppendLine($"return $.grep(this.failedAssertions{testScenarioCount}, function (failedAssertion) {{");
                sb.AppendLine("return (!filter.URL || failedAssertion.URL.toLowerCase().indexOf(filter.URL.toLowerCase()) > -1)");
                sb.AppendLine("&& (!filter.IsSuccessful || failedAssertion.IsSuccessful === filter.IsSuccessful)");
                sb.AppendLine("&& (!filter.AssertionType || failedAssertion.AssertionType.toLowerCase().indexOf(filter.AssertionType.toLowerCase()) > -1)");
                sb.AppendLine("&& (!filter.Exception || failedAssertion.Exception.toLowerCase().indexOf(filter.Exception.toLowerCase()) > -1);");
                sb.AppendLine("});");
                sb.AppendLine("}");
                sb.AppendLine("};");

                sb.AppendLine($"dbfailedAssertions{testScenarioCount}.failedAssertions{testScenarioCount} = [");
                foreach (var currentTestScenarioRunResult in currentTestScenarioResults.TestScenarioRunResults.Values)
                {
                    foreach (var requestResults in currentTestScenarioRunResult.RequestResults)
                    {
                        foreach (var responseAssertionResult in requestResults.ResponseAssertionResults)
                        {
                            if (responseAssertionResult != null && !responseAssertionResult.Passed)
                            {
                                sb.AppendLine("{");
                                sb.AppendLine($" \"URL\": \"{HttpUtility.JavaScriptStringEncode(requestResults?.RequestUrl)}\",");
                                sb.AppendLine($" \"IsSuccessful\": \"{responseAssertionResult?.Passed}\",");
                                sb.AppendLine($" \"AssertionType\": \"{responseAssertionResult?.AssertionType}\",");
                                sb.AppendLine($" \"Exception\": \"{HttpUtility.JavaScriptStringEncode(responseAssertionResult?.FailedMessage)}\",");
                                sb.AppendLine("},");
                            }
                        }
                    }
                }

                sb.AppendLine("];");
                sb.AppendLine($"$(\"#jsGridFailedAssertions{testScenarioCount}\").jsGrid({{");
                sb.AppendLine("width: \"100 % \",");
                sb.AppendLine("height: \"400px\",");
                sb.AppendLine("filtering: true,");
                sb.AppendLine("sorting: true,");
                sb.AppendLine("paging: true,");
                sb.AppendLine("autoload: true,");
                sb.AppendLine("pageSize: 5,");
                sb.AppendLine("pageButtonCount: 5,");
                sb.AppendLine($"controller: dbfailedAssertions{testScenarioCount},");
                sb.AppendLine("fields: [{");
                sb.AppendLine(" name: \"URL\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" width: 250");
                sb.AppendLine("},");
                sb.AppendLine("{");
                sb.AppendLine(" name: \"IsSuccessful\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" width: 50");
                sb.AppendLine("},");
                sb.AppendLine("{");
                sb.AppendLine(" name: \"AssertionType\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" width: 150");
                sb.AppendLine("},");
                sb.AppendLine("{");
                sb.AppendLine(" name: \"Exception\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" title: \"Exception Message\",");
                sb.AppendLine(" width: 300");
                sb.AppendLine("}");
                sb.AppendLine("]");
                sb.AppendLine("});");

                testScenarioCount++;
            }
        }

        private void GenerateAllAssertionsGridJS(LoadTestRunResults loadTestRunResults, StringBuilder sb)
        {
            int testScenarioCount = 1;
            foreach (var currentTestScenarioResults in loadTestRunResults.TestScenarioResults.Values)
            {
                sb.AppendLine($"var dballAssertions{testScenarioCount} = {{");
                sb.AppendLine("loadData: function (filter) {");
                sb.AppendLine($"return $.grep(this.allAssertions{testScenarioCount}, function (allAssertion) {{");
                sb.AppendLine("return (!filter.URL || allAssertion.URL.toLowerCase().indexOf(filter.URL.toLowerCase()) > -1)");
                sb.AppendLine("&& (!filter.IsSuccessful || allAssertion.IsSuccessful === filter.IsSuccessful)");
                sb.AppendLine("&& (!filter.AssertionType || allAssertion.AssertionType.toLowerCase().indexOf(filter.AssertionType.toLowerCase()) > -1)");
                sb.AppendLine("&& (!filter.Exception || allAssertion.Exception.toLowerCase().indexOf(filter.Exception.toLowerCase()) > -1);");
                sb.AppendLine("});");
                sb.AppendLine("}");
                sb.AppendLine("};");

                sb.AppendLine($"dballAssertions{testScenarioCount}.allAssertions{testScenarioCount} = [");
                foreach (var currentTestScenarioRunResult in currentTestScenarioResults.TestScenarioRunResults.Values)
                {
                    foreach (var requestResults in currentTestScenarioRunResult.RequestResults)
                    {
                        foreach (var responseAssertionResult in requestResults.ResponseAssertionResults)
                        {
                            sb.AppendLine("{");
                            sb.AppendLine($" \"URL\": \"{HttpUtility.JavaScriptStringEncode(requestResults?.RequestUrl)}\",");
                            sb.AppendLine($" \"IsSuccessful\": \"{responseAssertionResult?.Passed}\",");
                            sb.AppendLine($" \"AssertionType\": \"{responseAssertionResult?.AssertionType}\",");
                            sb.AppendLine($" \"Exception\": \"{HttpUtility.JavaScriptStringEncode(responseAssertionResult?.FailedMessage)}\",");
                            sb.AppendLine("},");
                        }
                    }
                }

                sb.AppendLine("];");
                sb.AppendLine($"$(\"#jsGridAllAssertions{testScenarioCount}\").jsGrid({{");
                sb.AppendLine("width: \"100 % \",");
                sb.AppendLine("height: \"400px\",");
                sb.AppendLine("filtering: true,");
                sb.AppendLine("sorting: true,");
                sb.AppendLine("paging: true,");
                sb.AppendLine("autoload: true,");
                sb.AppendLine("pageSize: 5,");
                sb.AppendLine("pageButtonCount: 5,");
                sb.AppendLine($"controller: dballAssertions{testScenarioCount},");
                sb.AppendLine("fields: [{");
                sb.AppendLine(" name: \"URL\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" width: 250");
                sb.AppendLine("},");
                sb.AppendLine("{");
                sb.AppendLine(" name: \"IsSuccessful\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" width: 50");
                sb.AppendLine("},");
                sb.AppendLine("{");
                sb.AppendLine(" name: \"AssertionType\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" width: 150");
                sb.AppendLine("},");
                sb.AppendLine("{");
                sb.AppendLine(" name: \"Exception\",");
                sb.AppendLine(" type: \"text\",");
                sb.AppendLine(" title: \"Exception Message\",");
                sb.AppendLine(" width: 300");
                sb.AppendLine("}");
                sb.AppendLine("]");
                sb.AppendLine("});");

                testScenarioCount++;
            }
        }

        private void GenerateTestScenariosChartJS(LoadTestRunResults loadTestRunResults, StringBuilder sb)
        {
            int testScenarioCount = 1;
            sb.AppendLine("var ctx = document.getElementById(\"testScenariosExecutionTimesChart\").getContext('2d');");
            sb.AppendLine("var myChart = new Chart(ctx, {");
            sb.AppendLine("type: 'line',");
            sb.AppendLine("data: {");
            sb.Append("labels: [");
            int maxTestScenarioRunCount = 0;
            foreach (var currentTestScenarioResults in loadTestRunResults.TestScenarioResults.Values)
            {
                if (currentTestScenarioResults.TimesExecuted > maxTestScenarioRunCount)
                {
                    maxTestScenarioRunCount = currentTestScenarioResults.TimesExecuted;
                }
            }

            for (int i = 1; i < maxTestScenarioRunCount + 1; i++)
            {
                sb.Append($"\"{i}\",");
            }

            sb.AppendLine("],");
            sb.AppendLine("datasets: [");
            int colorIndex = 0;
            foreach (var currentTestScenarioResults in loadTestRunResults.TestScenarioResults.Values)
            {
                sb.AppendLine("{");
                sb.AppendLine($"label: '{currentTestScenarioResults.TestName}',");
                sb.Append($"data: [");
                foreach (var testScenarioRunResults in currentTestScenarioResults.TestScenarioRunResults.Values)
                {
                    sb.Append($"\"{testScenarioRunResults.ExecutionTime.TotalSeconds}\",");
                }

                sb.AppendLine("],");
                sb.AppendLine($"backgroundColor: 'rgba({_hexChartColors[colorIndex]}, 0.2)',");
                sb.AppendLine($"borderColor: 'rgba({_hexChartColors[colorIndex]}, 1)',");
                sb.AppendLine("fill: false,");
                sb.AppendLine("borderWidth: 2");
                sb.AppendLine(" },");

                colorIndex++;
            }

            sb.AppendLine("]");
            sb.AppendLine("},");
            sb.AppendLine("options: {");
            sb.AppendLine("scales: {");
            sb.AppendLine("yAxes: [{");
            sb.AppendLine("ticks: {");
            sb.AppendLine("beginAtZero: true");
            sb.AppendLine("}");
            sb.AppendLine("}]");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine(" });");
        }
    }
}