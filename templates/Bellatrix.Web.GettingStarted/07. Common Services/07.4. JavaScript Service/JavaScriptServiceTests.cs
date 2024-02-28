using Bellatrix.Web.MSTest;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class JavaScriptServiceTests : NUnit.WebTest
{
    // 1. BELLATRIX gives you an interface for easier execution of JavaScript code using the JavaScriptService.
    // You need to make sure that you have navigated to the desired web page.
    [Test]
    [Category(Categories.CI)]
    public void FillUpAllFields()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/my-account/");

        // 2. Execute a JavaScript code on the page. Here we find an element with id = 'firstName' and sets its value to 'Bellatrix'.
        App.JavaScript.Execute("document.geTComponentById('username').value = 'Bellatrix';");

        App.Components.CreateById<Password>("password").SetPassword("Gorgeous");
        var button = App.Components.CreateByClassContaining<Button>("woocommerce-Button button");

        // 3. It is possible to pass an element, and the script executes on it.
        App.JavaScript.Execute("arguments[0].click();", button);
    }

    [Test]
    [Ignore("no need to run")]
    public void GeTComponentStyle()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        var resultsCount = App.Components.CreateByClassContaining<Component>("woocommerce-result-count");

        // 4. Get the results from a script. After that, get the value for a specific style and assert it.
        string fontSize = App.JavaScript.Execute("return arguments[0].style.font-size", resultsCount.WrappedElement);

        Assert.That("14px".Equals(fontSize));
    }
}