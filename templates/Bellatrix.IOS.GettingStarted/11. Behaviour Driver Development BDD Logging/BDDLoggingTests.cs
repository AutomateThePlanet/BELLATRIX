using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class BDDLoggingTests : MSTest.IOSTest
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
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void CommonAssertionsIOSControls()
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
            var button = App.ComponentCreateService.CreateByName<Button>("ComputeSumButton");

            button.Click();

            button.ValidateIsNotDisabled();

            var answerLabel = App.ComponentCreateService.CreateByName<Label>("Answer");

            answerLabel.ValidateIsVisible();

            var password = App.ComponentCreateService.CreateById<Password>("IntegerB");

            password.SetPassword("9");

            var textField = App.ComponentCreateService.CreateById<TextField>("IntegerA");

            textField.SetText("1");

            textField.ValidateTextIs("1");

            // 3. After the test is executed the following log is created:
            //  Start Test
            //  Class = BDDLoggingTests Name = CommonAssertionsIOSControls
            //  Validate control(Name = ComputeSumButton) is NOT disabled
            //  Validate control(Value containing Label) is visible
            //  Set password '9' in control(Id = IntegerB)
            //  Set text '1' in control(Id = IntegerA)
            //  Validate control(Id = IntegerA) text is '1'
        }
    }
}