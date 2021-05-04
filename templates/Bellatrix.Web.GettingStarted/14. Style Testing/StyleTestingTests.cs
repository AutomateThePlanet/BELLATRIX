using Bellatrix.Web.Assertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    // 1. Style testing is a module from BELLATRIX that allows you to test the CSS styles of your website such as
    // background, border and other colors, font size, size, weight and many others.
    [Browser(BrowserType.Chrome, DesktopWindowSize._1280_1024,  Lifecycle.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Chrome, DesktopWindowSize._1280_1024, Lifecycle.RestartEveryTime)]
    [TestClass]
    public class StyleTestingTests : MSTest.WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void TestStyles()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            Select sortDropDown = App.ComponentCreateService.CreateByNameEndingWith<Select>("orderby");
            Anchor protonRocketAnchor = App.ComponentCreateService.CreateByAttributesContaining<Anchor>("href", "/proton-rocket/");
            Anchor saturnVAnchor = App.ComponentCreateService.CreateByAttributesContaining<Anchor>("href", "/saturn-v/");

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
}