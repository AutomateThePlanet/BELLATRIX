using Bellatrix.Desktop.GettingStarted.Elements.ChildElements;
using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class ExtendExistingElementWithChildElementsTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonClicked_Wpf()
        {
            // 1. Instead of the regular button, we create the ExtendedButton, this way we can use its new methods.
            var button = App.ElementCreateService.CreateByName<ExtendedButton>("E Button");

            // 2. Use the new custom method provided by the ExtendedButton class.
            button.SubmitButtonWithEnter();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }
    }
}