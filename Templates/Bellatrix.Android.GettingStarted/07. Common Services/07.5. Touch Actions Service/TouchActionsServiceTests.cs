using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".ApiDemos",
        Lifecycle.RestartEveryTime)]
    public class TouchActionsServiceTests : MSTest.AndroidTest
    {
        // 1. BELLATRIX gives you an interface for easier work with touch actions through TouchActionsService.
        // Performing series of touch actions can be one of the most complicated jobs in automating mobile apps.
        // BELLATRIX touch APIs are simplified and made to be user-friendly as possible.
        // Their usage can eliminate lots of code duplication and boilerplate code.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementSwiped_When_CallSwipeByCoordinatesMethod()
        {
            App.AppService.StartActivity("com.example.android.apis", ".graphics.FingerPaint");

            var textField = App.ElementCreateService.CreateByIdContaining<TextField>("content");
            Point point = textField.Location;
            Size size = textField.Size;

            // Performs swipe by using coordinates.
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
        public void ElementTaped_When_CallTap()
        {
            var elements = App.ElementCreateService.CreateAllByClass<TextField>("android.widget.TextView");
            int initialCount = elements.Count();

            // Tap 10 times using BELLATRIX UI element directly.
            App.TouchActionsService.Tap(elements[6]).Perform();
        }

        [TestMethod]
        public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinates()
        {
            var elements = App.ElementCreateService.CreateAllByClass<TextField>("android.widget.TextView");
            var locationOne = elements[7].Location;
            var locationTwo = elements[1].Location;

            // Performs a series of actions using elements coordinates.
            App.TouchActionsService.Press(locationOne.X, locationOne.Y, 100).
                MoveTo(locationTwo.X, locationTwo.Y).
                Release().
                Perform();

            elements = App.ElementCreateService.CreateAllByClass<TextField>("android.widget.TextView");

            Assert.AreNotEqual(elements[7].Location.Y, elements[1].Location.Y);
        }

        [TestMethod]
        public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinatesMultiAction()
        {
            var elements = App.ElementCreateService.CreateAllByClass<TextField>("android.widget.TextView");
            var locationOne = elements[7].Location;
            var locationTwo = elements[1].Location;

            // Performs multiple actions.
            App.TouchActionsService.Press(locationOne.X, locationOne.Y, 100).
                MoveTo(locationTwo.X, locationTwo.Y).
                Release().
                Perform();

            elements = App.ElementCreateService.CreateAllByClass<TextField>("android.widget.TextView");

            Assert.AreNotEqual(elements[7].Location.Y, elements[1].Location.Y);
        }

        [TestMethod]
        [Ignore]
        public void TwoTouchActionExecutedInOneMultiAction_When_CallPerformAllActions()
        {
            string originalActivity = App.AppService.CurrentActivity;

            var elements = App.ElementCreateService.CreateAllByClass<TextField>("android.widget.TextView");

            // Executes two multi actions.
            App.TouchActionsService.Press(elements[5], 1500).Release();
            App.TouchActionsService.Press(elements[5], 1500).Release();
            App.TouchActionsService.Perform();
            elements = App.ElementCreateService.CreateAllByClass<TextField>("android.widget.TextView");

            App.TouchActionsService.Press(elements[1], 1500).Release();
            App.TouchActionsService.Press(elements[1], 1500).Release();
            App.TouchActionsService.Perform();

            Assert.AreNotEqual(originalActivity, App.AppService.CurrentActivity);
        }
    }
}