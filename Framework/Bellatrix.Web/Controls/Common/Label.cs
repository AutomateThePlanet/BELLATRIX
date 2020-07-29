// <copyright file="Label.cs" company="Automate The Planet Ltd.">
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
    public class Label : Element, IElementInnerText, IElementInnerHtml, IElementFor
    {
        public static Action<Label> OverrideHoverGlobally;
        public static Func<Label, string> OverrideInnerTextGlobally;
        public static Func<Label, string> OverrideInnerHtmlGlobally;
        public static Func<Label, string> OverrideForGlobally;

        public static Action<Label> OverrideHoverLocally;
        public static Func<Label, string> OverrideInnerTextLocally;
        public static Func<Label, string> OverrideInnerHtmlLocally;
        public static Func<Label, string> OverrideForLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideInnerTextLocally = null;
            OverrideInnerHtmlLocally = null;
            OverrideForLocally = null;
        }

        public override Type ElementType => GetType();

        public void Hover()
        {
            var action = InitializeAction(this, OverrideHoverGlobally, OverrideHoverLocally, DefaultHover);
            action();
        }

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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string For
        {
            get
            {
                var action = InitializeAction(this, OverrideForGlobally, OverrideForLocally, DefaultGetFor);
                return action();
            }
        }

        protected virtual void DefaultHover(Label label) => DefaultHover(label, Hovering, Hovered);

        protected virtual string DefaultInnerText(Label label) => base.DefaultInnerText(label);

        protected virtual string DefaultInnerHtml(Label label) => base.DefaultInnerHtml(label);

        protected virtual string DefaultGetFor(Label label) => base.DefaultGetFor(label);
    }
}