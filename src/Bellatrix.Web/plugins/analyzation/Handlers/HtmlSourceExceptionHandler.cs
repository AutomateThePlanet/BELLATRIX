// <copyright file="HtmlSourceExceptionHandler.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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
using System;
using Bellatrix.ExceptionAnalysation.Contracts;

namespace Bellatrix.Web.ExceptionAnalysation;

public abstract class HtmlSourceExceptionHandler : IExceptionAnalysationHandler
{
    public abstract string DetailedIssueExplanation { get; }

    protected abstract string TextToSearchInSource { get; }

    public bool IsApplicable(Exception ex = null, ServicesCollection container = null, params object[] context)
    {
        if (container == null)
        {
            container = ServicesCollection.Current;
        }

        BrowserService browserService = container.Resolve<BrowserService>();
        if (browserService == null)
        {
            throw new ArgumentNullException(nameof(BrowserService));
        }

        bool containsText = false;
        try
        {
            containsText = browserService.HtmlSource.Contains(TextToSearchInSource);

        }
        catch (Exception err)
        {
            Logger.LogError("Could not get HTML source: " + err.Message);
        }

        return containsText;
    }
}