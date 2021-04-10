using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    public class TextVerificationPDF : MSTest.WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MakeTextExtractionFromPDF()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/?add-to-cart=26");
         }
    }
}