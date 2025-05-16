// <copyright file="AssertionSkill.cs" company="Automate The Planet Ltd.">
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
public class AssertionSkill
{
    [KernelFunction]
    public string EvaluateAssertion(string htmlSummary, string assertInstruction)
    {
        return $"""
You are an expert test automation assistant.

Your task is to verify whether the user's **assertion** is satisfied, based on the structured JSON summary of the current web page's visible and interactive elements.

---

🔹 **User Assertion Instruction:**
{assertInstruction}

🔹 **Page Summary:**
{htmlSummary}

---

✅ If the condition is met, respond with:  
**PASS**

❌ If the condition is NOT met, respond with:  
**FAIL: [explanation]**

---

Only return **PASS** or **FAIL: explanation**.
""";
    }
}
