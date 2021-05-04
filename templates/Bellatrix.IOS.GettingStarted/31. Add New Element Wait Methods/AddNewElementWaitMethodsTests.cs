// 1. You need to add a using statement to the namespace where the new wait extension methods are situated.

using Bellatrix.Mobile.IOS.GettingStarted.ExtensionMethodsWaitMethods;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class AddNewElementWaitMethodsTests : MSTest.IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ClickButton_When_WaitForSpecificContent()
        {
            // 2. After that, you can use the new wait method as it was originally part of Bellatrix.
            var button = App.ComponentCreateService.CreateByName<Button>("ComputeSumButton").ToHaveSpecificContent("button");

            button.Click();
        }
    }
}