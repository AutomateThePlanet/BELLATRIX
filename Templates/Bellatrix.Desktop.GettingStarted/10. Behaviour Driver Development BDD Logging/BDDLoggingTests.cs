using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
    public class BDDLoggingTests : DesktopTest
    {
         // 1. There cases when you need to show your colleagues or managers what tests do you have.
        // Sometimes you may have manual test cases, but their maintenance and up-to-date state are questionable.
        // Also, many times you need additional work to associate the tests with the test cases.
        // Some frameworks give you a way to write human readable tests through the Gherkin language.
        // The main idea is non-technical people to write these tests. However, we believe this approach is doomed.
        // Or it is doable only for simple tests.
        // This is why in BELLATRIX we built a feature that generates the test cases after the tests execution.
        // After each action or assertion, a new entry is logged.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void CommonActionsWithDesktopControls_Wpf()
        {
            // 2. In the testFrameworkSettings.json file find a section called logging, responsible for controlling the BDD logs generation.
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
            var calendar = App.ElementCreateService.CreateByAutomationId<Calendar>("calendar");

            calendar.ValidateIsNotDisabled();

            var checkBox = App.ElementCreateService.CreateByName<CheckBox>("BellaCheckBox");

            checkBox.Check();

            checkBox.ValidateIsChecked();

            var comboBox = App.ElementCreateService.CreateByAutomationId<ComboBox>("select");

            comboBox.SelectByText("Item2");

            Assert.AreEqual("Item2", comboBox.InnerText);

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");

            label.ValidateIsVisible();

            var radioButton = App.ElementCreateService.CreateByName<RadioButton>("RadioButton");

            radioButton.Click();

            radioButton.ValidateIsChecked(timeout: 30, sleepInterval: 2);

            // 3. After the test is executed the following log is created:
            // Start Test
            // Class = ValidateAssertionsTests Name = CommonActionsWithDesktopControls_Wpf
            // Validate control (AutomationId = calendar) is NOT disabled
            // Check control (Name = BellaCheckBox) on WPF Sample App
            // Validate control (Name = BellaCheckBox) is checked
            // Select 'Item2' from control (AutomationId = select) on WPF Sample App
            // Click control (Name = RadioButton) on WPF Sample App
            // Validate control (Name = RadioButton) is checked
        }
    }
}