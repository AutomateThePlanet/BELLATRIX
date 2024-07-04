using Bellatrix.Playwright.Components.ShadowDom;
using Bellatrix.Playwright.Contracts;

namespace Bellatrix.Playwright.Components;
public class ShadowRoot : Component, IComponentInnerHtml
{
    public string InnerHtml => ShadowDomService.GetShadowHtml(this);
}
