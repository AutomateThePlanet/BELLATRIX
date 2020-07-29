// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Desktop.GettingStarted.Advanced.Elements.Extension.Methods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
    public class ExtendExistingElementWithExtensionMethodsTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonClicked_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            // 2. Use the custom added submit button behaviour through 'Enter' key.
            button.SubmitButtonWithEnter();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }
    }
}