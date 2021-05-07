using System;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Bellatrix.Web.Tests
{
    // uncomment to use MSTest
    ////[TestClass]
    [TestFixture]
    ////[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    public class LoginTests : NUnit.WebTest
    {
        public override void TestInit() => App.Navigation.Navigate("http://demos.bellatrix.solutions/my-account/");

        [TestCase(BrowserType.Chrome, 86)]
        [TestCase(BrowserType.Chrome, 87)]
        [TestCase(BrowserType.Firefox, 82)]
        [TestCase(BrowserType.Firefox, 83)]
        ////[Test]
        public void SuccessfullyLoginToMyAccount(BrowserType browserType, int browserVersion)
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

        ////[TestMethod]
        [TestCase(BrowserType.Chrome)]
        [TestCase(BrowserType.Edge)]
        [TestCase(BrowserType.Firefox)]
        public void SuccessfullyLoginToMyAccount1(BrowserType browserType)
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

        [Test]
        public void TestUrlDeterminer()
        {
            string cartUrl = UrlDeterminer.GetUrl<UrlSettings>(u => u.ShopUrl, "cart");

            App.Assert.AreEqual("http://demos.bellatrix.solutions/cart", cartUrl);
        }
    }
}