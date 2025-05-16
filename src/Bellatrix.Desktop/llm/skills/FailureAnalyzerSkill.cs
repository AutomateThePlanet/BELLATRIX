// <copyright file="FailureAnalyzerSkill.cs" company="Automate The Planet Ltd.">
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

public class FailureAnalyzerSkill
{
    [KernelFunction]
    public string GenerateFailureDiagnosis(
        string testName,
        string failingLog,
        string passedLog,
        string failingSummary,
        string passedSummary,
        string exceptionDetails,
        string screenshotHint = "See attached image.")
    {
        return $"""
You are an expert QA automation assistant. A test named **{testName}** has failed.

Analyze the provided logs, DOM snapshots, stack trace, and screenshot context to diagnose the issue.

Use the following structured response format:

---

### 🧠 Root Cause Classification:
Pick **only one** of the following (based on all evidence):
- `App Bug` — Application logic, UI, or backend failed
- `Test Issue` — Selector, timing, logic, or missing wait
- `Test Data Problem` — Data used is invalid, expired, or malformed
- `Environment Issue` — Network, DNS, service unavailability
- `Needs Investigation` — Inconclusive; recommend manual inspection

Also **justify** your classification briefly.

---

### 🛠 Recommended Actions:
List 2–5 specific steps the test engineer should take to resolve the issue.
Avoid vague suggestions. Reference concrete changes to the page object, environment, or test input.

---

### 🧩 Reasoning and Evidence:
- Point out key differences in DOMs
- Explain how the error message or visible UI supports your claim
- Reference stack trace if it's relevant
- Call out whether self-healing occurred or failed

---

### 📄 Provided Context:

🧪 Failed Test Log:
{failingLog}

✅ Previously Passed Log:
{passedLog}

📄 DOM Snapshot (Old - Passed):
{passedSummary}

📄 DOM Snapshot (New - Failed):
{failingSummary}

❌ Exception Stack Trace:
{exceptionDetails}

🖼️ Screenshot:
{screenshotHint}
""";
    }
}
