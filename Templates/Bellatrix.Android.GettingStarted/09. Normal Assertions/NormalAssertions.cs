using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.ReuseIfStarted)]
    public class NormalAssertions : MSTest.AndroidTest
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
            // This is so because most unit testing frameworks are created for much simpler and shorter unit tests. In next chapter, there is information how BELLATRIX solves
            // the problems with the introduction of Validate methods.
            // If the bellow assertion fails the following message is displayed: "Message: Assert.AreEqual failed. Expected:<false>. Actual:<true>. "
            // You can guess what happened, but you do not have information which element failed and on which screen.
            Assert.AreEqual(false, button.IsDisabled);

            var checkBox = App.ElementCreateService.CreateByIdContaining<CheckBox>("check1");

            checkBox.Check();

            // 2. Asserts that the checkbox is checked.
            // On fail the following message is displayed: "Message: Assert.IsTrue failed."
            // Cannot learn much about what happened.
            Assert.IsTrue(checkBox.IsChecked);

            var comboBox = App.ElementCreateService.CreateByIdContaining<ComboBox>("spinner1");

            comboBox.SelectByText("Jupiter");

            // 3. Assert that the proper item is selected from the combobox items.
            Assert.AreEqual("Jupiter", comboBox.GetText());

            var label = App.ElementCreateService.CreateByText<Label>("textColorPrimary");

            // 4. See if the element is present or not using the IsPresent property.
            Assert.IsTrue(label.IsPresent);

            var radioButton = App.ElementCreateService.CreateByIdContaining<RadioButton>("radio2");

            radioButton.Click();

            // 5. Assert that the radio button is clicked.
            Assert.IsTrue(radioButton.IsChecked);

            // 6. One more thing you need to keep in mind is that normal assertion methods do not include BDD logging and any available hooks.
            // BELLATRIX provides you with a full BDD logging support for Validate assertions and gives you a way to hook your logic in multiple places.

            // 7. You can execute multiple assertions failing only once viewing all results.
            Bellatrix.Assertions.Assert.Multiple(
               () => Assert.IsTrue(radioButton.IsChecked),
               () => Assert.IsTrue(label.IsPresent));
        }
    }
}