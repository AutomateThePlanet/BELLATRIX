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
    public class OverrideGloballyElementActionsTests : AndroidTest
    {
        public override void TestsArrange()
        {
            // 1. Extensibility and customization are one of the biggest advantages of Bellatrix.
            // So, each BELLATRIX desktop control gives you the possibility to override its behaviour for the whole test run.
            // You need to initialise the static delegates- Override{MethodName}Globally.
            //
            // 2. Below we override the behaviour of the button control with an anonymous lambda function.
            // Instead of using clicking the button directly first wait to be clickable and scroll to be visible.
            Button.OverrideClickGlobally = (e) =>
            {
                e.ToExists().ToBeClickable().WaitToBe();
                ////e.ScrollToVisible(ScrollDirection.Down);
                e.Click();
            };

            // 3. Override the radio button Click method by assigning a local private function to the global delegate.
            // Note 1: Keep in mind that once the control is overridden globally, all tests call your custom logic, the default behaviour is gone.
            // Note 2: Usually, we assign the control overrides in the AssemblyInitialize method which is called once for a test run.
            RadioButton.OverrideClickGlobally = CustomClick;

            // 4. Here is a list of all global override Button delegates:
            // OverrideClickGlobally
            // OverrideGetTextGlobally
            // OverrideIsDisabledGlobally
        }

        private void CustomClick(RadioButton radioButton)
        {
            ////radioButton.ScrollToVisible(ScrollDirection.Down);
            Thread.Sleep(100);
            radioButton.Click();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            // The overridden click delegate is called.
            button.Click();
        }
    }
}