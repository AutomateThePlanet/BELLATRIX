using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.OutputGenerators;
using System.Text.Json;

public class JsonTestCaseOutputGenerator : TestCaseOutputGenerator
{
    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategoty testCaseCategoty = TestCaseCategoty.All)
    {
        var jsonOutput = JsonSerializer.Serialize(FilterTestCasesByCategory(testCases, testCaseCategoty), new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine($"\n🔹 **Generated JSON Output ({methodName}):**\n");
        Console.WriteLine(jsonOutput);
    }
}