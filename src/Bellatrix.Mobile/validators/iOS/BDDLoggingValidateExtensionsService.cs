// <copyright file="BDDLoggingValidateExtensionsService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS
{
    public class BDDLoggingValidateExtensionsService : ValidateExtensionsEventHandlers
    {
        protected override void ValidatedIsVisibleEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} is visible");

        protected override void ValidatedIsNotVisibleEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} is NOT visible");

        protected override void ValidatedTimeIsEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} time is '{arg.ActionValue}'");

        protected override void ValidatedTextIsNotSetEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} text is NULL");

        protected override void ValidatedTextIsEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} text is '{arg.ActionValue}'");

        protected override void ValidatedIsSelectedEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} is selected");

        protected override void ValidatedIsNotSelectedEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} is NOT selected");

        protected override void ValidatedIsDisabledEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} is disabled");

        protected override void ValidatedIsNotDisabledEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} is NOT disabled");

        protected override void ValidatedDateIsEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} date is '{arg.ActionValue}'");

        protected override void ValidatedIsCheckedEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} is checked");

        protected override void ValidatedIsNotCheckedEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} is NOT checked");

        protected override void ValidatedIsOnEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} is ON");

        protected override void ValidatedIsOffEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} is OFF");

        protected override void ValidatedNumberIsEventHandler(object sender, ElementActionEventArgs<IOSElement> arg) => Logger.LogInformation($"Validate {arg.Element.ElementName} number is '{arg.ActionValue}'");
    }
}