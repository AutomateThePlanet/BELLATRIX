namespace Bellatrix.DataGeneration.Generators;
public interface ITestCaseOutputGenerator
{
    void GenerateOutput(string methodName, List<string[]> testCases);
}
