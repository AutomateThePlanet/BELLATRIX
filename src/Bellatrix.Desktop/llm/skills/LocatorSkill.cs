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

namespace Bellatrix.Desktop.LLM.Plugins;

public class LocatorSkill
{
    [KernelFunction]
    public string BuildLocatorPrompt(string viewSummaryJson, string instruction, List<string> failedSelectors)
    {
        var failedInfo = failedSelectors.Any()
            ? string.Join("\n", failedSelectors.Select(x => $"- {x}"))
            : "(none)";

        return $"""
You are an AI assistant helping with Desktop UI test automation using WinAppDriver.

Your task is to generate a valid, simple, and reliable **XPath selector** for locating a specific UI element,
based on:
- A user instruction
- A structured summary of visible elements from the current view (WinAppDriver XML parsed as JSON)
- A list of previously failed selectors

---

🔹 **User Instruction:**  
{instruction}

🔹 **Previously Tried and Failed Selectors:**  
{failedInfo}

🔹 **Visible Interactive Elements (JSON):**  
{viewSummaryJson}

---

**STRICT WinAppDriver XPath Rules:**

✅ Use only **PascalCase** element tags (e.g., `ComboBox`, `Edit`, `Button`, `Text`, `Pane`, etc.)  
✅ Use only **PascalCase** attribute names (`AutomationId`, `Name`, `ClassName`, `ControlType`, `HelpText`, `Value.Value`).
✅ Use only direct, case-sensitive attribute matches (no contains, no normalize-space, etc).
✅ Example valid XPath:
- `//ComboBox[@AutomationId='select']`
- `//Edit[@Name='Username']`
- `//Button[@AutomationId='SubmitBtn']`
- `//Text[@HelpText='Tooltip message']`

🚫 NEVER use:
- Lowercase tag or attribute names (e.g., `//combobox[@automationid='select']` is INVALID)
- Any case conversion or function (translate, lower-case, upper-case, etc)
- contains(), normalize-space(), substring(), axes, positions, or multiple conditions.

**You MUST match tag and attribute names in PascalCase exactly as in the WinAppDriver XML.**

---

**Return Format:**
Return only a single valid, case-sensitive XPath string using PascalCase for both tag and attribute, e.g.:
- //ComboBox[@AutomationId='select']
- //Edit[@Name='Username']

🚫 Do NOT include:
- Explanations
- Multiple lines
- Comments
- Markdown formatting
- Code blocks (no triple backticks or ``` around the XPath)
- Extra whitespace or newlines before or after the XPath

✅ Return ONLY the XPath string as a single line, nothing else.
""";
    }


    [KernelFunction]
    public string HealBrokenLocator(string failedLocator, string oldSnapshot, string newSnapshot)
    {
        return $"""
You are an AI assistant helping to fix broken Desktop UI locators for WinAppDriver.

The original XPath locator is broken:
❌ Failed XPath: {failedLocator}

Your goal is to generate a **new valid XPath** that locates the same UI element,
using:
- A previously working view summary (JSON)
- A new snapshot after the failure

---

🔹 **Old View Summary:**
{oldSnapshot}

🔹 **New View Summary:**
{newSnapshot}

---

**STRICT WinAppDriver XPath Rules:**

✅ Use only **PascalCase** element tags (e.g., `ComboBox`, `Edit`, `Button`, `Text`, `Pane`, etc.)
✅ Use only **PascalCase** attribute names (`AutomationId`, `Name`, `ClassName`, `ControlType`, `HelpText`, `Value.Value`)
✅ Use only direct, case-sensitive attribute matches—NO functions, partial, or case-insensitive matching

✅ Example valid XPath:
- //ComboBox[@AutomationId='select']
- //Edit[@Name='Username']
- //Button[@AutomationId='SubmitBtn']
- //Text[@HelpText='Tooltip message']

🚫 NEVER use:
- Lowercase tag or attribute names (e.g., `//combobox[@automationid='select']` is INVALID)
- Any case conversion or function (`translate`, `lower-case`, `upper-case`)
- contains(), normalize-space(), substring(), axes, positions, or multiple conditions

**You MUST match tag and attribute names in PascalCase exactly as in the WinAppDriver XML.**

---

**Return Format:**
Only return a single valid, case-sensitive XPath string using PascalCase for both tag and attribute, e.g.:
- //ComboBox[@AutomationId='select']
- //Edit[@Name='Username']

🚫 Do NOT include:
- Explanations
- Multiple lines
- Comments
- Markdown formatting
- Code blocks (no triple backticks or ``` around the XPath)
- Extra whitespace or newlines before or after the XPath

✅ Return ONLY the XPath string as a single line, nothing else.
""";
    }

}