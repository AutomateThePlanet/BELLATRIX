using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.OutputGenerators;

public abstract class TestCaseOutputGenerator : ITestCaseOutputGenerator
{
    public abstract void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategoty testCaseCategoty = TestCaseCategoty.All);

    protected HashSet<TestCase> FilterTestCasesByCategory(HashSet<TestCase> testCases, TestCaseCategoty testCaseCategoty)
    {
        return testCaseCategoty switch
        {
            TestCaseCategoty.Valid => testCases.Where(tc => tc.Values.All(v => v.Category == TestValueCategory.Valid || v.Category == TestValueCategory.BoundaryValid)).ToHashSet(),
            TestCaseCategoty.Validation => testCases.Where(tc => tc.Values.Any(v => v.Category == TestValueCategory.Invalid || v.Category == TestValueCategory.BoundaryInvalid)).ToHashSet(),
            _ => testCases // TestCaseCategoty.All - Keep all test cases
        };
    }
}
