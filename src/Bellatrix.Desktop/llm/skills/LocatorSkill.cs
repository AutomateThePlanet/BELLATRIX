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

**WinAppDriver XPath Guidelines (strict compatibility):**

✅ Use XPath expressions with **direct attribute matches only**:
- `@AutomationId`
- `@Name`
- `@ClassName`
- `@ControlType`
- `@HelpText`
- `@Value.Value` (optional, if available)

✅ Examples of valid XPath:
- `//Edit[@Name='Username']`
- `//Button[@AutomationId='SubmitBtn']`
- `//Text[@HelpText='Tooltip message']`

✅ Format rules:
- Use lowercase tag names (e.g., `edit`, `button`, `text`)
- Attribute values must be wrapped in single quotes: `@Name='Login'`
- Return the shortest valid XPath with a single attribute condition

🚫 Do NOT use:
- `contains(...)`
- `normalize-space(...)`
- `substring(...)`
- XPath axes like `ancestor::`, `following::`, `preceding-sibling::`
- Position-based XPath (e.g., `(//Edit)[2]`)
- Multiple conditions (e.g., `[@Name='X' and @AutomationId='Y']`)

---

**Return Format:**
Only return a single valid XPath string like:
- //Edit[@Name='Username']
- //Button[@AutomationId='LoginBtn']
- //Pane[@ClassName='MainPanel']

Do not include:
- Explanations
- Multiple lines
- Comments
- Markdown formatting

Only return the XPath string.
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

✅ XPath must match one of the following formats:
- //Edit[@Name='Username']
- //Button[@AutomationId='Submit']
- //Text[@HelpText='Tooltip']

🚫 Do NOT use:
- contains()
- normalize-space()
- substring()
- any complex or relative XPath

---

**Return Format:**
Return only a single valid XPath expression, as a one-line string.

Return NOTHING else.
""";
    }
}