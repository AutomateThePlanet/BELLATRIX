// <copyright file="ValidateControlExtensions.GetText.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS
{
    public static partial class ValidateControlExtensions
    {
        public static void ValidateTextIsNotSet<TElement>(this TElement control, int? timeout = null, int? sleepInterval = null)
            where TElement : IElementText, IElement<IOSElement>
        {
            ValidateControlWaitService.WaitUntil<IOSDriver<IOSElement>, IOSElement>(() => string.IsNullOrEmpty(control.GetText()), $"The control's text should be null but was '{control.GetText()}'.", timeout, sleepInterval);
            ValidatedTextIsNotSetEvent?.Invoke(control, new ElementActionEventArgs<IOSElement>(control));
        }

        public static void ValidateTextIs<TElement>(this TElement control, string value, int? timeout = null, int? sleepInterval = null)
             where TElement : IElementText, IElement<IOSElement>
        {
            ValidateControlWaitService.WaitUntil<IOSDriver<IOSElement>, IOSElement>(() => control.GetText().Equals(value), $"The control's text should be '{value}' but was '{control.GetText()}'.", timeout, sleepInterval);
            ValidatedTextIsEvent?.Invoke(control, new ElementActionEventArgs<IOSElement>(control, value));
        }

        public static event EventHandler<ElementActionEventArgs<IOSElement>> ValidatedTextIsNotSetEvent;
        public static event EventHandler<ElementActionEventArgs<IOSElement>> ValidatedTextIsEvent;
    }
}