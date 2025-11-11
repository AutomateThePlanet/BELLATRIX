// <copyright file="LocatorSkill.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.Mobile.LLM.Skills.iOS;

public class IOSLocatorSkill
{
    [KernelFunction]
    public string BuildLocatorPrompt(string viewSummaryJson, string instruction, List<string> failedSelectors)
    {
        var failedInfo = failedSelectors.Any()
            ? string.Join("\n", failedSelectors.Select(x => $"- {x}"))
            : "(none)";

        return $"""
You are an AI assistant helping with iOS mobile UI test automation using Appium.

Your job is to generate a **single valid MobileBy.IosNSPredicate locator** expression based on:
- The user’s instruction
- A snapshot of visible elements
- A list of failed previous attempts

---

🔹 **Instruction:**  
{instruction}

🔹 **Previously Failed:**  
{failedInfo}

🔹 **Visible Elements (JSON):**  
{viewSummaryJson}

---

✅ Format Guidelines:
- Always return a valid NSPredicate string prefixed like:  
  `nspredicate=name == 'Login'`  
  or  
  `nspredicate=label CONTAINS 'Submit'`

- Use any of:  
  `name`, `label`, `value`, `type`, `visible == 1`

- Prefer `==` or `CONTAINS`, quote values with `'single quotes'`

---

🚫 DO NOT:
- Return `xpath=...` or `accessibilityid=...`
- Return multiple lines, comments, or explanations

---

✅ Return Format Examples:
- nspredicate=name == 'Login'
- nspredicate=label CONTAINS 'Total'
- nspredicate=name == 'Checkout' AND visible == 1

Return only a single line NSPredicate locator string.
""";
    }

    [KernelFunction]
    public string HealBrokenLocator(string failedLocator, string oldSnapshot, string newSnapshot)
    {
        return $"""
You are an AI assistant helping to fix broken iOS Appium locators.

The original locator failed:
❌ {failedLocator}

Please return a **new valid MobileBy.IosNSPredicate locator** that finds the same element.

---

🔹 Old Snapshot:
{oldSnapshot}

🔹 New Snapshot:
{newSnapshot}

---

✅ Format:  
Always return one line starting with:
`nspredicate=...`

Use any of these: `name`, `label`, `value`, `visible == 1`

🚫 No fallback logic, comments, or XPath

---

✅ Example:
nspredicate=label CONTAINS 'Order Summary'

Return only one valid NSPredicate locator string.
""";
    }
}