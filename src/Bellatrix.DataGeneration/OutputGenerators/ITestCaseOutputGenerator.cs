using Bellatrix.DataGeneration.Models;

namespace Bellatrix.DataGeneration.OutputGenerators;
public interface ITestCaseOutputGenerator
{
    void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategoty testCaseCategoty = TestCaseCategoty.All);
}
