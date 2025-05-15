// <copyright file="MultipleSelectEventHandlers.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Playwright.Components.Common;
using Bellatrix.Playwright.Events;

namespace Bellatrix.Playwright.Controls.EventHandlers;

public class MultipleSelectEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        base.SubscribeToAll();
        MultipleSelect.Selecting += SelectingEventHandler;
        MultipleSelect.Selected += SelectedEventHandler;
        MultipleSelect.Hovering += HoveringEventHandler;
        MultipleSelect.Hovered += HoveredEventHandler;
        MultipleSelect.FailedSelection += FailedSelectionEventHandler;
        MultipleSelect.SelectedNotFound += SelectedNotFoundEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        base.UnsubscribeToAll();
        MultipleSelect.Selecting -= SelectingEventHandler;
        MultipleSelect.Selected -= SelectedEventHandler;
        MultipleSelect.Hovering -= HoveringEventHandler;
        MultipleSelect.Hovered -= HoveredEventHandler;
        MultipleSelect.FailedSelection -= FailedSelectionEventHandler;
        MultipleSelect.SelectedNotFound -= SelectedNotFoundEventHandler;
    }

    protected virtual void SelectingEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void SelectedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void FailedSelectionEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void SelectedNotFoundEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
