// <copyright file="LoadTestingValidateExtensionsService.cs" company="Automate The Planet Ltd.">
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
    public class LoadTestingValidateExtensionsService : ValidateExtensionsEventHandlers
    {
        protected override void ValidatedExceptionThrownEventHandler(object sender, ElementNotFulfillingValidateConditionEventArgs arg)
        {
        }

        protected override void ValidatedAltIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedAltIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedWrapIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedWrapIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedWidthIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedWidthIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedWeekIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsVisibleEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsNotVisibleEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedValueIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedValueIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedUrlIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTitleIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTitleIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTitleIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTimeIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTextIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTextContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTextNotContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTargetIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTargetIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTabIndexIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedTabIndexIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedStyleIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedStyleIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedStyleContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedStyleNotContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedStepIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedStepIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSrcSetIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSrcSetIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSrcIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSrcIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSrcIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSpellCheckIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSpellCheckIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSizesIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSizesIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSizeIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSizeIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsSelectedEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsNotSelectedEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedSearchIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedColsIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsRequiredEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsNotRequiredEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedRelIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedRelIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsReadonlyEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsNotReadonlyEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedRangeIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedPlaceholderIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedPlaceholderIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedPhoneIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedPasswordIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedNumberIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsMultipleEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsNotMultipleEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMonthIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMinTextIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMinTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMinLengthIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMinLengthIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMinIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMinIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMaxTextIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMaxTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMaxLengthIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMaxLengthIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMaxIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedMaxIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedLongDescIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedLongDescIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedListIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedListIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedLangIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedLangIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedInnerTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedInnerHtmlIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedInnerHtmlContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedHrefIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedHrefIsSetEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedHeightIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedHeightIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedForIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedForIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedEmailIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsDisabledEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsNotDisabledEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedDirIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedDirIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedDateIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedCssClassIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedCssClassIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedRowsIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedColorIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsCheckedEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedIsNotCheckedEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedAutoCompleteOnEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedAutoCompleteOffEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedAccessKeyIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedAccessKeyIsEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedAcceptIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
            AddResponseAssertionToHttpRequest(arg);
        }

        protected override void ValidatedValidateAcceptIsEventHandler(object sender, ElementActionEventArgs arg)
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
