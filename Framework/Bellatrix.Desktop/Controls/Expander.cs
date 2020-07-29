// <copyright file="Expander.cs" company="Automate The Planet Ltd.">
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
    public class Expander : Element, IElementDisabled
    {
        public static Action<Expander> OverrideClickGlobally;
        public static Action<Expander> OverrideHoverGlobally;
        public static Func<Expander, bool> OverrideIsDisabledGlobally;

        public static Action<Expander> OverrideClickLocally;
        public static Action<Expander> OverrideHoverLocally;
        public static Func<Expander, bool> OverrideIsDisabledLocally;

        public static event EventHandler<ElementActionEventArgs> Clicking;
        public static event EventHandler<ElementActionEventArgs> Clicked;
        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;

        public static new void ClearLocalOverrides()
        {
            OverrideClickLocally = null;
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
        }

        public void Click()
        {
            var action = InitializeAction(this, OverrideClickGlobally, OverrideClickLocally, DefaultClick);
            action();
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

        protected virtual void DefaultClick(Expander expander) => DefaultClick(expander, Clicking, Clicked);

        protected virtual void DefaultHover(Expander expander) => DefaultHover(expander, Hovering, Hovered);

        protected virtual bool DefaultIsDisabled(Expander expander) => base.DefaultIsDisabled(expander);
    }
}