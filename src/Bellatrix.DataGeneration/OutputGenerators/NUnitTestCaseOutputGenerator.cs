using Bellatrix.DataGeneration.Models;
using System.Diagnostics;
using System.Text;
using TextCopy;

namespace Bellatrix.DataGeneration.OutputGenerators;

public class NUnitTestCaseOutputGenerator : TestCaseOutputGenerator
{
    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategory testCaseCategory = TestCaseCategory.All)
    {
        var sb = new StringBuilder();

        sb.AppendLine("\n🔹 **Generated NUnit TestCaseSource Method:**\n");
        sb.AppendLine($"public static IEnumerable<object[]> {methodName}()");
        sb.AppendLine("{");
        sb.AppendLine("    return new List<object[]");
        sb.AppendLine("    {");

        foreach (var testCase in FilterTestCasesByCategory(testCases, testCaseCategory))
        {
            var values = testCase.Values.Select(x => $"\"{x.Value}\"").ToList();
            var message = testCase.Values.FirstOrDefault(v => !string.IsNullOrEmpty(v.ExpectedInvalidMessage))?.ExpectedInvalidMessage;
            if (!string.IsNullOrEmpty(message))
            {
                values.Add($"\"{message}\"");
            }

            sb.AppendLine($"        new object[] {{ {string.Join(", ", values)} }},");
        }

        sb.AppendLine("    };");
        sb.AppendLine("}");

        string output = sb.ToString();

        Console.WriteLine(output);
        Debug.WriteLine(output);

        ClipboardService.SetText(output);
        Console.WriteLine("✅ Method copied to clipboard.");
    }
}