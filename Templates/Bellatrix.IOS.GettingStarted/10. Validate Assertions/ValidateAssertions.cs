using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        AppBehavior.RestartEveryTime)]
    public class ValidateAssertions : IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void CommonAssertionsIOSControls()
        {
            var button = App.ElementCreateService.CreateByName<Button>("ComputeSumButton");

            button.Click();

            // 1. We can assert whether the control is disabled
            // The different BELLATRIX Android elements classes contain lots of these properties which are a representation
            // of the most important app element attributes.
            // The biggest drawback of using vanilla assertions is that the messages displayed on failure are not meaningful at all.
            // If the bellow assertion fails the following message is displayed: "Message: Assert.AreEqual failed. Expected:<false>. Actual:<true>. "
            // You can guess what happened, but you do not have information which element failed and on which page.
            //
            // If we use the Validate extension methods, BELLATRIX waits some time for the condition to pass. After this period if it is not successful, a beatified
            // meaningful exception message is displayed:
            // "The control should be disabled but it was NOT."
            button.ValidateIsNotDisabled();
            ////Assert.AreEqual(false, button.IsDisabled);

            var answerLabel = App.ElementCreateService.CreateByName<Label>("Answer");

            // 4. See if the element is present or not using the IsPresent property.
            answerLabel.ValidateIsVisible();
            ////Assert.IsTrue(label.IsPresent);

            var password = App.ElementCreateService.CreateById<Password>("IntegerB");

            password.SetPassword("9");

            var textField = App.ElementCreateService.CreateById<TextField>("IntegerA");

            textField.SetText("1");

            // 3. Assert that the proper text is set.
            textField.ValidateTextIs("1");
            ////Assert.AreEqual("1", textField.GetText());
        }

        [TestMethod]
        [Timeout(180000)]
        [IOS(Constants.AppleCalendarBundleId,
          Constants.IOSDefaultVersion,
          Constants.IOSDefaultDeviceName,
          AppBehavior.RestartEveryTime)]
        [Ignore]
        public void IsCheckedTrue_When_CheckBoxUncheckedAndCheckIt()
        {
            var addButton = App.ElementCreateService.CreateById<Button>("Add");
            addButton.Click();

            var checkBox = App.ElementCreateService.CreateByIOSNsPredicate<CheckBox>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            checkBox.Check();

            // 2. Here we assert that the checkbox is checked.
            // On fail the following message is displayed: "Message: Assert.IsTrue failed."
            // Cannot learn much about what happened.
            ////Assert.IsTrue(checkBox.IsChecked);
            //
            // Now if we use the ValidateIsChecked method and the assertion does not succeed the following error message is displayed:
            // "The control should be checked but was NOT."
            checkBox.ValidateIsChecked();

            checkBox.Uncheck();

            Assert.IsFalse(checkBox.IsChecked);
        }

        [TestMethod]
        [Timeout(180000)]
        [IOS(Constants.AppleCalendarBundleId,
            Constants.IOSDefaultVersion,
            Constants.IOSDefaultDeviceName,
            AppBehavior.RestartEveryTime)]
        [Ignore]
        public void ButtonClicked_When_ClickMethodCalled()
        {
            var addButton = App.ElementCreateService.CreateById<Button>("Add");
            addButton.Click();

            var radioButton = App.ElementCreateService.CreateByIOSNsPredicate<RadioButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            radioButton.Click();

            // 5. Assert that the radio button is clicked.
            ////Assert.IsTrue(radioButton.IsChecked);
            //
            // By default, all Validate methods have 5 seconds timeout. However, you can specify a custom timeout and sleep interval (period for checking again)
            radioButton.ValidateIsChecked(timeout: 30, sleepInterval: 2);
            ////Assert.IsTrue(radioButton.IsChecked);

            // 6. BELLATRIX provides you with a full BDD logging support for Validate assertions and gives you a way to hook your logic in multiple places.

            // 7. You can execute multiple Validate assertions failing only once viewing all results.
            Bellatrix.Assertions.Assert.Multiple(
               () => radioButton.ValidateIsChecked(timeout: 30, sleepInterval: 2),
               () => radioButton.ValidateIsChecked());
        }
    }
}