using System.Threading;
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
    public class OverrideLocallyElementActionsTests : AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ExecuteOverridenClick()
        {
            // 1. Extensibility and customization are one of the biggest advantages of Bellatrix.
            // So, each BELLATRIX Android control gives you the possibility to override its behaviour locally for current test only.
            // You need to initialise the static delegates- Override{MethodName}Locally.
            // This may be useful to make a temporary fix only for certain page where the default behaviour is not working as expected.
            //
            // 2. Below we override the behaviour of the button control with an anonymous lambda function.
            // Instead of using clicking the button directly first wait to be clickable and scroll to be visible.
            Button.OverrideClickLocally = (e) =>
            {
                e.ToExists().ToBeClickable().WaitToBe();
                ////e.ScrollToVisible(ScrollDirection.Down);
                e.Click();
            };

            // 3. Override the radio button Click method by assigning a local private function to the local delegate.
            // Note 1: Keep in mind that once the control is overridden locally, after the test's execution the default behaviour is restored.
            // Note 2: In most cases, you can call the local override in some page object, directly in the test or in the TestInit method.
            // Note 3: The local override has precedence over the global override.
            RadioButton.OverrideClickLocally = CustomClick;

            // 4. Here is a list of all local override Button delegates:
            // OverrideClickLocally
            // OverrideGetTextLocally
            // OverrideIsDisabledLocally
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            // The overridden click delegate is called.
            button.Click();
        }

        private void CustomClick(RadioButton radioButton)
        {
            ////radioButton.ScrollToVisible(ScrollDirection.Down);
            Thread.Sleep(100);
            radioButton.Click();
        }
    }
}