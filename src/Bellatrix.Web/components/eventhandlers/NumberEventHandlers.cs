﻿// <copyright file="NumberEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Events;

namespace Bellatrix.Web.Controls.EventHandlers;

public class NumberEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        base.SubscribeToAll();
        Number.Hovering += HoveringEventHandler;
        Number.Hovered += HoveredEventHandler;
        Number.SettingNumber += SettingNumberEventHandler;
        Number.NumberSet += NumberSetEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        base.UnsubscribeToAll();
        Number.Hovering -= HoveringEventHandler;
        Number.Hovered -= HoveredEventHandler;
        Number.SettingNumber -= SettingNumberEventHandler;
        Number.NumberSet -= NumberSetEventHandler;
    }

    protected virtual void SettingNumberEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void NumberSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
