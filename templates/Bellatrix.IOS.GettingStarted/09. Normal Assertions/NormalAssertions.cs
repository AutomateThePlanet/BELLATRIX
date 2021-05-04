using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class NormalAssertions : MSTest.IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void CommonAssertionsIOSControls()
        {
            var button = App.ComponentCreateService.CreateByName<Button>("ComputeSumButton");

            button.Click();

            // 1. We can assert whether the control is disabled
            // The different BELLATRIX Android elements classes contain lots of these properties which are a representation
            // of the most important app element attributes.
            // The biggest drawback of using vanilla assertions is that the messages displayed on failure are not meaningful at all.
            // This is so because most unit testing frameworks are created for much simpler and shorter unit tests. In next chapter, there is information how BELLATRIX solves
            // the problems with the introduction of Validate methods.
            // If the bellow assertion fails the following message is displayed: "Message: Assert.AreEqual failed. Expected:<false>. Actual:<true>. "
            // You can guess what happened, but you do not have information which element failed and on which screen.
            Assert.AreEqual(false, button.IsDisabled);

            var answerLabel = App.ComponentCreateService.CreateByName<Label>("Answer");

            // 2. See if the element is present or not using the IsPresent property.
            Assert.IsTrue(answerLabel.IsPresent);

            var password = App.ComponentCreateService.CreateById<Password>("IntegerB");

            password.SetPassword("9");

            var textField = App.ComponentCreateService.CreateById<TextField>("IntegerA");

            textField.SetText("1");

            // 3. Assert the correct text is set.
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
            var addButton = App.ComponentCreateService.CreateById<Button>("Add");
            addButton.Click();

            var checkBox = App.ComponentCreateService.CreateByIOSNsPredicate<CheckBox>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            checkBox.Check();

            // 4. Asserts that the checkbox is checked.
            // On fail the following message is displayed: "Message: Assert.IsTrue failed."
            // Cannot learn much about what happened.
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
            var addButton = App.ComponentCreateService.CreateById<Button>("Add");
            addButton.Click();

            var radioButton = App.ComponentCreateService.CreateByIOSNsPredicate<RadioButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            radioButton.Click();

            // 5. Assert that the radio button is clicked.
            Assert.IsTrue(radioButton.IsChecked);

            // 6. One more thing you need to keep in mind is that normal assertion methods do not include BDD logging and any available hooks.
            // BELLATRIX provides you with a full BDD logging support for Validate assertions and gives you a way to hook your logic in multiple places.

            // 7. You can execute multiple assertions failing only once viewing all results.
            Bellatrix.Assertions.Assert.Multiple(
               () => Assert.IsTrue(radioButton.IsChecked),
               () => Assert.IsFalse(!radioButton.IsChecked));
        }
    }
}