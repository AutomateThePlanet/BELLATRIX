// <copyright file="LoadTestReportGenerator.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using Bellatrix.LoadTesting.Model.Results;

namespace Bellatrix.LoadTesting.Report
{
    public class LoadTestReportGenerator
    {
        public void GenerateReport(LoadTestRunResults loadTestRunResults, string reportPath)
        {
            string htmlReportContent = GenerateHtml(loadTestRunResults);
            File.WriteAllText(reportPath, htmlReportContent);
        }

        private string GenerateHtml(LoadTestRunResults loadTestRunResults)
        {
            var testScenarioHtmlReportGenerator = new TestScenarioHtmlReportGenerator();
            var testScenarioJavaScriptReportGenerator = new TestScenarioJavaScriptReportGenerator();
            string htmlTemplate = File.ReadAllText("reportTemplate.html");
            string htmlPart = testScenarioHtmlReportGenerator.GenerateHtml(loadTestRunResults);
            string jsPart = testScenarioJavaScriptReportGenerator.GenerateJavaScript(loadTestRunResults);
            htmlTemplate = htmlTemplate.Replace("#htmlPlaceHolder#", htmlPart);
            htmlTemplate = htmlTemplate.Replace("#javaScriptPlaceHolder#", jsPart);
            return htmlTemplate;
        }
    }
}
