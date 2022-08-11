using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class LoggingTests : DesktopTest
{
    [Test]
    [Category(Categories.CI)]
    public void CommonActionsWithDesktopControls_Wpf()
    {
        var calendar = App.Components.CreateByAutomationId<Calendar>("calendar");

        calendar.ValidateIsNotDisabled();

        var checkBox = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        // 1. Sometimes is useful to add information to the generated test log.
        // To do it you can use the BELLATRIX built-in logger through accessing it via App service.
        Logger.LogInformation("$$$ Before checking the transfer checkbox. $$$");

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

        // Generated Log, as you can see the above custom message is added to the log.
        // Start Test
        // Class = ValidateAssertionsTests Name = CommonActionsWithDesktopControls_Wpf
        // Validate control (AutomationId = calendar) is NOT disabled
        // $$$ Before checking the transfer checkbox. $$$
        // Check control (Name = BellaCheckBox) on WPF Sample App
        // Validate control (Name = BellaCheckBox) is checked
        // Select 'Item2' from control (AutomationId = select) on WPF Sample App
        // Click control (Name = RadioButton) on WPF Sample App
        // Validate control (Name = RadioButton) is checked
    }
}