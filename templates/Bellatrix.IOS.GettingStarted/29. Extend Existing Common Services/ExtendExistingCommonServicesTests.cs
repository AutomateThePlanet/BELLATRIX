// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Mobile.IOS.GettingStarted.CommonServicesExtensions;

using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class ExtendExistingCommonServicesTests : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    [Ignore("API example purposes only. No need to run.")]
    public void ButtonClicked_When_CallClickMethod()
    {
        // 2. Use newly added login method which is not part of the original implementation of the common service.
        App.AppService.LoginToApp("bellatrix", "topSecret");

        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();
    }
}