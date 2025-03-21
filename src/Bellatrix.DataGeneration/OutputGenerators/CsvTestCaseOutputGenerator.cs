using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.OutputGenerators;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TextCopy;

public class CsvTestCaseOutputGenerator : TestCaseOutputGenerator
{
    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategory testCaseCategory = TestCaseCategory.All)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"\n🔹 **Generated CSV Output ({methodName}):**\n");

        foreach (var testCase in FilterTestCasesByCategory(testCases, testCaseCategory))
        {
            string csvLine = string.Join(",", testCase.Values.Select(value => $"\"{value}\""));
            sb.AppendLine(csvLine);
        }

        string output = sb.ToString();

        Console.WriteLine(output);
        Debug.WriteLine(output);

        ClipboardService.SetText(output);
        Console.WriteLine("✅ CSV output copied to clipboard.");
    }
}