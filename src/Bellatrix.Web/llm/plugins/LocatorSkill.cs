using Microsoft.SemanticKernel;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.Web.LLM.Plugins;
public class LocatorSkill
{
    [KernelFunction]
    public string BuildLocatorPrompt(string htmlSummary, string instruction, List<string> failedSelectors)
    {
        var failedInfo = failedSelectors.Any()
            ? string.Join("\n", failedSelectors.Select(x => $"- {x}"))
            : "(none)";

        return $"""
You are an AI assistant helping with UI test automation.

Your task is to generate a valid, simple, and reliable **XPath selector** for locating a specific UI element, based on:
- A user instruction
- A structured summary of all visible interactive elements
- A list of previously failed selectors

---

🔹 **User Instruction:**  
{instruction}

🔹 **Previously Tried and Failed Selectors:**  
{failedInfo}

🔹 **Visible Interactive Elements on the Page:**  
{htmlSummary}

---

**Guidelines for Generating XPath:**

✅ Always:
- Use **lowercase tag names**
- Prefer direct, short expressions using:
  - `@id`, `@name`, `@placeholder`, `@aria-label`, `@type`
- Use `contains(...)` and `normalize-space(...)` for partial or trimmed text matches
- Wrap all attribute values in **single quotes**: `@name='value'`
- Prefer `contains(@class,'xyz')` for class attributes

🚫 Avoid:
- Omitting quotes around attribute values (`@name=value` → ❌ invalid)
- Using XPath axes like `following::`, `ancestor::`, `preceding-sibling::` unless necessary
- Using `concat()` or `translate()` (these often produce broken XPath)
- Overly deep or complex paths that are fragile to layout changes
- Using `label` unless explicitly listed in the visible elements

If multiple correct selectors are possible, choose the **simplest, most direct, and reliable one**.

---

**Return Format:**
Only return a single, valid XPath string.

Examples:
- //input[@placeholder='Search']
- //button[contains(@class,'add-to-cart') and normalize-space()='Add to cart']

Do not include:
- Explanations
- Comments
- Code blocks
- Markdown formatting
- Multiple selectors

Return ONLY a single line of clean, valid XPath with quoted values.
""";
    }
}
