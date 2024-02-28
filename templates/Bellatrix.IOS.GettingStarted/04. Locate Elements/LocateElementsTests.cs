using System;
using Bellatrix.Mobile.Controls.IOS;
using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

// Please notice that we don't use the IOS attribute. A default device/selenium grid can be specified in the testFrameworkSettings.json file
// under the executionSettings section. There you can specify default lifecycle, version, grid URL, and arguments.
// You can still use the IOS attribute on top of classes or tests to override the default behavior.
[TestFixture]
public class LocateElementsTests : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ElementFound_When_CreateById_And_ElementIsOnScreen()
    {
        // 1. There are different ways to locate elements on the screen. To do it you use the element create service.
        // You need to know that BELLATRIX has a built-in complex mechanism for waiting for elements, so you do not need to worry about this anymore.
        // Keep in mind that when you use the Create methods, the element is not searched on the screen. All elements use lazy loading.
        // Which means that they are searched once you perform an action or assertion on them. By default on each new action, the element is searched again and be refreshed.
        var button = App.Components.CreateById<Button>("ComputeSumButton");

        button.ValidateIsVisible();

        // 2. Because of the proxy element mechanism (we have a separate type of element instead of single WebDriver IWebElement interface or Appium AppiumElement) we have several benefits.
        // Each control (element type- ComboBox, TextField and so on) contains only the actions you can do with it, and the methods are named properly.
        // In vanilla WebDriver to type the text you call SendKeys method.
        // Also, we have some additional properties in the proxy web control such as- By. Now you can get the locator with which you element was found.
        Console.WriteLine(button.By.Value);

        // 3. You can access the WebDriver wrapped element through WrappedElement and the current AppiumDriver instance through- WrappedDriver
        Console.WriteLine(button.WrappedElement.TagName);
    }

    [Test]
    [CancelAfter(180000)]
    [IOS(Constants.IOSNativeAppPath,
        Constants.AppleCalendarBundleId,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    [Ignore("API example purposes only. No need to run.")]
    public void ElementFound_When_CreateById_And_ElementIsNotOnScreen()
    {
        var alertButton = App.Components.CreateByName<Button>("Sunday, November 11");

        alertButton.Click();

        // 4. Sometimes, the elements you need to perform operations on are not in the visible part of the screen.
        // In order Appium to be able to locate them, you need to scroll to them first. To do so for iOS, you need to use
        // ScrollToVisible method
        var answerLabel = App.Components.CreateById<Button>("Bellatrix, from 11:00 PM to Monday, November 12, 12:00 AM");
        answerLabel.ScrollToVisible(ScrollDirection.Up);

        answerLabel.Click();

        var testLabel = App.Components.CreateById<Label>("Bellatrix");

        testLabel.ValidateTextIs("Bellatrix");
        testLabel.ValidateIsVisible();

        // 5. BELLATRIX extends the vanilla WebDriver (Appium) selectors and give you additional ones.
        // Available create methods:
        //
        // CreateById  --> App.Components.CreateById<Button>("myId");
        // Searches the element by its ID.
        //
        // CreateByName  --> App.Components.CreateByName<Button>("ComputeSumButton");
        // Searches the element by its name.
        //
        // CreateByValueContaining  --> App.Components.CreateByValueContaining<Label>("SumLabel");
        // Searches the element by its value if it contains specified value.
        //
        // CreateByIOSUIAutomation  --> App.Components.CreateByIOSUIAutomation<TextField>(".textFields().withPredicate("value == 'Search eBay'")");
        // Searches the element by iOS UIAutomation expressions.
        //
        // CreateByIOSNsPredicate  --> App.Components.CreateByIOSNsPredicate<RadioButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");
        // Searches the element by iOS NsPredicate expression.
        //
        // CreateByClass  --> App.Components.CreateByClass<TextField>("XCUIElementTypeTextField");
        // Searches the element by its class.
        //
        // CreateByXPath  --> App.Components.CreateByXPath<Button>("//XCUIElementTypeButton[@name=\"ComputeSumButton\"]");
        // Searches the element by XPath locator.
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ElementFound_When_CreateAllById_And_ElementIsOnScreen()
    {
        // 5. Sometimes we need to find more than one element. For example, in this test we want to locate all Add to Cart buttons.
        // To do it you can use the element create service CreateAll method.
        var buttons = App.Components.CreateAllById<Button>("ComputeSumButton");

        buttons[0].ValidateIsVisible();

        // Available create methods:
        //
        // CreateAllById  --> App.Components.CreateAllById<Button>("myId");
        // Searches the elements by their ID.
        //
        // CreateAllByName  --> App.Components.CreateAllByName<Button>("ComputeSumButton");
        // Searches the elements by their name.
        //
        // CreateByValueContaining  their App.Components.CreateAllByValueContaining<Label>("SumLabel");
        // Searches the element by its value if it contains specified value.
        //
        // CreateAllByIOSUIAutomation  --> App.Components.CreateAllByIOSUIAutomation<TextField>(".textFields().withPredicate("value == 'Search eBay'")");
        // Searches the elements by iOS UIAutomation expressions.
        //
        // CreateAllByIOSNsPredicate  --> App.Components.CreateAllByIOSNsPredicate<RadioButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");
        // Searches the elements by iOS NsPredicate expression.
        //
        // CreateAllByClass  --> App.Components.CreateAllByClass<TextField>("XCUIElementTypeTextField");
        // Searches the elements by their class.
        //
        // CreateAllByXPath  --> App.Components.CreateAllByXPath<Button>("//XCUIElementTypeButton[@name=\"ComputeSumButton\"]");
        // Searches the elements by XPath locator.
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ElementFound_When_CreateById_And_ElementIsOnScreen_NestedElement()
    {
        // 6. Sometimes it is easier to locate one element and then find the next one that you need, inside it.
        // For example in this test we want to locate the button inside the main view element.
        // To do it you can use the element's Create methods.
        var mainElement = App.Components.CreateByIOSNsPredicate<IOSComponent>("type == \"XCUIElementTypeApplication\" AND name == \"TestApp\"");

        // Note: it is entirely legal to create a Button instead of ToggleButton. BELLATRIX library does not care about the real type of the iOS elements.
        // The proxy types are convenience wrappers so to say. Meaning they give you a better interface of predefined properties and methods to make your tests more readable.
        var button = mainElement.CreateById<RadioButton>("ComputeSumButton");

        button.ValidateIsVisible();

        // Available create methods on element level:
        //
        // CreateById  --> element.CreateById<Button>("myId");
        // Searches the element by its ID.
        //
        // CreateByName  --> element.CreateByName<Button>("ComputeSumButton");
        // Searches the element by its name.
        //
        // CreateByValueContaining  --> element.CreateByValueContaining<Label>("SumLabel");
        // Searches the element by its value if it contains specified value.
        //
        // CreateByIOSUIAutomation  --> element.CreateByIOSUIAutomation<TextField>(".textFields().withPredicate("value == 'Search eBay'")");
        // Searches the element by iOS UIAutomation expressions.
        //
        // CreateByClass  --> element.CreateByClass<TextField>("XCUIElementTypeTextField");
        // Searches the element by its class.
        //
        // CreateByXPath  --> element.CreateByXPath<Button>("//XCUIElementTypeButton[@name=\"ComputeSumButton\"]");
        // Searches the element by XPath locator.
        //
        // CreateAllById  --> element.CreateAllById<Button>("myId");
        // Searches the elements by their ID.
        //
        // CreateAllByName  --> element.CreateAllByName<Button>("ComputeSumButton");
        // Searches the elements by their name.
        //
        // CreateByValueContaining  --> element.CreateAllByValueContaining<Label>("SumLabel");
        // Searches the element by their value if it contains specified value.
        //
        // CreateAllByIOSUIAutomation  --> element.CreateAllByIOSUIAutomation<TextField>(".textFields().withPredicate("value == 'Search eBay'")");
        // Searches the elements by iOS UIAutomation expressions.
        //
        // CreateAllByClass  --> element.CreateAllByClass<TextField>("XCUIElementTypeTextField");
        // Searches the elements by their class.
        //
        // CreateAllByXPath  --> element.CreateAllByXPath<Button>("//XCUIElementTypeButton[@name=\"ComputeSumButton\"]");
        // Searches the elements by XPath locator.
    }
}