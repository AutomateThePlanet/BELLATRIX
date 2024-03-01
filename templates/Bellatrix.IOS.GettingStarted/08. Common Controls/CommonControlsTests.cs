using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class CommonControlsTests : NUnit.IOSTest
{
    // 1. As mentioned before BELLATRIX exposes 15+ iOS controls. All of them implement Proxy design pattern which means that they are not located immediately when
    // they are created. Another benefit is that each of them includes only the actions that you should be able to do with the specific control and nothing more.
    // For example, you cannot type into a button. Moreover, this way all of the actions has meaningful names- Type not SendKeys as in vanilla WebDriver.
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void CommonActionsWithIOSControls()
    {
        // 2. Create methods accept a generic parameter the type of the iOS control. Then only the methods for this specific control are accessible.
        // Here we tell BELLATRIX to find your element by name equals the value 'button'.
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        // 3. Clicking the button. At this moment BELLATRIX locates the element.
        button.Click();

        var seekBar = App.Components.CreateByName<SeekBar>("AppElem");

        // 4. Moves the seekbar.
        seekBar.Set(9);

        // 5. Wait for the element to exists.
        seekBar.ToExists().WaitToBe();

        var answerLabel = App.Components.CreateByName<Label>("Answer");

        // 6. See if the element is present or not using the IsPresent property.
        Assert.That(answerLabel.IsPresent);

        var password = App.Components.CreateById<Password>("IntegerB");

        // 7. Instead of using the non-meaningful method SendKeys, BELLATRIX gives you more readable tests through proper methods and properties names.
        // In this case, we set the text in the password field using the SetPassword method and SetText for regular text fields.
        password.SetPassword("9");

        var textField = App.Components.CreateById<TextField>("IntegerA");

        textField.SetText("1");

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

        // 8. Checking and unchecking the checkbox with IOSNsPredicate = 'type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"'
        checkBox.Check();

        // 9. Asserting whether the check was successful.
        Assert.That(checkBox.IsChecked);

        checkBox.Uncheck();

        Assert.That(checkBox.IsChecked);
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

        // 10. Locating the radio button control using IOSNsPredicate type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"
        var radioButton = App.Components.CreateByIOSNsPredicate<RadioButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

        Assert.That(!radioButton.IsChecked);

        // 11. Select the radio button.
        radioButton.Click();

        // 12. Most IOS controls have properties such as checking whether the radio button is enabled or not.
        Assert.That(radioButton.IsChecked);

        // 13. Full list of all supported IOS controls, their methods and properties:
        // Common controls:
        //
        // Element - By, GetAttribute, ScrollToVisible, Create, CreateAll, WaitToBe, IsPresent, IsVisible, ElementName, PageName, Location, Size
        //
        // * All other controls have access to the above methods and properties
        //
        // Button- Click, GetText, IsDisabled
        // CheckBox- Check, Uncheck, GetText, IsDisabled, IsChecked
        // ComboBox- SelectByText, GetText, IsDisabled
        // Grid<TComponent>- GetAll
        // Image- same as element
        // ImageButton- Click, GetText, IsDisabled
        // Label- GetText
        // Number- SetNumber, GetNumber, IsDisabled
        // Password- GetPassword, SetPassword, IsDisabled
        // Progress- IsDisabled
        // RadioButton- Click, IsDisabled, IsChecked, GetText
        // RadioGroup- ClickByText, ClickByIndex, GetChecked, GetAll
        // SeekBar- Set, IsDisabled
        // Tabs<TComponent>- GetAll
        // TextField- SetText, GetText, IsDisabled
        // ToggleButton- TurnOn, TurnOff, GetText, IsDisabled, IsOn
    }
}