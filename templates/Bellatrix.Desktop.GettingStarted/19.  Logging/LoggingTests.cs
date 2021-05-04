using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class LoggingTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void CommonActionsWithDesktopControls_Wpf()
        {
            var calendar = App.ComponentCreateService.CreateByAutomationId<Calendar>("calendar");

            calendar.ValidateIsNotDisabled();

            var checkBox = App.ComponentCreateService.CreateByName<CheckBox>("BellaCheckBox");

            // 1. Sometimes is useful to add information to the generated test log.
            // To do it you can use the BELLATRIX built-in logger through accessing it via App service.
            Logger.LogInformation("$$$ Before checking the transfer checkbox. $$$");

            checkBox.Check();

            checkBox.ValidateIsChecked();

            var comboBox = App.ComponentCreateService.CreateByAutomationId<ComboBox>("select");

            comboBox.SelectByText("Item2");

            Assert.AreEqual("Item2", comboBox.InnerText);

            var label = App.ComponentCreateService.CreateByAutomationId<Label>("ResultLabelId");

            label.ValidateIsVisible();

            var radioButton = App.ComponentCreateService.CreateByName<RadioButton>("RadioButton");

            radioButton.Click();

            radioButton.ValidateIsChecked(timeout: 30, sleepInterval: 2);

            // 2. In the testFrameworkSettings.json file find a section called logging, responsible for controlling the logs generation.
            //  "loggingSettings": {
            //      "isEnabled": "true",
            //      "isConsoleLoggingEnabled": "true",
            //      "isDebugLoggingEnabled": "true",
            //      "isEventLoggingEnabled": "false",
            //      "isFileLoggingEnabled": "true",
            //      "outputTemplate":  "{Message:lj}{NewLine}",
            //  }
            //
            // You can disable the logs entirely. There are different places where the logs are populated.
            // By default, you can see the logs in the output window of each test.
            // Also, a file called logs.txt is generated in the folder with the DLLs of your tests.
            // If you execute your tests in CI with some CLI test runner the logs are printed there as well.
            // outputTemplate - controls how the message is formatted. You can add additional info such as timestamp and much more.
            // for more info visit- https://github.com/serilog/serilog/wiki/Formatting-Output
            //
            // 3. Generated Log, as you can see the above custom message is added to the log.
            // Start Test
            // Class = ValidateAssertionsTests Name = CommonActionsWithDesktopControls_Wpf
            // Validate control (AutomationId = calendar) is NOT disabled
            // $$$ Before checking the transfer checkbox. $$$
            // Check control (Name = BellaCheckBox) on WPF Sample App
            // Validate control (Name = BellaCheckBox) is checked
            // Select 'Item2' from control (AutomationId = select) on WPF Sample App
            // Click control (Name = RadioButton) on WPF Sample App
            // Validate control (Name = RadioButton) is checked
        }
    }
}