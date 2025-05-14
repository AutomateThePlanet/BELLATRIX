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
