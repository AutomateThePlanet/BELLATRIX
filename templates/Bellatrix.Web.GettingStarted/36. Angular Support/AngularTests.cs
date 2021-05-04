using Bellatrix.Web.Angular;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    public class AngularTests : MSTest.WebTest
    {
        [TestMethod]
        public void ShouldGreetUsingBinding()
        {
            App.NavigationService.Navigate("http://www.angularjs.org");

            // If the automatic wait for Angular is turned off, you can tell the framework explicitly to wait.
            App.BrowserService.WaitForAngular();

            // BELLATRIX can find elements through Angular locators, for example by the Angular ng-model attribute.
            var textField = App.ComponentCreateService.CreateByNgModel<TextField>("yourName");

            textField.SetText("Julie");

            App.BrowserService.WaitForAngular();

            // Find element by Angular ng-binding.
            var heading = App.ComponentCreateService.CreateByNgBinding<Heading>("yourName");

            heading.ValidateInnerTextIs("Hello Julie!");
        }

        [TestMethod]
        public void ShouldListTodos()
        {
            App.NavigationService.Navigate("http://www.angularjs.org");

            // Find element(s) by Angular ng-repeat.
            var labels = App.ComponentCreateService.CreateAllByNgRepeater<Label>("todo in todoList.todos");

            Assert.AreEqual("build an AngularJS app", labels[1].InnerText.Trim());
        }

        [TestMethod]
        public void Angular2Test()
        {
            App.NavigationService.Navigate("https://material.angular.io/");
            var button = App.ComponentCreateService.CreateByXpath<Button>("//a[@routerlink='/guide/getting-started']");
            button.Click();

            Assert.AreEqual("https://material.angular.io/", App.BrowserService.Url.ToString());
        }
    }
}