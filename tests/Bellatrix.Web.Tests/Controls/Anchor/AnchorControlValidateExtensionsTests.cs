// <copyright file="AnchorControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Anchor Control")]
[AllureFeature("ValidateExtensions")]
public class AnchorControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().AnchorLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_Anchor_When_StyleIsExact()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor1");

        anchorElement.Hover();

        anchorElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateInnerTextIs_DoesNotThrowException_Anchor_When_InnerTextIsExact()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor");

        anchorElement.ValidateInnerTextIs("Automate The Planet");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateInnerHtmlIs_DoesNotThrowException_Anchor_When_InnerHtmlIsExact()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor4");

        anchorElement.ValidateInnerHtmlIs("<button name=\"button\">Click me</button>");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateRelIs_DoesNotThrowException_Anchor_When_RelIsExact()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor5");

        anchorElement.ValidateRelIs("canonical");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateTargetIs_DoesNotThrowException_Anchor_When_TargetIsExact()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor");

        anchorElement.ValidateTargetIs("_self");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateHrefIs_DoesNotThrowException_Anchor_When_HrefIsExact()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor");

        anchorElement.ValidateHrefIs("https://automatetheplanet.com/");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateHrefSet_DoesNotThrowException_Anchor_When_HrefIsSet()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor2");

        anchorElement.ValidateHrefIsSet();
    }
}