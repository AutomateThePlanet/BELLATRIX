﻿// <copyright file="TouchActionsServiceTests.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using Bellatrix.Mobile.Controls.IOS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.Tests
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.ReuseIfStarted)]
    [AllureSuite("Services")]
    [AllureFeature("TouchActionsService")]
    public class TouchActionsServiceTests : MSTest.IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementSwiped_When_CallSwipeByCoordinatesMethod()
        {
            var textField = App.ComponentCreateService.CreateById<TextField>("IntegerA");
            Point point = textField.Location;
            Size size = textField.Size;

            App.TouchActionsService.Swipe(
                point.X + 5,
                point.Y + 5,
                point.X + size.Width - 5,
                point.Y + size.Height - 5,
                200);

            App.TouchActionsService.Swipe(
                point.X + size.Width - 5,
                point.Y + 5,
                point.X + 5,
                point.Y + size.Height - 5,
                2000);
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementTaped_When_CallTap()
        {
            var buttons = App.ComponentCreateService.CreateAllByClass<Button>("XCUIElementTypeButton");

            App.TouchActionsService.Tap(buttons[0], 10).Perform();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinates()
        {
            var element = App.ComponentCreateService.CreateByName<IOSComponent>("AppElem");
            int end = element.Size.Width;
            int y = element.Location.Y;
            int moveTo = (9 / 100) * end;

            App.TouchActionsService.Press(moveTo, y, 0).Release().Perform();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinatesMultiAction()
        {
            var element = App.ComponentCreateService.CreateByName<IOSComponent>("AppElem");
            int end = element.Size.Width;
            int y = element.Location.Y;
            int moveTo = (9 / 100) * end;

            App.TouchActionsService.Press(moveTo, y, 0).Release();
            App.TouchActionsService.Perform();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void TwoTouchActionExecutedInOneMultiAction_When_CallPerformAllActions()
        {
            var buttons = App.ComponentCreateService.CreateAllByClass<Button>("XCUIElementTypeButton");

            App.TouchActionsService.Tap(buttons[0], 10);
            App.TouchActionsService.Tap(buttons[0], 10);
            App.TouchActionsService.Perform();

            App.TouchActionsService.Tap(buttons[0], 10);
            App.TouchActionsService.Tap(buttons[0], 10);
            App.TouchActionsService.Perform();
        }
    }
}
