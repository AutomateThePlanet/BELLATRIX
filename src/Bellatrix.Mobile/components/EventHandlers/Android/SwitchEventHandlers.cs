// <copyright file="SwitchEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Android;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.EventHandlers.Android;

public class SwitchEventHandlers : ComponentEventHandlers
{
    public override void SubscribeToAll()
    {
        base.SubscribeToAll();
        Switch.TurningOn += TurningOnEventHandler;
        Switch.TurnedOn += TurnedOnEventHandler;
        Switch.TurningOff += TurningOffEventHandler;
        Switch.TurnedOff += TurnedOffEventHandler;
    }

    public override void UnsubscribeToAll()
    {
        base.UnsubscribeToAll();
        Switch.TurningOn -= TurningOnEventHandler;
        Switch.TurnedOn -= TurnedOnEventHandler;
        Switch.TurningOff -= TurningOffEventHandler;
        Switch.TurnedOff -= TurnedOffEventHandler;
    }

    protected virtual void TurningOffEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void TurnedOffEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void TurningOnEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }

    protected virtual void TurnedOnEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg)
    {
    }
}
