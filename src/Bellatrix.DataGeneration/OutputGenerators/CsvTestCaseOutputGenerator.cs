using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.OutputGenerators;

public class CsvTestCaseOutputGenerator : TestCaseOutputGenerator
{
    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategory testCaseCategoty = TestCaseCategory.All)
    {
        Console.WriteLine($"\n🔹 **Generated CSV Output ({methodName}):**\n");

        foreach (var testCase in FilterTestCasesByCategory(testCases, testCaseCategoty))
        {
            Console.WriteLine(string.Join(",", testCase.Values.Select(value => $"\"{value}\"")));
        }
    }
}
