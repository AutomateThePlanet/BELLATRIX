// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Mobile.IOS.GettingStarted.Custom;

using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class ExtendExistingElementWithExtensionMethodsTests : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        // 2. Use the custom added submit button  with scroll-to-visible lifecycle.
        button.SubmitButtonWithScroll();
    }
}