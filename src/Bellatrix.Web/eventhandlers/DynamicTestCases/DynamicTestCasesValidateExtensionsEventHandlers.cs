// <copyright file="DynamicTestCasesValidateExtensionsEventHandlers.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System.Collections.Generic;
using System.Text;
using Bellatrix.DynamicTestCases;
using Bellatrix.Web.Events;
using Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

namespace Bellatrix.Web.EventHandlers.DynamicTestCases
{
    public class DynamicTestCasesValidateExtensionsEventHandlers : ValidateExtensionsEventHandlers
    {
        protected DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

        protected override void ValidatedExceptionThrownEventHandler(object sender, ElementNotFulfillingValidateConditionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert Exception was thrown ", arg.Exception.Message);

        protected override void ValidatedAltIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} alt ", $"is NULL");

        protected override void ValidatedAltIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} alt ", $"is '{arg.ActionValue}'");

        protected override void ValidatedWrapIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} wrap ", $"is NULL");

        protected override void ValidatedWrapIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} wrap ", $"is '{arg.ActionValue}'");

        protected override void ValidatedWidthIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is null");

        protected override void ValidatedWidthIsNotNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is not NULL");

        protected override void ValidatedWeekIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} week ", $"is '{arg.ActionValue}'");

        protected override void ValidatedIsVisibleEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is visible");

        protected override void ValidatedIsNotVisibleEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT visible");

        protected override void ValidatedValueIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} value ", $"is NULL");

        protected override void ValidatedValueIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} value ", $"is '{arg.ActionValue}'");

        protected override void ValidatedUrlIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} url ", $"is '{arg.ActionValue}'");

        protected override void ValidatedTitleIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} title ", $"is NULL");

        protected override void ValidatedTitleIsNotNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} title ", $"is not NULL");

        protected override void ValidatedTitleIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} title ", $"is '{arg.ActionValue}'");

        protected override void ValidatedTimeIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} time ", $"is '{arg.ActionValue}'");

        protected override void ValidatedTextIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} text ", $"is NULL");

        protected override void ValidatedTextIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} text ", $"is '{arg.ActionValue}'");

        protected override void ValidatedTextContainsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} text ", $"contains '{arg.ActionValue}'");

        protected override void ValidatedTextNotContainsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} text ", $"doesn't contain '{arg.ActionValue}'");

        protected override void ValidatedTargetIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} target ", $"is NULL");

        protected override void ValidatedTargetIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} target ", $"is '{arg.ActionValue}'");

        protected override void ValidatedTabIndexIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} tabindex ", $"is NULL");

        protected override void ValidatedTabIndexIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} tabindex ", $"is '{arg.ActionValue}'");

        protected override void ValidatedStyleIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} style ", $"is NULL");

        protected override void ValidatedStyleIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} style ", $"is '{arg.ActionValue}'");

        protected override void ValidatedStyleContainsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} style", $"contains '{arg.ActionValue}'");

        protected override void ValidatedStyleNotContainsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} style ", $"don't contain '{arg.ActionValue}'");

        protected override void ValidatedStepIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} step ", $"is NULL");

        protected override void ValidatedStepIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} step ", $"is '{arg.ActionValue}'");

        protected override void ValidatedSrcSetIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} srcset ", $"is NULL");

        protected override void ValidatedSrcSetIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} srcset ", $"is '{arg.ActionValue}'");

        protected override void ValidatedSrcIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} src ", $"is NULL");

        protected override void ValidatedSrcIsNotNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} src ", $"is not NULL");

        protected override void ValidatedSrcIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} src ", $"is '{arg.ActionValue}'");

        protected override void ValidatedSpellCheckIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} spellcheck ", $"is NULL");

        protected override void ValidatedSpellCheckIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} spellcheck ", $"is '{arg.ActionValue}'");

        protected override void ValidatedSizesIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} sizes ", $"is NULL");

        protected override void ValidatedSizesIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} sizes ", $"is '{arg.ActionValue}'");

        protected override void ValidatedSizeIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} size ", $"is NULL");

        protected override void ValidatedSizeIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} size ", $"is '{arg.ActionValue}'");

        protected override void ValidatedIsSelectedEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is selected");

        protected override void ValidatedIsNotSelectedEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT selected");

        protected override void ValidatedSearchIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} search ", $"is '{arg.ActionValue}'");

        protected override void ValidatedColsIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} cols ", $"is '{arg.ActionValue}'");

        protected override void ValidatedIsRequiredEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is required");

        protected override void ValidatedIsNotRequiredEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT required");

        protected override void ValidatedRelIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} rel ", $"is NULL");

        protected override void ValidatedRelIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} rel ", $"is '{arg.ActionValue}'");

        protected override void ValidatedIsReadonlyEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is readonly");

        protected override void ValidatedIsNotReadonlyEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT readonly");

        protected override void ValidatedRangeIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} range ", $"is '{arg.ActionValue}'");

        protected override void ValidatedPlaceholderIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} placeholder ", $"is NULL");

        protected override void ValidatedPlaceholderIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} placeholder ", $"is '{arg.ActionValue}'");

        protected override void ValidatedPhoneIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} phone ", $"is '{arg.ActionValue}'");

        protected override void ValidatedPasswordIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} password ", $"is '{arg.ActionValue}'");

        protected override void ValidatedNumberIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} number ", $"is '{arg.ActionValue}'");

        protected override void ValidatedIsMultipleEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is multiple");

        protected override void ValidatedIsNotMultipleEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT multiple");

        protected override void ValidatedMonthIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} month ", $"is '{arg.ActionValue}'");

        protected override void ValidatedMinTextIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} min ", $"is NULL");

        protected override void ValidatedMinTextIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} min ", $"is '{arg.ActionValue}'");

        protected override void ValidatedMinLengthIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} minlength ", $"is NULL");

        protected override void ValidatedMinLengthIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} minlength ", $"is '{arg.ActionValue}'");

        protected override void ValidatedMinIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} min ", $"is NULL");

        protected override void ValidatedMinIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} min ", $"is '{arg.ActionValue}'");

        protected override void ValidatedMaxTextIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} max ", $"is NULL");

        protected override void ValidatedMaxTextIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} max ", $"is '{arg.ActionValue}'");

        protected override void ValidatedMaxLengthIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} maxlength ", $"is NULL");

        protected override void ValidatedMaxLengthIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} maxlength ", $"is '{arg.ActionValue}'");

        protected override void ValidatedMaxIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} max ", $"is NULL");

        protected override void ValidatedMaxIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} max ", $"is '{arg.ActionValue}'");

        protected override void ValidatedLongDescIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} longdesc ", $"is NULL");

        protected override void ValidatedLongDescIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} longdesc ", $"is '{arg.ActionValue}'");

        protected override void ValidatedListIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} list ", $"is NULL");

        protected override void ValidatedListIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} list ", $"is '{arg.ActionValue}'");

        protected override void ValidatedLangIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} lang ", $"is NULL");

        protected override void ValidatedLangIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} lang ", $"is '{arg.ActionValue}'");

        protected override void ValidatedInnerTextIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} inner text ", $"is '{arg.ActionValue}'");

        protected override void ValidatedInnerHtmlIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} inner HTML ", $"is '{arg.ActionValue}'");

        protected override void ValidatedInnerHtmlContainsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} inner HTML ", $"contains '{arg.ActionValue}'");

        protected override void ValidatedHrefIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} href ", $"is '{arg.ActionValue}'");

        protected override void ValidatedHrefIsSetEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} href ", $"is not empty'");

        protected override void ValidatedHeightIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is NULL");

        protected override void ValidatedHeightIsNotNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is NOT NULL");

        protected override void ValidatedForIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} for ", $"is NULL");

        protected override void ValidatedForIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} for ", $"is '{arg.ActionValue}'");

        protected override void ValidatedEmailIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} email ", $"is '{arg.ActionValue}'");

        protected override void ValidatedIsDisabledEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is disabled");

        protected override void ValidatedIsNotDisabledEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT disabled");

        protected override void ValidatedDirIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} dir ", $"is NULL");

        protected override void ValidatedDirIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} dir ", $"is '{arg.ActionValue}'");

        protected override void ValidatedDateIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} date ", $"is '{arg.ActionValue}'");

        protected override void ValidatedCssClassIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} CSS class ", $"is NULL");

        protected override void ValidatedCssClassIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} CSS class ", $"is '{arg.ActionValue}'");

        protected override void ValidatedRowsIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} rows ", $"is '{arg.ActionValue}'");

        protected override void ValidatedColorIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} color ", $"is '{arg.ActionValue}'");

        protected override void ValidatedIsCheckedEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is checked");

        protected override void ValidatedIsNotCheckedEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT checked");

        protected override void ValidatedAutoCompleteOnEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} autocomplete ", $"is ON");

        protected override void ValidatedAutoCompleteOffEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} autocomplete ", $"is OFF");

        protected override void ValidatedAccessKeyIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} accesskey ", $"is NULL");

        protected override void ValidatedAccessKeyIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} accesskey ", $"is '{arg.ActionValue}'");

        protected override void ValidatedAcceptIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} accept ", $"is NULL");

        protected override void ValidatedValidateAcceptIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} accept ", $"is '{arg.ActionValue}'");
    }
}
