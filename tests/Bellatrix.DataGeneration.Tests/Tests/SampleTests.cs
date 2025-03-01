//using System.Collections.Generic;
//using NUnit.Framework;
//using Bellatrix.DataGeneration;
//using Bellatrix.DataGeneration.Models;
//using Bellatrix.DataGeneration.Parameters;
//using Bellatrix.DataGeneration.Contracts;
//using Bellatrix.DataGeneration.OutputGenerators;
//using System;
//using System.Diagnostics;

//namespace Bellatrix.DataGeneration.Tests.Tests;

//[TestFixture]
//public class SampleTests
//{
//    // ✅ This method provides the test parameters.
//    public static List<IInputParameter> ABCGeneratedTestParameters()
//    {
//        return new List<IInputParameter>
//        {
//            new TextDataParameter(minBoundary: 6, maxBoundary: 12),
//            new EmailDataParameter(minBoundary: 5, maxBoundary: 10),
//            new PhoneDataParameter(minBoundary: 6, maxBoundary: 8),
//            new TextDataParameter(minBoundary: 4, maxBoundary: 10),
//        };
//    }
//    //public static List<IInputParameter> ABCGeneratedTestParameters()
//    //{
//    //    return new List<IInputParameter>
//    //    {
//    //        new TextDataParameter(isManualMode: true, customValues: new[]
//    //        {
//    //            new TestValue("Normal1", TestValueCategory.Valid),
//    //            new TestValue("BoundaryMin-1", TestValueCategory.BoundaryInvalid),
//    //            new TestValue("BoundaryMin", TestValueCategory.BoundaryValid),
//    //            new TestValue("BoundaryMax", TestValueCategory.BoundaryValid),
//    //            new TestValue("BoundaryMax+1", TestValueCategory.BoundaryInvalid),
//    //            new TestValue("Invalid1", TestValueCategory.Invalid)
//    //        }),
//    //        new EmailDataParameter(isManualMode: true, customValues: new[]
//    //        {
//    //            new TestValue("test@mail.comMIN-1", TestValueCategory.BoundaryInvalid),
//    //            new TestValue("test@mail.comMIN", TestValueCategory.BoundaryValid),
//    //            new TestValue("test@mail.comMAX", TestValueCategory.BoundaryValid),
//    //            new TestValue("test@mail.comMAX+1", TestValueCategory.BoundaryInvalid),
//    //            new TestValue("test@mail.com", TestValueCategory.Valid),
//    //            new TestValue("invalid@mail", TestValueCategory.Invalid)
//    //        }),
//    //        new PhoneDataParameter(isManualMode: true, customValues: new[]
//    //        {
//    //            new TestValue("+359888888888", TestValueCategory.Valid),
//    //            new TestValue("000000", TestValueCategory.Invalid)
//    //        }),
//    //        new TextDataParameter(isManualMode: true, customValues: new[]
//    //        {
//    //            new TestValue("NormalX", TestValueCategory.Valid)
//    //        }),
//    //    };
//    //}

//    // ✅ Test method using ABC-driven test cases
//    [Test, ABCTestCaseSource(nameof(ABCGeneratedTestParameters), TestCaseCategory.Validation)]
//    public void TestABCGeneration(string textValue, string email, string phone, string anotherText)
//    {
//        Debug.WriteLine($"Running test with: {textValue}, {email}, {phone}, {anotherText}");

//        Assert.That(textValue, Is.Not.Null);
//        Assert.That(email, Is.Not.Null);
//        Assert.That(phone, Is.Not.Null);
//        Assert.That(anotherText, Is.Not.Null);

//    }
//}
