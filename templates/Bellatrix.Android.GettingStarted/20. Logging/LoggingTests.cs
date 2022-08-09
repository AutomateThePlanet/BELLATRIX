using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]
public class LoggingTests : NUnit.AndroidTest
{
    [Test]
    [Category(Categories.CI)]
    public void ButtonClicked_When_CallClickMethod()
    {
        // 1. Sometimes is useful to add information to the generated test log.
        // To do it you can use the BELLATRIX built-in logger through accessing it via App service.
        Logger.LogInformation("$$$ Before clicking the button $$$");
        var button = App.Components.CreateByIdContaining<Button>("button");

        button.Click();

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
        //  Start Test
        //  Class = LoggingTests Name = ButtonClicked_When_CallClickMethod
        //  $$$ Before clicking the button $$$
        //  Click control(ID = button)
    }
}