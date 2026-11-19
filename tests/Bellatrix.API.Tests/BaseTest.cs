using Bellatrix.Api;

namespace Bellatrix.API.Tests;

public class BaseTest : MSTest.APITest
{
    public override void Configure()
    {
        base.Configure();
        
        BugReportingPlugin.Add();
        DynamicTestCasesPlugin.Add();
        
        BugReportingAssertExtensions.Add();
        DynamicTestCasesAssertExtensions.Add();
    }
}