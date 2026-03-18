namespace Bellatrix.Playwright;

public static class AppExtensions
{
    extension(App app)
    {
        // <summary>
        // Scans the current page DOM and returns a JSON summary of all visible elements, including their tag, id, class, text, and key attributes.
        // This method works by executing a recursive JavaScript function in the browser context, traversing the DOM and shadow DOMs, and collecting relevant data for each visible element.
        // Unlike using Chrome DevTools Protocol (CDP) snapshot features, this approach is browser-agnostic and does not rely on browser-specific APIs or remote debugging protocols.
        // As a result, it works in all browsers and web views supported by Selenium or Playwright, including environments where CDP is unavailable or restricted.
        // </summary>
        public string GetCurrentViewSnapshot()
        {
            const string script = /* lang=js */ """
                return JSON.stringify((function deepScan(root) {
                    const result = [];
                    const stack = [{ node: root, depth: 0 }];

                    while (stack.length) {
                        const { node, depth } = stack.pop();
                        if (!(node instanceof Element)) continue;

                        const style = window.getComputedStyle(node);
                        const rect = node.getBoundingClientRect();

                        if (style.display !== 'none' &&
                            style.visibility !== 'hidden' &&
                            rect.width > 0 &&
                            rect.height > 0) {
                            result.push({
                                tag: node.tagName.toLowerCase(),
                                id: node.id,
                                class: node.className.trim(),
                                text: node.innerText.trim(),
                                type: node.getAttribute('type'),
                                name: node.getAttribute('name'),
                                placeholder: node.getAttribute('placeholder'),
                                ariaLabel: node.getAttribute('aria-label'),
                                role: node.getAttribute('role'),
                                href: node.getAttribute('href'),
                                for: node.getAttribute('for'),
                                value: node.getAttribute('value'),
                                disabled: node.disabled,
                                checked: node.type === 'checkbox' ? node.checked : undefined,
                                selected: node.selected,
                                shadowDepth: depth,
                                isInShadowRoot: depth > 0
                            });
                        }

                        for (const child of node.children) {
                            stack.push({ node: child, depth });
                        }

                        if (node.shadowRoot) {
                            for (const shadowChild of node.shadowRoot.children) {
                                stack.push({ node: shadowChild, depth: depth + 1 });
                            }
                        }
                    }

                    return result;
                })(document.body));
                """;
            return app.JavaScript.Execute(script)?.ToString();
        }
    }
}