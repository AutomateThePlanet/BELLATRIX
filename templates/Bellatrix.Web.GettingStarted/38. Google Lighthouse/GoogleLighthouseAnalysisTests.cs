using System;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted
{
    [TestFixture]
    [LighthouseAnalysisRun]
    public class GoogleLighthouseAnalysisTests : NUnit.WebTest
    {
        public override void TestInit()
        {
            App.Navigation.Navigate("http://demos.bellatrix.solutions/my-account/");
        }

        [Test]
        public void LighthouseAnalysisLogin()
        {
            TextField userNameField = App.Components.CreateById<TextField>("username");
            Password passwordField = App.Components.CreateById<Password>("password");
            Button loginButton = App.Components.CreateByXpath<Button>("//button[@name='login']");

            userNameField.SetText("info@berlinspaceflowers.com");
            passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$");
            loginButton.Click();

            Div myAccountContentDiv = App.Components.CreateByClass<Div>("woocommerce-MyAccount-content");
            myAccountContentDiv.ValidateInnerTextContains("Hello info1");

            App.Lighthouse.PerformLighthouseAnalysis();

            App.Lighthouse.AssertFirstMeaningfulPaintScoreMoreThan(0.5);

            // specify what to be printed as title
            App.Lighthouse.AssertMetric(r => r.Categories.Pwa.Score).LessThan(2.3).Perform();
            App.Lighthouse.AssertMetric(r => r.Categories.Performance.Score).GreaterThan(0.5).Perform();
            App.Lighthouse.AssertMetric(r => r.Categories.Performance.Score).GreaterThanOrEqual(0.5).Perform();

            Anchor logoutLink = App.Components.CreateByInnerTextContaining<Anchor>("Log out");

            logoutLink.ValidateIsVisible();
            logoutLink.Click();
        }

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