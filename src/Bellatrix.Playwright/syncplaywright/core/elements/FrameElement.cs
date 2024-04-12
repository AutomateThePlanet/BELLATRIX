using System.Text.RegularExpressions;

namespace Bellatrix.Playwright.SyncPlaywright.Element;

/// <summary>
/// Wrapper for IFrameLocator with synchronous methods.
/// </summary>
public class FrameElement : WebElement
{
    internal FrameElement(BrowserPage page, ILocator locator)
        : base(page, locator)
    {
        WrappedFrameLocator = locator.FrameLocator(":scope");
    }

    internal FrameElement(BrowserPage page, WebElement element)
        : base(page, element)
    {
        WrappedFrameLocator = element.WrappedLocator.FrameLocator(":scope");
    }

    public IFrameLocator WrappedFrameLocator { get; set; }

    public override WebElement GetByAltText(string text, GetByAltTextOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByAltText(text, options.ConvertTo<FrameLocatorGetByAltTextOptions>()));
    }

    public override WebElement GetByAltText(Regex text, GetByAltTextOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByAltText(text, options.ConvertTo<FrameLocatorGetByAltTextOptions>()));
    }

    public override WebElement GetByLabel(string text, GetByLabelOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByLabel(text, options.ConvertTo<FrameLocatorGetByLabelOptions>()));
    }

    public override WebElement GetByLabel(Regex text, GetByLabelOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByLabel(text, options.ConvertTo<FrameLocatorGetByLabelOptions>()));
    }

    public override WebElement GetByPlaceholder(string text, GetByPlaceholderOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByPlaceholder(text, options.ConvertTo<FrameLocatorGetByPlaceholderOptions>()));
    }

    public override WebElement GetByPlaceholder(Regex text, GetByPlaceholderOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByPlaceholder(text, options.ConvertTo<FrameLocatorGetByPlaceholderOptions>()));
    }

    public override WebElement GetByRole(AriaRole role, GetByRoleOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByRole(role, options.ConvertTo<FrameLocatorGetByRoleOptions>()));
    }

    public override WebElement GetByTestId(string testId)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByTestId(testId));
    }

    public override WebElement GetByTestId(Regex testId)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByTestId(testId));
    }

    public override WebElement GetByText(string text, GetByTextOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByText(text, options.ConvertTo<FrameLocatorGetByTextOptions>()));
    }

    public override WebElement GetByText(Regex text, GetByTextOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByText(text, options.ConvertTo<FrameLocatorGetByTextOptions>()));
    }

    public override WebElement GetByTitle(string text, GetByTitleOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByTitle(text, options.ConvertTo<FrameLocatorGetByTitleOptions>()));
    }

    public override WebElement GetByTitle(Regex text, GetByTitleOptions options = null)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByTitle(text, options.ConvertTo<FrameLocatorGetByTitleOptions>()));
    }

    public override WebElement Locate(string selectorOrElement)
    {
        return new WebElement(Page, WrappedFrameLocator.Locator(selectorOrElement));
    }

    public override WebElement Locate(WebElement selectorOrElement)
    {
        return new WebElement(Page, WrappedFrameLocator.Locator(selectorOrElement.WrappedLocator));
    }

    public override FrameElement LocateFrame(string selector)
    {
        return new FrameElement(Page, WrappedFrameLocator.Locator(selector));
    }
}
