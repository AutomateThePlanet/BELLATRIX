using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class NormalAssertions : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void CommonAssertionsIOSControls()
    {
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();

        // 1. We can assert whether the control is disabled
        // The different BELLATRIX Android elements classes contain lots of these properties which are a representation
        // of the most important app element attributes.
        // The biggest drawback of using vanilla assertions is that the messages displayed on failure are not meaningful at all.
        // This is so because most unit testing frameworks are created for much simpler and shorter unit tests. In next chapter, there is information how BELLATRIX solves
        // the problems with the introduction of Validate methods.
        // If the bellow assertion fails the following message is displayed: "Message: Assert.AreEqual failed. Expected:<false>. Actual:<true>. "
        // You can guess what happened, but you do not have information which element failed and on which screen.
        Assert.That(button.IsDisabled);

        var answerLabel = App.Components.CreateByName<Label>("Answer");

        // 2. See if the element is present or not using the IsPresent property.
        Assert.That(answerLabel.IsPresent);

        var password = App.Components.CreateById<Password>("IntegerB");

        password.SetPassword("9");

        var textField = App.Components.CreateById<TextField>("IntegerA");

        textField.SetText("1");

        // 3. Assert the correct text is set.
        Assert.That("1".Equals(textField.GetText()));
    }

    [Test]
    [CancelAfter(180000)]
    [IOS(Constants.IOSNativeAppPath,
      Constants.AppleCalendarBundleId,
      Constants.IOSDefaultVersion,
      Constants.IOSDefaultDeviceName,
      Lifecycle.RestartEveryTime)]
    [Ignore("API example purposes only. No need to run.")]
    public void IsCheckedTrue_When_CheckBoxUncheckedAndCheckIt()
    {
        var addButton = App.Components.CreateById<Button>("Add");
        addButton.Click();

        var checkBox = App.Components.CreateByIOSNsPredicate<CheckBox>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

        checkBox.Check();

        // 4. Asserts that the checkbox is checked.
        // On fail the following message is displayed: "Message: Assert.IsTrue failed."
        // Cannot learn much about what happened.
        Assert.That(checkBox.IsChecked);

        checkBox.Uncheck();

        Assert.That(!checkBox.IsChecked);
    }

    [Test]
    [CancelAfter(180000)]
    [IOS(Constants.IOSNativeAppPath,
        Constants.AppleCalendarBundleId,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    [Ignore("API example purposes only. No need to run.")]
    public void ButtonClicked_When_ClickMethodCalled()
    {
        var addButton = App.Components.CreateById<Button>("Add");
        addButton.Click();

        var radioButton = App.Components.CreateByIOSNsPredicate<RadioButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

        radioButton.Click();

        // 5. Assert that the radio button is clicked.
        Assert.That(radioButton.IsChecked);

        // 6. One more thing you need to keep in mind is that normal assertion methods do not include BDD logging and any available hooks.
        // BELLATRIX provides you with a full BDD logging support for Validate assertions and gives you a way to hook your logic in multiple places.

        // 7. You can execute multiple assertions failing only once viewing all results.
        Bellatrix.Assertions.Assert.Multiple(
           () => Assert.That(radioButton.IsChecked),
           () => Assert.That(radioButton.IsChecked));
    }
}