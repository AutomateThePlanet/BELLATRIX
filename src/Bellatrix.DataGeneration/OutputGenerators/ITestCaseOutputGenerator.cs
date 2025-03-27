namespace Bellatrix.DataGeneration.OutputGenerators;
public interface ITestCaseOutputGenerator
{
    void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategory testCaseCategoty = TestCaseCategory.All);
}
