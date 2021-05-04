using Bellatrix.Web.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    public class JavaScriptServiceTests : MSTest.WebTest
    {
        // 1. BELLATRIX gives you an interface for easier execution of JavaScript code using the JavaScriptService.
        // You need to make sure that you have navigated to the desired web page.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void FillUpAllFields()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/my-account/");

            // 2. Execute a JavaScript code on the page. Here we find an element with id = 'firstName' and sets its value to 'Bellatrix'.
            App.JavaScriptService.Execute("document.getElementById('username').value = 'Bellatrix';");

            App.ElementCreateService.CreateById<Password>("password").SetPassword("Gorgeous");
            var button = App.ElementCreateService.CreateByClassContaining<Button>("woocommerce-Button button");

            // 3. It is possible to pass an element, and the script executes on it.
            App.JavaScriptService.Execute("arguments[0].click();", button);
        }

        [TestMethod]
        [Ignore]
        public void GetElementStyle()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            var resultsCount = App.ElementCreateService.CreateByClassContaining<Component>("woocommerce-result-count");

            // 4. Get the results from a script. After that, get the value for a specific style and assert it.
            string fontSize = App.JavaScriptService.Execute("return arguments[0].style.font-size", resultsCount.WrappedElement);

            Assert.AreEqual("14px", fontSize);
        }
    }
}