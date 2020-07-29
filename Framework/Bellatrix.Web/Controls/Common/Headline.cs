// <copyright file="Headline.cs" company="Automate The Planet Ltd.">
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
    public class Heading : Element, IElementInnerText
    {
        public static Action<Heading> OverrideHoverGlobally;
        public static Func<Heading, string> OverrideInnerTextGlobally;

        public static Action<Heading> OverrideHoverLocally;
        public static Func<Heading, string> OverrideInnerTextLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideInnerTextLocally = null;
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

        protected virtual void DefaultHover(Heading heading) => DefaultHover(heading, Hovering, Hovered);

        protected virtual string DefaultInnerText(Heading heading) => base.DefaultInnerText(heading);
    }
}