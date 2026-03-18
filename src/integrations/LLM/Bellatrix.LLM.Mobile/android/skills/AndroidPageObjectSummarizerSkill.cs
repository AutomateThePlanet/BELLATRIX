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

namespace Bellatrix.Mobile.LLM.Skills.Android;

public class AndroidPageObjectSummarizerSkill
{
    [KernelFunction]
    public string SummarizePageObjectCode(string code)
    {
        return $"""
You are analyzing a BELLATRIX Android PageObject class written in C#. 
It may be split into multiple partial classes (e.g., Map, Actions, Assertions), but your task is to extract only the **Map** section with component declarations.

Your job is to extract all **public UI elements** declared using `App.Components.CreateBy...` or `CreateAllBy...`.

Return a summary table in this format:

locator_name | human description | uiautomator=UIAutomator expression

---

✅ Mapping Rules for Android Locators:

| Method                              | Locator Format                                                   |
|-------------------------------------|------------------------------------------------------------------|
| CreateByIdContaining("val")         | uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceIdMatches(".*val.*")) |
| CreateByText("val")                 | uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().textContains("val"))         |
| CreateByClass("val")                | uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().className("val"))            |
| CreateByDescriptionContaining("val")| uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().descriptionContains("val"))  |
| CreateByXPath("xpath")              | xpath=xpath                                                       |

📝 Notes:
- Replace `val` with the actual string passed in the code.
- Only return `uiautomator=` or `xpath=...` format. Do not use `id=`, `name=`, etc.
- The `locator_name` should match the C# property (e.g., `UserName`)
- The human description should be a lowercase readable name like “user name field”

---

📌 Example Output:

UserName | user name field | uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceIdMatches(".*edit.*"))
Password | password field | uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceIdMatches(".*edit2.*"))
Results | results label | uiautomator=new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().textContains("textColorPrimary"))

---

Here is the PageObject code to summarize:
---
{code}
---

Return ONLY the summary table. No explanations. No markdown. No formatting.
""";
    }
}
