// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Desktop.GettingStarted.Advanced.Elements.Extension.Methods;
using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class ExtendExistingElementWithExtensionMethodsTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonClicked_Wpf()
        {
            var button = App.Components.CreateByName<Button>("E Button");

            // 2. Use the custom added submit button lifecycle through 'Enter' key.
            button.SubmitButtonWithEnter();

            var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }
    }
}