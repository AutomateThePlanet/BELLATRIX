using System;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Bellatrix.Web.Tests
{
    // uncomment to use MSTest
    ////[TestClass]
    [TestFixture]
    [Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    public class LoginTests : NUnit.WebTest
    {
        public override void TestInit() => App.Navigation.Navigate("http://demos.bellatrix.solutions/my-account/");

        ////[TestMethod]
        [Test]
        public void SuccessfullyLoginToMyAccount()
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

        ////[TestMethod]
        [Test]
        public void SuccessfullyLoginToMyAccount1()
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