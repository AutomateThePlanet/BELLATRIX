// <copyright file="GridCell.cs" company="Automate The Planet Ltd.">
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
    public class GridCell : Element, IElementInnerText, IElementInnerHtml
    {
        public static Action<GridCell> OverrideClickGlobally;
        public static Action<GridCell> OverrideClickLocally;
        public static Action<GridCell> OverrideFocusGlobally;
        public static Action<GridCell> OverrideHoverGlobally;
        public static Func<GridCell, string> OverrideInnerTextGlobally;
        public static Func<GridCell, string> OverrideInnerHtmlGlobally;

        public static Action<GridCell> OverrideHoverLocally;
        public static Action<GridCell> OverrideFocusLocally;
        public static Func<GridCell, string> OverrideInnerTextLocally;
        public static Func<GridCell, string> OverrideInnerHtmlLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Focusing;
        public static event EventHandler<ElementActionEventArgs> Focused;
        public static event EventHandler<ElementActionEventArgs> Clicking;
        public static event EventHandler<ElementActionEventArgs> Clicked;

        public static new void ClearLocalOverrides()
        {
            OverrideInnerTextLocally = null;
            OverrideInnerHtmlLocally = null;
            OverrideHoverLocally = null;
            OverrideFocusLocally = null;
            OverrideClickLocally = null;
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

        public void Click()
        {
            var action = InitializeAction(this, OverrideClickGlobally, OverrideClickLocally, DefaultClick);
            action();
        }

        public Type CellControlElementType { get; set; }

        public dynamic CellControlBy { get; set; }

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

        public TElement As<TElement>()
            where TElement : Element, new()
        {
            CellControlElementType = typeof(TElement);
            if (CellControlBy == null)
            {
                object instance = Activator.CreateInstance(CellControlElementType);
                var byProperty = CellControlElementType.GetProperty("By");
                byProperty?.SetValue(instance, By, null);

                var wrappedElementProperty = CellControlElementType.GetProperty("WrappedElement");
                var wrappedElementParent = CellControlElementType.GetProperty("ParentWrappedElement");
                var wrappedElementIndex = CellControlElementType.GetProperty("ElementIndex");

                wrappedElementProperty?.SetValue(instance, WrappedElement, null);
                wrappedElementParent?.SetValue(instance, ParentWrappedElement, null);
                wrappedElementIndex?.SetValue(instance, ElementIndex, null);

                var isRefreshableElementProperty = CellControlElementType.GetProperty("ShouldCacheElement");
                isRefreshableElementProperty?.SetValue(instance, true, null);
                return instance as TElement;
            }
            else
            {
                return Create(CellControlBy, typeof(TElement));
            }
        }

        public dynamic As()
        {
            if (CellControlElementType == null)
            {
                CellControlElementType = typeof(Label);
            }

            if (CellControlBy == null)
            {
                object instance = Activator.CreateInstance(CellControlElementType);
                var byProperty = CellControlElementType.GetProperty("By");
                byProperty?.SetValue(instance, By, null);

                var wrappedElementProperty = CellControlElementType.GetProperty("WrappedElement");
                var wrappedElementParent = CellControlElementType.GetProperty("ParentWrappedElement");
                var wrappedElementIndex = CellControlElementType.GetProperty("ElementIndex");

                wrappedElementProperty?.SetValue(instance, WrappedElement, null);
                wrappedElementParent?.SetValue(instance, ParentWrappedElement, null);
                wrappedElementIndex?.SetValue(instance, ElementIndex, null);

                var isRefreshableElementProperty = CellControlElementType.GetProperty("ShouldCacheElement");
                isRefreshableElementProperty?.SetValue(instance, true, null);

                return instance;
            }
            else
            {
                return Create(CellControlBy, CellControlElementType);
            }
        }

        internal void DefaultClick<TElement>(
           TElement element,
           EventHandler<ElementActionEventArgs> clicking,
           EventHandler<ElementActionEventArgs> clicked)
           where TElement : Element
        {
            clicking?.Invoke(this, new ElementActionEventArgs(element));

            element.ToExists().ToBeClickable().WaitToBe();
            element.WrappedElement.Click();

            clicked?.Invoke(this, new ElementActionEventArgs(element));
        }

        protected virtual void DefaultClick(GridCell gridCell) => DefaultClick(gridCell, Clicking, Clicked);

        protected virtual void DefaultFocus(GridCell gridCell) => DefaultFocus(gridCell, Focusing, Focused);

        protected virtual void DefaultHover(GridCell gridCell) => DefaultHover(gridCell, Hovering, Hovered);

        protected virtual string DefaultInnerText(GridCell gridCell) => base.DefaultInnerText(gridCell);

        protected virtual string DefaultInnerHtml(GridCell gridCell) => base.DefaultInnerHtml(gridCell);
    }
}