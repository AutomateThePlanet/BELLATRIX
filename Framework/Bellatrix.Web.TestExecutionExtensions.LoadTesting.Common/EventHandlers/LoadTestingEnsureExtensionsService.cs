// <copyright file="LoadTestingEnsureExtensionsService.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Linq;
using Bellatrix.Web.Events;
using Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

namespace Bellatrix.Web.TestExecutionExtensions.LoadTesting
{
    public class LoadTestingEnsureExtensionsService : EnsureExtensionsEventHandlers
    {
        protected override void EnsuredExceptionThrownEventHandler(object sender, ElementNotFulfillingEnsureConditionEventArgs arg)
        {
        }

        protected override void EnsuredAltIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredAltIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredWrapIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredWrapIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredWidthIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredWidthIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredWeekIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsVisibleEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsNotVisibleEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredValueIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredValueIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredUrlIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTitleIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTitleIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTitleIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTimeIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTextIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTextContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTextNotContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTargetIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTargetIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTabIndexIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredTabIndexIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredStyleIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredStyleIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredStyleContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredStyleNotContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredStepIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredStepIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSrcSetIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSrcSetIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSrcIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSrcIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSrcIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSpellCheckIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSpellCheckIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSizesIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSizesIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSizeIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSizeIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsSelectedEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsNotSelectedEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredSearchIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredColsIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsRequiredEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsNotRequiredEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredRelIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredRelIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsReadonlyEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsNotReadonlyEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredRangeIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredPlaceholderIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredPlaceholderIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredPhoneIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredPasswordIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredNumberIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsMultipleEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsNotMultipleEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMonthIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMinTextIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMinTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMinLengthIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMinLengthIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMinIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMinIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMaxTextIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMaxTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMaxLengthIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMaxLengthIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMaxIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredMaxIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredLongDescIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredLongDescIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredListIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredListIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredLangIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredLangIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredInnerTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredInnerHtmlIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredInnerHtmlContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredHrefIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredHrefIsSetEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredHeightIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredHeightIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredForIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredForIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredEmailIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsDisabledEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsNotDisabledEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredDirIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredDirIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredDateIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredCssClassIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredCssClassIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredRowsIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredColorIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsCheckedEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredIsNotCheckedEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredAutoCompleteOnEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredAutoCompleteOffEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredAccessKeyIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredAccessKeyIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredAcceptIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void EnsuredEnsureAcceptIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        private static void AddResponseAssertionToHttpRequest(ElementActionEventArgs arg)
        {
            var loadTestingWorkflowPluginContext = ServicesCollection.Current.Resolve<LoadTestingWorkflowPluginContext>();
            if (loadTestingWorkflowPluginContext.IsLoadTestingEnabled
                && !string.IsNullOrEmpty(loadTestingWorkflowPluginContext.CurrentTestName)
                && loadTestingWorkflowPluginContext.HttpRequestsPerTest != null
                && loadTestingWorkflowPluginContext.HttpRequestsPerTest.ContainsKey(loadTestingWorkflowPluginContext.CurrentTestName))
            {
                var currentHttpRequests = loadTestingWorkflowPluginContext.HttpRequestsPerTest[loadTestingWorkflowPluginContext.CurrentTestName].OrderBy(x => x.CreationTime);
                var httpRequest = currentHttpRequests.LastOrDefault(x => x.Headers.Any(y => y.Contains("text/html")));
                if (httpRequest != null)
                {
                    if (httpRequest.Headers.Any(x => x.Contains("Referer")))
                    {
                        string refererHeader = httpRequest.Headers.FirstOrDefault(x => x.Contains("Referer"));
                        string refererUrl = refererHeader?.Trim().Split(' ').LastOrDefault()?.Trim();
                        if (!string.IsNullOrEmpty(refererUrl))
                        {
                            var refererHttpRequest = currentHttpRequests.LastOrDefault(x => x.Headers.Any(y => y.Contains("text/html")) && x.Url.Equals(refererUrl));
                            if (refererHttpRequest != null)
                            {
                                httpRequest = refererHttpRequest;
                            }
                        }
                    }

                    var st = new StackTrace();
                    var sf = st.GetFrame(1);
                    var currentMethodName = sf.GetMethod();

                    var responseAssert = new ResponseAssertion
                    {
                        AssertionType = currentMethodName.Name.Replace("EventHandler", string.Empty),
                        ExpectedValue = arg.ActionValue,
                        Locator = arg.Element.LocatorType.FullName,
                        LocatorValue = arg.Element.LocatorValue,
                    };
                    httpRequest.ResponseAssertions.Add(responseAssert);
                }
            }
        }
    }
}
