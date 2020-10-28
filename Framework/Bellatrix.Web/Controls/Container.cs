// <copyright file="Container.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Events;

namespace Bellatrix.Web.Controls
{
    public class Container : Element
    {
        public static Action<Container> OverrideHoverGlobally;
        public static Func<Container, string> OverrideInnerTextGlobally;
        public static Func<Container, string> OverrideInnerHtmlGlobally;

        public Container()
        {
            Hover = InitializeAction(this, OverrideHoverGlobally, DefaultHover);
            InnerText = InitializeAction(this, OverrideInnerTextGlobally, DefaultInnerText);
            InnerHtml = InitializeAction(this, OverrideInnerHtmlGlobally, DefaultInnerHtml);
        }

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;

        public Action Hover { get; set; }

        public Action Focus { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Func<string> InnerText { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Func<string> InnerHtml { get; set; }

        protected virtual void DefaultHover(Container container) => DefaultHover(container, Hovering, Hovered);

        protected virtual string DefaultInnerText(Container container) => base.DefaultInnerText(container);

        protected virtual string DefaultInnerHtml(Container container) => base.DefaultInnerHtml(container);
    }
}