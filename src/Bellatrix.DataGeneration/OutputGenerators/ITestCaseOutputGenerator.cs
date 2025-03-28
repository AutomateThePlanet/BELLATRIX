namespace Bellatrix.DataGeneration.OutputGenerators;
public interface ITestCaseOutputGenerator
{
    void GenerateOutput(string methodName, IEnumerable<TestCase> testCases, TestCaseCategory testCaseCategoty = TestCaseCategory.All);
}
