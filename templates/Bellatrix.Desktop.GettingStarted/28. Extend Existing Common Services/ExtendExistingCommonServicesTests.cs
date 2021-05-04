// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Desktop.GettingStarted.AppService.Extensions;
using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class ExtendExistingCommonServicesTests : DesktopTest
    {
        [TestMethod]
        [Ignore]
        public void CommonActionsWithDesktopControls_Wpf()
        {
            // 2. Use newly added login method which is not part of the original implementation of the common service.
            App.AppService.LoginToApp("bellatrix", "topSecret");

            var calendar = App.ComponentCreateService.CreateByAutomationId<Calendar>("calendar");

            Assert.AreEqual(false, calendar.IsDisabled);

            var checkBox = App.ComponentCreateService.CreateByName<CheckBox>("BellaCheckBox");

            checkBox.Check();

            Assert.IsTrue(checkBox.IsChecked);

            var comboBox = App.ComponentCreateService.CreateByAutomationId<ComboBox>("select");

            comboBox.SelectByText("Item2");

            Assert.AreEqual("Item2", comboBox.InnerText);

            var label = App.ComponentCreateService.CreateByAutomationId<Label>("ResultLabelId");

            Assert.IsTrue(label.IsPresent);

            var radioButton = App.ComponentCreateService.CreateByName<RadioButton>("RadioButton");

            radioButton.Click();

            Assert.IsTrue(radioButton.IsChecked);
        }
    }
}