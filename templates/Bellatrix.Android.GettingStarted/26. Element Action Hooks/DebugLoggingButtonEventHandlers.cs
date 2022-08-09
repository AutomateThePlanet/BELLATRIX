using Bellatrix.Mobile.EventHandlers.Android;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android.GettingStarted;

public class DebugLoggingButtonEventHandlers : ButtonEventHandlers
{
    protected override void ClickingEventHandler(object sender, ComponentActionEventArgs<AndroidElement> arg)
    {
        DebugLogger.LogInfo($"Before clicking button. Coordinates: X={arg.Element.WrappedElement.Location.X} Y={arg.Element.WrappedElement.Location.Y}");
    }

    protected override void ClickedEventHandler(object sender, ComponentActionEventArgs<AndroidElement> arg)
    {
        DebugLogger.LogInfo($"After button clicked. Coordinates: X={arg.Element.WrappedElement.Location.X} Y={arg.Element.WrappedElement.Location.Y}");
    }
}
