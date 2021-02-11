using System.Drawing;
using Bellatrix.Mobile.Controls.IOS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class TouchActionsServiceTests : MSTest.IOSTest
    {
        // 1. BELLATRIX gives you an interface for easier work with touch actions through TouchActionsService.
        // Performing series of touch actions can be one of the most complicated jobs in automating mobile apps.
        // BELLATRIX touch APIs are simplified and made to be user-friendly as possible.
        // Their usage can eliminate lots of code duplication and boilerplate code.
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementSwiped_When_CallSwipeByCoordinatesMethod()
        {
            var textField = App.ElementCreateService.CreateById<TextField>("IntegerA");
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
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementTaped_When_CallTap()
        {
            var buttons = App.ElementCreateService.CreateAllByClass<Button>("XCUIElementTypeButton");

            // Tap 10 times using BELLATRIX UI element directly.
            App.TouchActionsService.Tap(buttons[0], 10).Perform();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinates()
        {
            var element = App.ElementCreateService.CreateByName<Element>("AppElem");
            int end = element.Size.Width;
            int y = element.Location.Y;
            int moveTo = (9 / 100) * end;

            // Performs a series of actions using elements coordinates.
            App.TouchActionsService.Press(moveTo, y, 0).Release().Perform();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementSwiped_When_CallPressWaitMoveToAndReleaseByCoordinatesMultiAction()
        {
            var element = App.ElementCreateService.CreateByName<Element>("AppElem");
            int end = element.Size.Width;
            int y = element.Location.Y;
            int moveTo = (9 / 100) * end;

            // Performs multiple actions.
            App.TouchActionsService.Press(moveTo, y, 0).Release();
            App.TouchActionsService.Perform();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void TwoTouchActionExecutedInOneMultiAction_When_CallPerformAllActions()
        {
            var buttons = App.ElementCreateService.CreateAllByClass<Button>("XCUIElementTypeButton");

            // Executes two multi actions.
            App.TouchActionsService.Tap(buttons[0], 10);
            App.TouchActionsService.Tap(buttons[0], 10);
            App.TouchActionsService.Perform();

            App.TouchActionsService.Tap(buttons[0], 10);
            App.TouchActionsService.Tap(buttons[0], 10);
            App.TouchActionsService.Perform();
        }
    }
}