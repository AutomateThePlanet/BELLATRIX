using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests;

[TestClass]
////[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
public class LoginTestsMSTest : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.Navigate("https://demos.bellatrix.solutions/my-account/");

    [TestMethod]
    public void SuccessfullyLoginToMyAccount()
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

    [TestMethod]
    public void SuccessfullyLoginToMyAccount1()
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