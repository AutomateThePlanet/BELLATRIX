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

namespace Bellatrix.Mobile.LLM.Skills.iOS;

public class IOSPageObjectSummarizerSkill
{
    [KernelFunction]
    public string SummarizePageObjectCode(string code)
    {
        return $"""
You are analyzing a BELLATRIX iOS PageObject class written in C#. 
Your job is to extract all **public UI element definitions** from the Map class,
specifically the components declared with `App.Components.CreateBy...`.

Output the locator information in the following format:

locator_name | human description | nspredicate=NSPredicate expression

---

✅ Mapping Rules:

| Method used in code                  | Return format                                           |
|-------------------------------------|----------------------------------------------------------|
| CreateById("val")                   | nspredicate=name == 'val'                                |
| CreateByName("val")                 | nspredicate=label == 'val'                               |
| CreateByIOSNsPredicate("expr")      | nspredicate=expr (use as-is)                             |
| CreateByXPath("xpath")              | xpath=xpath (only fallback, prefer nspredicate=...)      |

📝 Notes:
- Replace `val` with the actual value from the code.
- Use `name`, `label`, or any NSPredicate-compliant field.
- Always quote strings with single quotes.
- Output `nspredicate=...` or `xpath=...`, nothing else.

---

📌 Example:

NumberOne | first number input field | nspredicate=name == 'IntegerA'  
Compute | compute sum button | nspredicate=label == 'ComputeSumButton'  
Answer | answer label | nspredicate=label == 'Answer'

---

Here is the PageObject code to summarize:
---
{code}
---

Return ONLY the summary table. No explanations. No markdown. No formatting. No code blocks.
""";
    }
}
