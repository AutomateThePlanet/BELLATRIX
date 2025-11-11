using System;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

// Built by Google, Lighthouse is an open-source auditing tool for developers.
// It works over the chrome-dev-tools and runs a series of audits to measure the web application under test on the following:
// performance, accessibility, SEO, best practices, PWA (progressive web applications). Lighthouse works as a Chrome/Firefox add-ons or in a CLI mode, or as a node application.
// Requirements: Node.js + lighthouse CLI - https://github.com/GoogleChrome/lighthouse#using-lighthouse-in-chrome-devtools
// npm install -g lighthouse
// BELLATRIX uses under the hood the Lighthouse Node CLI. The Node CLI provides the most flexibility in how Lighthouse runs
// can be configured and reported. Users who want more advanced usage or want to run Lighthouse in an automated fashion should use the Node CLI.
//
// Our BELLATRIX integration allows you to analyze your pages automatically after you have logged in or performed actions on them.
// Additionally exposes many assertion methods for checking the scores and values of various Lighthouse audits.
// On top of this, HTML, CSV, and JSON reports will be published for each test in your CI jobs.
//
// To use the integration, you need to annotate your class with the LighthouseAnalysisRun attribute.
// The integration works for Chrome and Chrome headless. If you use BELLATRIX Selenium Grid Hub and Node Servlets, you will be able to execute the
// Lighthouse analysis even during Selenium Grid execution.
// Selenoid is not supported since they have their implementation of the Selenium Grid written in GO, so they don't support servlets (e.g., plugins)
// Different cloud providers have their integrations, with Lighthouse visualizes the reports in their systems but does not allow you to perform assertions.
[TestFixture]
[LighthouseAnalysisRun]
public class GoogleLighthouseAnalysisTests : NUnit.WebTest
{
    [Test]
    public void LighthouseAnalysisLogin()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/my-account/");
        TextField userNameField = App.Components.CreateById<TextField>("username");
        Password passwordField = App.Components.CreateById<Password>("password");
        Button loginButton = App.Components.CreateByXpath<Button>("//button[@name='login']");

        userNameField.SetText("info@berlinspaceflowers.com");
        passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$");
        loginButton.Click();

        Div myAccountContentDiv = App.Components.CreateByClass<Div>("woocommerce-MyAccount-content");
        myAccountContentDiv.ValidateInnerTextContains("Hello Berlin Spaceflowers");

        // Once you are ready. Call the PerformLighthouseAnalysis method, which will start the Lighthouse CLI.
        // Keep in mind that this will take 20-30 seconds since Lighthouse is starting its own Chrome tab.
        // In the testFrameworkSettings.json file, there is a section called lighthouseSettings.
        // There, you can enable/disable the integration + you can set default CLI arguments.
        // In the PerformLighthouseAnalysis method, you can add additional arguments merged with the default ones or override them completely.
        App.Lighthouse.PerformLighthouseAnalysis();

        // BELLATRIX exposes a few assertion methods for most essential metrics.
        App.Lighthouse.AssertFirstMeaningfulPaintScoreMoreThan(0.5);

        // Since there are thousands of possible values that you might be interested in validating, we give you the AssertMetric method.
        // Through it, you can use lambda syntax to pick the value that you want to assert. Through Fluent Builder API afterward you need to pick
        // what type of assertion you want to perform - equal, greaterThan, lessThan, and so on + the expected value.
        // Of course, don't forget to call the Perform method, which will do the actual assertion. The API works pretty much the same as Selenium Actions.
        App.Lighthouse.AssertMetric(r => r.Categories.Pwa.Score).LessThan(2.3).Perform();
        App.Lighthouse.AssertMetric(r => r.Categories.Performance.Score).GreaterThan(0.5).Perform();
        App.Lighthouse.AssertMetric(r => r.Categories.Performance.Score).GreaterThanOrEqual(0.5).Perform();

        Anchor logoutLink = App.Components.CreateByInnerTextContaining<Anchor>("Log out");

        logoutLink.ValidateIsVisible();
        logoutLink.Click();

        // By default if the integration is enabled the HTML, CSV and JSON files will be attached for each test in you CI job.
    }
}