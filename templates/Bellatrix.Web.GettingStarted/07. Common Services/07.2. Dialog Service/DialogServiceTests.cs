using Bellatrix.Web.MSTest;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class DialogServiceTests : NUnit.WebTest
{
    // 1. BELLATRIX gives you some methods for handling dialogs.
    [Test]
    [Category(Categories.CI)]
    public void AcceptDialogAlert()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/welcome/");

        var couponButton = App.Components.CreateById<Button>("couponBtn");
        couponButton.Click();

        // 2. You can click on the OK button and handle the alert.
        App.Dialogs.Handle();
    }

    [Test]
    [Category(Categories.CI)]
    public void HappyBirthdayCouponDisplayed_When_ClickOnCouponButton()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/welcome/");

        var couponButton = App.Components.CreateById<Button>("couponBtn");
        couponButton.Click();

        // 3. You can pass an anonymous lambda function and do something with the alert.
        ////App.Dialogs.Handle((a) => Assert.AreEqual("Try the coupon- happybirthday", a.Text));
    }

    [Test]
    [Ignore("no need to run")]
    public void DismissDialogAlert()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/welcome/");

        var couponButton = App.Components.CreateById<Button>("couponBtn");
        couponButton.Click();

        // 4. You can tell the dialog service to click a different button.
        App.Dialogs.Handle(dialogButton: DialogButton.Cancel);
    }
}