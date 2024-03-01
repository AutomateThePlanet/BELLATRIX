// <copyright file="ComponentWaitService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Events;
using Bellatrix.Mobile.Locators;
using Bellatrix.Mobile.Untils;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile.Services;

public class ComponentWaitService<TDriver, TDriverElement>
    where TDriver : AppiumDriver
    where TDriverElement : AppiumElement
{
    public static event EventHandler<ComponentNotFulfillingWaitConditionEventArgs> OnElementNotFulfillingWaitConditionEvent;

    internal void Wait<TUntil, TComponent>(TComponent element, TUntil until)
        where TUntil : WaitStrategy<TDriver, TDriverElement>
        where TComponent : Component<TDriver, TDriverElement>
    {
        try
        {
            WaitInternal(element.By, until);
        }
        catch (Exception ex)
        {
            OnElementNotFulfillingWaitConditionEvent?.Invoke(this, new ComponentNotFulfillingWaitConditionEventArgs(ex));
            throw;
        }
    }

    internal void WaitInternal<TBy, TUntil>(TBy by, TUntil until)
        where TBy : FindStrategy<TDriver, TDriverElement>
        where TUntil : WaitStrategy<TDriver, TDriverElement> => until?.WaitUntil(by);
}