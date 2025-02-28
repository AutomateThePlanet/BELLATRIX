using Bellatrix.DataGeneration.OutputGenerators;

public class CsvTestCaseOutputGenerator : ITestCaseOutputGenerator
{
    public void GenerateOutput(string methodName, List<string[]> testCases)
    {
        Console.WriteLine($"\n🔹 **Generated CSV Output ({methodName}):**\n");

        foreach (var testCase in testCases)
        {
            Console.WriteLine(string.Join(",", testCase.Select(value => $"\"{value}\"")));
        }
    }
}
