using System.Diagnostics;
using System.Text;
using TextCopy;

namespace Bellatrix.DataGeneration.OutputGenerators;


public class NUnitTestCaseAttributeOutputGenerator : TestCaseOutputGenerator
{
    public override void GenerateOutput(string methodName, IEnumerable<TestCase> testCases, TestCaseCategory testCaseCategory = TestCaseCategory.All)
    {
        var sb = new StringBuilder();

        sb.AppendLine("🔹 **Generated NUnit [TestCase(...)] Attributes:**\n");

        foreach (var testCase in FilterTestCasesByCategory(testCases, testCaseCategory))
        {
            var values = testCase.Values.Select(x => ToLiteral(x.Value)).ToList();

            var expectedMessage = testCase.Values.FirstOrDefault(v =>
                !string.IsNullOrWhiteSpace(v.ExpectedInvalidMessage))?.ExpectedInvalidMessage;

            if (!string.IsNullOrEmpty(expectedMessage))
            {
                sb.AppendLine($"[TestCase({string.Join(", ", values)}, ExpectedResult = {ToLiteral(expectedMessage)})]");
            }
            else
            {
                sb.AppendLine($"[TestCase({string.Join(", ", values)})]");
            }
        }

        string output = sb.ToString();

        Console.WriteLine(output);
        Debug.WriteLine(output);

        ClipboardService.SetText(output);
        Console.WriteLine("✅ Attributes copied to clipboard.");
    }

    private static string ToLiteral(object value)
    {
        return value switch
        {
            null => "null",
            string s => $"\"{s.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"",
            bool b => b.ToString().ToLowerInvariant(),
            _ => value.ToString()
        };
    }
}

//Example Output
//[TestCase("US", "en", "EU")]
//[TestCase("XX", "en", "EU", ExpectedResult = "Country code is invalid")]
//[TestCase("U1", "fr", "AS", ExpectedResult = "Country code must contain only letters")]
