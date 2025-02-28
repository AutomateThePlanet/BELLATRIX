using Bellatrix.DataGeneration.Generators;

public class NUnitTestCaseOutputGenerator : ITestCaseOutputGenerator
{
    public void GenerateOutput(string methodName, List<string[]> testCases)
    {
        Console.WriteLine("\n🔹 **Generated NUnit TestCaseSource Method:**\n");
        Console.WriteLine($"public static IEnumerable<object[]> {methodName}()");
        Console.WriteLine("{");
        Console.WriteLine("    return new List<object[]>");
        Console.WriteLine("    {");

        foreach (var testCase in testCases)
        {
            string formattedTestCase = string.Join("\", \"", testCase);
            Console.WriteLine($"        new object[] {{ \"{formattedTestCase}\" }},");
        }

        Console.WriteLine("    };");
        Console.WriteLine("}");
    }
}