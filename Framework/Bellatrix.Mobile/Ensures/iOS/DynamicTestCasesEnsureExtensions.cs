// <copyright file="DynamicTestCasesEnsureExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.DynamicTestCases;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS
{
    public class DynamicTestCasesEnsureExtensions : EnsureExtensionsEventHandlers
    {
        protected DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

        protected override void EnsuredIsVisibleEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "is visible");

        protected override void EnsuredIsNotVisibleEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "is NOT visible");

        protected override void EnsuredTimeIsEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} time ", $"is '{arg.ActionValue}'");

        protected override void EnsuredTextIsNotSetEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "text is NULL");

        protected override void EnsuredTextIsEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} text ", $"is '{arg.ActionValue}'");

        protected override void EnsuredIsSelectedEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "is selected");

        protected override void EnsuredIsNotSelectedEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "is NOT selected");

        protected override void EnsuredIsDisabledEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "is disabled");

        protected override void EnsuredIsNotDisabledEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "is NOT disabled");

        protected override void EnsuredDateIsEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} date ", $"is '{arg.ActionValue}'");

        protected override void EnsuredIsCheckedEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "is checked");

        protected override void EnsuredIsNotCheckedEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "is NOT checked");

        protected override void EnsuredIsOnEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "is ON");

        protected override void EnsuredIsOffEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} ", "is OFF");

        protected override void EnsuredNumberIsEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => DynamicTestCasesService.AddAssertStep($"Assert {arg.Element.ElementName} number ", $"is '{arg.ActionValue}'");
    }
}