using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        AppBehavior.ReuseIfStarted)]
    public class EnsureAssertions : AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void CommonAssertionsAndroidControls()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            // 1. We can assert whether the control is disabled
            // The different BELLATRIX Android elements classes contain lots of these properties which are a representation
            // of the most important app element attributes.
            // The biggest drawback of using vanilla assertions is that the messages displayed on failure are not meaningful at all.
            // If the bellow assertion fails the following message is displayed: "Message: Assert.AreEqual failed. Expected:<false>. Actual:<true>. "
            // You can guess what happened, but you do not have information which element failed and on which page.
            //
            // If we use the Ensure extension methods, BELLATRIX waits some time for the condition to pass. After this period if it is not successful, a beatified
            // meaningful exception message is displayed:
            // "The control should be disabled but it was NOT."
            button.EnsureIsNotDisabled();
            ////Assert.AreEqual(false, button.IsDisabled);

            var checkBox = App.ElementCreateService.CreateByIdContaining<CheckBox>("check1");

            checkBox.Check();

            // 2. Here we assert that the checkbox is checked.
            // On fail the following message is displayed: "Message: Assert.IsTrue failed."
            // Cannot learn much about what happened.
            ////Assert.IsTrue(checkBox.IsChecked);
            //
            // Now if we use the EnsureIsChecked method and the assertion does not succeed the following error message is displayed:
            // "The control should be checked but was NOT."
            checkBox.EnsureIsChecked();

            var comboBox = App.ElementCreateService.CreateByIdContaining<ComboBox>("spinner1");

            comboBox.SelectByText("Jupiter");

            // 3. Assert that the proper item is selected from the combobox items.
            comboBox.EnsureTextIs("Jupiter");
            ////Assert.AreEqual("Jupiter", comboBox.GetText());

            var label = App.ElementCreateService.CreateByText<Label>("textColorPrimary");

            // 4. See if the element is present or not using the IsPresent property.
            label.EnsureIsVisible();
            ////Assert.IsTrue(label.IsPresent);

            var radioButton = App.ElementCreateService.CreateByIdContaining<RadioButton>("radio2");

            radioButton.Click();

            // 5. Assert that the radio button is clicked.
            ////Assert.IsTrue(radioButton.IsChecked);
            //
            // By default, all Ensure methods have 5 seconds timeout. However, you can specify a custom timeout and sleep interval (period for checking again)
            radioButton.EnsureIsChecked(timeout: 30, sleepInterval: 2);
            ////Assert.IsTrue(radioButton.IsChecked);

            // 6. BELLATRIX provides you with a full BDD logging support for ensure assertions and gives you a way to hook your logic in multiple places.

            // 7. You can execute multiple ensure assertions failing only once viewing all results.
            Bellatrix.Assertions.Assert.Multiple(
               () => label.EnsureIsVisible(),
               () => Assert.IsTrue(label.IsPresent),
               () => comboBox.EnsureTextIs("Jupiter"));
        }
    }
}