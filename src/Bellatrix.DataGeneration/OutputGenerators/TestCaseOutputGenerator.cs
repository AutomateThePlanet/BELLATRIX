namespace Bellatrix.DataGeneration.OutputGenerators;

public abstract class TestCaseOutputGenerator : ITestCaseOutputGenerator
{
    public abstract void GenerateOutput(string methodName, IEnumerable<TestCase> testCases, TestCaseCategory testCaseCategoty = TestCaseCategory.All);

    protected IEnumerable<TestCase> FilterTestCasesByCategory(IEnumerable<TestCase> testCases, TestCaseCategory testCaseCategoty)
    {
        return testCaseCategoty switch
        {
            TestCaseCategory.Valid => testCases.Where(tc => tc.Values.All(v => v.Category == TestValueCategory.Valid || v.Category == TestValueCategory.BoundaryValid)).ToHashSet(),
            TestCaseCategory.Validation => testCases.Where(tc => tc.Values.Any(v => v.Category == TestValueCategory.Invalid || v.Category == TestValueCategory.BoundaryInvalid)).ToHashSet(),
            _ => testCases // TestCaseCategoty.All - Keep all test cases
        };
    }
}
