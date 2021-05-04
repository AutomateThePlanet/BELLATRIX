// <copyright file="TouchActionsService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;

namespace Bellatrix.Mobile.Services
{
    public class TouchActionsService<TDriver, TDriverElement> : MobileService<TDriver, TDriverElement>
        where TDriver : AppiumDriver<TDriverElement>
        where TDriverElement : AppiumWebElement
    {
        public TouchActionsService(TDriver wrappedDriver)
            : base(wrappedDriver)
        {
            WrappedMultiAction = new MultiAction(wrappedDriver);
        }

        public IMultiAction WrappedMultiAction { get; }

        public TouchActionsService<TDriver, TDriverElement> Tap<TElement>(TElement element, int count = 1)
            where TElement : Component<TDriver, TDriverElement>
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).Tap(element.Location.X, element.Location.Y, count));

            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> Tap(int x, int y, int count = 1)
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).Tap(x, y, count));
            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> Press<TElement>(TElement element, int waitTimeSeconds = 0)
            where TElement : Component<TDriver, TDriverElement>
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).Press(element.Location.X, element.Location.Y).Wait(TimeSpan.FromSeconds(waitTimeSeconds).Milliseconds));
            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> Press(int x, int y, int waitTimeSeconds = 0)
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).Press(x, y).Wait(TimeSpan.FromSeconds(waitTimeSeconds).Milliseconds));
            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> LongPress<TElement>(TElement element, int waitTimeSeconds)
            where TElement : Component<TDriver, TDriverElement>
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).LongPress(element.Location.X, element.Location.Y).Wait(TimeSpan.FromSeconds(waitTimeSeconds).Milliseconds));
            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> LongPress(int x, int y, int waitTimeSeconds)
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).LongPress(x, y).Wait(TimeSpan.FromSeconds(waitTimeSeconds).Milliseconds));
            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> Wait(long waitTimeMilliseconds)
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).Wait(waitTimeMilliseconds));
            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> MoveTo<TElement>(TElement element)
            where TElement : Component<TDriver, TDriverElement>
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).MoveTo(element.Location.X, element.Location.Y));
            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> MoveTo(int x, int y)
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).MoveTo(x, y));
            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> Release()
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).Release());
            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> Swipe(int startx, int starty, int endx, int endy, int duration)
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).Press(startx, starty).Wait(duration).MoveTo(endx, endy).Release());
            return this;
        }

        public TouchActionsService<TDriver, TDriverElement> Swipe<TElement>(TElement firstElement, TElement secondElement, int duration)
            where TElement : Component<TDriver, TDriverElement>
        {
            WrappedMultiAction.Add(new TouchAction(WrappedAppiumDriver).Press(firstElement.Location.X, firstElement.Location.Y).Wait(duration).MoveTo(secondElement.Location.X, secondElement.Location.Y).Release());
            return this;
        }

        public void Perform()
        {
            WrappedMultiAction.Perform();
        }
    }
}