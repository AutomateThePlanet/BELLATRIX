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

namespace Bellatrix.Web.LLM.Skills;

public class PageObjectSummarizerSkill
{
    [KernelFunction]
    public string SummarizePageObjectCode(string code)
    {
        return $"""
You are analyzing a BELLATRIX Desktop PageObject class written in C#. 
The class may be split into partial classes (e.g., Map, Actions, Assertions), but your focus is to extract the **Map** definitions.

Your goal is to extract:
1. All public UI element definitions from the Map class using `App.Components.CreateBy...` or `CreateAllBy...`.

Return a summary in the following format:

locator_name | human description | xpath=XPath expression

---

✅ XPath Strategy Mapping Rules:

| Method                           | XPath Format Example                                              |
|----------------------------------|-------------------------------------------------------------------|
| CreateByAutomationId("value")    | xpath=//*[@AutomationId='value']                                  |
| CreateByName("value")            | xpath=//*[@Name='value']                                          |
| CreateByClassName("value")       | xpath=//*[@ClassName='value']                                     |
| CreateByXPath("value")           | xpath=value                                                       |
| FindIdEndingWithStrategy("val")  | xpath=//*[ends-with(@id, 'value')]                                |
| Others                           | xpath=//tag[@attribute='value']                                   |

📝 Replace `value` with the actual value passed to the method.

---

📌 Summary Format Example:
Transfer | transfer button | xpath=//*[@Name='E Button']
UserName | user name text field | xpath=//*[@AutomationId='textBox']

---

Here is the PageObject code to analyze:
---
{code}
---

Return ONLY the summary table. No explanations. No markdown. No formatting. No code block.
""";
    }
}