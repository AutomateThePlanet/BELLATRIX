// <copyright file="TouchActionsService.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium.Interactions;
using SI = OpenQA.Selenium.Interactions;

namespace Bellatrix.Mobile.Services;

public class TouchActionsService<TDriver, TDriverElement> : MobileService<TDriver, TDriverElement>
    where TDriver : AppiumDriver
    where TDriverElement : AppiumElement
{
    private readonly List<SI.ActionSequence> _actionSequences = new();

    public TouchActionsService(TDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public TouchActionsService<TDriver, TDriverElement> Tap<TComponent>(TComponent element, int count = 1)
        where TComponent : Component<TDriver, TDriverElement>
    {
        for (int i = 0; i < count; i++)
        {
            var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
            var sequence = new SI.ActionSequence(touchAction, 0);
            sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, element.Location.X, element.Location.Y, TimeSpan.Zero));
            sequence.AddAction(touchAction.CreatePointerDown(PointerButton.TouchContact));
            sequence.AddAction(touchAction.CreatePointerUp(PointerButton.TouchContact));
            _actionSequences.Add(sequence);
        }
        return this;
    }

    public TouchActionsService<TDriver, TDriverElement> Tap(int x, int y, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
            var sequence = new SI.ActionSequence(touchAction, 0);
            sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, x, y, TimeSpan.Zero));
            sequence.AddAction(touchAction.CreatePointerDown(PointerButton.TouchContact));
            sequence.AddAction(touchAction.CreatePointerUp(PointerButton.TouchContact));
            _actionSequences.Add(sequence);
        }
        return this;
    }

    public TouchActionsService<TDriver, TDriverElement> Press<TComponent>(TComponent element, int waitTimeSeconds = 0)
        where TComponent : Component<TDriver, TDriverElement>
    {
        var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
        var sequence = new SI.ActionSequence(touchAction, 0);
        sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, element.Location.X, element.Location.Y, TimeSpan.Zero));
        sequence.AddAction(touchAction.CreatePointerDown(PointerButton.TouchContact));
        //if (waitTimeSeconds > 0)
        //{
        //    sequence.AddAction(new SI.PauseAction(touchAction, TimeSpan.FromSeconds(waitTimeSeconds)));
        //}
        sequence.AddAction(touchAction.CreatePointerUp(PointerButton.TouchContact));
        _actionSequences.Add(sequence);
        return this;
    }

    public TouchActionsService<TDriver, TDriverElement> Press(int x, int y, int waitTimeSeconds = 0)
    {
        var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
        var sequence = new SI.ActionSequence(touchAction, 0);
        sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, x, y, TimeSpan.Zero));
        sequence.AddAction(touchAction.CreatePointerDown(PointerButton.TouchContact));
        //if (waitTimeSeconds > 0)
        //{
        //    sequence.AddAction(new PauseAction(touchAction, TimeSpan.FromSeconds(waitTimeSeconds)));
        //}
        sequence.AddAction(touchAction.CreatePointerUp(PointerButton.TouchContact));
        _actionSequences.Add(sequence);
        return this;
    }

    public TouchActionsService<TDriver, TDriverElement> LongPress<TComponent>(TComponent element, int waitTimeSeconds)
        where TComponent : Component<TDriver, TDriverElement>
    {
        var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
        var sequence = new SI.ActionSequence(touchAction, 0);
        sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, element.Location.X, element.Location.Y, TimeSpan.Zero));
        sequence.AddAction(touchAction.CreatePointerDown(PointerButton.TouchContact));
        //sequence.AddAction(new PauseAction(touchAction, TimeSpan.FromSeconds(waitTimeSeconds)));
        sequence.AddAction(touchAction.CreatePointerUp(PointerButton.TouchContact));
        _actionSequences.Add(sequence);
        return this;
    }

    public TouchActionsService<TDriver, TDriverElement> LongPress(int x, int y, int waitTimeSeconds)
    {
        var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
        var sequence = new SI.ActionSequence(touchAction, 0);
        sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, x, y, TimeSpan.Zero));
        sequence.AddAction(touchAction.CreatePointerDown(PointerButton.TouchContact));
        //sequence.AddAction(new PauseAction(touchAction, TimeSpan.FromSeconds(waitTimeSeconds)));
        sequence.AddAction(touchAction.CreatePointerUp(PointerButton.TouchContact));
        _actionSequences.Add(sequence);
        return this;
    }

    //public TouchActionsService<TDriver, TDriverElement> Wait(long waitTimeMilliseconds)
    //{
    //    var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
    //    var sequence = new SI.ActionSequence(touchAction, 0);
    //    sequence.AddAction(new PauseAction(touchAction, TimeSpan.FromMilliseconds(waitTimeMilliseconds)));
    //    _actionSequences.Add(sequence);
    //    return this;
    //}

    public TouchActionsService<TDriver, TDriverElement> MoveTo<TComponent>(TComponent element)
        where TComponent : Component<TDriver, TDriverElement>
    {
        var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
        var sequence = new SI.ActionSequence(touchAction, 0);
        sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, element.Location.X, element.Location.Y, TimeSpan.Zero));
        _actionSequences.Add(sequence);
        return this;
    }

    public TouchActionsService<TDriver, TDriverElement> MoveTo(int x, int y)
    {
        var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
        var sequence = new SI.ActionSequence(touchAction, 0);
        sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, x, y, TimeSpan.Zero));
        _actionSequences.Add(sequence);
        return this;
    }

    public TouchActionsService<TDriver, TDriverElement> Release()
    {
        // Release is handled by pointer up in the other actions.
        return this;
    }

    public TouchActionsService<TDriver, TDriverElement> Swipe(int startx, int starty, int endx, int endy, int duration)
    {
        var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
        var sequence = new SI.ActionSequence(touchAction, 0);
        sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, startx, starty, TimeSpan.Zero));
        sequence.AddAction(touchAction.CreatePointerDown(PointerButton.TouchContact));
        sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, endx, endy, TimeSpan.FromMilliseconds(duration)));
        sequence.AddAction(touchAction.CreatePointerUp(PointerButton.TouchContact));
        _actionSequences.Add(sequence);
        return this;
    }

    public TouchActionsService<TDriver, TDriverElement> Swipe<TComponent>(TComponent firstComponent, TComponent secondElement, int duration)
        where TComponent : Component<TDriver, TDriverElement>
    {
        var touchAction = new PointerInputDevice(SI.PointerKind.Touch);
        var sequence = new SI.ActionSequence(touchAction, 0);
        sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, firstComponent.Location.X, firstComponent.Location.Y, TimeSpan.Zero));
        sequence.AddAction(touchAction.CreatePointerDown(PointerButton.TouchContact));
        sequence.AddAction(touchAction.CreatePointerMove(SI.CoordinateOrigin.Viewport, secondElement.Location.X, secondElement.Location.Y, TimeSpan.FromMilliseconds(duration)));
        sequence.AddAction(touchAction.CreatePointerUp(PointerButton.TouchContact));
        _actionSequences.Add(sequence);
        return this;
    }

    public void Perform()
    {
        if (_actionSequences.Count > 0)
        {
            WrappedAppiumDriver.PerformActions(_actionSequences);
            _actionSequences.Clear();
        }
    }
}
