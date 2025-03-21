using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.OutputGenerators;
using System.Diagnostics;
using System.Text.Json;
using TextCopy;

public class JsonTestCaseOutputGenerator : TestCaseOutputGenerator
{
    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategory testCaseCategory = TestCaseCategory.All)
    {
        var filteredTestCases = FilterTestCasesByCategory(testCases, testCaseCategory);

        var jsonOutput = JsonSerializer.Serialize(
            filteredTestCases,
            new JsonSerializerOptions { WriteIndented = true });

        Console.WriteLine($"\n🔹 **Generated JSON Output ({methodName}):**\n");
        Console.WriteLine(jsonOutput);
        Debug.WriteLine(jsonOutput);

        ClipboardService.SetText(jsonOutput);
        Console.WriteLine("✅ JSON output copied to clipboard.");
    }
}