using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, BrowserBehavior.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, BrowserBehavior.RestartEveryTime)]
    public class DialogServiceTests : WebTest
    {
        // 1. BELLATRIX gives you some methods for handling dialogs.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void AcceptDialogAlert()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/welcome/");

            var couponButton = App.ElementCreateService.CreateById<Button>("couponBtn");
            couponButton.Click();

            // 2. You can click on the OK button and handle the alert.
            App.DialogService.Handle();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void HappyBirthdayCouponDisplayed_When_ClickOnCouponButton()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/welcome/");

            var couponButton = App.ElementCreateService.CreateById<Button>("couponBtn");
            couponButton.Click();

            // 3. You can pass an anonymous lambda function and do something with the alert.
            ////App.DialogService.Handle((a) => Assert.AreEqual("Try the coupon- happybirthday", a.Text));
        }

        [TestMethod]
        [Ignore]
        public void DismissDialogAlert()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/welcome/");

            var couponButton = App.ElementCreateService.CreateById<Button>("couponBtn");
            couponButton.Click();

            // 4. You can tell the dialog service to click a different button.
            App.DialogService.Handle(dialogButton: DialogButton.Cancel);
        }
    }
}