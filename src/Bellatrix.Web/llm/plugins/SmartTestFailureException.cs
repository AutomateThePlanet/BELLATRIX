using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.Web.LLM.Plugins;
public class SmartTestFailureException : Exception
{
    public SmartTestFailureException(string aiSummary, string fullAnalysis, Exception originalException)
        : base(Format(aiSummary, fullAnalysis, originalException))
    {
    }

    private static string Format(string summary, string details, Exception ex)
    {
        return $"""
🧠 AI-Driven Root Cause Summary:
{summary}

🧩 Extended Analysis:
{details}

❌ Original Exception:
{ex?.Message}
{ex?.StackTrace}
""";
    }
}
