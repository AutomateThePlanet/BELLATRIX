using Bellatrix.Mobile.LLM;
using Bellatrix.Mobile.Services;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile;

public static class AppServiceExtensions
{
    extension(AppService<AndroidDriver, AppiumElement> appService)
    {
        /// <summary>
        /// Returns a structured JSON summary of the current view hierarchy.
        /// This method parses the full UI tree as provided by the Appium driver, not just elements currently visible on the screen.
        /// It includes all elements present in the page source, regardless of their visibility or interactability.
        /// For each element, it extracts key attributes such as tag, resource-id, name, text, class, content-desc, label, and type.
        /// The result is a JSON array of summarized element objects, filtered to include only those with at least one of: Id, Text, or ContentDesc.
        /// </summary>
        /// <returns>JSON string summarizing the current UI hierarchy.</returns>
        public string GetCurrentViewSnapshot()
        {
            var xml = appService.PageSource;
            var doc = new HtmlDocument();
            doc.LoadHtml(xml);

            var nodes = doc.DocumentNode.SelectNodes("//*");
            if (nodes == null || nodes.Count == 0)
            {
                return "[]";
            }

            var summary = nodes.Select(node => new MobileElementSummary
                {
                    Tag = node.Name,
                    Id = node.GetAttributeValue("resource-id", null),
                    Text = node.GetAttributeValue("text", null),
                    Class = node.GetAttributeValue("class", null),
                    ContentDesc = node.GetAttributeValue("content-desc", null),
                    Type = node.GetAttributeValue("type", null)
                })
                .Where(e => !string.IsNullOrEmpty(e.Id) || !string.IsNullOrEmpty(e.Text) || !string.IsNullOrEmpty(e.ContentDesc))
                .ToList();

            return JsonConvert.SerializeObject(summary, Formatting.None);
        }
    }
    
    extension(AppService<IOSDriver, AppiumElement> appService)
    {
        /// <summary>
        /// Returns a structured JSON summary of the current view hierarchy.
        /// This method parses the full UI tree as provided by the Appium driver, not just elements currently visible on the screen.
        /// It includes all elements present in the page source, regardless of their visibility or interactability.
        /// For each element, it extracts key attributes such as tag, resource-id, name, text, class, content-desc, label, and type.
        /// The result is a JSON array of summarized element objects, filtered to include only those with at least one of: Id, Text, or ContentDesc.
        /// </summary>
        /// <returns>JSON string summarizing the current UI hierarchy.</returns>
        public string GetCurrentViewSnapshot()
        {
            var xml = appService.PageSource;
            var doc = new HtmlDocument();
            doc.LoadHtml(xml);

            var nodes = doc.DocumentNode.SelectNodes("//*");
            if (nodes == null || nodes.Count == 0)
            {
                return "[]";
            }

            var summary = nodes.Select(node => new MobileElementSummary
                {
                    Tag = node.Name,
                    Id = node.GetAttributeValue("name", null),
                    Text = node.GetAttributeValue("text", null),
                    Class = node.GetAttributeValue("class", null),
                    ContentDesc = node.GetAttributeValue("label", null),
                    Type = node.GetAttributeValue("type", null)
                })
                .Where(e => !string.IsNullOrEmpty(e.Id) || !string.IsNullOrEmpty(e.Text) || !string.IsNullOrEmpty(e.ContentDesc))
                .ToList();

            return JsonConvert.SerializeObject(summary, Formatting.None);
        }
    }
}