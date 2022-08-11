using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class BDDLoggingTests : DesktopTest
{
     // There cases when you need to show your colleagues or managers what tests do you have.
    // Sometimes you may have manual test cases, but their maintenance and up-to-date state are questionable.
    // Also, many times you need additional work to associate the tests with the test cases.
    // Some frameworks give you a way to write human readable tests through the Gherkin language.
    // The main idea is non-technical people to write these tests. However, we believe this approach is doomed.
    // Or it is doable only for simple tests.
    // This is why in BELLATRIX we built a feature that generates the test cases after the tests execution.
    // After each action or assertion, a new entry is logged.
    [Test]
    [Category(Categories.CI)]
    public void CommonActionsWithDesktopControls_Wpf()
    {
        var calendar = App.Components.CreateByAutomationId<Calendar>("calendar");

        calendar.ValidateIsNotDisabled();

        var checkBox = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        checkBox.Check();

        checkBox.ValidateIsChecked();

        var comboBox = App.Components.CreateByAutomationId<ComboBox>("select");

        comboBox.SelectByText("Item2");

        Assert.AreEqual("Item2", comboBox.InnerText);

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");

        label.ValidateIsVisible();

        var radioButton = App.Components.CreateByName<RadioButton>("RadioButton");

        radioButton.Click();

        radioButton.ValidateIsChecked(timeout: 30, sleepInterval: 2);

        // After the test is executed the following log is created:
        // Start Test
        // Class = ValidateAssertionsTests Name = CommonActionsWithDesktopControls_Wpf
        // Validate control (AutomationId = calendar) is NOT disabled
        // Check control (Name = BellaCheckBox) on WPF Sample App
        // Validate control (Name = BellaCheckBox) is checked
        // Select 'Item2' from control (AutomationId = select) on WPF Sample App
        // Click control (Name = RadioButton) on WPF Sample App
        // Validate control (Name = RadioButton) is checked
    }
}