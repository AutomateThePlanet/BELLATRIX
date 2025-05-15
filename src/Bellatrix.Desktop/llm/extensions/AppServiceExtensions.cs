using Bellatrix.Desktop.Services;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.Desktop;

public static class AppServiceExtensions
{
    public static string GetPageSummaryJson(this AppService app)
    {
        var sourceXml = app.WrappedDriver.PageSource;

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
                Value = node.GetAttributeValue("Value.Value", null),
                HelpText = node.GetAttributeValue("HelpText", null)
            })
            .Where(x => !string.IsNullOrWhiteSpace(x.Name) || !string.IsNullOrWhiteSpace(x.AutomationId))
            .ToList();

        return JsonConvert.SerializeObject(elements, Formatting.None);
    }
}
