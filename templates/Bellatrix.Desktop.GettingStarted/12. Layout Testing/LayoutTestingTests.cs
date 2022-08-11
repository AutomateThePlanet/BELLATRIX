using Bellatrix.Desktop.NUnit;
using Bellatrix.Layout;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]

// 1. Layout testing is a module from BELLATRIX that allows you to test the responsiveness of your app.
// You need to add a using statement to Bellatrix.Layout
//
// using Bellatrix.Layout;
//
// After that 100 assertion extensions methods are available to you to check the exact position of your desktop elements.
// App attribute gives you the option to resize your browser window so that you can test the rearrangement of the elements on your screens.
// To make it, even more, easier for you, we included a couple of enums containing the most popular desktop, mobile and tablet resolutions.
// Of course, you always have the option to set a custom size.
// [App(Constants.WpfAppPath, MobileWindowSize._360_640,  Lifecycle.RestartEveryTime)]
// [App(Constants.WpfAppPath, TabletWindowSize._600_1024,  Lifecycle.RestartEveryTime)]
// [App(Constants.WpfAppPath, width: 600, height: 900, behavior: Lifecycle.RestartEveryTime)]
public class LayoutTestingTests : DesktopTest
{
    [Test]
    public void CommonActionsWithDesktopControls_Wpf()
    {
        var button = App.Components.CreateByName<Button>("E Button");
        var calendar = App.Components.CreateByAutomationId<Calendar>("calendar");
        var radioButton = App.Components.CreateByName<RadioButton>("RadioButton");
        var selectedRadioButton = App.Components.CreateByName<RadioButton>("SelectedRadioButton");

        // 2. Depending on what you want to check, BELLATRIX gives lots of options. You can test px perfect or just that some element is below another.
        // Check that the button is above the calendar.
        button.AssertAboveOf(calendar);

        // 3. Assert with the exact distance between them.
        button.AssertAboveOf(calendar, 106);

        // All layout assertion methods throw LayoutAssertFailedException if the check is not successful with beatified troubleshooting message:
        // ########################################
        //
        //             control (Name = E Button) should be 41 px above of control (name = calendar) but was 42 px.
        //
        // ########################################

        // 4. For each available method you have variations of it such as, >, >=, <, <=, between and approximate to some expected value by specified %.
        button.AssertAboveOfGreaterThan(calendar, 100);
        button.AssertAboveOfGreaterThanOrEqual(calendar, 106);
        button.AssertAboveOfLessThan(calendar, 110);
        button.AssertAboveOfLessThanOrEqual(calendar, 106);

        // 5. All assertions have alternative names containing the word 'Near'. We added them to make your tests more readable depending on your preference.
        button.AssertNearTopOfGreaterThan(calendar, 100);
        button.AssertNearTopOfGreaterThanOrEqual(calendar, 106);
        button.AssertNearTopOfLessThan(calendar, 107);
        button.AssertNearTopOfLessThanOrEqual(calendar, 106);

        // The expected distance is ~40px with 10% tolerance
        button.AssertAboveOfApproximate(calendar, 105, percent: 10);

        // The expected px distance is between 100 and 115 px
        button.AssertAboveOfBetween(calendar, 100, 115);

        // 6. You can assert the position of elements again each other in all directions- above, below, right, left, top right, top left, below left, below right
        // saturnVAnchor.AssertNearBottomRightOf(sortDropDown);
        // sortDropDown.AssertNearTopLeftOf(saturnVAnchor);

        // 7. You can tests whether different elements are aligned correctly.
        // LayoutAssert.AssertAlignedHorizontallyAll(button, button1);
        // LayoutAssert.AssertAlignedHorizontallyCentered(protonRocketAnchor, protonMAnchor, saturnVAnchor);
        // LayoutAssert.AssertAlignedHorizontallyBottom(protonRocketAnchor, protonMAnchor, saturnVAnchor);

        // 9. You can check vertical alignment as well.
        LayoutAssert.AssertAlignedVerticallyLeft(radioButton, selectedRadioButton);

        // LayoutAssert.AssertAlignedVerticallyCentered(radioButton, selectedRadioButton);
        // LayoutAssert.AssertAlignedVerticallyRight(radioButton, selectedRadioButton);

        // 10. You can check that some element is inside in another.
        // Assert that the rating div is present in the Saturn V anchor.
        // firstNumber.AssertInsideOf(calendar);

        // 11. Verify the height and width of elements.
        button.AssertHeightLessThan(100);
        button.AssertWidthBetween(70, 80);

         // 12. All layout assertion methods have full BDD logging support. Below you can find the generated BDD log.
        // Of course if you use BELLATRIX page objects the log looks even better as mentioned in previous chapters.
        //  Start Test
        //  Class = LayoutTestingTests Name = TestPageLayout
        //  Assert control (Name = Transfer Button) is above of control (automationId = calendar).
        //  Assert control (Name = Transfer Button) is 42 px above of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is >40 px above of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is >=41 px above of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is <50 px above of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is <=43 px above of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is >40 px near top of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is >=41 px near top of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is <50 px near top of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is <=43 px near top of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is 40 px above of control (automationId = TransferCalendar). (10% tolerance)
        //  Assert control (Name = Transfer Button) is 30-50 px above of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is near bottom of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is near right of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is near top of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is near left of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is left inside of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is right inside of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is top inside of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) is bottom inside of control (automationId = TransferCalendar).
        //  Assert control (Name = Transfer Button) height is <100 px.
        //  Assert control (Name = Transfer Button) width is 50-70 px.
    }
}