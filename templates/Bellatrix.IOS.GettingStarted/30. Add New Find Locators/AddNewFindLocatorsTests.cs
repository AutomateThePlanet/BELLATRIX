// 1. You need to add a using statement to the namespace where the extension methods for new locator are situated.
using Bellatrix.Mobile.IOS.GettingStarted.ExtensionMethodsLocators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class AddNewFindLocatorsTests : MSTest.IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        public void ButtonClicked_When_CallClickMethod()
        {
            // 2. After that, you can use the new locator as it was originally part of Bellatrix.
            var button = App.Components.CreateByNameStartingWith<Button>("Compute");

            button.Click();
        }
    }
}