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
You are an AI assistant for Android UI test automation using Appium.

Your task is to generate a **valid UiAutomator selector string** for locating an Android element based on:
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

**UiAutomator Selector Rules (Strict):**

✅ Return only one of the following as a single line:
- new UiSelector().text("Login")
- new UiSelector().textContains("Settings")
- new UiSelector().description("Save")
- new UiSelector().descriptionContains("Edit")
- new UiSelector().resourceId("com.example:id/button")
- new UiSelector().className("android.widget.CheckBox")
- new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text("Submit"))

✅ If scrolling is needed, use:
- new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text("..."))

🚫 Do NOT use:
- Any `uiautomator=` prefix
- Any `=` between selectors
- XPath locators
- Explanations, comments, markdown, or code blocks
- Multiple selectors, only a single valid one

---

**Return Format:**  
Return ONLY the UiAutomator selector string as a single line.  
For example:
- new UiSelector().text("Login")
- new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().className("android.widget.CheckBox"))

Return nothing else.
""";
    }

    [KernelFunction]
    public string HealBrokenLocator(string failedLocator, string oldSnapshot, string newSnapshot)
    {
        return $"""
You are an AI assistant for fixing broken Android UI locators for Appium.

The original UiAutomator locator failed:
❌ Failed: {failedLocator}

Your task is to generate a **new valid UiAutomator selector string** based on:
- A previously working view summary (JSON)
- A new view snapshot after the failure

---

🔹 **Old View Snapshot (JSON):**
{oldSnapshot}

🔹 **New View Snapshot (JSON):**
{newSnapshot}

---

**Guidelines:**

✅ Use ONLY UiSelector or UiScrollable + UiSelector, e.g.:
- new UiSelector().text("Login")
- new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().descriptionContains("Settings"))

🚫 Do NOT use:
- Any `uiautomator=` prefix
- XPath selectors
- Comments, explanations, markdown, or code blocks

---

**Output Format:**  
Return ONLY the new UiAutomator selector as a single line, nothing else.
For example:
- new UiSelector().text("Submit")
- new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().textContains("Continue"))
""";
    }
}
