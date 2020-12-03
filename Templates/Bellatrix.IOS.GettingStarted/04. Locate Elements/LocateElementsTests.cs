using System;
using Bellatrix.Mobile.Controls.IOS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        AppBehavior.ReuseIfStarted)]
    public class LocateElementsTests : IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateById_And_ElementIsOnScreen()
        {
            // 1. There are different ways to locate elements on the screen. To do it you use the element create service.
            // You need to know that BELLATRIX has a built-in complex mechanism for waiting for elements, so you do not need to worry about this anymore.
            // Keep in mind that when you use the Create methods, the element is not searched on the screen. All elements use lazy loading.
            // Which means that they are searched once you perform an action or assertion on them. By default on each new action, the element is searched again and be refreshed.
            var button = App.ElementCreateService.CreateById<Button>("ComputeSumButton");

            button.ValidateIsVisible();

            // 2. Because of the proxy element mechanism (we have a separate type of element instead of single WebDriver IWebElement interface or Appium IOSElement) we have several benefits.
            // Each control (element type- ComboBox, TextField and so on) contains only the actions you can do with it, and the methods are named properly.
            // In vanilla WebDriver to type the text you call SendKeys method.
            // Also, we have some additional properties in the proxy web control such as- By. Now you can get the locator with which you element was found.
            Console.WriteLine(button.By.Value);

            // 3. You can access the WebDriver wrapped element through WrappedElement and the current AppiumDriver instance through- WrappedDriver
            Console.WriteLine(button.WrappedElement.TagName);
        }

        [TestMethod]
        [Timeout(180000)]
        [IOS(Constants.AppleCalendarBundleId,
            Constants.IOSDefaultVersion,
            Constants.IOSDefaultDeviceName,
            AppBehavior.RestartEveryTime)]
        [Ignore]
        public void ElementFound_When_CreateById_And_ElementIsNotOnScreen()
        {
            var alertButton = App.ElementCreateService.CreateByName<Button>("Sunday, November 11");

            alertButton.Click();

            // 4. Sometimes, the elements you need to perform operations on are not in the visible part of the screen.
            // In order Appium to be able to locate them, you need to scroll to them first. To do so for iOS, you need to use
            // ScrollToVisible method
            var answerLabel = App.ElementCreateService.CreateById<Button>("Bellatrix, from 11:00 PM to Monday, November 12, 12:00 AM");
            answerLabel.ScrollToVisible(ScrollDirection.Up);

            answerLabel.Click();

            var testLabel = App.ElementCreateService.CreateById<Label>("Bellatrix");

            testLabel.ValidateTextIs("Bellatrix");
            testLabel.ValidateIsVisible();

            // 5. BELLATRIX extends the vanilla WebDriver (Appium) selectors and give you additional ones.
            // Available create methods:
            //
            // CreateById  --> App.ElementCreateService.CreateById<Button>("myId");
            // Searches the element by its ID.
            //
            // CreateByName  --> App.ElementCreateService.CreateByName<Button>("ComputeSumButton");
            // Searches the element by its name.
            //
            // CreateByValueContaining  --> App.ElementCreateService.CreateByValueContaining<Label>("SumLabel");
            // Searches the element by its value if it contains specified value.
            //
            // CreateByIOSUIAutomation  --> App.ElementCreateService.CreateByIOSUIAutomation<TextField>(".textFields().withPredicate("value == 'Search eBay'")");
            // Searches the element by iOS UIAutomation expressions.
            //
            // CreateByIOSNsPredicate  --> App.ElementCreateService.CreateByIOSNsPredicate<RadioButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");
            // Searches the element by iOS NsPredicate expression.
            //
            // CreateByClass  --> App.ElementCreateService.CreateByClass<TextField>("XCUIElementTypeTextField");
            // Searches the element by its class.
            //
            // CreateByXPath  --> App.ElementCreateService.CreateByXPath<Button>("//XCUIElementTypeButton[@name=\"ComputeSumButton\"]");
            // Searches the element by XPath locator.
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllById_And_ElementIsOnScreen()
        {
            // 5. Sometimes we need to find more than one element. For example, in this test we want to locate all Add to Cart buttons.
            // To do it you can use the element create service CreateAll method.
            var buttons = App.ElementCreateService.CreateAllById<Button>("ComputeSumButton");

            buttons[0].ValidateIsVisible();

            // Available create methods:
            //
            // CreateAllById  --> App.ElementCreateService.CreateAllById<Button>("myId");
            // Searches the elements by their ID.
            //
            // CreateAllByName  --> App.ElementCreateService.CreateAllByName<Button>("ComputeSumButton");
            // Searches the elements by their name.
            //
            // CreateByValueContaining  their App.ElementCreateService.CreateAllByValueContaining<Label>("SumLabel");
            // Searches the element by its value if it contains specified value.
            //
            // CreateAllByIOSUIAutomation  --> App.ElementCreateService.CreateAllByIOSUIAutomation<TextField>(".textFields().withPredicate("value == 'Search eBay'")");
            // Searches the elements by iOS UIAutomation expressions.
            //
            // CreateAllByIOSNsPredicate  --> App.ElementCreateService.CreateAllByIOSNsPredicate<RadioButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");
            // Searches the elements by iOS NsPredicate expression.
            //
            // CreateAllByClass  --> App.ElementCreateService.CreateAllByClass<TextField>("XCUIElementTypeTextField");
            // Searches the elements by their class.
            //
            // CreateAllByXPath  --> App.ElementCreateService.CreateAllByXPath<Button>("//XCUIElementTypeButton[@name=\"ComputeSumButton\"]");
            // Searches the elements by XPath locator.
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateById_And_ElementIsOnScreen_NestedElement()
        {
            // 6. Sometimes it is easier to locate one element and then find the next one that you need, inside it.
            // For example in this test we want to locate the button inside the main view element.
            // To do it you can use the element's Create methods.
            var mainElement = App.ElementCreateService.CreateByIOSNsPredicate<Element>("type == \"XCUIElementTypeApplication\" AND name == \"TestApp\"");

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
}