using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

// 1. This is the main attribute that you need to mark each class that contains NUnit tests.
[TestFixture]

// 2. This is the attribute for automatic start/control of iOS apps by Bellatrix. If you have to do it manually properly, you will need thousands of lines of code.
// 2.1. appPath- sets the path where your application is.
// 2.2. Lifecycle enum controls when the app is started and stopped. This can drastically increase or decrease the tests execution time, depending on your needs.
// However you need to be careful because in case of tests failures the app may need to be restarted.
// Available options:
// RestartEveryTime- for each test a separate Appium instance is created and the previous app instance is closed.
// RestartOnFail- the app is only restarted if the previous test failed. Alternatively, if the previous test's app was different.
// ReuseIfStarted- the app is only restarted if the previous test's app was different. In all other cases, the app is reused if possible.
// Note: However, use this option with caution since in some rare cases if you have not properly setup your tests you may need to restart the app if the test fails otherwise all other tests may fail too.
//
// There are even more things you can do with this attribute, but we look into them in the next sections.
//
// If you place attribute over the class all tests inherit the lifecycle. It is possible to place it over each test and this way it overrides the class lifecycle only for this particular test.
//
// If you don't use the attribute, the default information from the configuration will be used placed under the executionSettings section.
// Also, you can add additional driver arguments under the arguments section array in the configuration file.
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.ReuseIfStarted)]

// 2.2. All iOS BELLATRIX test classes should inherit from the IOSTest base class.
// This way you can use all built-in BELLATRIX tools and functionalities.
public class BellatrixAppLifecycleTests : NUnit.IOSTest
{
    // 2.3. All NUnit tests should be marked with the TestMethod attribute.
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ButtonClicked_When_CallClickMethod()
    {
        // Use the element creation service to create an instance of the button. There are much more details about this process in the next sections.
        // There is more about the App class in the next sections. However, it is the primary point where you access the BELLATRIX services.
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]

    // 2.4. As mentioned above you can override the app lifecycle for a particular test. The global lifecycle for all tests in the class is to reuse an instance of the app.
    // Only for this particular test, BELLATRIX opens the app and restarts it only on fail.
    [IOS(Constants.IOSNativeAppPath,
        Constants.AppleCalendarBundleId,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartOnFail)]
    public void ReturnsTrue_When_CallButtonIsPresent()
    {
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        Assert.That(button.IsPresent);
    }
}