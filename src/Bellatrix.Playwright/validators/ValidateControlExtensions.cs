// <copyright file="ValidateControlExtensions.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Playwright.Events;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Settings;

namespace Bellatrix.Playwright;

public static partial class ValidateControlExtensions
{
    private static void WaitUntil(Func<bool> waitCondition, string exceptionMessage, int? timeoutInSeconds, int? sleepIntervalInSeconds)
    {
        var localTimeout = timeoutInSeconds ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.ValidationsTimeout;
        var localSleepInterval = sleepIntervalInSeconds ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.SleepInterval;
        var wrappedBrowser = ServicesCollection.Current.Resolve<WrappedBrowser>();
        try
        {
            Bellatrix.Utilities.Wait.Until(waitCondition, localTimeout, String.Empty, localSleepInterval);
        }
        catch (TimeoutException)
        {
            var elementPropertyValidateException = new ComponentPropertyValidateException(exceptionMessage, wrappedBrowser.CurrentPage.Url);
            ValidatedExceptionThrowedEvent?.Invoke(waitCondition, new ElementNotFulfillingValidateConditionEventArgs(elementPropertyValidateException));
            throw elementPropertyValidateException;
        }
    }

    public static event EventHandler<ElementNotFulfillingValidateConditionEventArgs> ValidatedExceptionThrowedEvent;
}