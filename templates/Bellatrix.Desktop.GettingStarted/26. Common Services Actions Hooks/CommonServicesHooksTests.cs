using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class CommonServicesHooksTests : DesktopTest
{
    // 1. Another way to extend BELLATRIX is to use the common services hooks. This is how the failed tests analysis works.
    // The base class for all web elements- Element provides a few special events as well:
    // ScrollingToVisible - called before scrolling
    // ScrolledToVisible - called after scrolling
    // CreatingElement - called before creating the element
    // CreatedElement - called after the creation of the element
    // CreatingElements - called before the creation of nested element
    // CreatedElements - called after the creation of nested element
    // ReturningWrappedElement - called before searching for native WebDriver element
    //
    // To add custom logic to the element's methods you can create a class that derives from ElementEventHandlers. The override the methods you like.
    [Test]
    [Category(Categories.CI)]
    public void CommonActionsWithDesktopControls_Wpf()
    {
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