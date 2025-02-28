namespace Bellatrix.DataGeneration.OutputGenerators;
public interface ITestCaseOutputGenerator
{
    void GenerateOutput(string methodName, List<string[]> testCases);
}
