// <copyright file="ValidateExtensionsEventHandlers.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
using Bellatrix.Web.Events;

namespace Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

public abstract class ValidateExtensionsEventHandlers
{
    public virtual void SubscribeToAll()
    {
        // TODO: Test with unit tests. Create elements and mock the result?
        ValidateControlExtensions.ValidatedAccessKeyIsNullEvent += ValidatedAccessKeyIsNullEventHandler;
        ValidateControlExtensions.ValidatedAccessKeyIsEvent += ValidatedAccessKeyIsEventHandler;
        ValidateControlExtensions.ValidatedAcceptIsNullEvent += ValidatedAcceptIsNullEventHandler;
        ValidateControlExtensions.ValidatedAcceptIsEvent += ValidatedValidateAcceptIsEventHandler;
        ValidateControlExtensions.ValidatedAutoCompleteOnEvent += ValidatedAutoCompleteOnEventHandler;
        ValidateControlExtensions.ValidatedAutoCompleteOffEvent += ValidatedAutoCompleteOffEventHandler;
        ValidateControlExtensions.ValidatedIsCheckedEvent += ValidatedIsCheckedEventHandler;
        ValidateControlExtensions.ValidatedIsNotCheckedEvent += ValidatedIsNotCheckedEventHandler;
        ValidateControlExtensions.ValidatedColorIsEvent += ValidatedColorIsEventHandler;
        ValidateControlExtensions.ValidatedRowsIsEvent += ValidatedRowsIsEventHandler;
        ValidateControlExtensions.ValidatedCssClassIsNullEvent += ValidatedCssClassIsNullEventHandler;
        ValidateControlExtensions.ValidatedCssClassIsEvent += ValidatedCssClassIsEventHandler;
        ValidateControlExtensions.ValidatedDateIsEvent += ValidatedDateIsEventHandler;
        ValidateControlExtensions.ValidatedDirIsNullEvent += ValidatedDirIsNullEventHandler;
        ValidateControlExtensions.ValidatedDirIsEvent += ValidatedDirIsEventHandler;
        ValidateControlExtensions.ValidatedIsDisabledEvent += ValidatedIsDisabledEventHandler;
        ValidateControlExtensions.ValidatedIsNotDisabledEvent += ValidatedIsNotDisabledEventHandler;
        ValidateControlExtensions.ValidatedEmailIsEvent += ValidatedEmailIsEventHandler;
        ValidateControlExtensions.ValidatedForIsNullEvent += ValidatedForIsNullEventHandler;
        ValidateControlExtensions.ValidatedForIsEvent += ValidatedForIsEventHandler;
        ValidateControlExtensions.ValidatedHeightIsNullEvent += ValidatedHeightIsNullEventHandler;
        ValidateControlExtensions.ValidatedHeightIsNotNullEvent += ValidatedHeightIsNotNullEventHandler;
        ValidateControlExtensions.ValidatedHrefIsEvent += ValidatedHrefIsEventHandler;
        ValidateControlExtensions.ValidatedHrefIsSetEvent += ValidatedHrefIsSetEventHandler;
        ValidateControlExtensions.ValidatedInnerHtmlIsEvent += ValidatedInnerHtmlIsEventHandler;
        ValidateControlExtensions.ValidatedInnerHtmlContainsEvent += ValidatedInnerHtmlContainsEventHandler;
        ValidateControlExtensions.ValidatedInnerTextIsEvent += ValidatedInnerTextIsEventHandler;
        ValidateControlExtensions.ValidatedLangIsNullEvent += ValidatedLangIsNullEventHandler;
        ValidateControlExtensions.ValidatedLangIsEvent += ValidatedLangIsEventHandler;
        ValidateControlExtensions.ValidatedLongDescIsNullEvent += ValidatedLongDescIsNullEventHandler;
        ValidateControlExtensions.ValidatedLongDescIsEvent += ValidatedLongDescIsEventHandler;
        ValidateControlExtensions.ValidatedMaxIsNullEvent += ValidatedMaxIsNullEventHandler;
        ValidateControlExtensions.ValidatedMaxIsEvent += ValidatedMaxIsEventHandler;
        ValidateControlExtensions.ValidatedMaxLengthIsNullEvent += ValidatedMaxLengthIsNullEventHandler;
        ValidateControlExtensions.ValidatedMaxLengthIsEvent += ValidatedMaxLengthIsEventHandler;
        ValidateControlExtensions.ValidatedMaxTextIsNullEvent += ValidatedMaxTextIsNullEventHandler;
        ValidateControlExtensions.ValidatedMaxTextIsEvent += ValidatedMaxTextIsEventHandler;
        ValidateControlExtensions.ValidatedMinIsNullEvent += ValidatedMinIsNullEventHandler;
        ValidateControlExtensions.ValidatedMinIsEvent += ValidatedMinIsEventHandler;
        ValidateControlExtensions.ValidatedMinLengthIsNullEvent += ValidatedMinLengthIsNullEventHandler;
        ValidateControlExtensions.ValidatedMinLengthIsEvent += ValidatedMinLengthIsEventHandler;
        ValidateControlExtensions.ValidatedMinTextIsNullEvent += ValidatedMinTextIsNullEventHandler;
        ValidateControlExtensions.ValidatedMinTextIsEvent += ValidatedMinTextIsEventHandler;
        ValidateControlExtensions.ValidatedMonthIsEvent += ValidatedMonthIsEventHandler;
        ValidateControlExtensions.ValidatedIsMultipleEvent += ValidatedIsMultipleEventHandler;
        ValidateControlExtensions.ValidatedIsNotMultipleEvent += ValidatedIsNotMultipleEventHandler;
        ValidateControlExtensions.ValidatedNumberIsEvent += ValidatedNumberIsEventHandler;
        ValidateControlExtensions.ValidatedPasswordIsEvent += ValidatedPasswordIsEventHandler;
        ValidateControlExtensions.ValidatedPhoneIsEvent += ValidatedPhoneIsEventHandler;
        ValidateControlExtensions.ValidatedPlaceholderIsNullEvent += ValidatedPlaceholderIsNullEventHandler;
        ValidateControlExtensions.ValidatedPlaceholderIsEvent += ValidatedPlaceholderIsEventHandler;
        ValidateControlExtensions.ValidatedRangeIsEvent += ValidatedRangeIsEventHandler;
        ValidateControlExtensions.ValidatedIsReadonlyEvent += ValidatedIsReadonlyEventHandler;
        ValidateControlExtensions.ValidatedIsNotReadonlyEvent += ValidatedIsNotReadonlyEventHandler;
        ValidateControlExtensions.ValidatedRelIsNullEvent += ValidatedRelIsNullEventHandler;
        ValidateControlExtensions.ValidatedRelIsEvent += ValidatedRelIsEventHandler;
        ValidateControlExtensions.ValidatedIsRequiredEvent += ValidatedIsRequiredEventHandler;
        ValidateControlExtensions.ValidatedIsNotRequiredEvent += ValidatedIsNotRequiredEventHandler;
        ValidateControlExtensions.ValidatedColsIsEvent += ValidatedColsIsEventHandler;
        ValidateControlExtensions.ValidatedSearchIsEvent += ValidatedSearchIsEventHandler;
        ValidateControlExtensions.ValidatedIsSelectedEvent += ValidatedIsSelectedEventHandler;
        ValidateControlExtensions.ValidatedIsNotSelectedEvent += ValidatedIsNotSelectedEventHandler;
        ValidateControlExtensions.ValidatedSizeIsNullEvent += ValidatedSizeIsNullEventHandler;
        ValidateControlExtensions.ValidatedSizeIsEvent += ValidatedSizeIsEventHandler;
        ValidateControlExtensions.ValidatedSizesIsNullEvent += ValidatedSizesIsNullEventHandler;
        ValidateControlExtensions.ValidatedSizesIsEvent += ValidatedSizesIsEventHandler;
        ValidateControlExtensions.ValidatedSpellCheckIsNullEvent += ValidatedSpellCheckIsNullEventHandler;
        ValidateControlExtensions.ValidatedSpellCheckIsEvent += ValidatedSpellCheckIsEventHandler;
        ValidateControlExtensions.ValidatedSrcIsNullEvent += ValidatedSrcIsNullEventHandler;
        ValidateControlExtensions.ValidatedSrcIsNotNullEvent += ValidatedSrcIsNotNullEventHandler;
        ValidateControlExtensions.ValidatedSrcIsEvent += ValidatedSrcIsEventHandler;
        ValidateControlExtensions.ValidatedSrcSetIsNullEvent += ValidatedSrcSetIsNullEventHandler;
        ValidateControlExtensions.ValidatedSrcSetIsEvent += ValidatedSrcSetIsEventHandler;
        ValidateControlExtensions.ValidatedStepIsNullEvent += ValidatedStepIsNullEventHandler;
        ValidateControlExtensions.ValidatedStepIsEvent += ValidatedStepIsEventHandler;
        ValidateControlExtensions.ValidatedStyleIsNullEvent += ValidatedStyleIsNullEventHandler;
        ValidateControlExtensions.ValidatedStyleIsEvent += ValidatedStyleIsEventHandler;
        ValidateControlExtensions.ValidatedStyleContainsEvent += ValidatedStyleContainsEventHandler;
        ValidateControlExtensions.ValidatedStyleNotContainsEvent += ValidatedStyleNotContainsEventHandler;
        ValidateControlExtensions.ValidatedTabIndexIsNullEvent += ValidatedTabIndexIsNullEventHandler;
        ValidateControlExtensions.ValidatedTabIndexIsEvent += ValidatedTabIndexIsEventHandler;
        ValidateControlExtensions.ValidatedTargetIsNullEvent += ValidatedTargetIsNullEventHandler;
        ValidateControlExtensions.ValidatedTargetIsEvent += ValidatedTargetIsEventHandler;
        ValidateControlExtensions.ValidatedTextIsNullEvent += ValidatedTextIsNullEventHandler;
        ValidateControlExtensions.ValidatedTextIsEvent += ValidatedTextIsEventHandler;
        ValidateControlExtensions.ValidatedTextContainsEvent += ValidatedTextContainsEventHandler;
        ValidateControlExtensions.ValidatedTextNotContainsEvent += ValidatedTextNotContainsEventHandler;
        ValidateControlExtensions.ValidatedTimeIsEvent += ValidatedTimeIsEventHandler;
        ValidateControlExtensions.ValidatedTitleIsNullEvent += ValidatedTitleIsNullEventHandler;
        ValidateControlExtensions.ValidatedTitleIsNotNullEvent += ValidatedTitleIsNotNullEventHandler;
        ValidateControlExtensions.ValidatedTitleIsEvent += ValidatedTitleIsEventHandler;
        ValidateControlExtensions.ValidatedUrlIsEvent += ValidatedUrlIsEventHandler;
        ValidateControlExtensions.ValidatedValueIsNullEvent += ValidatedValueIsNullEventHandler;
        ValidateControlExtensions.ValidatedValueIsEvent += ValidatedValueIsEventHandler;
        ValidateControlExtensions.ValidatedIsVisibleEvent += ValidatedIsVisibleEventHandler;
        ValidateControlExtensions.ValidatedIsNotVisibleEvent += ValidatedIsNotVisibleEventHandler;
        ValidateControlExtensions.ValidatedWeekIsEvent += ValidatedWeekIsEventHandler;
        ValidateControlExtensions.ValidatedWidthIsNullEvent += ValidatedWidthIsNullEventHandler;
        ValidateControlExtensions.ValidatedWidthIsNotNullEvent += ValidatedWidthIsNotNullEventHandler;
        ValidateControlExtensions.ValidatedWrapIsNullEvent += ValidatedWrapIsNullEventHandler;
        ValidateControlExtensions.ValidatedWrapIsEvent += ValidatedWrapIsEventHandler;
        ValidateControlExtensions.ValidatedExceptionThrowedEvent += ValidatedExceptionThrownEventHandler;
        ValidateControlExtensions.ValidatedListIsEvent += ValidatedListIsEventHandler;
        ValidateControlExtensions.ValidatedListIsNullEvent += ValidatedListIsNullEventHandler;
        ValidateControlExtensions.ValidatedAltIsNullEvent += ValidatedAltIsNullEventHandler;
        ValidateControlExtensions.ValidatedAltIsEvent += ValidatedAltIsEventHandler;
    }

    protected virtual void ValidatedExceptionThrownEventHandler(object sender, ElementNotFulfillingValidateConditionEventArgs arg)
    {
    }

    protected virtual void ValidatedAltIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedAltIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedWrapIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedWrapIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedWidthIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedWidthIsNotNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedWeekIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsVisibleEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsNotVisibleEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedValueIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedValueIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedUrlIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTitleIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTitleIsNotNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTitleIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTimeIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTextIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTextIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTextContainsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTextNotContainsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTargetIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTargetIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTabIndexIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedTabIndexIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedStyleIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedStyleIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedStyleContainsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedStyleNotContainsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedStepIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedStepIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSrcSetIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSrcSetIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSrcIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSrcIsNotNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSrcIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSpellCheckIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSpellCheckIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSizesIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSizesIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSizeIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSizeIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsSelectedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsNotSelectedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedSearchIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedColsIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsRequiredEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsNotRequiredEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedRelIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedRelIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsReadonlyEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsNotReadonlyEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedRangeIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedPlaceholderIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedPlaceholderIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedPhoneIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedPasswordIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedNumberIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsMultipleEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsNotMultipleEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMonthIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMinTextIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMinTextIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMinLengthIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMinLengthIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMinIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMinIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMaxTextIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMaxTextIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMaxLengthIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMaxLengthIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMaxIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedMaxIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedLongDescIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedLongDescIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedListIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedListIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedLangIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedLangIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedInnerTextIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedInnerHtmlIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedInnerHtmlContainsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedHrefIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedHrefIsSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedHeightIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedHeightIsNotNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedForIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedForIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedEmailIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsDisabledEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsNotDisabledEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedDirIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedDirIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedDateIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedCssClassIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedCssClassIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedRowsIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedColorIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsCheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedIsNotCheckedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedAutoCompleteOnEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedAutoCompleteOffEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedAccessKeyIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedAccessKeyIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedAcceptIsNullEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ValidatedValidateAcceptIsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}