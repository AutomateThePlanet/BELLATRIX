using NUnit.Framework;
using OpenQA.Selenium;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class InteractionsServiceTests : NUnit.WebTest
{
    // 1. BELLATRIX gives you an interface for easier execution of complex UI interactions such as drag & drop, move to element, double click, etc.
    // BELLATRIX interaction APIs are simplified and made to be user-friendly as possible.
    // Their usage can eliminate lots of code duplication and boilerplate code.
    [Test]
    [Category(Categories.CI)]
    [Ignore("Currently not working in the latest version of WebDriver")]
    public void DragAndDrop()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        Anchor protonRocketAnchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/proton-rocket/");
        Anchor protonMAnchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/proton-m/");

        // 2. You can access the interaction methods through the App class.
        // 3. You can chain more than one method.
        // 4. At the end of the method chain you need to call the Perform method.
        App.Interactions.MoveToElement(protonRocketAnchor).DragAndDrop(protonRocketAnchor, protonMAnchor).Perform();
    }

    [Test]
    [Category(Categories.CI)]
    public void KeyUp()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        Anchor protonRocketAnchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/proton-rocket/");
        Anchor protonMAnchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/proton-m/");

        App.Interactions.MoveToElement(protonRocketAnchor).KeyUp(Keys.LeftShift).ContextClick().Perform();
        App.Interactions.DoubleClick(protonRocketAnchor).KeyUp(Keys.LeftShift).ContextClick().Perform();
    }
}