using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class CommonControlsTests : DesktopTest
{
    // 1. As mentioned before BELLATRIX exposes 18+ desktop controls. All of them implement Proxy design pattern which means that they are not located immediately when
    // they are created. Another benefit is that each of them includes only the actions that you should be able to do with the specific control and nothing more.
    // For example, you cannot type into a button. Moreover, this way all of the actions has meaningful names- Type not SendKeys as in vanilla WebDriver.
    [Test]
    [Category(Categories.CI)]
    public void CommonActionsWithDesktopControls_Wpf()
    {
        // 2. Create methods accept a generic parameter the type of the web control. Then only the methods for this specific control are accessible.
        // Here we tell BELLATRIX to find your element by name attribute equals to 'E Button'.
        var button = App.Components.CreateByName<Button>("E Button");

        // 3. Clicking the button. At this moment BELLATRIX locates the element.
        button.Click();

        // 4. Locating the calendar control using automationId = calendar
        var calendar = App.Components.CreateByAutomationId<Calendar>("calendar");

        // 5. Most desktop controls have properties such as checking whether the calendar is enabled or not.
        Assert.AreEqual(false, calendar.IsDisabled);

        var checkBox = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        // 6. Checking and unchecking the checkbox with name = 'BellaCheckBox'
        checkBox.Check();

        // 7. Asserting whether the check was successful.
        Assert.IsTrue(checkBox.IsChecked);

        checkBox.Uncheck();

        Assert.IsFalse(checkBox.IsChecked);

        var comboBox = App.Components.CreateByAutomationId<ComboBox>("select");

        // 8. Select a value in combobox but text.
        comboBox.SelectByText("Item2");

        // 9. Get the current comboBox text through InnerText property.
        Assert.AreEqual("Item2", comboBox.InnerText);

        var datePicker = App.Components.CreateByAutomationId<Date>("DatePicker");

        // 10. You can hover on most desktop controls or search for elements inside them.
        datePicker.Hover();

        var element = App.Components.CreateByName<Component>("DisappearAfterButton1");

        // 11. Wait for the element to disappear.
        element.ToNotExists().WaitToBe();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");

        // 12. See if the element is present or not using the IsPresent property.
        Assert.IsTrue(label.IsPresent);

        var password = App.Components.CreateByAutomationId<Password>("passwordBox");

        // 13. Instead of using the non-meaningful method SendKeys, BELLATRIX gives you more readable tests through proper methods and properties names.
        // In this case, we set the text in the password field using the SetPassword method and SetText for regular text fields.
        password.SetPassword("topsecret");

        var textField = App.Components.CreateByAutomationId<TextField>("textBox");

        textField.SetText("Meissa Is Beautiful!");

        Assert.AreEqual("Meissa Is Beautiful!", textField.InnerText);

        var radioButton = App.Components.CreateByName<RadioButton>("RadioButton");

        // 14. Select the radio button.
        radioButton.Click();

        Assert.IsTrue(radioButton.IsChecked);

        // 15. Full list of all supported web controls, their methods and properties:
        // Common controls:
        //
        // Element - By, GetAttribute, ScrollToVisible, Create, CreateAll, WaitToBe, IsPresent, IsVisible, ElementName, PageName
        //
        // * All other controls have access to the above methods and properties
        //
        // Button- Click, Hover, InnerText, IsDisabled
        // Calendar- Hover, IsDisabled
        // Checkbox- Check, Uncheck, Hover, IsDisabled, IsChecked
        // ComboBox- Hover, SelectByText, InnerText, IsDisabled
        // Date- GetDate, SetDate, Hover, IsDisabled
        // Expander- Click, Hover, IsDisabled
        // Image- Hover
        // Label- Hover, InnerText
        // ListBox- Hover, IsDisabled
        // Menu- Hover
        // Password- GetPassword, SetPassword, Hover, IsDisabled
        // Progress- Hover
        // RadioButton- Hover, Click, IsDisabled, IsChecked
        // Tabs- Hover
        // TextArea- GetText, SetText, Hover, InnerText, IsDisabled
        // TextField- SetText, Hover, InnerText, IsDisabled
        // Time- GetTime, SetTime, Hover, IsDisabled
    }
}