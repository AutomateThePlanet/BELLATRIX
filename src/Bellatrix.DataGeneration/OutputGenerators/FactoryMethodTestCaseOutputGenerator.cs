using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.OutputGenerators;

public class FactoryMethodTestCaseOutputGenerator : TestCaseOutputGenerator
{
    private readonly string _modelName;
    private readonly string _methodName;

    public FactoryMethodTestCaseOutputGenerator(string modelName = "CheckoutFormModel", string methodName = "CreateTestCases")
    {
        _modelName = modelName;
        _methodName = methodName;
    }

    public override void GenerateOutput(string methodName, HashSet<TestCase> testCases, TestCaseCategoty testCaseCategoty = TestCaseCategoty.All)
    {
        Console.WriteLine($"\n🔹 **Generated Factory Method Output for {_modelName}:**\n");
        Console.WriteLine($"public static IEnumerable<{_modelName}> {_methodName}()");
        Console.WriteLine("{");
        Console.WriteLine("    return new List<CheckoutFormModel>");
        Console.WriteLine("    {");

        foreach (var testCase in FilterTestCasesByCategory(testCases, testCaseCategoty))
        {
            Console.WriteLine("        new CheckoutFormModel");
            Console.WriteLine("        {");
            Console.WriteLine($"            FirstName = \"{testCase.Values[0]}\",");
            Console.WriteLine($"            LastName = \"{testCase.Values[1]}\",");
            Console.WriteLine($"            ZipCode = \"{testCase.Values[2]}\",");
            Console.WriteLine($"            Phone = \"{testCase.Values[3]}\",");
            Console.WriteLine($"            Email = \"{testCase.Values[4]}\",");
            Console.WriteLine($"            Company = \"{testCase.Values[5]}\",");
            Console.WriteLine($"            Address = \"{testCase.Values[6]}\"");
            Console.WriteLine("        },");
        }

        Console.WriteLine("    };");
        Console.WriteLine("}");
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