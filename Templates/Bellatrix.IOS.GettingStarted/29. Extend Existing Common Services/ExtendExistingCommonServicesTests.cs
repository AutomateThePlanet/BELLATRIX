// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Mobile.IOS.GettingStarted.CommonServicesExtensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        AppBehavior.RestartEveryTime)]
    public class ExtendExistingCommonServicesTests : IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        [Ignore]
        public void ButtonClicked_When_CallClickMethod()
        {
            // 2. Use newly added login method which is not part of the original implementation of the common service.
            App.AppService.LoginToApp("bellatrix", "topSecret");

            var button = App.ElementCreateService.CreateByName<Button>("ComputeSumButton");

            button.Click();
        }
    }
}