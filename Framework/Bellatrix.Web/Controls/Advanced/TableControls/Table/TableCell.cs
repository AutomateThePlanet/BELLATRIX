// <copyright file="TableCell.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;

namespace Bellatrix.Web
{
    public class TableCell : Element, IElementInnerText, IElementInnerHtml
    {
        public static Action<TableCell> OverrideFocusGlobally;
        public static Action<TableCell> OverrideHoverGlobally;
        public static Func<TableCell, string> OverrideInnerTextGlobally;
        public static Func<TableCell, string> OverrideInnerHtmlGlobally;

        public static Action<TableCell> OverrideHoverLocally;
        public static Action<TableCell> OverrideFocusLocally;
        public static Func<TableCell, string> OverrideInnerTextLocally;
        public static Func<TableCell, string> OverrideInnerHtmlLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Focusing;
        public static event EventHandler<ElementActionEventArgs> Focused;

        public static new void ClearLocalOverrides()
        {
            OverrideInnerTextLocally = null;
            OverrideInnerHtmlLocally = null;
            OverrideHoverLocally = null;
            OverrideFocusLocally = null;
        }

        public void Hover()
        {
            var action = InitializeAction(this, OverrideHoverGlobally, OverrideHoverLocally, DefaultHover);
            action();
        }

        public void Focus()
        {
            var action = InitializeAction(this, OverrideFocusGlobally, OverrideFocusLocally, DefaultFocus);
            action();
        }

        public int Column { get; set; }

        public int Row { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string InnerText
        {
            get
            {
                var action = InitializeAction(this, OverrideInnerTextGlobally, OverrideInnerTextLocally, DefaultInnerText);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string InnerHtml
        {
            get
            {
                var action = InitializeAction(this, OverrideInnerHtmlGlobally, OverrideInnerHtmlLocally, DefaultInnerHtml);
                return action();
            }
        }

        protected virtual void DefaultFocus(TableCell tableCell) => DefaultFocus(tableCell, Focusing, Focused);

        protected virtual void DefaultHover(TableCell tableCell) => DefaultHover(tableCell, Hovering, Hovered);

        protected virtual string DefaultInnerText(TableCell tableCell) => base.DefaultInnerText(tableCell);

        protected virtual string DefaultInnerHtml(TableCell tableCell) => base.DefaultInnerHtml(tableCell);
    }
}