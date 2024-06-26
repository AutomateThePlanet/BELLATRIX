﻿namespace Bellatrix.Playwright.GettingStarted.Advanced.Elements.Extension.Methods;

public static class ButtonExtensions
{
    // 1. One way to extend an existing element is to create an extension method for the additional action.
    // 1.1. Place it in a static class like this one.
    // 1.2. Create a static method for the action.
    // 1.3. Pass the extended element as a parameter with the keyword 'this'.
    // 1.4. Access the native element through the WrappedElement property and the browser via WrappedBrowser.
    //
    // Later to use the method in your tests, add a using statement containing this class's namespace.
    public static void SubmitButtonWithEnter(this Button button)
    {
        var action = new InteractionsService(button.WrappedBrowser);
        action.MoveToElement(button).SendKeys("Enter").Perform();
    }
}