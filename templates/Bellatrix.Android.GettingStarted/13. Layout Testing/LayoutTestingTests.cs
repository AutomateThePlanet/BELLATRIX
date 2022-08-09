using Bellatrix.Layout;
using Bellatrix.Mobile.Controls.Android;
using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

// 1. Layout testing is a module from BELLATRIX that allows you to test the responsiveness of your app.
// You need to add a using statement to Bellatrix.Layout
//
// using Bellatrix.Layout;
//
// After that 100 assertion extensions methods are available to you to check the exact position of your Android elements.
[TestFixture]
public class LayoutTestingTests : NUnit.AndroidTest
{
    [Test]
    [Category(Categories.CI)]
    public void TestPageLayout()
    {
        var button = App.Components.CreateByIdContaining<Button>("button");
        var secondButton = App.Components.CreateByIdContaining<Button>("button_disabled");
        var checkBox = App.Components.CreateByIdContaining<CheckBox>("check1");
        var secondCheckBox = App.Components.CreateByIdContaining<CheckBox>("check2");
        var mainElement = App.Components.CreateById<AndroidComponent>("android:id/content");

        // 2. Depending on what you want to check, BELLATRIX gives lots of options. You can test px perfect or just that some element is below another.
        // Check that the button is above the checkbox.
        button.AssertAboveOf(checkBox);

        // 3. Assert with the exact distance between them.
        button.AssertAboveOf(checkBox, 105);

        // All layout assertion methods throw LayoutAssertFailedException if the check is not successful with beautified troubleshooting message:
        // ########################################
        //
        //             control (ID = button) should be 41 px above of control (ID = check1) but was 105 px.
        //
        // ########################################

        // 4. For each available method you have variations of it such as, >, >=, <, <=, between and approximate to some expected value by specified %.
        button.AssertAboveOfGreaterThan(checkBox, 100);
        button.AssertAboveOfGreaterThanOrEqual(checkBox, 105);
        button.AssertAboveOfLessThan(checkBox, 110);
        button.AssertAboveOfLessThanOrEqual(checkBox, 105);

        // 5. All assertions have alternative names containing the word 'Near'. We added them to make your tests more readable depending on your preference.
        button.AssertNearTopOfGreaterThan(checkBox, 100);
        button.AssertNearTopOfGreaterThanOrEqual(checkBox, 105);
        button.AssertNearTopOfLessThan(checkBox, 106);
        button.AssertNearTopOfLessThanOrEqual(checkBox, 105);

        // The expected distance is ~40px with 10% tolerance
        button.AssertAboveOfApproximate(checkBox, 104, percent: 10);

        // The expected px distance is between 30 and 50 px
        button.AssertAboveOfBetween(checkBox, 100, 120);

        // 6. You can assert the position of elements again each other in all directions- above, below, right, left, top right, top left, below left, below right
        // Assert that the checkbox is positioned near the top right of the button.
        ////checkBox.AssertNearBottomRightOf(button);
        ////button.AssertNearTopLeftOf(checkBox);

        // 7. You can tests whether different Android elements are aligned correctly.
        LayoutAssert.AssertAlignedHorizontallyAll(button, secondButton);

        // 8. You can pass as many elements as you like.
        LayoutAssert.AssertAlignedHorizontallyTop(button, secondButton);
        LayoutAssert.AssertAlignedHorizontallyCentered(button, secondButton, secondButton);
        LayoutAssert.AssertAlignedHorizontallyBottom(button, secondButton, secondButton);

        // 9. You can check vertical alignment as well.
        LayoutAssert.AssertAlignedVerticallyAll(secondCheckBox, checkBox);

        // Assert that the elements are aligned vertically only from the left side.
        LayoutAssert.AssertAlignedVerticallyLeft(secondCheckBox, checkBox);
        LayoutAssert.AssertAlignedVerticallyCentered(secondCheckBox, checkBox);
        LayoutAssert.AssertAlignedVerticallyRight(secondCheckBox, checkBox);

        // 10. You can check that some element is inside in another.
        // Assert that the button is present in the main view element.
        button.AssertInsideOf(mainElement);

        // 11. Verify the height and width of elements.
        button.AssertHeightLessThan(100);
        button.AssertWidthBetween(50, 80);

        // 13. All layout assertion methods have full BDD logging support. Below you can find the generated BDD log.
        // Of course if you use BELLATRIX page objects the log looks even better as mentioned in previous chapters.
        //  Start Test
        //  Class = LayoutTestingTests Name = TestPageLayout
        //  Assert control(ID = button) is above of control(ID = check1).
        //  Assert control(ID = button) is 105 px above of control(ID = check1).
        //  Assert control(ID = button) is > 100 px above of control(ID = check1).
        //  Assert control(ID = button) is >= 105 px above of control(ID = check1).
        //  Assert control(ID = button) is < 110 px above of control(ID = check1).
        //  Assert control(ID = button) is <= 105 px above of control(ID = check1).
        //  Assert control(ID = button) is > 100 px near top of control(ID = check1).
        //  Assert control(ID = button) is >= 105 px near top of control(ID = check1).
        //  Assert control(ID = button) is < 106 px near top of control(ID = check1).
        //  Assert control(ID = button) is <= 105 px near top of control(ID = check1).
        //  Assert control(ID = button) is 104 px above of control(ID = check1). (10 % tolerance)
        //  Assert control(ID = button) is 100 - 120 px above of control(ID = check1).
        //  Assert control(ID = button) is left inside of control(ID = android:id / content).
        //  Assert control(ID = button) is right inside of control(ID = android:id / content).
        //  Assert control(ID = button) is top inside of control(ID = android:id / content).
        //  Assert control(ID = button) is bottom inside of control(ID = android:id / content).
        //  Assert control(ID = button) height is < 100 px.
        //  Assert control(ID = button) width is 50 - 80 px.
    }
}