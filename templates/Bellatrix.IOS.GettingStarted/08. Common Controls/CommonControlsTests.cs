using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class CommonControlsTests : MSTest.IOSTest
    {
        // 1. As mentioned before BELLATRIX exposes 15+ iOS controls. All of them implement Proxy design pattern which means that they are not located immediately when
        // they are created. Another benefit is that each of them includes only the actions that you should be able to do with the specific control and nothing more.
        // For example, you cannot type into a button. Moreover, this way all of the actions has meaningful names- Type not SendKeys as in vanilla WebDriver.
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void CommonActionsWithIOSControls()
        {
            // 2. Create methods accept a generic parameter the type of the iOS control. Then only the methods for this specific control are accessible.
            // Here we tell BELLATRIX to find your element by name equals the value 'button'.
            var button = App.ElementCreateService.CreateByName<Button>("ComputeSumButton");

            // 3. Clicking the button. At this moment BELLATRIX locates the element.
            button.Click();

            var seekBar = App.ElementCreateService.CreateByName<SeekBar>("AppElem");

            // 4. Moves the seekbar.
            seekBar.Set(9);

            // 5. Wait for the element to exists.
            seekBar.ToExists().WaitToBe();

            var answerLabel = App.ElementCreateService.CreateByName<Label>("Answer");

            // 6. See if the element is present or not using the IsPresent property.
            Assert.IsTrue(answerLabel.IsPresent);

            var password = App.ElementCreateService.CreateById<Password>("IntegerB");

            // 7. Instead of using the non-meaningful method SendKeys, BELLATRIX gives you more readable tests through proper methods and properties names.
            // In this case, we set the text in the password field using the SetPassword method and SetText for regular text fields.
            password.SetPassword("9");

            var textField = App.ElementCreateService.CreateById<TextField>("IntegerA");

            textField.SetText("1");

            Assert.AreEqual("1", textField.GetText());
        }

        [TestMethod]
        [Timeout(180000)]
        [IOS(Constants.AppleCalendarBundleId,
            Constants.IOSDefaultVersion,
            Constants.IOSDefaultDeviceName,
            Lifecycle.RestartEveryTime)]
        [Ignore]
        public void IsCheckedTrue_When_CheckBoxUncheckedAndCheckIt()
        {
            var addButton = App.ElementCreateService.CreateById<Button>("Add");
            addButton.Click();

            var checkBox = App.ElementCreateService.CreateByIOSNsPredicate<CheckBox>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            // 8. Checking and unchecking the checkbox with IOSNsPredicate = 'type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"'
            checkBox.Check();

            // 9. Asserting whether the check was successful.
            Assert.IsTrue(checkBox.IsChecked);

            checkBox.Uncheck();

            Assert.IsFalse(checkBox.IsChecked);
        }

        [TestMethod]
        [Timeout(180000)]
        [IOS(Constants.AppleCalendarBundleId,
            Constants.IOSDefaultVersion,
            Constants.IOSDefaultDeviceName,
            Lifecycle.RestartEveryTime)]
        [Ignore]
        public void ButtonClicked_When_ClickMethodCalled()
        {
            var addButton = App.ElementCreateService.CreateById<Button>("Add");
            addButton.Click();

            // 10. Locating the radio button control using IOSNsPredicate type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"
            var radioButton = App.ElementCreateService.CreateByIOSNsPredicate<RadioButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            Assert.IsFalse(radioButton.IsChecked);

            // 11. Select the radio button.
            radioButton.Click();

            // 12. Most IOS controls have properties such as checking whether the radio button is enabled or not.
            Assert.IsTrue(radioButton.IsChecked);

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
            // Grid<TElement>- GetAll
            // Image- same as element
            // ImageButton- Click, GetText, IsDisabled
            // Label- GetText
            // Number- SetNumber, GetNumber, IsDisabled
            // Password- GetPassword, SetPassword, IsDisabled
            // Progress- IsDisabled
            // RadioButton- Click, IsDisabled, IsChecked, GetText
            // RadioGroup- ClickByText, ClickByIndex, GetChecked, GetAll
            // SeekBar- Set, IsDisabled
            // Tabs<TElement>- GetAll
            // TextField- SetText, GetText, IsDisabled
            // ToggleButton- TurnOn, TurnOff, GetText, IsDisabled, IsOn
        }
    }
}