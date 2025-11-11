using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]
[Android(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.Controls1",
    Lifecycle.ReuseIfStarted)]
public class CommonControlsTests : NUnit.AndroidTest
{
    // 1. As mentioned before BELLATRIX exposes 18+ Android controls. All of them implement Proxy design pattern which means that they are not located immediately when
    // they are created. Another benefit is that each of them includes only the actions that you should be able to do with the specific control and nothing more.
    // For example, you cannot type into a button. Moreover, this way all of the actions has meaningful names- Type not SendKeys as in vanilla WebDriver.
    [Test]
    [Category(Categories.CI)]
    public void CommonActionsWithAndroidControls()
    {
        // 2. Create methods accept a generic parameter the type of the Android control. Then only the methods for this specific control are accessible.
        // Here we tell BELLATRIX to find your element by ID containing the value 'button'.
        var button = App.Components.CreateByIdContaining<Button>("button");

        // 3. Clicking the button. At this moment BELLATRIX locates the element.
        button.Click();

        // 4. Locating the radio button control using ID equal to value 'com.example.android.apis:id/radio2'.
        var radioButton = App.Components.CreateByIdContaining<RadioButton>("radio2");

        // 5. Select the radio button.
        radioButton.Click();

        Assert.That(radioButton.IsChecked);

        // 6. Most Android controls have properties such as checking whether the radio button is enabled or not.
        Assert.That(radioButton.IsDisabled);

        var checkBox = App.Components.CreateByIdContaining<CheckBox>("check1");

        // 7. Checking and unchecking the checkbox with id = 'check1'
        checkBox.Check();

        // 8. Asserting whether the check was successful.
        Assert.That(checkBox.IsChecked);

        checkBox.Uncheck();

        Assert.That(!checkBox.IsChecked);

        var comboBox = App.Components.CreateByIdContaining<ComboBox>("spinner1");

        // 9. Select a value in combobox by text.
        comboBox.SelectByText("Jupiter");

        // 10. Get the current comboBox text through GetText method.
        Assert.That("Jupiter".Equals(comboBox.GetText()));

        App.Components.CreateByClass<SeekBar>("android.widget.SeekBar");

        // 11. Moves the seekbar.
        ////seekBar.Set(9);

        // 12. Wait for the element to exists.
        comboBox.ToExists().WaitToBe();

        var label = App.Components.CreateByText<Label>("textColorPrimary");

        // 13. See if the element is present or not using the IsPresent property.
        Assert.That(label.IsPresent);

        // ReSharper disable once UnusedVariable
        var password = App.Components.CreateByDescription<Password>("passwordBox");

        // 14. Instead of using the non-meaningful method SendKeys, BELLATRIX gives you more readable tests through proper methods and properties names.
        // In this case, we set the text in the password field using the SetPassword method and SetText for regular text fields.
        ////password.SetPassword("topsecret");

        var textField = App.Components.CreateByIdContaining<TextField>("edit");

        textField.SetText("Bellatrix");

        Assert.That("Bellatrix".Equals(textField.GetText()));

        // 15. Full list of all supported Android controls, their methods and properties:
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
        // Switch- TurnOn, TurnOff, GetText, IsDisabled, IsOn
        // Tabs<TComponent>- GetAll
        // TextField- SetText, GetText, IsDisabled
        // ToggleButton- TurnOn, TurnOff, GetText, IsDisabled, IsOn
    }
}