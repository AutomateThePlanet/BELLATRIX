// 1. You need to add a using statement to the namespace where the new wait extension methods are situated.

using Bellatrix.Mobile.IOS.GettingStarted.ExtensionMethodsWaitMethods;

using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class AddNewElementWaitMethodsTests : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ClickButton_When_WaitForSpecificContent()
    {
        // 2. After that, you can use the new wait method as it was originally part of Bellatrix.
        var button = App.Components.CreateByName<Button>("ComputeSumButton").ToHaveSpecificContent("button");

        button.Click();
    }
}