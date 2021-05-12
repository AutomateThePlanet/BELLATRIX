using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted
{
    [TestFixture]
    public class LoggingTests : NUnit.WebTest
    {
        [Test]
        [Category(Categories.CI)]
        public void AddCustomMessagesToLog()
        {
            App.Navigation.Navigate("http://demos.bellatrix.solutions/");

            Select sortDropDown = App.Components.CreateByNameEndingWith<Select>("orderby");
            Anchor protonMReadMoreButton = App.Components.CreateByInnerTextContaining<Anchor>("Read more");
            Anchor addToCartFalcon9 = App.Components.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
            Anchor viewCartButton = App.Components.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();

            sortDropDown.SelectByText("Sort by price: low to high");
            protonMReadMoreButton.Hover();

            // 1. Sometimes is useful to add information to the generated test log.
            // To do it you can use the BELLATRIX built-in logger through accessing it via App service.
            Logger.LogInformation("Before adding Falcon 9 rocket to cart.");

            addToCartFalcon9.Focus();
            addToCartFalcon9.Click();
            viewCartButton.Click();

            // 2. In the testFrameworkSettings.json file find a section called logging, responsible for controlling the logs generation.
            //  "loggingSettings": {
            //      "isEnabled": "true",
            //      "isConsoleLoggingEnabled": "true",
            //      "isDebugLoggingEnabled": "true",
            //      "isEventLoggingEnabled": "false",
            //      "isFileLoggingEnabled": "true",
            //      "outputTemplate":  "{Message:lj}{NewLine}",
            //      "addUrlToBddLogging": "false"
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
            // #### Start Chrome on PORT = 53153
            // Start Test
            //     Class = LoggingTests Name = AddCustomMessagesToLog
            // Select 'Sort by price: low to high' from control (Name ending with orderby)
            // Hover control (InnerText containing Read more)
            // Before adding Falcon 9 rocket to cart.
            // Focus control (data-product_id = 28)
            // Click control (data-product_id = 28)
            // Click control (Class = added_to_cart wc-forward)
        }
    }
}