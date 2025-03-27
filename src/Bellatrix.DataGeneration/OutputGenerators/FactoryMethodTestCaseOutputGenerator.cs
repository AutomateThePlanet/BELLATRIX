using System.Diagnostics;
using System.Text;
using TextCopy;

namespace Bellatrix.DataGeneration.OutputGenerators;

public class FactoryMethodTestCaseOutputGenerator : TestCaseOutputGenerator
{
    private readonly string _modelName;
    private readonly string _methodName;

    public FactoryMethodTestCaseOutputGenerator(string modelName = "CheckoutFormModel", string methodName = "CreateTestCases")
    {
        _modelName = modelName;
        _methodName = methodName;
    }

    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategory testCaseCategory = TestCaseCategory.All)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"\n🔹 **Generated Factory Method Output for {_modelName}:**\n");
        sb.AppendLine($"public static IEnumerable<{_modelName}> {_methodName}()");
        sb.AppendLine("{");
        sb.AppendLine($"    return new List<{_modelName}>");
        sb.AppendLine("    {");

        foreach (var testCase in FilterTestCasesByCategory(testCases, testCaseCategory))
        {
            sb.AppendLine($"        new {_modelName}");
            sb.AppendLine("        {");
            sb.AppendLine($"            FirstName = \"{testCase.Values[0].Value}\",");
            sb.AppendLine($"            LastName = \"{testCase.Values[1].Value}\",");
            sb.AppendLine($"            ZipCode = \"{testCase.Values[2].Value}\",");
            sb.AppendLine($"            Phone = \"{testCase.Values[3].Value}\",");
            sb.AppendLine($"            Email = \"{testCase.Values[4].Value}\",");
            sb.AppendLine($"            Company = \"{testCase.Values[5].Value}\",");
            sb.AppendLine($"            Address = \"{testCase.Values[6].Value}\"");

            var message = testCase.Values.FirstOrDefault(v => !string.IsNullOrEmpty(v.ExpectedInvalidMessage))?.ExpectedInvalidMessage;
            if (!string.IsNullOrEmpty(message))
            {
                sb.AppendLine($"            ,ExpectedInvalidMessage = \"{message}\"");
            }

            sb.AppendLine("        },");
        }

        sb.AppendLine("    };");
        sb.AppendLine("}");

        var output = sb.ToString();

        Console.WriteLine(output);
        Debug.WriteLine(output);

        ClipboardService.SetText(output);
        Console.WriteLine("✅ Factory method output copied to clipboard.");
    }
}


// Example generated output:
//public static IEnumerable<CheckoutFormModel> CreateTestCases()
//{
//    return new List<CheckoutFormModel>
//    {
//        new CheckoutFormModel
//        {
//            FirstName = "John",
//            LastName = "Doe",
//            ZipCode = "12345",
//            Phone = "+359888888888",
//            Email = "john.doe@example.com",
//            Company = "SomeCompany",
//            Address = "123 Main St"
//        },
//        new CheckoutFormModel
//        {
//            FirstName = "A",
//            LastName = "B",
//            ZipCode = "99999",
//            Phone = "+359777777777",
//            Email = "invalid-email@",
//            Company = "Short",
//            Address = "XYZ Street"
//        },
//        new CheckoutFormModel
//        {
//            FirstName = "LongFirstNameExceedsLimit",
//            LastName = "ValidLastName",
//            ZipCode = "00000",
//            Phone = "+359000000000",
//            Email = "test@mail.com",
//            Company = "Enterprise",
//            Address = "456 Business Ave"
//        }
//    };
//}