// <copyright file="TouchService.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Bellatrix.Web
{
    public class TouchService : WebService
    {
        public TouchService(IWebDriver wrappedDriver)
            : base(wrappedDriver)
        {
            WrappedTouchActions = new TouchActions(wrappedDriver);
        }

        public TouchActions WrappedTouchActions { get; }

        public TouchService Swipe(Component element)
        {
            WrappedTouchActions.Flick(element.WrappedElement, -200, 0, 10);
            return this;
        }

        public TouchService Tap(Component element)
        {
            IAction builtAction = WrappedTouchActions.SingleTap(element.WrappedElement);
            return this;
        }

        public TouchService DoubleTap(Component element)
        {
            IAction builtAction = WrappedTouchActions.DoubleTap(element.WrappedElement);
            return this;
        }

        private TouchService Scroll(Component element, int xOffset, int yOffset)
        {
            IAction builtAction = WrappedTouchActions.Scroll(element.WrappedElement, xOffset, yOffset);
            return this;
        }

        private TouchService Scroll(int xOffset, int yOffset)
        {
            IAction builtAction = WrappedTouchActions.Scroll(xOffset, yOffset);
            return this;
        }

        public TouchService ScrollPage(Component fromElement, Component toElement)
        {
            Scroll(fromElement, 0, toElement.Location.Y - fromElement.Location.Y);
            return this;
        }

        public void Perform()
        {
            WrappedTouchActions.Build().Perform();
        }
    }
}