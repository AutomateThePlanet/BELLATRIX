using System.Diagnostics;
using System.Text;
using TextCopy;

namespace Bellatrix.DataGeneration.OutputGenerators;

public class CsvTestCaseOutputGenerator : TestCaseOutputGenerator
{
    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategory testCaseCategory = TestCaseCategory.All)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"\n🔹 **Generated CSV Output ({methodName}):**\n");

        foreach (var testCase in FilterTestCasesByCategory(testCases, testCaseCategory))
        {
            var csvValues = testCase.Values.Select(v => $"\"{v.Value}\"").ToList();
            var message = testCase.Values.FirstOrDefault(v => !string.IsNullOrEmpty(v.ExpectedInvalidMessage))?.ExpectedInvalidMessage;
            if (!string.IsNullOrEmpty(message))
            {
                csvValues.Add($"\"{message}\"");
            }

            string csvLine = string.Join(",", csvValues);
            sb.AppendLine(csvLine);
        }

        string output = sb.ToString();

        Console.WriteLine(output);
        Debug.WriteLine(output);

        ClipboardService.SetText(output);
        Console.WriteLine("✅ CSV output copied to clipboard.");
    }
}