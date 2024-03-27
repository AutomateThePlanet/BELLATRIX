using System;
using Bellatrix.Playwright.Enums;

////using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Bellatrix.Playwright.Tests;

[TestFixture]
public class ExecuteSameTestDifferentBrowsersTests : NUnit.WebTest
{
    public override void TestInit()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/my-account/");
    }

    // The browser versioning will work only if you execute the tests with Selenium grid and only with Chrome - locally or in the cloud.
    // This feature works only for NUnit.
    [TestCase(BrowserChoice.Chrome, 86)]
    [TestCase(BrowserChoice.Chrome, 87)]
    [TestCase(BrowserChoice.Firefox, 82)]
    [TestCase(BrowserChoice.Firefox, 83)]
    [Ignore("no need to run")]
    public void SuccessfullyLoginToMyAccount(BrowserChoice BrowserChoice, int browserVersion)
    {
        TextField userNameField = App.Components.CreateById<TextField>("username");
        Password passwordField = App.Components.CreateById<Password>("password");
        Button loginButton = App.Components.CreateByXpath<Button>("//button[@name='login']");

        userNameField.SetText("info@berlinspaceflowers.com");
        passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$");
        loginButton.Click();

        Assert.Fail("Check Video");
        Div myAccountContentDiv = App.Components.CreateByClass<Div>("woocommerce-MyAccount-content");
        myAccountContentDiv.ValidateInnerTextContains("Hello info1");

        Anchor logoutLink = App.Components.CreateByInnerTextContaining<Anchor>("Log out");

        logoutLink.ValidateIsVisible();
        logoutLink.Click();
    }

    // You can use the feature for local execution using your browser versions.
    [TestCase(BrowserChoice.Chrome)]
    [TestCase(BrowserChoice.Edge)]
    [TestCase(BrowserChoice.Firefox)]
    public void SuccessfullyLoginToMyAccount1(BrowserChoice BrowserChoice)
    {
        TextField userNameField = App.Components.CreateById<TextField>("username");
        Password passwordField = App.Components.CreateById<Password>("password");
        Button loginButton = App.Components.CreateByXpath<Button>("//button[@name='login']");

        userNameField.SetText("info@berlinspaceflowers.com");
        passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$");
        loginButton.Click();

        Div myAccountContentDiv = App.Components.CreateByClass<Div>("woocommerce-MyAccount-content");
        myAccountContentDiv.ValidateInnerTextContains("Hello Berlin Spaceflowers");

        Anchor logoutLink = App.Components.CreateByInnerTextContaining<Anchor>("Log out");

        logoutLink.ValidateIsVisible();
        logoutLink.Click();
    }
}