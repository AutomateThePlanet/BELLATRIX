// <copyright file="TextFieldEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Events;

namespace Bellatrix.Web.Controls.EventHandlers;

public class TextFieldEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        base.SubscribeToAll();
        TextField.Hovering += HoveringEventHandler;
        TextField.Hovered += HoveredEventHandler;
        TextField.SettingText += SettingTextEventHandler;
        TextField.TextSet += TextSetEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        base.UnsubscribeToAll();
        TextField.Hovering -= HoveringEventHandler;
        TextField.Hovered -= HoveredEventHandler;
        TextField.SettingText -= SettingTextEventHandler;
        TextField.TextSet -= TextSetEventHandler;
    }

    protected virtual void SettingTextEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }

    protected virtual void TextSetEventHandler(object sender, ComponentActionEventArgs arg)
    {
    }
}
