using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class ValidateAssertionsTests : DesktopTest
{
    [Test]
    [Category(Categories.CI)]
    public void CommonActionsWithDesktopControls_Wpf()
    {
        var calendar = App.Components.CreateByAutomationId<Calendar>("calendar");

        // 1. We can assert whether the control is disabled
        // The different BELLATRIX desktop elements classes contain lots of these properties which are a representation
        // of the most important app element attributes.
        // The biggest drawback of using vanilla assertions is that the messages displayed on failure are not meaningful at all.
        // If the bellow assertion fails the following message is displayed: "Message: Assert.AreEqual failed. Expected:<false>. Actual:<true>. "
        // You can guess what happened, but you do not have information which element failed and on which page.
        //
        // If we use the Validate extension methods, BELLATRIX waits some time for the condition to pass. After this period if it is not successful, a beatified
        // meaningful exception message is displayed:
        // "The control should be disabled but it was NOT."
        calendar.ValidateIsNotDisabled();
        ////Assert.AreEqual(false, calendar.IsDisabled);

        var checkBox = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        checkBox.Check();

        // 2. Here we assert that the checkbox is checked.
        // On fail the following message is displayed: "Message: Assert.IsTrue failed."
        // Cannot learn much about what happened.
        ////Assert.IsTrue(checkBox.IsChecked);
        //
        // Now if we use the ValidateIsChecked method and the assertion does not succeed the following error message is displayed:
        // "The control should be checked but was NOT."
        checkBox.ValidateIsChecked();

        var comboBox = App.Components.CreateByAutomationId<ComboBox>("select");

        comboBox.SelectByText("Item2");

        // 3. Assert that the proper item is selected from the combobox items.
        Assert.AreEqual("Item2", comboBox.InnerText);

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");

        // 4. See if the element is present or not using the IsPresent property.
        ////Assert.IsTrue(label.IsPresent);
        label.ValidateIsVisible();

        var radioButton = App.Components.CreateByName<RadioButton>("RadioButton");

        radioButton.Click();

        // 5. Assert that the radio button is clicked.
        ////Assert.IsTrue(radioButton.IsChecked);
        //
        // By default, all Validate methods have 5 seconds timeout. However, you can specify a custom timeout and sleep interval (period for checking again)
        radioButton.ValidateIsChecked(timeout: 30, sleepInterval: 2);

        // 6. BELLATRIX provides you with a full BDD logging support for Validate assertions and gives you a way to hook your logic in multiple places.

        // 7. You can execute multiple Validate assertions failing only once viewing all results.
        Bellatrix.Assertions.Assert.Multiple(
           () => radioButton.ValidateIsChecked(timeout: 30, sleepInterval: 2),
           () => checkBox.ValidateIsChecked(),
           () => Assert.AreEqual("Item2", comboBox.InnerText));
    }
}