// <copyright file="TouchActionsServiceTests.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.Tests;

[TestClass]
[Android(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".ApiDemos",
    Lifecycle.RestartEveryTime)]
[AllureSuite("Services")]
[AllureFeature("TouchActionsService")]
public class TouchActionsServiceTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementSwiped_When_CallSwipeByCoordinatesMethod()
    {
        App.AppService.StartActivity(Constants.AndroidNativeAppId, ".graphics.FingerPaint");

        var textField = App.Components.CreateByIdContaining<TextField>("content");
        Point point = textField.Location;
        Size size = textField.Size;

        App.TouchActions.Swipe(
            point.X + 5,
            point.Y + 5,
            point.X + size.Width - 5,
            point.Y + size.Height - 5,
            200);

        App.TouchActions.Swipe(
            point.X + size.Width - 5,
            point.Y + 5,
            point.X + 5,
            point.Y + size.Height - 5,
            2000);
    }

    [TestMethod]
    public void ElementTaped_When_CallTap()
    {
        var elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");
        int initialCount = elements.Count();

        App.TouchActions.Tap(elements[6]).Perform();

        var consumerTextView = App.Components.CreateByText<TextField>("Consumer IR");
        consumerTextView.ToBeVisible().WaitToBe();

        elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");

        Assert.AreNotEqual(initialCount, elements.Count());
        Assert.AreEqual(2, elements.Count());
    }

    [TestMethod]
    public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinates()
    {
        var elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");
        var locationOne = elements[7].Location;
        var locationTwo = elements[1].Location;

        App.TouchActions.Press(locationOne.X, locationOne.Y, 100).
            MoveTo(locationTwo.X, locationTwo.Y).
            Release().
            Perform();

        elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");

        Assert.AreNotEqual(elements[7].Location.Y, elements[1].Location.Y);
    }

    [TestMethod]
    public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinatesMultiAction()
    {
        var elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");
        var locationOne = elements[7].Location;
        var locationTwo = elements[1].Location;

        var swipe = App.TouchActions.Press(locationOne.X, locationOne.Y, 100).
            MoveTo(locationTwo.X, locationTwo.Y).
            Release();
        App.TouchActions.Perform();

        elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");

        Assert.AreNotEqual(elements[7].Location.Y, elements[1].Location.Y);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.KnownIssue)]
    [Ignore]
    public void TwoTouchActionExecutedInOneMultiAction_When_CallPerformAllActions()
    {
        string originalActivity = App.AppService.CurrentActivity;

        var elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");

        App.TouchActions.Press(elements[5], 1500).Release();
        App.TouchActions.Press(elements[5], 1500).Release();
        App.TouchActions.Perform();
        elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");

        App.TouchActions.Press(elements[1], 1500).Release();
        App.TouchActions.Press(elements[1], 1500).Release();
        App.TouchActions.Perform();

        Assert.AreNotEqual(originalActivity, App.AppService.CurrentActivity);
    }
}