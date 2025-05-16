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

namespace Bellatrix.Mobile.LLM.Skills;

public class AndroidLocatorSkill
{
    [KernelFunction]
    public string BuildLocatorPrompt(string viewSummaryJson, string instruction, List<string> failedSelectors)
    {
        var failedInfo = failedSelectors.Any()
            ? string.Join("\n", failedSelectors.Select(x => $"- {x}"))
            : "(none)";

        return $"""
You are an AI assistant helping with Android mobile UI test automation using Appium.

Your task is to generate a valid, reliable **Android UIAutomator locator** that will scroll to and find the element based on:
- A user instruction
- A structured snapshot of visible elements (JSON)
- A list of previously failed selectors

---

🔹 **User Instruction:**  
{instruction}

🔹 **Previously Tried and Failed Locators:**  
{failedInfo}

🔹 **Visible Elements Snapshot (JSON):**  
{viewSummaryJson}

---

✅ Return a **UiSelector** expression inside **UiScrollable** to scroll to the element:
- textContains("...")
- descriptionContains("...")
- className("...")
- resourceIdMatches(".*value.*")

✅ Format Examples:
- uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().textContains("Login"))
- uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().descriptionContains("Settings"))
- uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceIdMatches(".*submit.*"))
- uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().className("android.widget.Button"))

🚫 Do NOT use:
- XPath locators
- Multiple expressions or fallback options
- Explanations or comments

---

**Return Format:**
Only return a single line like:
uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().textContains("Login"))

Do not include anything else.
""";
    }

    [KernelFunction]
    public string HealBrokenLocator(string failedLocator, string oldSnapshot, string newSnapshot)
    {
        return $"""
You are an AI assistant helping to fix broken Android locators for Appium tests.

The original locator failed:
❌ Failed: {failedLocator}

Your job is to return a **new valid uiautomator locator** using the updated view snapshot.

---

🔹 **Old View Snapshot (JSON):**
{oldSnapshot}

🔹 **New View Snapshot (JSON):**
{newSnapshot}

---

✅ Guidelines:
- Use UiScrollable + UiSelector syntax
- Prefer textContains, descriptionContains, resourceIdMatches
- One clean line, no explanations

---

**Output Format Example:**
uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().textContains("Login"))

Return ONLY a single locator line.
""";
    }
}
