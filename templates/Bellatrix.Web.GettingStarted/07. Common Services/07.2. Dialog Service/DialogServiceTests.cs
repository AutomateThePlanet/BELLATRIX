using Bellatrix.Web.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    public class DialogServiceTests : MSTest.WebTest
    {
        // 1. BELLATRIX gives you some methods for handling dialogs.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void AcceptDialogAlert()
        {
            App.Navigation.Navigate("http://demos.bellatrix.solutions/welcome/");

            var couponButton = App.Components.CreateById<Button>("couponBtn");
            couponButton.Click();

            // 2. You can click on the OK button and handle the alert.
            App.Dialogs.Handle();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void HappyBirthdayCouponDisplayed_When_ClickOnCouponButton()
        {
            App.Navigation.Navigate("http://demos.bellatrix.solutions/welcome/");

            var couponButton = App.Components.CreateById<Button>("couponBtn");
            couponButton.Click();

            // 3. You can pass an anonymous lambda function and do something with the alert.
            ////App.Dialogs.Handle((a) => Assert.AreEqual("Try the coupon- happybirthday", a.Text));
        }

        [TestMethod]
        [Ignore]
        public void DismissDialogAlert()
        {
            App.Navigation.Navigate("http://demos.bellatrix.solutions/welcome/");

            var couponButton = App.Components.CreateById<Button>("couponBtn");
            couponButton.Click();

            // 4. You can tell the dialog service to click a different button.
            App.Dialogs.Handle(dialogButton: DialogButton.Cancel);
        }
    }
}