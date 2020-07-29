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
using Bellatrix.Desktop.Contracts;
using Bellatrix.Desktop.Events;
using Bellatrix.Layout;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Desktop
{
    public partial class Element : IElementVisible, IElement, ILayoutElement
    {
        protected virtual bool DefaultIsPresent(Element element)
        {
            try
            {
                var nativeElement = GetWebDriverElement(true);
                if (nativeElement != null)
                {
                    return true;
                }
            }
            catch (WebDriverException)
            {
                return false;
            }

            return false;
        }

        protected virtual bool DefaultIsVisible(Element element)
        {
            try
            {
                var nativeElement = GetWebDriverElement(true);
                if (nativeElement != null)
                {
                    return GetWebDriverElement(true).Displayed;
                }
            }
            catch (WebDriverException)
            {
                return false;
            }

            return false;
        }

        protected virtual string DefaultGetAttribute(Element element, string name)
        {
            var result = WrappedElement.GetAttribute(name);
            return result;
        }

        protected virtual void DefaultScrollToVisible(Element element)
        {
            ScrollingToVisible?.Invoke(this, new ElementActionEventArgs(element));

            var touchActions = new RemoteTouchScreen(WrappedDriver);
            System.Threading.Thread.Sleep(2000);
            touchActions.Scroll(WrappedElement.Coordinates, 0, 0);
            element.ToBeVisible().ToExists().WaitToBe();
            ScrolledToVisible?.Invoke(this, new ElementActionEventArgs(element));
        }

        internal void DefaultClick<TElement>(TElement element, EventHandler<ElementActionEventArgs> clicking, EventHandler<ElementActionEventArgs> clicked)
            where TElement : Element
        {
            clicking?.Invoke(this, new ElementActionEventArgs(element));

            element.WrappedElement.Click();

            clicked?.Invoke(this, new ElementActionEventArgs(element));
        }

        internal void DefaultHover<TElement>(TElement element, EventHandler<ElementActionEventArgs> hovering, EventHandler<ElementActionEventArgs> hovered)
            where TElement : Element
        {
            hovering?.Invoke(this, new ElementActionEventArgs(element));

            WrappedDriver.Mouse.MouseMove(element.WrappedElement.Coordinates);

            hovered?.Invoke(this, new ElementActionEventArgs(element));
        }

        internal string DefaultInnerText<TElement>(TElement element)
            where TElement : Element
            => WrappedElement.Text.Replace("\r\n", string.Empty);

        internal bool DefaultIsDisabled<TElement>(TElement element)
            where TElement : Element
            => !WrappedElement.Enabled;

        internal string DefaultGetText<TElement>(TElement element)
            where TElement : Element
            => WrappedElement.Text;

        internal void DefaultSetText<TElement>(TElement element, EventHandler<ElementActionEventArgs> settingValue, EventHandler<ElementActionEventArgs> valueSet, string value)
            where TElement : Element
        {
            settingValue?.Invoke(this, new ElementActionEventArgs(element, value));
            WrappedElement.Clear();
            WrappedElement.SendKeys(value);
            valueSet?.Invoke(this, new ElementActionEventArgs(element, value));
        }
    }
}