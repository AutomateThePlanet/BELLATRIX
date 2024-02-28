// <copyright file="DynamicTestCasesValidateExtensionsEventHandlers.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Collections.Generic;
using System.Text;
using Bellatrix.DynamicTestCases;
using Bellatrix.Web.Events;
using Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

namespace Bellatrix.Web.EventHandlers.DynamicTestCases;

public class DynamicTestCasesValidateExtensionsEventHandlers : ValidateExtensionsEventHandlers
{
    protected DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

    protected override void ValidatedExceptionThrownEventHandler(object sender, ElementNotFulfillingValidateConditionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert Exception was thrown ", arg.Exception.Message);

    protected override void ValidatedAltIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} alt ", $"is NULL");

    protected override void ValidatedAltIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} alt ", $"is '{arg.ActionValue}'");

    protected override void ValidatedWrapIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} wrap ", $"is NULL");

    protected override void ValidatedWrapIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} wrap ", $"is '{arg.ActionValue}'");

    protected override void ValidatedWidthIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} width ", $"is null");

    protected override void ValidatedWidthIsNotNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} width ", $"is not NULL");

    protected override void ValidatedWeekIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} week ", $"is '{arg.ActionValue}'");

    protected override void ValidatedIsVisibleEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is visible");

    protected override void ValidatedIsNotVisibleEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is NOT visible");

    protected override void ValidatedValueIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} value ", $"is NULL");

    protected override void ValidatedValueIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} value ", $"is '{arg.ActionValue}'");

    protected override void ValidatedUrlIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} url ", $"is '{arg.ActionValue}'");

    protected override void ValidatedTitleIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} title ", $"is NULL");

    protected override void ValidatedTitleIsNotNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} title ", $"is not NULL");

    protected override void ValidatedTitleIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} title ", $"is '{arg.ActionValue}'");

    protected override void ValidatedTimeIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} time ", $"is '{arg.ActionValue}'");

    protected override void ValidatedTextIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} text ", $"is NULL");

    protected override void ValidatedTextIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} text ", $"is '{arg.ActionValue}'");

    protected override void ValidatedTextContainsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} text ", $"contains '{arg.ActionValue}'");

    protected override void ValidatedTextNotContainsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} text ", $"doesn't contain '{arg.ActionValue}'");

    protected override void ValidatedTargetIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} target ", $"is NULL");

    protected override void ValidatedTargetIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} target ", $"is '{arg.ActionValue}'");

    protected override void ValidatedTabIndexIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} tabindex ", $"is NULL");

    protected override void ValidatedTabIndexIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} tabindex ", $"is '{arg.ActionValue}'");

    protected override void ValidatedStyleIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} style ", $"is NULL");

    protected override void ValidatedStyleIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} style ", $"is '{arg.ActionValue}'");

    protected override void ValidatedStyleContainsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} style", $"contains '{arg.ActionValue}'");

    protected override void ValidatedStyleNotContainsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} style ", $"don't contain '{arg.ActionValue}'");

    protected override void ValidatedStepIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} step ", $"is NULL");

    protected override void ValidatedStepIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} step ", $"is '{arg.ActionValue}'");

    protected override void ValidatedSrcSetIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} srcset ", $"is NULL");

    protected override void ValidatedSrcSetIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} srcset ", $"is '{arg.ActionValue}'");

    protected override void ValidatedSrcIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} src ", $"is NULL");

    protected override void ValidatedSrcIsNotNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} src ", $"is not NULL");

    protected override void ValidatedSrcIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} src ", $"is '{arg.ActionValue}'");

    protected override void ValidatedSpellCheckIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} spellcheck ", $"is NULL");

    protected override void ValidatedSpellCheckIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} spellcheck ", $"is '{arg.ActionValue}'");

    protected override void ValidatedSizesIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} sizes ", $"is NULL");

    protected override void ValidatedSizesIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} sizes ", $"is '{arg.ActionValue}'");

    protected override void ValidatedSizeIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} size ", $"is NULL");

    protected override void ValidatedSizeIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} size ", $"is '{arg.ActionValue}'");

    protected override void ValidatedIsSelectedEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is selected");

    protected override void ValidatedIsNotSelectedEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is NOT selected");

    protected override void ValidatedSearchIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} search ", $"is '{arg.ActionValue}'");

    protected override void ValidatedColsIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} cols ", $"is '{arg.ActionValue}'");

    protected override void ValidatedIsRequiredEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is required");

    protected override void ValidatedIsNotRequiredEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is NOT required");

    protected override void ValidatedRelIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} rel ", $"is NULL");

    protected override void ValidatedRelIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} rel ", $"is '{arg.ActionValue}'");

    protected override void ValidatedIsReadonlyEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is readonly");

    protected override void ValidatedIsNotReadonlyEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is NOT readonly");

    protected override void ValidatedRangeIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} range ", $"is '{arg.ActionValue}'");

    protected override void ValidatedPlaceholderIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} placeholder ", $"is NULL");

    protected override void ValidatedPlaceholderIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} placeholder ", $"is '{arg.ActionValue}'");

    protected override void ValidatedPhoneIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} phone ", $"is '{arg.ActionValue}'");

    protected override void ValidatedPasswordIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} password ", $"is '{arg.ActionValue}'");

    protected override void ValidatedNumberIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} number ", $"is '{arg.ActionValue}'");

    protected override void ValidatedIsMultipleEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is multiple");

    protected override void ValidatedIsNotMultipleEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is NOT multiple");

    protected override void ValidatedMonthIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} month ", $"is '{arg.ActionValue}'");

    protected override void ValidatedMinTextIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} min ", $"is NULL");

    protected override void ValidatedMinTextIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} min ", $"is '{arg.ActionValue}'");

    protected override void ValidatedMinLengthIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} minlength ", $"is NULL");

    protected override void ValidatedMinLengthIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} minlength ", $"is '{arg.ActionValue}'");

    protected override void ValidatedMinIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} min ", $"is NULL");

    protected override void ValidatedMinIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} min ", $"is '{arg.ActionValue}'");

    protected override void ValidatedMaxTextIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} max ", $"is NULL");

    protected override void ValidatedMaxTextIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} max ", $"is '{arg.ActionValue}'");

    protected override void ValidatedMaxLengthIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} maxlength ", $"is NULL");

    protected override void ValidatedMaxLengthIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} maxlength ", $"is '{arg.ActionValue}'");

    protected override void ValidatedMaxIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} max ", $"is NULL");

    protected override void ValidatedMaxIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} max ", $"is '{arg.ActionValue}'");

    protected override void ValidatedLongDescIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} longdesc ", $"is NULL");

    protected override void ValidatedLongDescIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} longdesc ", $"is '{arg.ActionValue}'");

    protected override void ValidatedListIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} list ", $"is NULL");

    protected override void ValidatedListIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} list ", $"is '{arg.ActionValue}'");

    protected override void ValidatedLangIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} lang ", $"is NULL");

    protected override void ValidatedLangIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} lang ", $"is '{arg.ActionValue}'");

    protected override void ValidatedInnerTextIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} inner text ", $"is '{arg.ActionValue}'");

    protected override void ValidatedInnerHtmlIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} inner HTML ", $"is '{arg.ActionValue}'");

    protected override void ValidatedInnerHtmlContainsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} inner HTML ", $"contains '{arg.ActionValue}'");

    protected override void ValidatedHrefIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} href ", $"is '{arg.ActionValue}'");

    protected override void ValidatedHrefIsSetEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} href ", $"is not empty'");

    protected override void ValidatedHeightIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} height ", $"is NULL");

    protected override void ValidatedHeightIsNotNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} height ", $"is NOT NULL");

    protected override void ValidatedForIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} for ", $"is NULL");

    protected override void ValidatedForIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} for ", $"is '{arg.ActionValue}'");

    protected override void ValidatedEmailIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} email ", $"is '{arg.ActionValue}'");

    protected override void ValidatedIsDisabledEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is disabled");

    protected override void ValidatedIsNotDisabledEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is NOT disabled");

    protected override void ValidatedDirIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} dir ", $"is NULL");

    protected override void ValidatedDirIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} dir ", $"is '{arg.ActionValue}'");

    protected override void ValidatedDateIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} date ", $"is '{arg.ActionValue}'");

    protected override void ValidatedCssClassIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} CSS class ", $"is NULL");

    protected override void ValidatedCssClassIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} CSS class ", $"is '{arg.ActionValue}'");

    protected override void ValidatedRowsIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} rows ", $"is '{arg.ActionValue}'");

    protected override void ValidatedColorIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} color ", $"is '{arg.ActionValue}'");

    protected override void ValidatedIsCheckedEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is checked");

    protected override void ValidatedIsNotCheckedEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} ", $"is NOT checked");

    protected override void ValidatedAutoCompleteOnEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} autocomplete ", $"is ON");

    protected override void ValidatedAutoCompleteOffEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} autocomplete ", $"is OFF");

    protected override void ValidatedAccessKeyIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} accesskey ", $"is NULL");

    protected override void ValidatedAccessKeyIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} accesskey ", $"is '{arg.ActionValue}'");

    protected override void ValidatedAcceptIsNullEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} accept ", $"is NULL");

    protected override void ValidatedValidateAcceptIsEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ComponentName} accept ", $"is '{arg.ActionValue}'");
}
