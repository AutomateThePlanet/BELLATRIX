using Bellatrix.Web.Tests.MetaheuristicVersion2.Generators;
using System;
using System.Collections.Generic;
using System.Linq;

public class CsvTestCaseOutputGenerator : ITestCaseOutputGenerator
{
    public void GenerateOutput(string methodName, List<string[]> testCases)
    {
        Console.WriteLine($"\n🔹 **Generated CSV Output ({methodName}):**\n");

        foreach (var testCase in testCases)
        {
            Console.WriteLine(string.Join(",", testCase.Select(value => $"\"{value}\"")));
        }
    }
}
