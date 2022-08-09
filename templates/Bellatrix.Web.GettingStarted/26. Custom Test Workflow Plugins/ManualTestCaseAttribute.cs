using System;

namespace Bellatrix.Web.GettingStarted;

[AttributeUsage(AttributeTargets.Method)]
public class ManualTestCaseAttribute : Attribute
{
    public ManualTestCaseAttribute(int testCaseId) => TestCaseId = testCaseId;

    public int TestCaseId { get; set; }
}