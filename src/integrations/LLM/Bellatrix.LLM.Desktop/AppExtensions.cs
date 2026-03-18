using HtmlAgilityPack;
using Newtonsoft.Json;

namespace Bellatrix.Desktop;

public static class AppExtensions
{
    extension(App app)
    {
        /// <summary>
        /// Generates a JSON summary of the current desktop application's UI tree.
        /// Loads the current page source as XML, parses all nodes, and extracts key attributes
        /// (such as Tag, AutomationId, Name, ClassName, ControlType, Value, and HelpText) for each element.
        /// Only elements with a non-empty Name or AutomationId are included in the summary.
        /// Returns the result as a compact JSON array of element summaries.
        /// </summary>
        public string GetCurrentViewSnapshot()
        {
            var sourceXml = app.AppService.WrappedDriver.PageSource;

            var doc = new HtmlDocument();
            doc.LoadHtml(sourceXml);

            var nodes = doc.DocumentNode.SelectNodes("//*");
            if (nodes == null || nodes.Count == 0)
            {
                return "[]";
            }
            
            List<DesktopElementSummary> elements = nodes
                .Select(node => new DesktopElementSummary
                {
                    Tag = node.Name,
                    AutomationId = node.GetAttributeValue("AutomationId", null),
                    Name = node.GetAttributeValue("Name", null),
                    ClassName = node.GetAttributeValue("ClassName", null),
                    ControlType = node.GetAttributeValue("ControlType", null),
                    Value = node.GetAttributeValue("Value.Value", null) ?? node.GetAttributeValue("Value", null),
                    HelpText = node.GetAttributeValue("HelpText", null),
                    IsSelected = ParseNullableBool(node.GetAttributeValue("IsSelected", null)),
                    ToggleState = node.GetAttributeValue("ToggleState", null),
                    ExpandCollapseState = node.GetAttributeValue("ExpandCollapseState", null),
                    Selection = node.GetAttributeValue("Selection", null),
                    IsEnabled = ParseNullableBool(node.GetAttributeValue("IsEnabled", null)),
                    IsOffscreen = ParseNullableBool(node.GetAttributeValue("IsOffscreen", null)),
                    IsPassword = ParseNullableBool(node.GetAttributeValue("IsPassword", null)),
                    IsAvailable = ParseNullableBool(node.GetAttributeValue("IsAvailable", null)),
                    HasKeyboardFocus = ParseNullableBool(node.GetAttributeValue("HasKeyboardFocus", null)),
                    IsKeyboardFocusable = ParseNullableBool(node.GetAttributeValue("IsKeyboardFocusable", null)),
                    Orientation = node.GetAttributeValue("Orientation", null),
                    Minimum = node.GetAttributeValue("Minimum", null),
                    Maximum = node.GetAttributeValue("Maximum", null),
                    LargeChange = node.GetAttributeValue("LargeChange", null),
                    SmallChange = node.GetAttributeValue("SmallChange", null)
                })
                .Where(x => !string.IsNullOrWhiteSpace(x.Name) || !string.IsNullOrWhiteSpace(x.AutomationId))
                .ToList();

            return JsonConvert.SerializeObject(elements, Formatting.None);
        }

        private static bool? ParseNullableBool(string value)
        {
            if (value == null)
                return null;
            if (bool.TryParse(value, out var result))
                return result;
            return null;
        }
    }
}