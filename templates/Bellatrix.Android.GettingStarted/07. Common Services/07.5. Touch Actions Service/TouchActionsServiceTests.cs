using System.Drawing;
using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]
[Android(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".ApiDemos",
    Lifecycle.RestartEveryTime)]
public class TouchActionsServiceTests : NUnit.AndroidTest
{
    // 1. BELLATRIX gives you an interface for easier work with touch actions through TouchActionsService.
    // Performing series of touch actions can be one of the most complicated jobs in automating mobile apps.
    // BELLATRIX touch APIs are simplified and made to be user-friendly as possible.
    // Their usage can eliminate lots of code duplication and boilerplate code.
    [Test]
    [Category(Categories.CI)]
    public void ElementSwiped_When_CallSwipeByCoordinatesMethod()
    {
        App.AppService.StartActivity("com.example.android.apis", ".graphics.FingerPaint");

        var textField = App.Components.CreateByIdContaining<TextField>("content");
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
    public void ElementTaped_When_CallTap()
    {
        var elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");
        int initialCount = elements.Count();

        // Tap 10 times using BELLATRIX UI element directly.
        App.TouchActions.Tap(elements[6]).Perform();
    }

    [Test]
    public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinates()
    {
        var elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");
        var locationOne = elements[7].Location;
        var locationTwo = elements[1].Location;

        // Performs a series of actions using elements coordinates.
        App.TouchActions.Press(locationOne.X, locationOne.Y, 100).
            MoveTo(locationTwo.X, locationTwo.Y).
            Release().
            Perform();

        elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");

        Assert.That(!elements[7].Location.Y.Equals(elements[1].Location.Y));
    }

    [Test]
    public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinatesMultiAction()
    {
        var elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");
        var locationOne = elements[7].Location;
        var locationTwo = elements[1].Location;

        // Performs multiple actions.
        App.TouchActions.Press(locationOne.X, locationOne.Y, 100).
            MoveTo(locationTwo.X, locationTwo.Y).
            Release().
            Perform();

        elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");

        Assert.That(!elements[7].Location.Y.Equals(elements[1].Location.Y));
    }

    [Test]
    [Ignore("API example purposes only. No need to run.")]
    public void TwoTouchActionExecutedInOneMultiAction_When_CallPerformAllActions()
    {
        string originalActivity = App.AppService.CurrentActivity;

        var elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");

        // Executes two multi actions.
        App.TouchActions.Press(elements[5], 1500).Release();
        App.TouchActions.Press(elements[5], 1500).Release();
        App.TouchActions.Perform();
        elements = App.Components.CreateAllByClass<TextField>("android.widget.TextView");

        App.TouchActions.Press(elements[1], 1500).Release();
        App.TouchActions.Press(elements[1], 1500).Release();
        App.TouchActions.Perform();

        Assert.That(originalActivity.Equals(App.AppService.CurrentActivity));
    }
}