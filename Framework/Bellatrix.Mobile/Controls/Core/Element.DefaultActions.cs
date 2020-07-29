// <copyright file="Element.DefaultActions.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using Bellatrix.Layout;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium;

namespace Bellatrix.Mobile.Core
{
    public partial class Element<TDriver, TDriverElement> : IElementVisible, ILayoutElement
    {
        protected virtual bool DefaultIsPresent(Element<TDriver, TDriverElement> element)
        {
            try
            {
                var nativeElement = GetWebDriverElement(true);
                if (nativeElement != null)
                {
                    return true;
                }
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return false;
        }

        protected virtual bool DefaultIsVisible(Element<TDriver, TDriverElement> element)
        {
            try
            {
                var nativeElement = GetWebDriverElement(true);
                if (nativeElement != null)
                {
                    return GetWebDriverElement(true).Displayed;
                }
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return false;
        }

        protected virtual string DefaultGetAttribute(Element<TDriver, TDriverElement> element, string name)
        {
            var result = WrappedElement.GetAttribute(name);
            return result;
        }

        protected virtual bool DefaultScrollToVisible(Element<TDriver, TDriverElement> element, ScrollDirection direction)
        {
            ScrollingToVisible?.Invoke(this, new ElementActionEventArgs<TDriverElement>(element));
            int timeOut = 10000;

            while (!element.IsVisible && timeOut > 0)
            {
                MobileScroll(direction);
                timeOut -= 30;
            }

            bool isFound = element.IsVisible;
            ScrolledToVisible?.Invoke(this, new ElementActionEventArgs<TDriverElement>(element));

            return isFound;
        }

        internal void DefaultClick<TElement>(TElement element, EventHandler<ElementActionEventArgs<TDriverElement>> clicking, EventHandler<ElementActionEventArgs<TDriverElement>> clicked)
            where TElement : Element<TDriver, TDriverElement>
        {
            clicking?.Invoke(this, new ElementActionEventArgs<TDriverElement>(element));

            element.WrappedElement.Click();

            clicked?.Invoke(this, new ElementActionEventArgs<TDriverElement>(element));
        }

        internal bool DefaultIsCheckedValue<TElement>(TElement element)
            where TElement : Element<TDriver, TDriverElement>
        {
            string value = DefaultGetAttribute(this, "value");
            if (value == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool DefaultIsChecked<TElement>(TElement element)
            where TElement : Element<TDriver, TDriverElement>
            => bool.Parse(DefaultGetAttribute(this, "checked"));

        internal string DefaultGetText<TElement>(TElement element)
            where TElement : Element<TDriver, TDriverElement>
            => WrappedElement.Text.Replace("\r\n", string.Empty);

        internal string DefaultGetValue<TElement>(TElement element)
            where TElement : Element<TDriver, TDriverElement>
            => DefaultGetAttribute(this, "value");

        internal bool DefaultIsDisabled<TElement>(TElement element)
            where TElement : Element<TDriver, TDriverElement>
            => !WrappedElement.Enabled;

        internal void DefaultSetValue<TElement>(TElement element, EventHandler<ElementActionEventArgs<TDriverElement>> settingValue, EventHandler<ElementActionEventArgs<TDriverElement>> valueSet, string value)
            where TElement : Element<TDriver, TDriverElement>
        {
            settingValue?.Invoke(this, new ElementActionEventArgs<TDriverElement>(element, value));
            WrappedElement.Clear();
            WrappedElement.SetImmediateValue(value);
            valueSet?.Invoke(this, new ElementActionEventArgs<TDriverElement>(element, value));
        }

        internal void DefaultSetText<TElement>(TElement element, EventHandler<ElementActionEventArgs<TDriverElement>> settingValue, EventHandler<ElementActionEventArgs<TDriverElement>> valueSet, string value)
            where TElement : Element<TDriver, TDriverElement>
        {
            settingValue?.Invoke(this, new ElementActionEventArgs<TDriverElement>(element, value));
            WrappedElement.Clear();
            WrappedElement.SendKeys(value);

            try
            {
                WrappedDriver.HideKeyboard();
            }
            catch
            {
                // ignore
            }

            valueSet?.Invoke(this, new ElementActionEventArgs<TDriverElement>(element, value));
        }

        private void MobileScroll(ScrollDirection direction)
        {
            var js = (IJavaScriptExecutor)WrappedDriver;
            var swipeObject = new Dictionary<string, string>();
            switch (direction)
            {
                case ScrollDirection.Up:
                    swipeObject.Add("direction", "up");
                    break;
                case ScrollDirection.Down:
                    swipeObject.Add("direction", "down");
                    break;
                case ScrollDirection.Right:
                    swipeObject.Add("direction", "right");
                    break;
                case ScrollDirection.Left:
                    swipeObject.Add("direction", "left");
                    break;
            }

            swipeObject.Add("element", WrappedElement.Id);
            js.ExecuteScript("mobile:swipe", swipeObject);
        }
    }
}