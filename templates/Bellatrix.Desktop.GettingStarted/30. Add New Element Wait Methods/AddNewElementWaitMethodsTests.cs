// 1. You need to add a using statement to the namespace where the new wait extension methods are situated.
using Bellatrix.Desktop.GettingStarted.ExtensionMethodsWaitMethods;
using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class AddNewElementWaitMethodsTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            // 2. After that, you can use the new wait method as it was originally part of Bellatrix.
            var button = App.ComponentCreateService.CreateByName<Button>("E Button").ToHaveSpecificContent("E Button");

            button.Hover();

            var label = App.ComponentCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }
    }
}