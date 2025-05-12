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
