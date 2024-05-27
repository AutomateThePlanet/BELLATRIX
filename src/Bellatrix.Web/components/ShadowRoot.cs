using Bellatrix.Web.Contracts;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Bellatrix.Web.Components;
public class ShadowRoot : Component, IComponentInnerHtml
{
    public override IWebElement WrappedElement
    {
        get => new ShadowSearchContext((WebElement)base.WrappedElement);
        set => base.WrappedElement = value;
    }

    public string InnerHtml => JavaScriptService.Execute("return arguments[0].shadowRoot.innerHTML", WrappedElement);

    private class ShadowSearchContext : WebElement
    {
        public ShadowSearchContext(WebElement element) : base(WebDriver(element), ElementId(element))
        {
        }

        public override IWebElement FindElement(By by) => GetShadowRoot().FindElement(by);

        public override ReadOnlyCollection<IWebElement> FindElements(By by) => GetShadowRoot().FindElements(by);

        private static string ElementId(WebElement element) => (string)typeof(WebElement).GetField("elementId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(element);

        private static WebDriver WebDriver(WebElement element) => (WebDriver)typeof(WebElement).GetField("driver", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(element);
    }
}
