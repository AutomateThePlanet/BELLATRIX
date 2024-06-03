using Bellatrix.Playwright.Contracts;

namespace Bellatrix.Playwright.Components;
public class ShadowRoot : Component, IComponentInnerHtml
{
    public string InnerHtml => WrappedElement.Evaluate<string>("el => el.shadowRoot.innerHTML");
}
