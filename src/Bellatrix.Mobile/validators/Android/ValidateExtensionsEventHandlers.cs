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
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android;

public abstract class ValidateExtensionsEventHandlers
{
    public virtual void SubscribeToAll()
    {
        ValidateControlExtensions.ValidatedIsCheckedEvent += ValidatedIsCheckedEventHandler;
        ValidateControlExtensions.ValidatedIsNotCheckedEvent += ValidatedIsNotCheckedEventHandler;
        ValidateControlExtensions.ValidatedDateIsEvent += ValidatedDateIsEventHandler;
        ValidateControlExtensions.ValidatedIsDisabledEvent += ValidatedIsDisabledEventHandler;
        ValidateControlExtensions.ValidatedIsNotDisabledEvent += ValidatedIsNotDisabledEventHandler;
        ValidateControlExtensions.ValidatedIsSelectedEvent += ValidatedIsSelectedEventHandler;
        ValidateControlExtensions.ValidatedIsNotSelectedEvent += ValidatedIsNotSelectedEventHandler;
        ValidateControlExtensions.ValidatedTextIsNotSetEvent += ValidatedTextIsNotSetEventHandler;
        ValidateControlExtensions.ValidatedTextIsEvent += ValidatedTextIsEventHandler;
        ValidateControlExtensions.ValidatedTimeIsEvent += ValidatedTimeIsEventHandler;
        ValidateControlExtensions.ValidatedIsVisibleEvent += ValidatedIsVisibleEventHandler;
        ValidateControlExtensions.ValidatedIsNotVisibleEvent += ValidatedIsNotVisibleEventHandler;
        ValidateControlExtensions.ValidatedIsOnEvent += ValidatedIsOnEventHandler;
        ValidateControlExtensions.ValidatedIsOffEvent += ValidatedIsOffEventHandler;
        ValidateControlExtensions.ValidatedNumberIsEvent += ValidatedNumberIsEventHandler;
    }

    protected virtual void ValidatedIsVisibleEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedIsNotVisibleEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedTimeIsEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedTextIsNotSetEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedTextIsEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedIsSelectedEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedIsNotSelectedEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedIsDisabledEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedIsNotDisabledEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedDateIsEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedIsCheckedEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedIsNotCheckedEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedIsOnEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedIsOffEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ValidatedNumberIsEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }
}
