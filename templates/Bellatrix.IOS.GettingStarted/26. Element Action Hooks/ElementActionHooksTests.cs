using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class ElementActionHooksTests : NUnit.IOSTest
{
    // 1. Another way to extend BELLATRIX is to use the controls hooks. This is how the BDD logging is implemented.
    // For each method of the control, there are two hooks- one that is called before the action and one after.
    // For example, the available hooks for the button are:
    // Clicking - an event executed before button click
    // Clicked - an event executed after the button is clicked
    //
    // 2. You need to implement the event handlers for these events and subscribe them.
    // 3. BELLATRIX gives you again a shortcut- you need to create a class and inherit the {ControlName}EventHandlers
    // In the example, DebugLogger is called for each button event printing to Debug window the coordinates of the button.
    // You can call external logging provider, making screenshots before or after each action, the possibilities are limitless.
    //
    // 4. Once you have created the EventHandlers class, you need to tell BELLATRIX to use it. To do so call the App service method
    // Note: Usually, we add element event handlers in the AssemblyInitialize method which is called once for a test run.
    public override void TestsArrange()
    {
        App.AddComponentEventHandler<DebugLoggingButtonEventHandlers>();

        // If you need to remove it during the run you can use the method bellow.
        App.RemoveComponentEventHandler<DebugLoggingButtonEventHandlers>();

        // 5. Each BELLATRIX Validate method gives you a hook too.
        // To implement them you can derive the ValidateExtensionsEventHandlers base class and override the event handler methods you need.
        // For example for the method ValidateIsChecked, ValidatedIsCheckedEvent event is called after the check is done.
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();
    }
}