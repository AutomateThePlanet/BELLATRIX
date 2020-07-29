// <copyright file="EnsureExtensionsEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Logging;
using Bellatrix.Web.Events;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers
{
    public abstract class EnsureExtensionsEventHandlers
    {
        protected IBellaLogger Logger => ServicesCollection.Current.Resolve<IBellaLogger>();

        public virtual void SubscribeToAll()
        {
            // TODO: Test with unit tests. Create elements and mock the result?
            EnsureControlExtensions.EnsuredAccessKeyIsNullEvent += EnsuredAccessKeyIsNullEventHandler;
            EnsureControlExtensions.EnsuredAccessKeyIsEvent += EnsuredAccessKeyIsEventHandler;
            EnsureControlExtensions.EnsuredAcceptIsNullEvent += EnsuredAcceptIsNullEventHandler;
            EnsureControlExtensions.EnsuredAcceptIsEvent += EnsuredEnsureAcceptIsEventHandler;
            EnsureControlExtensions.EnsuredAutoCompleteOnEvent += EnsuredAutoCompleteOnEventHandler;
            EnsureControlExtensions.EnsuredAutoCompleteOffEvent += EnsuredAutoCompleteOffEventHandler;
            EnsureControlExtensions.EnsuredIsCheckedEvent += EnsuredIsCheckedEventHandler;
            EnsureControlExtensions.EnsuredIsNotCheckedEvent += EnsuredIsNotCheckedEventHandler;
            EnsureControlExtensions.EnsuredColorIsEvent += EnsuredColorIsEventHandler;
            EnsureControlExtensions.EnsuredRowsIsEvent += EnsuredRowsIsEventHandler;
            EnsureControlExtensions.EnsuredCssClassIsNullEvent += EnsuredCssClassIsNullEventHandler;
            EnsureControlExtensions.EnsuredCssClassIsEvent += EnsuredCssClassIsEventHandler;
            EnsureControlExtensions.EnsuredDateIsEvent += EnsuredDateIsEventHandler;
            EnsureControlExtensions.EnsuredDirIsNullEvent += EnsuredDirIsNullEventHandler;
            EnsureControlExtensions.EnsuredDirIsEvent += EnsuredDirIsEventHandler;
            EnsureControlExtensions.EnsuredIsDisabledEvent += EnsuredIsDisabledEventHandler;
            EnsureControlExtensions.EnsuredIsNotDisabledEvent += EnsuredIsNotDisabledEventHandler;
            EnsureControlExtensions.EnsuredEmailIsEvent += EnsuredEmailIsEventHandler;
            EnsureControlExtensions.EnsuredForIsNullEvent += EnsuredForIsNullEventHandler;
            EnsureControlExtensions.EnsuredForIsEvent += EnsuredForIsEventHandler;
            EnsureControlExtensions.EnsuredHeightIsNullEvent += EnsuredHeightIsNullEventHandler;
            EnsureControlExtensions.EnsuredHeightIsNotNullEvent += EnsuredHeightIsNotNullEventHandler;
            EnsureControlExtensions.EnsuredHrefIsEvent += EnsuredHrefIsEventHandler;
            EnsureControlExtensions.EnsuredHrefIsSetEvent += EnsuredHrefIsSetEventHandler;
            EnsureControlExtensions.EnsuredInnerHtmlIsEvent += EnsuredInnerHtmlIsEventHandler;
            EnsureControlExtensions.EnsuredInnerHtmlContainsEvent += EnsuredInnerHtmlContainsEventHandler;
            EnsureControlExtensions.EnsuredInnerTextIsEvent += EnsuredInnerTextIsEventHandler;
            EnsureControlExtensions.EnsuredLangIsNullEvent += EnsuredLangIsNullEventHandler;
            EnsureControlExtensions.EnsuredLangIsEvent += EnsuredLangIsEventHandler;
            EnsureControlExtensions.EnsuredLongDescIsNullEvent += EnsuredLongDescIsNullEventHandler;
            EnsureControlExtensions.EnsuredLongDescIsEvent += EnsuredLongDescIsEventHandler;
            EnsureControlExtensions.EnsuredMaxIsNullEvent += EnsuredMaxIsNullEventHandler;
            EnsureControlExtensions.EnsuredMaxIsEvent += EnsuredMaxIsEventHandler;
            EnsureControlExtensions.EnsuredMaxLengthIsNullEvent += EnsuredMaxLengthIsNullEventHandler;
            EnsureControlExtensions.EnsuredMaxLengthIsEvent += EnsuredMaxLengthIsEventHandler;
            EnsureControlExtensions.EnsuredMaxTextIsNullEvent += EnsuredMaxTextIsNullEventHandler;
            EnsureControlExtensions.EnsuredMaxTextIsEvent += EnsuredMaxTextIsEventHandler;
            EnsureControlExtensions.EnsuredMinIsNullEvent += EnsuredMinIsNullEventHandler;
            EnsureControlExtensions.EnsuredMinIsEvent += EnsuredMinIsEventHandler;
            EnsureControlExtensions.EnsuredMinLengthIsNullEvent += EnsuredMinLengthIsNullEventHandler;
            EnsureControlExtensions.EnsuredMinLengthIsEvent += EnsuredMinLengthIsEventHandler;
            EnsureControlExtensions.EnsuredMinTextIsNullEvent += EnsuredMinTextIsNullEventHandler;
            EnsureControlExtensions.EnsuredMinTextIsEvent += EnsuredMinTextIsEventHandler;
            EnsureControlExtensions.EnsuredMonthIsEvent += EnsuredMonthIsEventHandler;
            EnsureControlExtensions.EnsuredIsMultipleEvent += EnsuredIsMultipleEventHandler;
            EnsureControlExtensions.EnsuredIsNotMultipleEvent += EnsuredIsNotMultipleEventHandler;
            EnsureControlExtensions.EnsuredNumberIsEvent += EnsuredNumberIsEventHandler;
            EnsureControlExtensions.EnsuredPasswordIsEvent += EnsuredPasswordIsEventHandler;
            EnsureControlExtensions.EnsuredPhoneIsEvent += EnsuredPhoneIsEventHandler;
            EnsureControlExtensions.EnsuredPlaceholderIsNullEvent += EnsuredPlaceholderIsNullEventHandler;
            EnsureControlExtensions.EnsuredPlaceholderIsEvent += EnsuredPlaceholderIsEventHandler;
            EnsureControlExtensions.EnsuredRangeIsEvent += EnsuredRangeIsEventHandler;
            EnsureControlExtensions.EnsuredIsReadonlyEvent += EnsuredIsReadonlyEventHandler;
            EnsureControlExtensions.EnsuredIsNotReadonlyEvent += EnsuredIsNotReadonlyEventHandler;
            EnsureControlExtensions.EnsuredRelIsNullEvent += EnsuredRelIsNullEventHandler;
            EnsureControlExtensions.EnsuredRelIsEvent += EnsuredRelIsEventHandler;
            EnsureControlExtensions.EnsuredIsRequiredEvent += EnsuredIsRequiredEventHandler;
            EnsureControlExtensions.EnsuredIsNotRequiredEvent += EnsuredIsNotRequiredEventHandler;
            EnsureControlExtensions.EnsuredColsIsEvent += EnsuredColsIsEventHandler;
            EnsureControlExtensions.EnsuredSearchIsEvent += EnsuredSearchIsEventHandler;
            EnsureControlExtensions.EnsuredIsSelectedEvent += EnsuredIsSelectedEventHandler;
            EnsureControlExtensions.EnsuredIsNotSelectedEvent += EnsuredIsNotSelectedEventHandler;
            EnsureControlExtensions.EnsuredSizeIsNullEvent += EnsuredSizeIsNullEventHandler;
            EnsureControlExtensions.EnsuredSizeIsEvent += EnsuredSizeIsEventHandler;
            EnsureControlExtensions.EnsuredSizesIsNullEvent += EnsuredSizesIsNullEventHandler;
            EnsureControlExtensions.EnsuredSizesIsEvent += EnsuredSizesIsEventHandler;
            EnsureControlExtensions.EnsuredSpellCheckIsNullEvent += EnsuredSpellCheckIsNullEventHandler;
            EnsureControlExtensions.EnsuredSpellCheckIsEvent += EnsuredSpellCheckIsEventHandler;
            EnsureControlExtensions.EnsuredSrcIsNullEvent += EnsuredSrcIsNullEventHandler;
            EnsureControlExtensions.EnsuredSrcIsNotNullEvent += EnsuredSrcIsNotNullEventHandler;
            EnsureControlExtensions.EnsuredSrcIsEvent += EnsuredSrcIsEventHandler;
            EnsureControlExtensions.EnsuredSrcSetIsNullEvent += EnsuredSrcSetIsNullEventHandler;
            EnsureControlExtensions.EnsuredSrcSetIsEvent += EnsuredSrcSetIsEventHandler;
            EnsureControlExtensions.EnsuredStepIsNullEvent += EnsuredStepIsNullEventHandler;
            EnsureControlExtensions.EnsuredStepIsEvent += EnsuredStepIsEventHandler;
            EnsureControlExtensions.EnsuredStyleIsNullEvent += EnsuredStyleIsNullEventHandler;
            EnsureControlExtensions.EnsuredStyleIsEvent += EnsuredStyleIsEventHandler;
            EnsureControlExtensions.EnsuredStyleContainsEvent += EnsuredStyleContainsEventHandler;
            EnsureControlExtensions.EnsuredStyleNotContainsEvent += EnsuredStyleNotContainsEventHandler;
            EnsureControlExtensions.EnsuredTabIndexIsNullEvent += EnsuredTabIndexIsNullEventHandler;
            EnsureControlExtensions.EnsuredTabIndexIsEvent += EnsuredTabIndexIsEventHandler;
            EnsureControlExtensions.EnsuredTargetIsNullEvent += EnsuredTargetIsNullEventHandler;
            EnsureControlExtensions.EnsuredTargetIsEvent += EnsuredTargetIsEventHandler;
            EnsureControlExtensions.EnsuredTextIsNullEvent += EnsuredTextIsNullEventHandler;
            EnsureControlExtensions.EnsuredTextIsEvent += EnsuredTextIsEventHandler;
            EnsureControlExtensions.EnsuredTextContainsEvent += EnsuredTextContainsEventHandler;
            EnsureControlExtensions.EnsuredTextNotContainsEvent += EnsuredTextNotContainsEventHandler;
            EnsureControlExtensions.EnsuredTimeIsEvent += EnsuredTimeIsEventHandler;
            EnsureControlExtensions.EnsuredTitleIsNullEvent += EnsuredTitleIsNullEventHandler;
            EnsureControlExtensions.EnsuredTitleIsNotNullEvent += EnsuredTitleIsNotNullEventHandler;
            EnsureControlExtensions.EnsuredTitleIsEvent += EnsuredTitleIsEventHandler;
            EnsureControlExtensions.EnsuredUrlIsEvent += EnsuredUrlIsEventHandler;
            EnsureControlExtensions.EnsuredValueIsNullEvent += EnsuredValueIsNullEventHandler;
            EnsureControlExtensions.EnsuredValueIsEvent += EnsuredValueIsEventHandler;
            EnsureControlExtensions.EnsuredIsVisibleEvent += EnsuredIsVisibleEventHandler;
            EnsureControlExtensions.EnsuredIsNotVisibleEvent += EnsuredIsNotVisibleEventHandler;
            EnsureControlExtensions.EnsuredWeekIsEvent += EnsuredWeekIsEventHandler;
            EnsureControlExtensions.EnsuredWidthIsNullEvent += EnsuredWidthIsNullEventHandler;
            EnsureControlExtensions.EnsuredWidthIsNotNullEvent += EnsuredWidthIsNotNullEventHandler;
            EnsureControlExtensions.EnsuredWrapIsNullEvent += EnsuredWrapIsNullEventHandler;
            EnsureControlExtensions.EnsuredWrapIsEvent += EnsuredWrapIsEventHandler;
            EnsureControlExtensions.EnsuredExceptionThrowedEvent += EnsuredExceptionThrownEventHandler;
            EnsureControlExtensions.EnsuredListIsEvent += EnsuredListIsEventHandler;
            EnsureControlExtensions.EnsuredListIsNullEvent += EnsuredListIsNullEventHandler;
            EnsureControlExtensions.EnsuredAltIsNullEvent += EnsuredAltIsNullEventHandler;
            EnsureControlExtensions.EnsuredAltIsEvent += EnsuredAltIsEventHandler;
        }

        protected virtual void EnsuredExceptionThrownEventHandler(object sender, ElementNotFulfillingEnsureConditionEventArgs arg)
        {
        }

        protected virtual void EnsuredAltIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredAltIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredWrapIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredWrapIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredWidthIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredWidthIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredWeekIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsVisibleEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsNotVisibleEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredValueIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredValueIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredUrlIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTitleIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTitleIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTitleIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTimeIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTextIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTextContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTextNotContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTargetIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTargetIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTabIndexIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredTabIndexIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredStyleIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredStyleIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredStyleContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredStyleNotContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredStepIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredStepIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSrcSetIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSrcSetIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSrcIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSrcIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSrcIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSpellCheckIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSpellCheckIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSizesIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSizesIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSizeIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSizeIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsSelectedEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsNotSelectedEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredSearchIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredColsIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsRequiredEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsNotRequiredEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredRelIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredRelIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsReadonlyEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsNotReadonlyEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredRangeIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredPlaceholderIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredPlaceholderIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredPhoneIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredPasswordIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredNumberIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsMultipleEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsNotMultipleEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMonthIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMinTextIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMinTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMinLengthIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMinLengthIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMinIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMinIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMaxTextIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMaxTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMaxLengthIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMaxLengthIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMaxIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredMaxIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredLongDescIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredLongDescIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredListIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredListIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredLangIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredLangIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredInnerTextIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredInnerHtmlIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredInnerHtmlContainsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredHrefIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredHrefIsSetEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredHeightIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredHeightIsNotNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredForIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredForIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredEmailIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsDisabledEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsNotDisabledEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredDirIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredDirIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredDateIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredCssClassIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredCssClassIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredRowsIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredColorIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsCheckedEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredIsNotCheckedEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredAutoCompleteOnEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredAutoCompleteOffEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredAccessKeyIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredAccessKeyIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredAcceptIsNullEventHandler(object sender, ElementActionEventArgs arg)
        {
        }

        protected virtual void EnsuredEnsureAcceptIsEventHandler(object sender, ElementActionEventArgs arg)
        {
        }
    }
}