// <copyright file="PageObjectSummarizerSkill.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
// <note>This file is part of an academic research project exploring autonomous test agents using LLMs and Semantic Kernel.
// The architecture and agent logic are original contributions by Anton Angelov, forming the foundation for a PhD dissertation.
// Please cite or credit appropriately if reusing in academic or commercial work.</note>
using Microsoft.SemanticKernel;

namespace Bellatrix.Playwright.LLM.Plugins;

public class PageObjectSummarizerSkill
{
    [KernelFunction]
    public string SummarizePageObjectCode(string code)
    {
        return $"""
You are analyzing a BELLATRIX Web PageObject class written in C#. 
PageObjects may be split into partial classes (Map, Actions, Assertions), but your task is to extract the **Map**.

Your job is to extract:
1. The page URL if present (e.g., public override string Url => "https://...";)
2. All public UI element definitions declared using App.Components.CreateBy... or CreateAllBy... calls

Return the summary in this format:

PAGE_URL=https://some-url-if-present

locator_name | human description | xpath=XPath expression

---

✅ Mapping Rules (strict XPath mode):

- CreateById("value") → xpath=//*[@id='value']
- CreateByName("value") → xpath=//*[@name='value']
- CreateByClass("value") → xpath=//*[@class='value']
- CreateByXPath("value") → xpath=value (use as-is)
- CreateByCss("value") → xpath=//tag[contains(@class,'value')] — only if value maps to class
- CreateByAttribute("attr", "val") → xpath=//*[@attr='val']
- FindClassContainingStrategy("val") → xpath=//*[contains(@class,'val')]
- FindIdContainingStrategy("val") → xpath=//*[contains(@id,'val')]
- FindLinkTextContainsStrategy("val") → xpath=//a[contains(normalize-space(text()),'val')]
- FindInnerTextContainsStrategy("val") → xpath=//*[contains(normalize-space(text()),'val')]

⚠ Only return `xpath=...`. Do not use `id=`, `name=`, `css=`, `attribute=`, etc.

📝 locator_name = property name  
📝 human description = lowercased explanation (e.g., "checkout button")

---

📌 Examples:

PAGE_URL=https://demos.bellatrix.solutions/cart/
CheckoutButton | checkout button | xpath=//*[@id='checkout']
CartTotal | cart total span | xpath=//*[@class='order-total']//span
ViewCartLink | view cart link | xpath=//a[contains(normalize-space(text()),'View Cart')]

---

Here is the PageObject code to analyze:
---
{code}
---

Return ONLY the summary table. No explanations. No formatting. No code blocks.
""";
    }
}
