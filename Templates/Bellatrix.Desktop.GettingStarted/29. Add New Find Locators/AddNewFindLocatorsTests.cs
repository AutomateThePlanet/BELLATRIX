// 1. You need to add a using statement to the namespace where the extension methods for new locator are situated.
using Bellatrix.Desktop.GettingStarted.ExtensionMethodsLocators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
    public class AddNewFindLocatorsTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            // 2. After that, you can use the new locator as it was originally part of Bellatrix.
            var button = App.ElementCreateService.CreateByNameStartingWith<Button>("E Butto");

            button.Hover();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }
    }
}