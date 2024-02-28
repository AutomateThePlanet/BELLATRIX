// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Desktop.GettingStarted.AppService.Extensions;
using Bellatrix.Desktop.NUnit;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class ExtendExistingCommonServicesTests : DesktopTest
{
    [Test]
    [Ignore("API example purposes only. No need to run.")]
    public void CommonActionsWithDesktopControls_Wpf()
    {
        // 2. Use newly added login method which is not part of the original implementation of the common service.
        App.AppService.LoginToApp("bellatrix", "topSecret");

        var calendar = App.Components.CreateByAutomationId<Calendar>("calendar");

        Assert.AreEqual(false, calendar.IsDisabled);

        var checkBox = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        checkBox.Check();

        Assert.IsTrue(checkBox.IsChecked);

        var comboBox = App.Components.CreateByAutomationId<ComboBox>("select");

        comboBox.SelectByText("Item2");

        Assert.AreEqual("Item2", comboBox.InnerText);

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");

        Assert.IsTrue(label.IsPresent);

        var radioButton = App.Components.CreateByName<RadioButton>("RadioButton");

        radioButton.Click();

        Assert.IsTrue(radioButton.IsChecked);
    }
}