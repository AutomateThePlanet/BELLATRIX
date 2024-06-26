﻿namespace Bellatrix.Playwright.GettingStarted.Advanced.Elements.ChildElements;

// 1. The second way of extending an existing element is to create a child ComponentCreateService.
// Inherit the element you want to extend. In this case, two methods are added to the standard Button ComponentCreateService.
// Next in your tests, use the ExtendedButton instead of regular Button to have access to these methods.
//
// 2. The same strategy can be used to create a completely new element that BELLATRIX does not provide.
// You need to extend the 'Element' as a base class.
public class ExtendedButton : Button
{
    public void SubmitButtonWithEnter()
    {
        var action = new InteractionsService(WrappedBrowser);
        action.MoveToElement(this).SendKeys("Enter").Perform();
    }

    public void JavaScriptFocus()
    {
        JavaScriptService.Execute("window.focus();");
        JavaScriptService.Execute("el => el.focus();", this);
    }
}