﻿// <copyright file="ComponentEventHandlers.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
using Bellatrix.BugReporting;
using Bellatrix.DynamicTestCases;
using Bellatrix.Mobile.Controls.IOS;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.EventHandlers.IOS;

public class ComponentEventHandlers : IControlEventHandlers
{
    // These three properties were added to reduce code duplication in child classes and improve readability. However, we realize that the SOLID principles are not followed thoroughly.
    protected BugReportingContextService BugReportingContextService => ServicesCollection.Current.Resolve<BugReportingContextService>();
    protected DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

    public virtual void SubscribeToAll()
    {
        IOSComponent.ScrollingToVisible += ScrollingToVisibleEventHandler;
        IOSComponent.ScrolledToVisible += ScrolledToVisibleEventHandler;
        IOSComponent.CreatingComponent += CreatingComponentEventHandler;
        IOSComponent.CreatedComponent += CreatedComponentEventHandler;
        IOSComponent.CreatingComponents += CreatingComponentsEventHandler;
        IOSComponent.CreatedComponents += CreatedComponentsEventHandler;
        IOSComponent.ReturningWrappedElement += ReturningWrappedElementEventHandler;
    }

    public virtual void UnsubscribeToAll()
    {
        IOSComponent.ScrollingToVisible -= ScrollingToVisibleEventHandler;
        IOSComponent.ScrolledToVisible -= ScrolledToVisibleEventHandler;
        IOSComponent.CreatingComponent -= CreatingComponentEventHandler;
        IOSComponent.CreatedComponent -= CreatedComponentEventHandler;
        IOSComponent.CreatingComponents -= CreatingComponentsEventHandler;
        IOSComponent.CreatedComponents -= CreatedComponentsEventHandler;
        IOSComponent.ReturningWrappedElement -= ReturningWrappedElementEventHandler;
    }

    protected virtual void ScrollingToVisibleEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ScrolledToVisibleEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void CreatingComponentEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void CreatedComponentEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void CreatingComponentsEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void CreatedComponentsEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void ReturningWrappedElementEventHandler(object sender, NativeElementActionEventArgs<AppiumElement> arg)
    {
    }
}
