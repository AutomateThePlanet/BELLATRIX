using Bellatrix.Layout;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

// 1. Layout testing is a module from BELLATRIX that allows you to test the responsiveness of your website.
// You need to add a using statement to Bellatrix.Layout
//
// using Bellatrix.Layout;
//
// After that 100 assertion extensions methods are available to you to check the exact position of your web elements.
// Browser attribute gives you the option to resize your browser window so that you can test the rearrangement of the web elements on your pages.
// To make it, even more, easier for you, we included a couple of enums containing the most popular desktop, mobile and tablet resolutions.
// Of course, you always have the option to set a custom size.
// [Browser(BrowserType.FirefoxHeadless, MobileWindowSize._360_640,  Lifecycle.RestartEveryTime)]
// [Browser(BrowserType.FirefoxHeadless, TabletWindowSize._600_1024,  Lifecycle.RestartEveryTime)]
// [Browser(BrowserType.FirefoxHeadless, width: 600, height: 900, behavior: Lifecycle.RestartEveryTime)]
[Browser(BrowserType.Chrome, DesktopWindowSize._1280_1024,  Lifecycle.RestartEveryTime)]
[Browser(OS.OSX, BrowserType.Chrome, DesktopWindowSize._1280_1024, Lifecycle.RestartEveryTime)]
[TestFixture]
public class LayoutTestingTests : NUnit.WebTest
{
    [Test]
    [Category(Categories.CI)]
    public void TestPageLayout()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        Select sortDropDown = App.Components.CreateByNameEndingWith<Select>("orderby");
        Anchor protonRocketAnchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/proton-rocket/");
        Anchor protonMAnchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/proton-m/");
        Anchor saturnVAnchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/saturn-v/");
        Anchor falconHeavyAnchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/falcon-heavy/");
        Anchor falcon9Anchor = App.Components.CreateByAttributesContaining<Anchor>("href", "/falcon-9/");
        Div saturnVRating = saturnVAnchor.CreateByClassContaining<Div>("star-rating");

        // 2. Depending on what you want to check, BELLATRIX gives lots of options. You can test px perfect or just that some element is below another.
        // Check that the popularity sort dropdown is above the proton rocket image.
        sortDropDown.AssertAboveOf(protonRocketAnchor);

        // 3. Assert with the exact distance between them.
        sortDropDown.AssertAboveOf(protonRocketAnchor, 41);

        // All layout assertion methods throw LayoutAssertFailedException if the check is not successful with beatified troubleshooting message:
        // ########################################
        //
        //             control (Name ending with orderby) should be 41 px above of control (href = /proton-rocket/) but was 42 px.
        //
        // ########################################

        // 4. For each available method you have variations of it such as, >, >=, <, <=, between and approximate to some expected value by specified %.
        sortDropDown.AssertAboveOfGreaterThan(protonRocketAnchor, 40);
        sortDropDown.AssertAboveOfGreaterThanOrEqual(protonRocketAnchor, 41);
        sortDropDown.AssertAboveOfLessThan(protonRocketAnchor, 50);
        sortDropDown.AssertAboveOfLessThanOrEqual(protonRocketAnchor, 43);

        // 5. All assertions have alternative names containing the word 'Near'. We added them to make your tests more readable depending on your preference.
        sortDropDown.AssertNearTopOfGreaterThan(protonRocketAnchor, 40);
        sortDropDown.AssertNearTopOfGreaterThanOrEqual(protonRocketAnchor, 41);
        sortDropDown.AssertNearTopOfLessThan(protonRocketAnchor, 50);
        sortDropDown.AssertNearTopOfLessThanOrEqual(protonRocketAnchor, 43);

        // The expected distance is ~40px with 10% tolerance
        sortDropDown.AssertAboveOfApproximate(protonRocketAnchor, 40, percent: 10);

        // The expected px distance is between 30 and 50 px
        sortDropDown.AssertAboveOfBetween(protonRocketAnchor, 30, 50);

        // 6. You can assert the position of elements again each other in all directions- above, below, right, left, top right, top left, below left, below right
        // Assert that the sort dropdown is positioned near the top right of the Saturn B link.
        saturnVAnchor.AssertNearBottomRightOf(sortDropDown);
        sortDropDown.AssertNearTopLeftOf(saturnVAnchor);

        // 7. You can tests whether different web elements are aligned correctly.
        ////LayoutAssert.AssertAlignedHorizontallyAll(protonRocketAnchor, protonMAnchor);

        // 8. You can pass as many elements as you like.
        LayoutAssert.AssertAlignedHorizontallyTop(protonRocketAnchor, protonMAnchor, saturnVAnchor);
        ////LayoutAssert.AssertAlignedHorizontallyCentered(protonRocketAnchor, protonMAnchor, saturnVAnchor);
        ////LayoutAssert.AssertAlignedHorizontallyBottom(protonRocketAnchor, protonMAnchor, saturnVAnchor);

        // 9. You can check vertical alignment as well.
        LayoutAssert.AssertAlignedVerticallyAll(falcon9Anchor, falconHeavyAnchor);

        // Assert that the elements are aligned vertically only from the left side.
        LayoutAssert.AssertAlignedVerticallyLeft(falcon9Anchor, falconHeavyAnchor);
        LayoutAssert.AssertAlignedVerticallyCentered(falcon9Anchor, falconHeavyAnchor);
        LayoutAssert.AssertAlignedVerticallyRight(falcon9Anchor, falconHeavyAnchor);

        // 10. You can check that some element is inside in another.
        // Assert that the rating div is present in the Saturn V anchor.
        //saturnVRating.AssertInsideOf(saturnVAnchor);

        // 11. Verify the height and width of elements.
        //saturnVRating.AssertHeightLessThan(100);
        //saturnVRating.AssertWidthBetween(50, 70);

        // 12. You can use for all layout assertions the special web elements- Viewport and Screen.
        // Screen - represents the whole page area inside browser even that which is not visible.
        // Viewport - it takes the browsers client window.
        // It is useful if you want to check some fixed element on the screen which sticks to viewport even when you scroll.
        ////saturnVRating.ScrollToVisible();
        ////saturnVRating.AssertInsideOf(SpecialElements.Viewport);
        ////saturnVRating.AssertInsideOf(SpecialElements.Screen);

        // 13. All layout assertion methods have full BDD logging support. Below you can find the generated BDD log.
        // Of course if you use BELLATRIX page objects the log looks even better as mentioned in previous chapters.
        //  Start Test
        //  Class = LayoutTestingTests Name = TestPageLayout
        //  Assert control (Name ending with orderby) is above of control (href = /proton-rocket/).
        //  Assert control (Name ending with orderby) is 42 px above of control (href = /proton-rocket/).
        //  Assert control (Name ending with orderby) is >40 px above of control (href = /proton-rocket/).
        //  Assert control (Name ending with orderby) is >=41 px above of control (href = /proton-rocket/).
        //  Assert control (Name ending with orderby) is <50 px above of control (href = /proton-rocket/).
        //  Assert control (Name ending with orderby) is <=43 px above of control (href = /proton-rocket/).
        //  Assert control (Name ending with orderby) is >40 px near top of control (href = /proton-rocket/).
        //  Assert control (Name ending with orderby) is >=41 px near top of control (href = /proton-rocket/).
        //  Assert control (Name ending with orderby) is <50 px near top of control (href = /proton-rocket/).
        //  Assert control (Name ending with orderby) is <=43 px near top of control (href = /proton-rocket/).
        //  Assert control (Name ending with orderby) is 40 px above of control (href = /proton-rocket/). (10% tolerance)
        //  Assert control (Name ending with orderby) is 30-50 px above of control (href = /proton-rocket/).
        //  Assert control (href = /saturn-v/) is near bottom of control (Name ending with orderby).
        //  Assert control (Name ending with orderby) is near right of control (href = /saturn-v/).
        //  Assert control (Name ending with orderby) is near top of control (href = /saturn-v/).
        //  Assert control (href = /saturn-v/) is near left of control (Name ending with orderby).
        //  Assert control (Class = star-rating) is left inside of control (href = /saturn-v/).
        //  Assert control (Class = star-rating) is right inside of control (href = /saturn-v/).
        //  Assert control (Class = star-rating) is top inside of control (href = /saturn-v/).
        //  Assert control (Class = star-rating) is bottom inside of control (href = /saturn-v/).
        //  Assert control (Class = star-rating) height is <100 px.
        //  Assert control (Class = star-rating) width is 50-70 px.
    }
}