// <copyright file="ComponentEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.BugReporting;
using Bellatrix.DynamicTestCases;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;

namespace Bellatrix.Web.Controls.EventHandlers;

public class ComponentEventHandlers : IControlEventHandlers
{
    // These three properties were added to reduce code duplication in child classes and improve readability. However, we realize that the SOLID principles are not followed thoroughly.
    protected DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    protected BugReportingContextService BugReportingContextService => ServicesCollection.Current.Resolve<BugReportingContextService>();

    public virtual void SubscribeToAll()
    {
        Component.ScrollingToVisible += ScrollingToVisibleEventHandler;
        Component.ScrolledToVisible += ScrolledToVisibleEventHandler;
        Component.CreatingComponent += CreatingElementEventHandler;
        Component.CreatedComponent += CreatedElementEventHandler;
        Component.CreatingComponents += CreatingElementsEventHandler;
        Component.CreatedComponents += CreatedElementsEventHandler;
        Component.ReturningWrappedElement += ReturningWrappedElementEventHandler;
        Component.Focusing += FocusingEventHandler;
        Component.Focused += FocusedEventHandler;
    }

    public virtual void UnsubscribeToAll()
    {
        Component.ScrollingToVisible -= ScrollingToVisibleEventHandler;
        Component.ScrolledToVisible -= ScrolledToVisibleEventHandler;
        Component.CreatingComponent -= CreatingElementEventHandler;
        Component.CreatedComponent -= CreatedElementEventHandler;
        Component.CreatingComponents -= CreatingElementsEventHandler;
        Component.CreatedComponents -= CreatedElementsEventHandler;
        Component.ReturningWrappedElement -= ReturningWrappedElementEventHandler;
        Component.Focusing -= FocusingEventHandler;
        Component.Focused -= FocusedEventHandler;
    }

    protected virtual void ScrollingToVisibleEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ScrolledToVisibleEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void CreatingElementEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void CreatedElementEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void CreatingElementsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void CreatedElementsEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ReturningWrappedElementEventHandler(object sender, NativeElementActionEventArgs arg)
    {
    }

    protected virtual void FocusingEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void FocusedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void HoveringEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void HoveredEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ClickingEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void ClickedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
