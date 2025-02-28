using Bellatrix.DataGeneration.OutputGenerators;
using System.Text.Json;

public class JsonTestCaseOutputGenerator : ITestCaseOutputGenerator
{
    public void GenerateOutput(string methodName, List<string[]> testCases)
    {
        var jsonOutput = JsonSerializer.Serialize(testCases, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine($"\n🔹 **Generated JSON Output ({methodName}):**\n");
        Console.WriteLine(jsonOutput);
    }
}