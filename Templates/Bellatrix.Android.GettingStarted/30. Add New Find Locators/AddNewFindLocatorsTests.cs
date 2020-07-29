// 1. You need to add a using statement to the namespace where the extension methods for new locator are situated.
// ReSharper disable once RedundantUsingDirective
using Bellatrix.Mobile.Android.GettingStarted.ExtensionMethodsLocators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        AppBehavior.ReuseIfStarted)]
    public class AddNewFindLocatorsTests : AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ButtonClicked_When_CallClickMethod()
        {
            // 2. After that, you can use the new locator as it was originally part of Bellatrix.
            var button = App.ElementCreateService.CreateByIdStartingWith<Button>("button");

            button.Click();
        }
    }
}