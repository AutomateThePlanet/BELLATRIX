using System.Drawing;
using Bellatrix.Mobile.Controls.IOS;
using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class TouchActionsServiceTests : NUnit.IOSTest
{
    // 1. BELLATRIX gives you an interface for easier work with touch actions through TouchActionsService.
    // Performing series of touch actions can be one of the most complicated jobs in automating mobile apps.
    // BELLATRIX touch APIs are simplified and made to be user-friendly as possible.
    // Their usage can eliminate lots of code duplication and boilerplate code.
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ElementSwiped_When_CallSwipeByCoordinatesMethod()
    {
        var textField = App.Components.CreateById<TextField>("IntegerA");
        Point point = textField.Location;
        Size size = textField.Size;

        // Performs swipe by using coordinates.
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

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ElementTaped_When_CallTap()
    {
        var buttons = App.Components.CreateAllByClass<Button>("XCUIElementTypeButton");

        // Tap 10 times using BELLATRIX UI element directly.
        App.TouchActions.Tap(buttons[0], 10).Perform();
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinates()
    {
        var element = App.Components.CreateByName<IOSComponent>("AppElem");
        int end = element.Size.Width;
        int y = element.Location.Y;
        int moveTo = (9 / 100) * end;

        // Performs a series of actions using elements coordinates.
        App.TouchActions.Press(moveTo, y, 0).Release().Perform();
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinatesMultiAction()
    {
        var element = App.Components.CreateByName<IOSComponent>("AppElem");
        int end = element.Size.Width;
        int y = element.Location.Y;
        int moveTo = (9 / 100) * end;

        // Performs multiple actions.
        App.TouchActions.Press(moveTo, y, 0).Release();
        App.TouchActions.Perform();
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void TwoTouchActionExecutedInOneMultiAction_When_CallPerformAllActions()
    {
        var buttons = App.Components.CreateAllByClass<Button>("XCUIElementTypeButton");

        // Executes two multi actions.
        App.TouchActions.Tap(buttons[0], 10);
        App.TouchActions.Tap(buttons[0], 10);
        App.TouchActions.Perform();

        App.TouchActions.Tap(buttons[0], 10);
        App.TouchActions.Tap(buttons[0], 10);
        App.TouchActions.Perform();
    }
}