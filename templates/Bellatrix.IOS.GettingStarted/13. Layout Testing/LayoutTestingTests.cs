using Bellatrix.Layout;
using Bellatrix.Mobile.Controls.IOS;
using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

// 1. Layout testing is a module from BELLATRIX that allows you to test the responsiveness of your app.
// You need to add a using statement to Bellatrix.Layout
//
// using Bellatrix.Layout;
//
// After that 100 assertion extensions methods are available to you to check the exact position of your iOS elements.
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
[TestFixture]
public class LayoutTestingTests : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void TestPageLayout()
    {
        var numberOneTextField = App.Components.CreateById<TextField>("IntegerA");
        var computeButton = App.Components.CreateByName<Button>("ComputeSumButton");
        ////var answerLabel = App.Components.CreateByName<Label>("Answer");
        var mainElement = App.Components.CreateByIOSNsPredicate<IOSComponent>("type == \"XCUIElementTypeApplication\" AND name == \"TestApp\"");

        // 2. Depending on what you want to check, BELLATRIX gives lots of options. You can test px perfect or just that some element is below another.
        // Check that the text field is above the button.
        numberOneTextField.AssertAboveOf(computeButton);

        // 3. Assert with the exact distance between them.
        numberOneTextField.AssertAboveOf(computeButton, 61);

        // All layout assertion methods throw LayoutAssertFailedException if the check is not successful with beautified troubleshooting message:
        // ########################################
        //
        //             control (Id = IntegerA) should be 60 px above of control (Name = button) but was 61 px.
        //
        // ########################################

        // 4. For each available method you have variations of it such as, >, >=, <, <=, between and approximate to some expected value by specified %.
        numberOneTextField.AssertAboveOfGreaterThan(computeButton, 60);
        numberOneTextField.AssertAboveOfGreaterThanOrEqual(computeButton, 61);
        numberOneTextField.AssertAboveOfLessThan(computeButton, 70);
        numberOneTextField.AssertAboveOfLessThanOrEqual(computeButton, 61);

        // 5. All assertions have alternative names containing the word 'Near'. We added them to make your tests more readable depending on your preference.
        numberOneTextField.AssertNearTopOfGreaterThan(computeButton, 60);
        numberOneTextField.AssertNearTopOfGreaterThanOrEqual(computeButton, 61);
        numberOneTextField.AssertNearTopOfLessThan(computeButton, 70);
        numberOneTextField.AssertNearTopOfLessThanOrEqual(computeButton, 61);

        // The expected distance is ~60px with 10% tolerance
        numberOneTextField.AssertAboveOfApproximate(computeButton, 60, percent: 10);

        // The expected px distance is between 60 and 70 px
        numberOneTextField.AssertAboveOfBetween(computeButton, 60, 70);

        // 6. You can assert the position of elements again each other in all directions- above, below, right, left, top right, top left, below left, below right
        // Assert that the text field is positioned near the top right of the button.
        ////numberOneTextField.AssertNearBottomRightOf(computeButton);
        ////computeButton.AssertNearTopLeftOf(numberOneTextField);

        // 7. You can tests whether different iOS elements are aligned correctly.
        ////LayoutAssert.AssertAlignedHorizontallyAll(numberOneTextField, numberTwoTextField);

        // 8. You can pass as many elements as you like.
        ////LayoutAssert.AssertAlignedHorizontallyTop(numberOneTextField, numberTwoTextField);
        ////LayoutAssert.AssertAlignedHorizontallyCentered(numberOneTextField, numberTwoTextField, numberTwoTextField);
        ////LayoutAssert.AssertAlignedHorizontallyBottom(numberOneTextField, numberTwoTextField, numberTwoTextField);

        // 9. You can check vertical alignment as well.
        ////LayoutAssert.AssertAlignedVerticallyAll(answerLabel, computeButton);

        // Assert that the elements are aligned vertically only from the left side.
        ////LayoutAssert.AssertAlignedVerticallyLeft(answerLabel, computeButton);
        ////LayoutAssert.AssertAlignedVerticallyCentered(answerLabel, computeButton);
        ////LayoutAssert.AssertAlignedVerticallyRight(answerLabel, computeButton);

        // 10. You can check that some element is inside in another.
        // Assert that the button is present in the main view element.
        numberOneTextField.AssertInsideOf(mainElement);

        // 11. Verify the height and width of elements.
        numberOneTextField.AssertHeightLessThan(100);
        numberOneTextField.AssertWidthBetween(80, 120);

        // 13. All layout assertion methods have full BDD logging support. Below you can find the generated BDD log.
        // Of course if you use BELLATRIX page objects the log looks even better as mentioned in previous chapters.
        //  Start Test
        //  Class = LayoutTestingTests Name = TestPageLayout
        //  Assert control(Id = IntegerA) is above of control(Name = button).
        //  Assert control(Id = IntegerA) is 105 px above of control(Name = button).
        //  Assert control(Id = IntegerA) is > 100 px above of control(Name = button).
        //  Assert control(Id = IntegerA) is >= 105 px above of control(Name = button).
        //  Assert control(Id = IntegerA) is < 110 px above of control(Name = button).
        //  Assert control(Id = IntegerA) is <= 105 px above of control(Name = button).
        //  Assert control(Id = IntegerA) is > 100 px near top of control(Name = button).
        //  Assert control(Id = IntegerA) is >= 105 px near top of control(Name = button).
        //  Assert control(Id = IntegerA) is < 106 px near top of control(Name = button).
        //  Assert control(Id = IntegerA) is <= 105 px near top of control(Name = button).
        //  Assert control(Id = IntegerA) is 104 px above of control(Name = button). (10 % tolerance)
        //  Assert control(Id = IntegerA) is 100 - 120 px above of control(Name = button).
        //  Assert control(Id = IntegerA) is left inside of control(IName = button).
        //  Assert control(Id = IntegerA) is right inside of control(Name = button).
        //  Assert control(Id = IntegerA) is top inside of control(Name = button).
        //  Assert control(Id = IntegerA) is bottom inside of control(Name = button).
        //  Assert control(Id = IntegerA) height is < 100 px.
        //  Assert control(Id = IntegerA) width is 50 - 80 px.
    }
}