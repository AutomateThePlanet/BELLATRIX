// 1. You need to add a using statement to the namespace where the new wait extension methods are situated.

using Bellatrix.Mobile.Android.GettingStarted.ExtensionMethodsWaitMethods;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.ReuseIfStarted)]
    public class AddNewElementWaitMethodsTests : MSTest.AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ButtonClicked_When_WaitToHaveSpecificContent()
        {
            // 2. After that, you can use the new wait method as it was originally part of Bellatrix.
            var button = App.Components.CreateByIdContaining<Button>("button").ToHaveSpecificContent("button");

            button.Click();
        }
    }
}