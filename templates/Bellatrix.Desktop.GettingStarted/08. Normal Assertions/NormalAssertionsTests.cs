using Bellatrix.Desktop.NUnit;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class NormalAssertionsTests : DesktopTest
{
    [Test]
    [Category(Categories.CI)]
    public void CommonActionsWithDesktopControls_Wpf()
    {
        var calendar = App.Components.CreateByAutomationId<Calendar>("calendar");

        // 1. We can assert whether the control is disabled
        // The different BELLATRIX desktop elements classes contain lots of these properties which are a representation
        // of the most important app element attributes.
        //
        // the biggest drawback of using vanilla assertions is that the messages displayed on failure are not meaningful at all.
        // This is so because most unit testing frameworks are created for much simpler and shorter unit tests. In next chapter, there is information how BELLATRIX solves
        // the problems with the introduction of Validate methods.
        // If the bellow assertion fails the following message is displayed: "Message: Assert.AreEqual failed. Expected:<false>. Actual:<true>. "
        // You can guess what happened, but you do not have information which element failed and on which page.
        Assert.AreEqual(false, calendar.IsDisabled);

        var checkBox = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        checkBox.Check();

        // 2. Here we assert that the checkbox is checked.
        // On fail the following message is displayed: "Message: Assert.IsTrue failed."
        // Cannot learn much about what happened.
        Assert.IsTrue(checkBox.IsChecked);

        var comboBox = App.Components.CreateByAutomationId<ComboBox>("select");

        comboBox.SelectByText("Item2");

        // 3. Assert that the proper item is selected from the combobox items.
        Assert.AreEqual("Item2", comboBox.InnerText);

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");

        // 4. See if the element is present or not using the IsPresent property.
        Assert.IsTrue(label.IsPresent);

        var radioButton = App.Components.CreateByName<RadioButton>("RadioButton");

        radioButton.Click();

        // 5. Assert that the radio button is clicked.
        Assert.IsTrue(radioButton.IsChecked);

        // 6. One more thing you need to keep in mind is that normal assertion methods do not include BDD logging and any available hooks.
        // BELLATRIX provides you with a full BDD logging support for Validate assertions and gives you a way to hook your logic in multiple places.

        // 7. You can execute multiple assertions failing only once viewing all results.
        Bellatrix.Assertions.Assert.Multiple(
           () => Assert.IsTrue(radioButton.IsChecked),
           () => Assert.AreEqual("Item2", comboBox.InnerText));
    }
}