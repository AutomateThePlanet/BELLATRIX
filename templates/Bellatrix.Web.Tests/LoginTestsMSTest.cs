using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests
{
    [TestClass]
    ////[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    public class LoginTestsMSTest : MSTest.WebTest
    {
        public override void TestInit() => App.Navigation.Navigate("http://demos.bellatrix.solutions/my-account/");

        [DataRow(BrowserType.Chrome, 86)]
        [DataRow(BrowserType.Chrome, 87)]
        [DataRow(BrowserType.Firefox, 82)]
        [DataRow(BrowserType.Firefox, 83)]
        [DataTestMethod]
        public void SuccessfullyLoginToMyAccount(BrowserType browserType, int browserVersion)
        {
            TextField userNameField = App.Components.CreateById<TextField>("username");
            Password passwordField = App.Components.CreateById<Password>("password");
            Button loginButton = App.Components.CreateByXpath<Button>("//button[@name='login']");

            userNameField.SetText("info@berlinspaceflowers.com");
            passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$");
            loginButton.Click();

            Div myAccountContentDiv = App.Components.CreateByClass<Div>("woocommerce-MyAccount-content");
            myAccountContentDiv.ValidateInnerTextContains("Hello info1");

            Anchor logoutLink = App.Components.CreateByInnerTextContaining<Anchor>("Log out");

            logoutLink.ValidateIsVisible();
            logoutLink.Click();
        }

        [DataRow(BrowserType.Chrome, 86)]
        [DataRow(BrowserType.Chrome, 87)]
        [DataRow(BrowserType.Firefox, 82)]
        [DataRow(BrowserType.Firefox, 83)]
        [DataTestMethod]
        public void SuccessfullyLoginToMyAccount1(BrowserType browserType, int browserVersion)
        {
            TextField userNameField = App.Components.CreateById<TextField>("username");
            Password passwordField = App.Components.CreateById<Password>("password");
            Button loginButton = App.Components.CreateByXpath<Button>("//button[@name='login']");

            userNameField.SetText("info@berlinspaceflowers.com");
            passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$");
            loginButton.Click();

            Div myAccountContentDiv = App.Components.CreateByClass<Div>("woocommerce-MyAccount-content");
            myAccountContentDiv.ValidateInnerTextContains("Hello info1");

            Anchor logoutLink = App.Components.CreateByInnerTextContaining<Anchor>("Log out");

            logoutLink.ValidateIsVisible();
            logoutLink.Click();
        }
    }
}