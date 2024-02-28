// <copyright file="InteractionsService.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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

namespace Bellatrix.Web;

/// <summary>
/// Provides a mechanism for building advanced interactions with the browser.
/// </summary>
public class InteractionsService : WebService
{
    public InteractionsService(IWebDriver wrappedDriver)
        : base(wrappedDriver)
    {
        WrappedActions = new Actions(wrappedDriver);
    }

    public Actions WrappedActions { get; }

    /// <summary>
    /// Releases the mouse button at the last known mouse coordinates.
    /// </summary>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService Release()
    {
        WrappedActions.Release();
        return this;
    }

    /// <summary>Releases the mouse button on the specified element.</summary>
    /// <param name="element">The element on which to release the button.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService Release(Component element)
    {
        WrappedActions.Release(element.WrappedElement);
        return this;
    }

    /// <summary>Sends a modifier key down message to the browser.</summary>
    /// <param name="theKey">The key to be sent.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService KeyDown(string theKey)
    {
        WrappedActions.KeyDown(theKey);
        return this;
    }

    /// <summary>
    /// Sends a modifier key down message to the specified element in the browser.
    /// </summary>
    /// <param name="element">The element to which to send the key command.</param>
    /// <param name="theKey">The key to be sent.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService KeyDown(Component element, string theKey)
    {
        WrappedActions.KeyDown(element.WrappedElement, theKey);
        return this;
    }

    /// <summary>Sends a modifier key up message to the browser.</summary>
    /// <param name="theKey">The key to be sent.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService KeyUp(string theKey)
    {
        WrappedActions.KeyUp(theKey);
        return this;
    }

    /// <summary>
    /// Sends a modifier up down message to the specified element in the browser.
    /// </summary>
    /// <param name="element">The element to which to send the key command.</param>
    /// <param name="theKey">The key to be sent.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService KeyUp(Component element, string theKey)
    {
        WrappedActions.KeyUp(element.WrappedElement, theKey);
        return this;
    }

    /// <summary>Sends a sequence of keystrokes to the browser.</summary>
    /// <param name="keysToSend">The keystrokes to send to the browser.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService SendKeys(string keysToSend)
    {
        WrappedActions.SendKeys(keysToSend);
        return this;
    }

    /// <summary>
    /// Sends a sequence of keystrokes to the specified element in the browser.
    /// </summary>
    /// <param name="element">The element to which to send the keystrokes.</param>
    /// <param name="keysToSend">The keystrokes to send to the browser.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService SendKeys(Component element, string keysToSend)
    {
        WrappedActions.SendKeys(element.WrappedElement, keysToSend);
        return this;
    }

    /// <summary>Clicks the mouse at the last known mouse coordinates.</summary>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService Click()
    {
        WrappedActions.Click();
        return this;
    }

    /// <summary>Clicks the mouse on the specified element.</summary>
    /// <param name="element">The element on which to click.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService Click(Component element)
    {
        WrappedActions.Click(element.WrappedElement);
        return this;
    }

    /// <summary>
    /// Clicks and holds the mouse button at the last known mouse coordinates.
    /// </summary>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService ClickAndHold()
    {
        WrappedActions.ClickAndHold();
        return this;
    }

    /// <summary>
    /// Clicks and holds the mouse button down on the specified element.
    /// </summary>
    /// <param name="element">The element on which to click and hold.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService ClickAndHold(Component element)
    {
        WrappedActions.ClickAndHold(element.WrappedElement);
        return this;
    }

    /// <summary>
    /// Double-clicks the mouse at the last known mouse coordinates.
    /// </summary>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService DoubleClick()
    {
        WrappedActions.DoubleClick();
        return this;
    }

    /// <summary>Double-clicks the mouse on the specified element.</summary>
    /// <param name="element">The element on which to double-click.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService DoubleClick(Component element)
    {
        WrappedActions.DoubleClick(element.WrappedElement);
        return this;
    }

    /// <summary>
    /// Right-clicks the mouse at the last known mouse coordinates.
    /// </summary>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService ContextClick()
    {
        WrappedActions.ContextClick();
        return this;
    }

    /// <summary>Right-clicks the mouse on the specified element.</summary>
    /// <param name="element">The element on which to right-click.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService ContextClick(Component element)
    {
        WrappedActions.ContextClick(element.WrappedElement);
        return this;
    }

    /// <summary>
    /// Performs a drag-and-drop operation from one element to another.
    /// </summary>
    /// <param name="sourceElement">The element on which the drag operation is started.</param>
    /// <param name="destinationElement">The element on which the drop is performed.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService DragAndDrop(Component sourceElement, Component destinationElement)
    {
        WrappedActions.DragAndDrop(sourceElement.WrappedElement, destinationElement.WrappedElement);
        return this;
    }

    /// <summary>
    /// Performs a drag-and-drop operation on one element to a specified offset.
    /// </summary>
    /// <param name="sourceElement">The element on which the drag operation is started.</param>
    /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
    /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService DragAndDrop(Component sourceElement, int offsetX, int offsetY)
    {
        WrappedActions.DragAndDropToOffset(sourceElement.WrappedElement, offsetX, offsetY);
        return this;
    }

    /// <summary>Moves the mouse to the specified element.</summary>
    /// <param name="element">The element to which to move the mouse.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService MoveToElement(Component element)
    {
        WrappedActions.MoveToElement(element.WrappedElement);
        return this;
    }

    /// <summary>
    /// Moves the mouse to the specified offset of the top-left corner of the specified element.
    /// </summary>
    /// <param name="sourceElement">The element to which to move the mouse.</param>
    /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
    /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService MoveToElement(Component sourceElement, int offsetX, int offsetY)
    {
        WrappedActions.MoveToElement(sourceElement.WrappedElement, offsetX, offsetY);
        return this;
    }

    /// <summary>
    /// Moves the mouse to the specified offset of the last known mouse coordinates.
    /// </summary>
    /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
    /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
    /// <returns>A self-reference to this <see cref="Bellatrix.Web.InteractionsService" />.</returns>
    public InteractionsService MoveByOffset(int offsetX, int offsetY)
    {
        WrappedActions.MoveByOffset(offsetX, offsetY);
        return this;
    }

    public void Perform()
    {
        WrappedActions.Build().Perform();
    }
}