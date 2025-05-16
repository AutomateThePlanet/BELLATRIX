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

namespace Bellatrix.Web.LLM.Plugins;

public class PageObjectSummarizerSkill
{
    [KernelFunction]
    public string SummarizePageObjectCode(string code)
    {
        return $"""
You are analyzing a BELLATRIX PageObject class written in C#. 
BELLATRIX separates PageObjects into `Map`, `Actions`, and `Assertions` files using partial classes, but sometimes they are combined into a single file.

Your goal is to extract:

1. The public override string Url if it exists.
2. All public UI element definitions from the Map part of the class using App.Components.CreateBy... or CreateAllBy... methods.

Return a summary in the following format:

PAGE_URL=https://some-url-if-present

locator_name | human description | locator strategy=locator value

---

✅ Rules for extracting:

- If the class contains a line like: public override string Url => "https://..."; extract and return that as PAGE_URL=...
- If no Url is defined, omit the PAGE_URL line.
- locator_name is the property name (e.g., BillingFirstName)
- human description is a readable lowercase explanation (e.g., "billing first name field")
- locator strategy must be one of: id=..., class=..., css=..., name=..., xpath=..., or attribute=...=...

---

🔹 Special Handling for BELLATRIX Extended Strategies:

- FindAttributeContainingStrategy → xpath=//*[@ATTRIBUTE*='VALUE']
- FindClassContainingStrategy → xpath=//*[contains(@class, 'VALUE')]
- FindIdContainingStrategy → xpath=//*[contains(@id, 'VALUE')]
- FindIdEndingWithStrategy → xpath=//*[substring(@id, string-length(@id) - string-length('VALUE') + 1) = 'VALUE']
- FindInnerTextContainsStrategy → xpath=//*[contains(normalize-space(text()), 'VALUE')]
- FindLinkTextContainsStrategy → xpath=//a[contains(normalize-space(text()), 'VALUE')]
- FindNameEndingWithStrategy → xpath=//*[substring(@name, string-length(@name) - string-length('VALUE') + 1) = 'VALUE']
- FindValueContainingStrategy → xpath=//*[contains(@value, 'VALUE')]

Replace ATTRIBUTE and VALUE with the actual values found in the code.

---

📝 Examples:
PAGE_URL=https://demos.bellatrix.solutions/cart/
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