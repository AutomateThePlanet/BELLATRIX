using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class LoggingTests : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ButtonClicked_When_CallClickMethod()
    {
        // Sometimes is useful to add information to the generated test log.
        // To do it you can use the BELLATRIX built-in logger through accessing it via App service.
        Logger.LogInformation("$$$ Before clicking the button $$$");
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();

        // Generated Log, as you can see the above custom message is added to the log.
        //  Start Test
        //  Class = LoggingTests Name = ButtonClicked_When_CallClickMethod
        //  $$$ Before clicking the button $$$
        //  Click control(name = ComputeSumButton)
    }
}