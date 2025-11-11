using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class WaiTComponentsTests : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ButtonClicked_When_ClickMethodCalled()
    {
        // 1. Besides the ToBe methods that you can use on element creation, you have a couple of other options if you need to wait for elements.
        // For example, if you want to reuse your element in multiple tests or if you use it through page objects (more about that in later chapters),
        // you may not want to wait for all conditions to be executed every time. Sometimes the mentioned conditions during creation may not be correct for some
        // specific test case. E.g. button wait to be disabled, but in most cases, you need to wait for it to be enabled. To give you more options Bellatrix
        // has a special method called WaitToBe. The big difference compared to ToBe methods is that it forces BELLATRIX to locate your element immediately
        // and wait for the condition to be satisfied.

        // This is also valid syntax the conditions are performed once the Click method is called. It is the same as placing ToBe methods after CreateByName.
        var button = App.Components.CreateByName<Button>("ComputeSumButton");
        button.ToBeClickable().ToBeVisible().Click();

        // 2. Why we have two different syntaxes for almost the same thing? Because sometimes you do not need to perform an action or assertion against the element.
        // In the above example, statement waits for the button to be clickable and visible before the click. However, in some cases, you want some element to show up
        // but not act on it. This means the above syntax does not help you since the element is not searched in the DOM at all because of the lazy loading.
        // Using the WaitToBe method forces BELLATRIX to locate your element and wait for the condition without the need to do an action or assertion.
        button.ToBeClickable().ToBeVisible().WaitToBe();
    }
}