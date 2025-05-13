namespace Bellatrix.Web.llm;
public static class BrowserServiceExtensions
{
    public static string GetPageSummaryJson(this BrowserService browser)
    {
        var javaScriptService = new JavaScriptService(browser.WrappedDriver);
        var script = @"
    return JSON.stringify(Array.from(document.querySelectorAll('*')).filter(el => {
        const style = window.getComputedStyle(el);
        const rect = el.getBoundingClientRect();
        return style.display !== 'none' &&
               style.visibility !== 'hidden' &&
               rect.width > 0 && rect.height > 0;
    }).map(el => {
        const tag = el.tagName.toLowerCase();
        const type = el.getAttribute('type');
        const isCheckbox = tag === 'input' && type === 'checkbox';
        return {
            tag: tag,
            id: el.id,
            class: el.className.trim(),
            text: el.innerText.trim(),
            type: type,
            name: el.getAttribute('name'),
            placeholder: el.getAttribute('placeholder'),
            ariaLabel: el.getAttribute('aria-label'),
            role: el.getAttribute('role'),
            href: el.getAttribute('href'),
            for: el.getAttribute('for'),
            value: el.getAttribute('value'),
            disabled: el.disabled,
            checked: isCheckbox ? el.checked : undefined,
            selected: el.selected
        };
    }));
";
        return (string)javaScriptService.Execute(script);
    }
}
