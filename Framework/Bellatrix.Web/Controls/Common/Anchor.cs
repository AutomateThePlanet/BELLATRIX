// <copyright file="Anchor.cs" company="Automate The Planet Ltd.">
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
using System.Web;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;

namespace Bellatrix.Web
{
    public class Anchor : Element, IElementHref, IElementInnerText, IElementInnerHtml, IElementRel, IElementTarget
    {
        public static Action<Anchor> OverrideClickGlobally;
        public static Action<Anchor> OverrideHoverGlobally;
        public static Func<Anchor, string> OverrideInnerHtmlGlobally;
        public static Func<Anchor, string> OverrideInnerTextGlobally;
        public static Func<Anchor, string> OverrideHrefGlobally;
        public static Func<Anchor, string> OverrideGetTargetGlobally;
        public static Func<Anchor, string> OverrideGetRelGlobally;

        public static Action<Anchor> OverrideClickLocally;
        public static Action<Anchor> OverrideHoverLocally;
        public static Func<Anchor, string> OverrideInnerHtmlLocally;
        public static Func<Anchor, string> OverrideInnerTextLocally;
        public static Func<Anchor, string> OverrideHrefLocally;
        public static Func<Anchor, string> OverrideGetTargetLocally;
        public static Func<Anchor, string> OverrideGetRelLocally;

        public static event EventHandler<ElementActionEventArgs> Clicking;
        public static event EventHandler<ElementActionEventArgs> Clicked;
        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;

        public static new void ClearLocalOverrides()
        {
            OverrideClickLocally = null;
            OverrideHoverLocally = null;
            OverrideInnerHtmlLocally = null;
            OverrideInnerTextLocally = null;
            OverrideHrefLocally = null;
            OverrideGetTargetLocally = null;
            OverrideGetRelLocally = null;
        }

        public override Type ElementType => GetType();

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
        public string Href
        {
            get
            {
                var action = InitializeAction(this, OverrideHrefGlobally, OverrideHrefLocally, DefaultHref);
                return action();
            }
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
        public string Target
        {
            get
            {
                var action = InitializeAction(this, OverrideGetTargetGlobally, OverrideGetTargetLocally, DefaultGetType);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Rel
        {
            get
            {
                var action = InitializeAction(this, OverrideGetRelGlobally, OverrideGetRelLocally, DefaultGetRel);
                return action();
            }
        }

        protected virtual void DefaultClick(Anchor anchor) => DefaultClick(anchor, Clicking, Clicked);

        protected virtual void DefaultHover(Anchor anchor) => DefaultHover(anchor, Hovering, Hovered);

        protected virtual string DefaultInnerHtml(Anchor anchor) => base.DefaultInnerHtml(anchor);

        protected virtual string DefaultInnerText(Anchor anchor) => base.DefaultInnerText(anchor);

        protected virtual string DefaultHref(Anchor anchor) => HttpUtility.HtmlDecode(HttpUtility.UrlDecode(GetAttribute("href")));

        protected virtual string DefaultGetType(Anchor anchor) => WrappedElement.GetAttribute("target");

        protected virtual string DefaultGetRel(Anchor anchor) => WrappedElement.GetAttribute("rel");
    }
}