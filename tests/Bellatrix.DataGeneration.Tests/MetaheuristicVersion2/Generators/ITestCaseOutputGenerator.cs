using System.Collections.Generic;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Generators;
public interface ITestCaseOutputGenerator
{
    void GenerateOutput(string methodName, List<string[]> testCases);
}
