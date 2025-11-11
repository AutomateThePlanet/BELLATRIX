using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class BDDLoggingTests : NUnit.IOSTest
{
    // 1. There cases when you need to show your colleagues or managers what tests do you have.
    // Sometimes you may have manual test cases, but their maintenance and up-to-date state are questionable.
    // Also, many times you need additional work to associate the tests with the test cases.
    // Some frameworks give you a way to write human readable tests through the Gherkin language.
    // The main idea is non-technical people to write these tests. However, we believe this approach is doomed.
    // Or it is doable only for simple tests.
    // This is why in BELLATRIX we built a feature that generates the test cases after the tests execution.
    // After each action or assertion, a new entry is logged.
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void CommonAssertionsIOSControls()
    {
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();

        button.ValidateIsNotDisabled();

        var answerLabel = App.Components.CreateByName<Label>("Answer");

        answerLabel.ValidateIsVisible();

        var password = App.Components.CreateById<Password>("IntegerB");

        password.SetPassword("9");

        var textField = App.Components.CreateById<TextField>("IntegerA");

        textField.SetText("1");

        textField.ValidateTextIs("1");

        // After the test is executed the following log is created:
        //  Start Test
        //  Class = BDDLoggingTests Name = CommonAssertionsIOSControls
        //  Validate control(Name = ComputeSumButton) is NOT disabled
        //  Validate control(Value containing Label) is visible
        //  Set password '9' in control(Id = IntegerB)
        //  Set text '1' in control(Id = IntegerA)
        //  Validate control(Id = IntegerA) text is '1'
    }
}