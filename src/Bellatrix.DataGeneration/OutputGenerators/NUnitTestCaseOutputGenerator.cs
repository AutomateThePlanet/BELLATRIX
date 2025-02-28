using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.OutputGenerators;

public class NUnitTestCaseOutputGenerator : TestCaseOutputGenerator
{
    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategory testCaseCategoty = TestCaseCategory.All)
    {
        Console.WriteLine("\n🔹 **Generated NUnit TestCaseSource Method:**\n");
        Console.WriteLine($"public static IEnumerable<object[]> {methodName}()");
        Console.WriteLine("{");
        Console.WriteLine("    return new List<object[]>");
        Console.WriteLine("    {");

        foreach (var testCase in FilterTestCasesByCategory(testCases, testCaseCategoty))
        {
            string formattedTestCase = string.Join("\", \"", testCase.Values.Select(x => x.Value));
            Console.WriteLine($"        new object[] {{ \"{formattedTestCase}\" }},");
        }

        Console.WriteLine("    };");
        Console.WriteLine("}");
    }
}