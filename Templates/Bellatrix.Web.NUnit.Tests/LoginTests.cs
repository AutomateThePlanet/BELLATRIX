using System;
using NUnit.Framework;

namespace Bellatrix.Web.NUnit.Tests
{
    [TestFixture]
    [Browser(BrowserType.Chrome, BrowserBehavior.ReuseIfStarted)]
    [VideoRecording(VideoRecordingMode.DoNotRecord)]
    [ScreenshotOnFail(true)]
    public class LoginTests : WebTest
    {
        public override void TestInit() => App.NavigationService.Navigate("http://demos.bellatrix.solutions/my-account/");

        [Test]
        ////[VideoRecording(VideoRecordingMode.Ignore)]
        ////[ScreenshotOnFail(false)]
        public void SuccessfullyLoginToMyAccount()
        {
            TextField userNameField = App.ElementCreateService.CreateById<TextField>("username");
            Password passwordField = App.ElementCreateService.CreateById<Password>("password");
            Button loginButton = App.ElementCreateService.CreateByXpath<Button>("//button[@name='login']");

            userNameField.SetText("info@berlinspaceflowers.com");
            passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$");
            loginButton.Click();

            Div myAccountContentDiv = App.ElementCreateService.CreateByClass<Div>("woocommerce-MyAccount-content");
            myAccountContentDiv.EnsureInnerTextContains("Hello info1");

            Anchor logoutLink = App.ElementCreateService.CreateByInnerTextContaining<Anchor>("Log out");

            logoutLink.EnsureIsVisible();
            logoutLink.Click();
        }

        [Test]
        public void SuccessfullyLoginToMyAccount1()
        {
            TextField userNameField = App.ElementCreateService.CreateById<TextField>("username");
            Password passwordField = App.ElementCreateService.CreateById<Password>("password");
            Button loginButton = App.ElementCreateService.CreateByXpath<Button>("//button[@name='login']");

            userNameField.SetText("info@berlinspaceflowers.com");
            passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$");
            loginButton.Click();

            Div myAccountContentDiv = App.ElementCreateService.CreateByClass<Div>("woocommerce-MyAccount-content");
            myAccountContentDiv.EnsureInnerTextContains("Hello info1");

            Anchor logoutLink = App.ElementCreateService.CreateByInnerTextContaining<Anchor>("Log out");

            logoutLink.EnsureIsVisible();
            logoutLink.Click();
        }
    }
}