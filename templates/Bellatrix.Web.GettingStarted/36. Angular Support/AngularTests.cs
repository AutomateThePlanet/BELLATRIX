using Bellatrix.Web.Angular;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class AngularTests : NUnit.WebTest
{
    [Test]
    public void ShouldGreetUsingBinding()
    {
        App.Navigation.Navigate("http://www.angularjs.org");

        // If the automatic wait for Angular is turned off, you can tell the framework explicitly to wait.
        App.Browser.WaitForAngular();

        // BELLATRIX can find elements through Angular locators, for example by the Angular ng-model attribute.
        var textField = App.Components.CreateByNgModel<TextField>("yourName");

        textField.SetText("Julie");

        App.Browser.WaitForAngular();

        // Find element by Angular ng-binding.
        var heading = App.Components.CreateByNgBinding<Heading>("yourName");

        heading.ValidateInnerTextIs("Hello Julie!");
    }

    [Test]
    public void ShouldListTodos()
    {
        App.Navigation.Navigate("http://www.angularjs.org");

        // Find element(s) by Angular ng-repeat.
        var labels = App.Components.CreateAllByNgRepeater<Label>("todo in todoList.todos");

        Assert.That("build an AngularJS app".Equals(labels[1].InnerText.Trim()));
    }

    [Test]
    public void Angular2Test()
    {
        App.Navigation.Navigate("https://material.angular.io/");
        var button = App.Components.CreateByXpath<Button>("//a[@routerlink='/guide/getting-started']");
        button.Click();

        Assert.That("https://material.angular.io/".Equals(App.Browser.Url.ToString()));
    }
}