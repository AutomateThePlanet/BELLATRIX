// 1. You need to add a using statement to the namespace where the extension methods for new locator are situated.
// ReSharper disable once RedundantUsingDirective
using Bellatrix.Mobile.Android.GettingStarted.ExtensionMethodsLocators;

using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestFixture]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.ReuseIfStarted)]
    public class AddNewFindLocatorsTests : MSTest.AndroidTest
    {
        [Test]
        [Category(Categories.CI)]
        public void ButtonClicked_When_CallClickMethod()
        {
            // 2. After that, you can use the new locator as it was originally part of Bellatrix.
            var button = App.Components.CreateByIdStartingWith<Button>("button");

            button.Click();
        }
    }
}