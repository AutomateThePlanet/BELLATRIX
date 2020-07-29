// <copyright file="BDDLoggingEnsureExtensionsService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Events;
using ElementNotFulfillingEnsureConditionEventArgs = Bellatrix.Desktop.Ensures.ElementNotFulfillingEnsureConditionEventArgs;

namespace Bellatrix.Desktop
{
    public class BDDLoggingEnsureExtensionsService : EnsureExtensionsEventHandlers
    {
        protected override void EnsuredExceptionThrowedEventHandler(object sender, ElementNotFulfillingEnsureConditionEventArgs arg) => Logger.LogInformation($"{arg.Exception.Message}");

        protected override void EnsuredIsVisibleEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} is visible");

        protected override void EnsuredIsNotVisibleEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} is NOT visible");

        protected override void EnsuredTimeIsEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} time is '{arg.ActionValue}'");

        protected override void EnsuredTextIsNullEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} text is NULL");

        protected override void EnsuredTextIsEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} text is '{arg.ActionValue}'");

        protected override void EnsuredIsSelectedEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} is selected");

        protected override void EnsuredIsNotSelectedEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} is NOT selected");

        protected override void EnsuredInnerTextIsEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} inner text is '{arg.ActionValue}'");

        protected override void EnsuredIsDisabledEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} is disabled");

        protected override void EnsuredIsNotDisabledEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} is NOT disabled");

        protected override void EnsuredDateIsEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} date is '{arg.ActionValue}'");

        protected override void EnsuredIsCheckedEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} is checked");

        protected override void EnsuredIsNotCheckedEventHandler(object sender, ElementActionEventArgs arg) => Logger.LogInformation($"Ensure {arg.Element.ElementName} is NOT checked");
    }
}