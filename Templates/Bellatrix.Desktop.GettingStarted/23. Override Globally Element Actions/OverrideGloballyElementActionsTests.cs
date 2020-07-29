using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [VideoRecording(VideoRecordingMode.OnlyFail)]
    [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
    public class OverrideGloballyElementActionsTests : DesktopTest
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
                e.ScrollToVisible();
                e.Click();
            };

            // 3. Override the expander Click method by assigning a local private function to the global delegate.
            // Note 1: Keep in mind that once the control is overridden globally, all tests call your custom logic, the default behaviour is gone.
            // Note 2: Usually, we assign the control overrides in the AssemblyInitialize method which is called once for a test run.
            Expander.OverrideClickGlobally = CustomClick;

            // 4. Here is a list of all global override Button delegates:
            // OverrideClickGlobally
            // OverrideHoverGlobally
            // OverrideInnerTextGlobally
            // OverrideIsDisabledGlobally
        }

        private void CustomClick(Expander expander)
        {
            expander.ScrollToVisible();
            Thread.Sleep(100);
            expander.Click();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            button.Hover();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }
    }
}