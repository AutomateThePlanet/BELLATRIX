// <copyright file="UrlExceptionHandler.cs" company="Automate The Planet Ltd.">
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
using System;
using Bellatrix.ExceptionAnalysation.Contracts;

namespace Bellatrix.Web.ExceptionAnalysation;

public abstract class UrlExceptionHandler : IExceptionAnalysationHandler
{
    public abstract string DetailedIssueExplanation { get; }

    protected abstract string TextToSearchInUrl { get; }

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

        var result = browserService.Url.AbsoluteUri.Contains(TextToSearchInUrl);
        return result;
    }
}