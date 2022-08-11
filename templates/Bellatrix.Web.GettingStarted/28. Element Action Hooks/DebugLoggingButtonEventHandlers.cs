using Bellatrix.Web.Controls.EventHandlers;
using Bellatrix.Web.Events;

namespace Bellatrix.Web.GettingStarted.Advanced._27._Element_Actions_Hooks;

public class DebugLoggingButtonEventHandlers : ButtonEventHandlers
{
    protected override void ClickingEventHandler(object sender, ComponentActionEventArgs arg)
    {
        DebugLogger.LogInfo($"Before clicking button. Coordinates: X={arg.Element.WrappedElement.Location.X} Y={arg.Element.WrappedElement.Location.Y}");
    }

    protected override void ClickedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        DebugLogger.LogInfo($"After button clicked. Coordinates: X={arg.Element.WrappedElement.Location.X} Y={arg.Element.WrappedElement.Location.Y}");
    }

    protected override void HoveringEventHandler(object sender, ComponentActionEventArgs arg)
    {
        DebugLogger.LogInfo($"Before hovering button. Coordinates: X={arg.Element.WrappedElement.Location.X} Y={arg.Element.WrappedElement.Location.Y}");
    }

    protected override void HoveredEventHandler(object sender, ComponentActionEventArgs arg)
    {
        DebugLogger.LogInfo($"After button hovered. Coordinates: X={arg.Element.WrappedElement.Location.X} Y={arg.Element.WrappedElement.Location.Y}");
    }

    protected override void FocusingEventHandler(object sender, ComponentActionEventArgs arg)
    {
        DebugLogger.LogInfo($"Before focusing button. Coordinates: X={arg.Element.WrappedElement.Location.X} Y={arg.Element.WrappedElement.Location.Y}");
    }

    protected override void FocusedEventHandler(object sender, ComponentActionEventArgs arg)
    {
        DebugLogger.LogInfo($"After button focused. Coordinates: X={arg.Element.WrappedElement.Location.X} Y={arg.Element.WrappedElement.Location.Y}");
    }
}
