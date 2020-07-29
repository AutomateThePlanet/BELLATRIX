// <copyright file="HtmlSourceExceptionHandler.cs" company="Automate The Planet Ltd.">
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
using System;
using Bellatrix.ExceptionAnalysation.Contracts;

namespace Bellatrix.Web.ExceptionAnalysation
{
    public abstract class HtmlSourceExceptionHandler : IExceptionAnalysationHandler
    {
        public abstract string DetailedIssueExplanation { get; }

        protected abstract string TextToSearchInSource { get; }

        public bool IsApplicable(Exception ex = null, params object[] context)
        {
            var browser = ServicesCollection.Current.Resolve<BrowserService>();
            if (browser == null)
            {
                throw new ArgumentNullException(nameof(BrowserService));
            }

            var result = browser.HtmlSource.Contains(TextToSearchInSource);
            return result;
        }
    }
}