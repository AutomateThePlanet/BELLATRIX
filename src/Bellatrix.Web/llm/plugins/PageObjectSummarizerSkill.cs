using DocumentFormat.OpenXml.Presentation;
using Microsoft.SemanticKernel;

namespace Bellatrix.Web.LLM.Plugins;

public class PageObjectSummarizerSkill
{
    [KernelFunction]
    public string SummarizePageObjectCode(string code)
    {
        return $"""
You are analyzing a BELLATRIX PageObject class written in C#. 
BELLATRIX separates PageObjects into `Map`, `Actions`, and `Assertions` files using partial classes, but sometimes they are combined into a single file.

Your goal is to extract **only the public UI element definitions** from the `Map` part of the class. 
These are defined using `App.Components.CreateBy...` or `App.Components.CreateAllBy...` methods.

Return a summary in the following format (one per line):

locator_name | human description | locator strategy=locator value

---

✅ Rules for extracting:

- **locator_name** is the name of the property (e.g., `BillingFirstName`)
- **human description** is a readable lowercase explanation (e.g., "billing first name field")
- **locator strategy** must be one of: `id=...`, `class=...`, `css=...`, `name=...`, `xpath=...`, or `attribute=...=...` (when using custom strategies)

---

🔹 Special Handling for BELLATRIX Extended Strategies:

These map to XPath or CSS selectors as follows:

- `FindAttributeContainingStrategy` → `xpath=//*[@ATTRIBUTE*='VALUE']`
- `FindClassContainingStrategy` → `xpath=//*[contains(@class, 'VALUE')]`
- `FindIdContainingStrategy` → `xpath=//*[contains(@id, 'VALUE')]`
- `FindIdEndingWithStrategy` → `xpath=//*[substring(@id, string-length(@id) - string-length('VALUE') + 1) = 'VALUE']`
- `FindInnerTextContainsStrategy` → `xpath=//*[contains(normalize-space(text()), 'VALUE')]`
- `FindLinkTextContainsStrategy` → `xpath=//a[contains(normalize-space(text()), 'VALUE')]`
- `FindNameEndingWithStrategy` → `xpath=//*[substring(@name, string-length(@name) - string-length('VALUE') + 1) = 'VALUE']`
- `FindValueContainingStrategy` → `xpath=//*[contains(@value, 'VALUE')]`

Always convert the above to simplified XPath format when returning the final locator.
Replace ATTRIBUTE and VALUE with the actual attribute name and value from the code.

---

📝 Examples:
BillingFirstName | billing first name field | id=billing_first_name  
ViewCartButton | view cart button | class=added_to_cart wc-forward  
AddToCartFalcon9 | add to cart falcon 9 | attribute=data-product_id=28  
TotalSpan | total price span | xpath=//*[@class='order-total']//span

---

Here is the PageObject code to analyze:
---
{code}
---

Return ONLY the summary table. No explanation. No formatting. No code block.
""";
    }
}
