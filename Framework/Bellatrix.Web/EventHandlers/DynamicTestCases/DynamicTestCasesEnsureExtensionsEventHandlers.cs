// <copyright file="DynamicTestCasesEnsureExtensionsEventHandlers.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Text;
using Bellatrix.DynamicTestCases;
using Bellatrix.Web.Events;
using Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

namespace Bellatrix.Web.EventHandlers.DynamicTestCases
{
    public class DynamicTestCasesEnsureExtensionsEventHandlers : EnsureExtensionsEventHandlers
    {
        protected DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

        protected override void EnsuredExceptionThrownEventHandler(object sender, ElementNotFulfillingEnsureConditionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert Exception was thrown ", arg.Exception.Message);

        protected override void EnsuredAltIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} alt ", $"is NULL");

        protected override void EnsuredAltIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} alt ", $"is '{arg.ActionValue}'");

        protected override void EnsuredWrapIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} wrap ", $"is NULL");

        protected override void EnsuredWrapIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} wrap ", $"is '{arg.ActionValue}'");

        protected override void EnsuredWidthIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is null");

        protected override void EnsuredWidthIsNotNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} width ", $"is not NULL");

        protected override void EnsuredWeekIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} week ", $"is '{arg.ActionValue}'");

        protected override void EnsuredIsVisibleEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is visible");

        protected override void EnsuredIsNotVisibleEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT visible");

        protected override void EnsuredValueIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} value ", $"is NULL");

        protected override void EnsuredValueIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} value ", $"is '{arg.ActionValue}'");

        protected override void EnsuredUrlIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} url ", $"is '{arg.ActionValue}'");

        protected override void EnsuredTitleIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} title ", $"is NULL");

        protected override void EnsuredTitleIsNotNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} title ", $"is not NULL");

        protected override void EnsuredTitleIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} title ", $"is '{arg.ActionValue}'");

        protected override void EnsuredTimeIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} time ", $"is '{arg.ActionValue}'");

        protected override void EnsuredTextIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} text ", $"is NULL");

        protected override void EnsuredTextIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} text ", $"is '{arg.ActionValue}'");

        protected override void EnsuredTextContainsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} text ", $"contains '{arg.ActionValue}'");

        protected override void EnsuredTextNotContainsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} text ", $"doesn't contain '{arg.ActionValue}'");

        protected override void EnsuredTargetIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} target ", $"is NULL");

        protected override void EnsuredTargetIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} target ", $"is '{arg.ActionValue}'");

        protected override void EnsuredTabIndexIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} tabindex ", $"is NULL");

        protected override void EnsuredTabIndexIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} tabindex ", $"is '{arg.ActionValue}'");

        protected override void EnsuredStyleIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} style ", $"is NULL");

        protected override void EnsuredStyleIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} style ", $"is '{arg.ActionValue}'");

        protected override void EnsuredStyleContainsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} style", $"contains '{arg.ActionValue}'");

        protected override void EnsuredStyleNotContainsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} style ", $"don't contain '{arg.ActionValue}'");

        protected override void EnsuredStepIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} step ", $"is NULL");

        protected override void EnsuredStepIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} step ", $"is '{arg.ActionValue}'");

        protected override void EnsuredSrcSetIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} srcset ", $"is NULL");

        protected override void EnsuredSrcSetIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} srcset ", $"is '{arg.ActionValue}'");

        protected override void EnsuredSrcIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} src ", $"is NULL");

        protected override void EnsuredSrcIsNotNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} src ", $"is not NULL");

        protected override void EnsuredSrcIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} src ", $"is '{arg.ActionValue}'");

        protected override void EnsuredSpellCheckIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} spellcheck ", $"is NULL");

        protected override void EnsuredSpellCheckIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} spellcheck ", $"is '{arg.ActionValue}'");

        protected override void EnsuredSizesIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} sizes ", $"is NULL");

        protected override void EnsuredSizesIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} sizes ", $"is '{arg.ActionValue}'");

        protected override void EnsuredSizeIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} size ", $"is NULL");

        protected override void EnsuredSizeIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} size ", $"is '{arg.ActionValue}'");

        protected override void EnsuredIsSelectedEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is selected");

        protected override void EnsuredIsNotSelectedEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT selected");

        protected override void EnsuredSearchIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} search ", $"is '{arg.ActionValue}'");

        protected override void EnsuredColsIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} cols ", $"is '{arg.ActionValue}'");

        protected override void EnsuredIsRequiredEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is required");

        protected override void EnsuredIsNotRequiredEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT required");

        protected override void EnsuredRelIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} rel ", $"is NULL");

        protected override void EnsuredRelIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} rel ", $"is '{arg.ActionValue}'");

        protected override void EnsuredIsReadonlyEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is readonly");

        protected override void EnsuredIsNotReadonlyEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT readonly");

        protected override void EnsuredRangeIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} range ", $"is '{arg.ActionValue}'");

        protected override void EnsuredPlaceholderIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} placeholder ", $"is NULL");

        protected override void EnsuredPlaceholderIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} placeholder ", $"is '{arg.ActionValue}'");

        protected override void EnsuredPhoneIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} phone ", $"is '{arg.ActionValue}'");

        protected override void EnsuredPasswordIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} password ", $"is '{arg.ActionValue}'");

        protected override void EnsuredNumberIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} number ", $"is '{arg.ActionValue}'");

        protected override void EnsuredIsMultipleEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is multiple");

        protected override void EnsuredIsNotMultipleEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT multiple");

        protected override void EnsuredMonthIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} month ", $"is '{arg.ActionValue}'");

        protected override void EnsuredMinTextIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} min ", $"is NULL");

        protected override void EnsuredMinTextIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} min ", $"is '{arg.ActionValue}'");

        protected override void EnsuredMinLengthIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} minlength ", $"is NULL");

        protected override void EnsuredMinLengthIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} minlength ", $"is '{arg.ActionValue}'");

        protected override void EnsuredMinIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} min ", $"is NULL");

        protected override void EnsuredMinIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} min ", $"is '{arg.ActionValue}'");

        protected override void EnsuredMaxTextIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} max ", $"is NULL");

        protected override void EnsuredMaxTextIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} max ", $"is '{arg.ActionValue}'");

        protected override void EnsuredMaxLengthIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} maxlength ", $"is NULL");

        protected override void EnsuredMaxLengthIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} maxlength ", $"is '{arg.ActionValue}'");

        protected override void EnsuredMaxIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} max ", $"is NULL");

        protected override void EnsuredMaxIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} max ", $"is '{arg.ActionValue}'");

        protected override void EnsuredLongDescIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} longdesc ", $"is NULL");

        protected override void EnsuredLongDescIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} longdesc ", $"is '{arg.ActionValue}'");

        protected override void EnsuredListIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} list ", $"is NULL");

        protected override void EnsuredListIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} list ", $"is '{arg.ActionValue}'");

        protected override void EnsuredLangIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} lang ", $"is NULL");

        protected override void EnsuredLangIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} lang ", $"is '{arg.ActionValue}'");

        protected override void EnsuredInnerTextIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} inner text ", $"is '{arg.ActionValue}'");

        protected override void EnsuredInnerHtmlIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} inner HTML ", $"is '{arg.ActionValue}'");

        protected override void EnsuredInnerHtmlContainsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} inner HTML ", $"contains '{arg.ActionValue}'");

        protected override void EnsuredHrefIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} href ", $"is '{arg.ActionValue}'");

        protected override void EnsuredHrefIsSetEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} href ", $"is not empty'");

        protected override void EnsuredHeightIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is NULL");

        protected override void EnsuredHeightIsNotNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} height ", $"is NOT NULL");

        protected override void EnsuredForIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} for ", $"is NULL");

        protected override void EnsuredForIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} for ", $"is '{arg.ActionValue}'");

        protected override void EnsuredEmailIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} email ", $"is '{arg.ActionValue}'");

        protected override void EnsuredIsDisabledEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is disabled");

        protected override void EnsuredIsNotDisabledEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT disabled");

        protected override void EnsuredDirIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} dir ", $"is NULL");

        protected override void EnsuredDirIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} dir ", $"is '{arg.ActionValue}'");

        protected override void EnsuredDateIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} date ", $"is '{arg.ActionValue}'");

        protected override void EnsuredCssClassIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} CSS class ", $"is NULL");

        protected override void EnsuredCssClassIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} CSS class ", $"is '{arg.ActionValue}'");

        protected override void EnsuredRowsIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} rows ", $"is '{arg.ActionValue}'");

        protected override void EnsuredColorIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} color ", $"is '{arg.ActionValue}'");

        protected override void EnsuredIsCheckedEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is checked");

        protected override void EnsuredIsNotCheckedEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", $"is NOT checked");

        protected override void EnsuredAutoCompleteOnEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} autocomplete ", $"is ON");

        protected override void EnsuredAutoCompleteOffEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} autocomplete ", $"is OFF");

        protected override void EnsuredAccessKeyIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} accesskey ", $"is NULL");

        protected override void EnsuredAccessKeyIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} accesskey ", $"is '{arg.ActionValue}'");

        protected override void EnsuredAcceptIsNullEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} accept ", $"is NULL");

        protected override void EnsuredEnsureAcceptIsEventHandler(object sender, ElementActionEventArgs arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} accept ", $"is '{arg.ActionValue}'");
    }
}
