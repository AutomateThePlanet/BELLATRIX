//using Bellatrix.Web.GettingStarted._12._Page_Objects;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;

//namespace Bellatrix.Web.GettingStarted;

//// TODO: Bootstrap example w. inputs with all attributes test
//[TestFixture]
//[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
//public class PageObjectsTests : NUnit.WebTest
//{
//    [Test]
//    [TestCaseSource("GetTestCases")]
//    //[TestCase("John", "Doe", "12345", "0899000000", "john.doe@example.com")]
//    public void PurchaseRocketWithPageObjects_ABC_Bees(
//        string firstName,
//        string lastName,
//        string zip,
//        string phone,
//        string email)
//    {
//        var homePage = App.GoTo<HomePage>();
//        homePage.AddProductById(28);
//        homePage.ViewCartButton.Click();
//        var cartPage = App.Create<CartPage>();
//        cartPage.ProceedToCheckout.Click();
//        var checkoutPage = App.Create<CheckoutPage>();
//        GenerateAndPrintTestCases(checkoutPage);
//    }

//    private void GenerateAndPrintTestCases(CheckoutPage checkoutPage)
//    {
//        var parameters = new List<IInputParameter>
//        {
//            new ComponentInputParameter<TextField>(checkoutPage.BillingFirstName),
//            new ComponentInputParameter<TextField>(checkoutPage.BillingLastName),
//            new ComponentInputParameter<TextField>(checkoutPage.BillingZip),
//            new ComponentInputParameter<Phone>(checkoutPage.BillingPhone),
//            new ComponentInputParameter<Email>(checkoutPage.BillingEmail),
            
//            new ManualInputParameter(InputType.Text, 3, 10), // company
//            new ManualInputParameter(InputType.Text, 5, 25)  // address
//        };

//        var generator = new ABCTestCaseGenerator(allowMultipleInvalidInputs:false);
//        generator.PrintTestCaseSourceMethod("GetTestCases", parameters);
//    }

//    public static IEnumerable<object[]> GetTestCases()
//    {
//        return new List<object[]>
//        {
//            new object[] { "AAAAAAAAAAAAAAAAAAAAA", "AAAAAAAAAAAAAAAAAAAAA", "AAAAAAAAAAAAAAAAAAAA", "+35999999999999", "x@x.x", "AAA", "AAAAA" },
//            new object[] { "AAAA", "AAAA", "AAAAA", "+35999999999999", "aaaaaaaaaaaaaa@mail.com", "AAAAAAAAAA", "AAAAA" },
//            new object[] { "AAAAA", "AAAAA", "AAAAAA", "+3599999", "x@x.x", "AAAAAAAAAA", "AAAAA" },
//            new object[] { "AAAAAAAAAAAAAAAAAAAA", "AAAAAAAAAAAAAAAAAAAAA", "AAAAAAAAAAAAAAAAAAAAA", "+3599999", "aaaaaaaaaaaaaaa@mail.com", "AAA", "AAAAA" },
//            new object[] { "AAAA", "AAAAAAAAAAAAAAAAAAAAA", "AAAAAA", "+3599999", "aaaaaaaaaaaaaa@mail.com", "AAAAAAAAAA", "AAAAAAAAAAAAAAAAAAAAAAAAA" },
//            new object[] { "AAAA", "AAAAAAAAAAAAAAAAAAAAA", "AAAA", "12345", "x@x.x", "AAA", "AAAAA" },
//            new object[] { "AAAAAAAAAAAAAAAAAAAA", "AAAAAAAAAAAAAAAAAAAA", "AAAAAA", "12345", "aaaaaaaaaaaaaa@mail.com", "AAAAAAAAAA", "AAAAA" },
//            new object[] { "AAAAA", "AAAAAAAAAAAAAAAAAAAAA", "AAAAA", "+35999999999999", "x@x.x", "AAAAAAAAAA", "AAAAAAAAAAAAAAAAAAAAAAAAA" },
//            new object[] { "AAAAAA", "AAAA", "AAAA", "12345", "invalid-email@", "AAAAAAAAAA", "AAAAAAAAAAAAAAAAAAAAAAAAA" },
//            new object[] { "AAAAAAAAAAAAAAAAAAAA", "AAAAAAAAAAAAAAAAAAAAA", "AAAAA", "12345", "aaaaaaaaaaaaaa@mail.com", "AAAAAAAAAA", "AAAAAAAAAAAAAAAAAAAAAAAAA" },
//        };
//    }
//}

//// TODO: write unit tests to test that all boundary values are tested at least once.
////🔹 ** Generated NUnit TestCaseSource Method:**

