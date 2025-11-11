using Bellatrix.Web.Assertions;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

// 1. Style testing is a module from BELLATRIX that allows you to test the CSS styles of your website such as
// background, border and other colors, font size, size, weight and many others.
[Browser(BrowserType.Chrome, DesktopWindowSize._1280_1024,  Lifecycle.RestartEveryTime)]
[Browser(OS.OSX, BrowserType.Chrome, DesktopWindowSize._1280_1024, Lifecycle.RestartEveryTime)]
[TestFixture]
public class StyleTestingTests : NUnit.WebTest
{
    [Test]
    [Category(Categories.CI)]
    public void TestStyles()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        Select sortDropDown = App.Components.CreateByNameEndingWith<Select>("orderby");
        Anchor protonRocketAnchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/proton-rocket/");
        Anchor saturnVAnchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/saturn-v/");

        sortDropDown.AssertFontSize("14px");
        sortDropDown.AssertFontWeight("400");
        sortDropDown.AssertFontFamily("\"Source Sans Pro\", HelveticaNeue-Light, \"Helvetica Neue Light\", \"Helvetica Neue\", Helvetica, Arial, \"Lucida Grande\", sans-serif");

        protonRocketAnchor.AssertColor("rgba(150, 88, 138, 1)");
        protonRocketAnchor.AssertBackgroundColor("rgba(0, 0, 0, 0)");
        protonRocketAnchor.AssertBorderColor("rgb(150, 88, 138)");

        protonRocketAnchor.AssertTextAlign("center");
        protonRocketAnchor.AssertVerticalAlign("baseline");
    }
}