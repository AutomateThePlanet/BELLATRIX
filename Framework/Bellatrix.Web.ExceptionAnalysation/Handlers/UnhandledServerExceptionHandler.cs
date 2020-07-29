// <copyright file="UnhandledServerExceptionHandler.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.ExceptionAnalysation.Pages;

namespace Bellatrix.Web.ExceptionAnalysation
{
    public class UnhandledServerExceptionHandler : HtmlSourceExceptionHandler
    {
        public override string DetailedIssueExplanation
        {
            get
            {
                var unhandledExceptionPage = new UnhandledExceptionPage();
                var result =
                    $"IT IS NOT A TEST PROBLEM. AN UNHANDLED EXCEPTION OCCURED.\n\nEXCEPTION MESSAGE:\n{unhandledExceptionPage.ExceptionMessage.InnerText}\n\nSTACK TRACE:\n{unhandledExceptionPage.ExceptionStackTrace.InnerText}";
                return result;
            }
        }

        protected override string TextToSearchInSource => "Server Error in '/";
    }
}