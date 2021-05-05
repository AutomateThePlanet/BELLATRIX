using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.ReuseIfStarted)]
    public class BDDLoggingTests : MSTest.AndroidTest
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
        public void CommonAssertionsAndroidControls()
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
            var button = App.Components.CreateByIdContaining<Button>("button");

            button.ValidateIsNotDisabled();

            var checkBox = App.Components.CreateByIdContaining<CheckBox>("check1");

            checkBox.Check();

            checkBox.ValidateIsChecked();

            var comboBox = App.Components.CreateByIdContaining<ComboBox>("spinner1");

            comboBox.SelectByText("Jupiter");

            comboBox.ValidateTextIs("Jupiter");

            var label = App.Components.CreateByText<Label>("textColorPrimary");

            label.ValidateIsVisible();

            var radioButton = App.Components.CreateByIdContaining<RadioButton>("radio2");

            radioButton.Click();

            radioButton.ValidateIsChecked(timeout: 30, sleepInterval: 2);

            // 3. After the test is executed the following log is created:
            //  Start Test
            //  Class = BDDLoggingTests Name = CommonAssertionsAndroidControls
            //  Validate control(ID = button) is NOT disabled
            //  Check control(ID = check1) on
            //  Validate control(ID = check1) is checked
            //  Select 'Jupiter' from control (ID = spinner1) on
            //  Click control(Text = Jupiter) on
            //  Validate control(ID = spinner1) text is 'Jupiter'
            //  Validate control(Text = textColorPrimary) is visible
            //  Click control(ID = radio2) on
            //  Validate control(ID = radio2) is checked
        }
    }
}