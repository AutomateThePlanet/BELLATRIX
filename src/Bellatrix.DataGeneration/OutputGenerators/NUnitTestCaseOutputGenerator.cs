using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.OutputGenerators;
using System.Diagnostics;
using System.Text;
using TextCopy;

public class NUnitTestCaseOutputGenerator : TestCaseOutputGenerator
{
    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategory testCaseCategory = TestCaseCategory.All)
    {
        var sb = new StringBuilder();

        sb.AppendLine("\n🔹 **Generated NUnit TestCaseSource Method:**\n");
        sb.AppendLine($"public static IEnumerable<object[]> {methodName}()");
        sb.AppendLine("{");
        sb.AppendLine("    return new List<object[]>");
        sb.AppendLine("    {");

        foreach (var testCase in FilterTestCasesByCategory(testCases, testCaseCategory))
        {
            string formattedTestCase = string.Join("\", \"", testCase.Values.Select(x => x.Value));
            sb.AppendLine($"        new object[] {{ \"{formattedTestCase}\" }},");
        }

        sb.AppendLine("    };");
        sb.AppendLine("}");

        string output = sb.ToString();

        Console.WriteLine(output);
        Debug.WriteLine(output);

        ClipboardService.SetText("output");
        Console.WriteLine("✅ Method copied to clipboard.");
    }
}