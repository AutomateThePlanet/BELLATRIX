// <copyright file="Time.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using Bellatrix.Desktop.Contracts;
using Bellatrix.Desktop.Events;

namespace Bellatrix.Desktop
{
    public class Time : Element, IElementDisabled, IElementTime
    {
        public static Action<Time> OverrideHoverGlobally;
        public static Func<Time, bool> OverrideIsDisabledGlobally;
        public static Func<Time, string> OverrideGetTimeGlobally;
        public static Action<Time, int, int> OverrideSetTimeGlobally;

        public static Action<Time> OverrideHoverLocally;
        public static Func<Time, bool> OverrideIsDisabledLocally;
        public static Func<Time, string> OverrideGetTimeLocally;
        public static Action<Time, int, int> OverrideSetTimeLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingTime;
        public static event EventHandler<ElementActionEventArgs> TimeSet;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideGetTimeLocally = null;
            OverrideSetTimeLocally = null;
        }

        public string GetTime()
        {
            var action = InitializeAction(this, OverrideGetTimeGlobally, OverrideGetTimeLocally, DefaultInnerText);
            return action();
        }

        public void SetTime(int hours, int minutes)
        {
            var action = InitializeAction(this, OverrideSetTimeGlobally, OverrideSetTimeLocally, DefaultSetTime);
            action(hours, minutes);
        }

        public void Hover()
        {
            var action = InitializeAction(this, OverrideHoverGlobally, OverrideHoverLocally, DefaultHover);
            action();
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsDisabled
        {
            get
            {
                var action = InitializeAction(this, OverrideIsDisabledGlobally, OverrideIsDisabledLocally, DefaultIsDisabled);
                return action();
            }
        }

        protected virtual void DefaultHover(Time time) => DefaultHover(time, Hovering, Hovered);

        protected virtual bool DefaultIsDisabled(Time time) => base.DefaultIsDisabled(time);

        protected virtual void DefaultSetTime(Time time, int hours, int minutes)
        {
            DefaultSetText(time, SettingTime, TimeSet, $"{hours}:{minutes}:00");
        }
    }
}