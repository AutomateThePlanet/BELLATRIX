﻿// <copyright file="ComboBoxEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Events;

namespace Bellatrix.Desktop.EventHandlers;

public class ComboBoxEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        base.SubscribeToAll();
        ComboBox.Selecting += SelectingEventHandler;
        ComboBox.Selected += SelectedEventHandler;
        ComboBox.Hovering += HoveringEventHandler;
        ComboBox.Hovered += HoveredEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        base.UnsubscribeToAll();
        ComboBox.Selecting -= SelectingEventHandler;
        ComboBox.Selected -= SelectedEventHandler;
        ComboBox.Hovering -= HoveringEventHandler;
        ComboBox.Hovered -= HoveredEventHandler;
    }

    protected virtual void SelectingEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void SelectedEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
