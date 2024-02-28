// <copyright file="CustomHtmlExceptionHandler.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
// Licensed under the Royalty-free End-user License Agreement, Version 1.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://bellatrix.solutions/licensing-royalty-free/
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
namespace Bellatrix.Web.ExceptionAnalysation;

public class CustomHtmlExceptionHandler : HtmlSourceExceptionHandler
{
    public CustomHtmlExceptionHandler(string textToSearchInSource, string detailedIssueExplanation)
    {
        TextToSearchInSource = textToSearchInSource;
        DetailedIssueExplanation = detailedIssueExplanation;
    }

    public CustomHtmlExceptionHandler()
    {
    }

    public override string DetailedIssueExplanation { get; }

    protected override string TextToSearchInSource { get; }
}