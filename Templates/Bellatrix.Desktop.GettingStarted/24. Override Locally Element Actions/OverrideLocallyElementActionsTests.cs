using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [VideoRecording(VideoRecordingMode.OnlyFail)]
    [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
    public class OverrideLocallyElementActionsTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            // 1. Extensibility and customisation are one of the biggest advantages of Bellatrix.
            // So, each BELLATRIX desktop control gives you the possibility to override its behaviour locally for current test only.
            // You need to initialise the static delegates- Override{MethodName}Locally.
            // This may be useful to make a temporary fix only for certain page where the default behaviour is not working as expected.
            //
            // 2. Below we override the behaviour of the button control with an anonymous lambda function.
            // Instead of using clicking the button directly first wait to be clickable and scroll to be visible.
            Button.OverrideClickLocally = (e) =>
            {
                e.ToExists().ToBeClickable().WaitToBe();
                e.ScrollToVisible();
                e.Click();
            };

            // 3. Override the expander Click method by assigning a local private function to the local delegate.
            // Note 1: Keep in mind that once the control is overridden locally, after the test's execution the default behaviour is restored.
            // Note 2: In most cases, you can call the local override in some page object, directly in the test or in the TestInit method.
            // Note 3: The local override has precedence over the global override.
            Expander.OverrideClickLocally = CustomFocus;

            // 4. Here is a list of all local override Button delegates:
            // OverrideClickLocally
            // OverrideHoverLocally
            // OverrideInnerTextLocally
            // OverrideIsDisabledLocally
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            button.Hover();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }

        private void CustomFocus(Expander expander)
        {
            expander.ScrollToVisible();
            Thread.Sleep(100);
            expander.Click();
        }
    }
}