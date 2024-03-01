// <copyright file="SeekBar.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.IOS;
using Bellatrix.Mobile.Events;
using Bellatrix.Mobile.Services;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS;

public class SeekBar : IOSComponent, IComponentDisabled
{
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> SettingPercentage;
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> PercentageSet;

    public virtual void Set(double value)
    {
        SettingPercentage?.Invoke(this, new ComponentActionEventArgs<AppiumElement>(this, value.ToString()));
        int end = WrappedElement.Size.Width;
        int y = WrappedElement.Location.Y;
        var touchActionsService = ServicesCollection.Current.Resolve<TouchActionsService<IOSDriver, AppiumElement>>();
        int moveTo = (int)((value / 100) * end);
        touchActionsService.Press(moveTo, y, 0).Release().Perform();
        PercentageSet?.Invoke(this, new ComponentActionEventArgs<AppiumElement>(this, value.ToString()));
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetIsDisabled();
}