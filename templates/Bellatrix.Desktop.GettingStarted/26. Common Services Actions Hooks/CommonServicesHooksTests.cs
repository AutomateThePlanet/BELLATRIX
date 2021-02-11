using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class CommonServicesHooksTests : DesktopTest
    {
        // 1. Another way to extend BELLATRIX is to use the common services hooks. This is how the failed tests analysis works.
        // The base class for all web elements- Element provides a few special events as well:
        // ScrollingToVisible - called before scrolling
        // ScrolledToVisible - called after scrolling
        // CreatingElement - called before creating the element
        // CreatedElement - called after the creation of the element
        // CreatingElements - called before the creation of nested element
        // CreatedElements - called after the creation of nested element
        // ReturningWrappedElement - called before searching for native WebDriver element
        //
        // To add custom logic to the element's methods you can create a class that derives from ElementEventHandlers. The override the methods you like.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void CommonActionsWithDesktopControls_Wpf()
        {
            var calendar = App.ElementCreateService.CreateByAutomationId<Calendar>("calendar");

            Assert.AreEqual(false, calendar.IsDisabled);

            var checkBox = App.ElementCreateService.CreateByName<CheckBox>("BellaCheckBox");

            checkBox.Check();

            Assert.IsTrue(checkBox.IsChecked);

            var comboBox = App.ElementCreateService.CreateByAutomationId<ComboBox>("select");

            comboBox.SelectByText("Item2");

            Assert.AreEqual("Item2", comboBox.InnerText);

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");

            Assert.IsTrue(label.IsPresent);

            var radioButton = App.ElementCreateService.CreateByName<RadioButton>("RadioButton");

            radioButton.Click();

            Assert.IsTrue(radioButton.IsChecked);
        }
    }
}