using System.Diagnostics;
using System.Text.Json;
using TextCopy;

namespace Bellatrix.DataGeneration.OutputGenerators;

public class JsonTestCaseOutputGenerator : TestCaseOutputGenerator
{
    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategory testCaseCategory = TestCaseCategory.All)
    {
        var filteredTestCases = FilterTestCasesByCategory(testCases, testCaseCategory);

        var jsonReadyCases = filteredTestCases.Select(tc =>
        {
            var values = tc.Values.Select(v => v.Value).ToList();
            var message = tc.Values.FirstOrDefault(v => !string.IsNullOrEmpty(v.ExpectedInvalidMessage))?.ExpectedInvalidMessage;
            if (!string.IsNullOrEmpty(message))
            {
                values.Add(message);
            }
            return values;
        });

        var jsonOutput = JsonSerializer.Serialize(jsonReadyCases, new JsonSerializerOptions { WriteIndented = true });

        Console.WriteLine($"\n🔹 **Generated JSON Output ({methodName}):**\n");
        Console.WriteLine(jsonOutput);
        Debug.WriteLine(jsonOutput);

        ClipboardService.SetText(jsonOutput);
        Console.WriteLine("✅ JSON output copied to clipboard.");
    }
}