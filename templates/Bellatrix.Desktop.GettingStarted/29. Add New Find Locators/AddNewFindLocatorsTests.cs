// 1. You need to add a using statement to the namespace where the extension methods for new locator are situated.
using Bellatrix.Desktop.GettingStarted.ExtensionMethodsLocators;
using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class AddNewFindLocatorsTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            // 2. After that, you can use the new locator as it was originally part of Bellatrix.
            var button = App.ComponentCreateService.CreateByNameStartingWith<Button>("E Butto");

            button.Hover();

            var label = App.ComponentCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }
    }
}