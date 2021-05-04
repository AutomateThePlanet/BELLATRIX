using System;
using Bellatrix.Mobile.Controls.Android;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.ControlsMaterialDark",
        Lifecycle.RestartEveryTime)]
    public class LocateElementsTests : MSTest.AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByIdContaining_And_ElementIsOnScreen()
        {
            // 1. There are different ways to locate elements on the screen. To do it you use the element create service.
            // You need to know that BELLATRIX has a built-in complex mechanism for waiting for elements, so you do not need to worry about this anymore.
            // Keep in mind that when you use the Create methods, the element is not searched on the screen. All elements use lazy loading.
            // Which means that they are searched once you perform an action or assertion on them. By default on each new action, the element is searched again and be refreshed.
            var button = App.ComponentCreateService.CreateByIdContaining<Button>("button");

            button.ValidateIsVisible();

            // 2. Because of the proxy element mechanism (we have a separate type of element instead of single WebDriver IWebElement interface or Appium AndroidElement) we have several benefits.
            // Each control (element type- ComboBox, TextField and so on) contains only the actions you can do with it, and the methods are named properly.
            // In vanilla WebDriver to type the text you call SendKeys method.
            // Also, we have some additional properties in the proxy web control such as- By. Now you can get the locator with which you element was found.
            Console.WriteLine(button.By.Value);

            // 3. You can access the WebDriver wrapped element through WrappedElement and the current AppiumDriver instance through- WrappedDriver
            Console.WriteLine(button.WrappedElement.TagName);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByIdContaining_And_ElementIsNotOnScreen()
        {
            // 4. Sometimes, the elements you need to perform operations on are not in the visible part of the screen.
            // In order Appium to be able to locate them, you need to scroll to them first. To do so for Android, you need to use
            // complex AndroidUIAutomator expressions. To save you lots of trouble and complex code, most of BELLATRIX locators
            // contains the scroll logic built-in. The below element is initially not visible on the screen. BELLATRIX automatically
            // scrolls down till the element is visible and then searches for it.
            var textField = App.ComponentCreateService.CreateByIdContaining<TextField>("edit");

            textField.ValidateIsVisible();

            // 5. BELLATRIX extends the vanilla WebDriver (Appium) selectors and give you additional ones.
            // Available create methods:
            //
            // CreateById  --> App.ComponentCreateService.CreateById<Button>("myId");
            // Searches the element by its ID.
            //
            // CreateByIdContaining  --> App.ComponentCreateService.CreateByIdContaining<Button>("myIdMiddle");
            // Searches the element by ID containing the specified value.
            //
            // CreateByDescription  --> App.ComponentCreateService.CreateByDescription<Button>("myDescription");
            // Searches the element by ID ending with the locator.
            //
            // CreateByDescriptionContaining  --> App.ComponentCreateService.CreateByDescriptionContaining<Button>("description");
            // Searches the element by its description if it contains specified value.
            //
            // CreateByText  --> App.ComponentCreateService.CreateByText<Button>("text");
            // Searches the element by its text.
            //
            // CreateByTextContaining  --> App.ComponentCreateService.CreateByTextContaining<Button>("partOfText");
            // Searches the element by its text if it contains specified value.
            //
            // CreateByClass  --> App.ComponentCreateService.CreateByClass<Button>("myClass");
            // Searches the element by its class.
            //
            // CreateByAndroidUIAutomator  --> App.ComponentCreateService.CreateByAndroidUIAutomator<Button>("ui-automator-expression");
            // Searches the element by Android UIAutomator expression.
            //
            // CreateByXPath  --> App.ComponentCreateService.CreateByXPath<Button>("//*[@title='Add to cart']");
            // Searches the element by XPath locator.
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByIdContaining_And_ElementIsOnScreen()
        {
            // 5. Sometimes we need to find more than one element. For example, in this test we want to locate all Add to Cart buttons.
            // To do it you can use the element create service CreateAll method.
            var buttons = App.ComponentCreateService.CreateAllByIdContaining<Button>("button");

            buttons[0].ValidateIsVisible();

            // Available create methods:
            //
            // CreateAllById  --> App.ComponentCreateService.CreateAllById<Button>("myId");
            // Searches the elements by its ID.
            //
            // CreateAllByIdContaining  --> App.ComponentCreateService.CreateAllByIdContaining<Button>("myIdMiddle");
            // Searches the elements by ID containing the specified value.
            //
            // CreateAllByDescription  --> App.ComponentCreateService.CreateAllByDescription<Button>("myDescription");
            // Searches the elements by ID ending with the locator.
            //
            // CreateAllByDescriptionContaining  --> App.ComponentCreateService.CreateAllByDescriptionContaining<Button>("description");
            // Searches the elements by its description if it contains specified value.
            //
            // CreateAllByText  --> App.ComponentCreateService.CreateAllByText<Button>("text");
            // Searches the elements by its text.
            //
            // CreateAllByTextContaining  --> App.ComponentCreateService.CreateAllByTextContaining<Button>("partOfText");
            // Searches the elements by its text if it contains specified value.
            //
            // CreateAllByClass  --> App.ComponentCreateService.CreateAllByClass<Button>("myClass");
            // Searches the elements by its class.
            //
            // CreateAllByAndroidUIAutomator  --> App.ComponentCreateService.CreateAllByAndroidUIAutomator<Button>("ui-automator-expression");
            // Searches the elements by Android UIAutomator expression.
            //
            // CreateAllByXPath  --> App.ComponentCreateService.CreateAllByXPath<Button>("//*[@title='Add to cart']");
            // Searches the elements by XPath locator.
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByIdContaining_And_ElementIsOnScreen_NestedElement()
        {
            // 6. Sometimes it is easier to locate one element and then find the next one that you need, inside it.
            // For example in this test we want to locate the button inside the main view element.
            // To do it you can use the element's Create methods.
            var mainElement = App.ComponentCreateService.CreateByIdContaining<AndroidComponent>("decor_content_parent");

            // Note: it is entirely legal to create a Button instead of ToggleButton. BELLATRIX library does not care about the real type of the Android elements.
            // The proxy types are convenience wrappers so to say. Meaning they give you a better interface of predefined properties and methods to make your tests more readable.
            var button = mainElement.CreateByIdContaining<Button>("button");

            button.ValidateIsVisible();

            // Available create methods on element level:
            //
            // CreateById  --> element.CreateById<Button>("myId");
            // Searches the element by its ID.
            //
            // CreateByIdContaining  --> element.CreateByIdContaining<Button>("myIdMiddle");
            // Searches the element by ID containing the specified value.
            //
            // CreateByDescription  --> element.CreateByDescription<Button>("myDescription");
            // Searches the element by ID ending with the locator.
            //
            // CreateByDescriptionContaining  --> element.CreateByDescriptionContaining<Button>("description");
            // Searches the element by its description if it contains specified value.
            //
            // CreateByText  --> element.CreateByText<Button>("text");
            // Searches the element by its text.
            //
            // CreateByTextContaining  --> element.CreateByTextContaining<Button>("partOfText");
            // Searches the element by its text if it contains specified value.
            //
            // CreateByClass  --> element.CreateByClass<Button>("myClass");
            // Searches the element by its class.
            //
            // CreateByAndroidUIAutomator  --> element.CreateByAndroidUIAutomator<Button>("ui-automator-expression");
            // Searches the element by Android UIAutomator expression.
            //
            // CreateByXPath  --> element.CreateByXPath<Button>("//*[@title='Add to cart']");
            // Searches the element by XPath locator.
            //
            // CreateAllById  --> element.CreateAllById<Button>("myId");
            // Searches the elements by its ID.
            //
            // CreateAllByIdContaining  --> element.CreateAllByIdContaining<Button>("myIdMiddle");
            // Searches the elements by ID containing the specified value.
            //
            // CreateAllByDescription  --> element.CreateAllByDescription<Button>("myDescription");
            // Searches the elements by ID ending with the locator.
            //
            // CreateAllByDescriptionContaining  --> element.CreateAllByDescriptionContaining<Button>("description");
            // Searches the elements by its description if it contains specified value.
            //
            // CreateAllByText  --> element.CreateAllByText<Button>("text");
            // Searches the elements by its text.
            //
            // CreateAllByTextContaining  --> element.CreateAllByTextContaining<Button>("partOfText");
            // Searches the elements by its text if it contains specified value.
            //
            // CreateAllByClass  --> element.CreateAllByClass<Button>("myClass");
            // Searches the elements by its class.
            //
            // CreateAllByAndroidUIAutomator  --> element.CreateAllByAndroidUIAutomator<Button>("ui-automator-expression");
            // Searches the elements by Android UIAutomator expression.
            //
            // CreateAllByXPath  --> element.CreateAllByXPath<Button>("//*[@title='Add to cart']");
            // Searches the elements by XPath locator.
        }
    }
}