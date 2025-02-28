//using Bellatrix.Web;
//using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
//using Bellatrix.Web.Tests.MetaheuristicVersion2.TestValueProviders;
//using NUnit.Framework;
//using System.Collections.Generic;

//namespace Bellatrix.DataGeneration.Tests.Tests;

//[TestFixture]
//[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
//public class TestPageABCAlgorithmTests : NUnit.WebTest
//{
//    public override void Configure()
//    {
//        ServicesCollection.Current.RegisterType<IComponentTestValuesProviderStrategy<Email>, EmailTestValueProviderStrategy>();
//        ServicesCollection.Current.RegisterType<IComponentTestValuesProviderStrategy<Phone>, PhoneTestValueProviderStrategy>();
//        ServicesCollection.Current.RegisterType<IComponentTestValuesProviderStrategy<TextField>, TextFieldTestValueProviderStrategy>();
//    }

//    [Test]
//    public void SubmitFormWithPageObjects_ABC_Bees()
//    {
//        var testPage = App.GoTo<TestPage>();

//        GenerateAndPrintTestCases(testPage);
//    }

//    private void GenerateAndPrintTestCases(TestPage testPage)
//    {
//        var parameters = new List<IInputParameter>
//        {
//            new ComponentInputParameter<TextField>(testPage.FirstName),
//            new ComponentInputParameter<TextField>(testPage.LastName),
//            new ComponentInputParameter<TextField>(testPage.ZipCode),
//            new ComponentInputParameter<Phone>(testPage.Phone),
//            new ComponentInputParameter<Email>(testPage.Email),
//            new ComponentInputParameter<TextField>(testPage.Company, isManualMode: true), // No WebDriver calls for unit tests
//            new ComponentInputParameter<TextField>(testPage.Address, isManualMode: true)  // No WebDriver calls for unit tests
//        };

//        var generator = new HybridArtificialBeeColonyTestCaseGenerator(allowMultipleInvalidInputs: false);
//        //generator.GenerateTestCases("GetTestCases", parameters);
//    }
//}
