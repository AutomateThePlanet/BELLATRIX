// <copyright file="SpanControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Span Control")]
[AllureFeature("ValidateExtensions")]
public class SpanControlValidateExtensionsTests : MSTest.WebTest
{
    private string _url = ConfigurationService.GetSection<TestPagesSettings>().SpanLocalPage;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(_url);
        ////_url = App.Browser.Url.ToString();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover_Edge()
    {
        var spanElement = App.Components.CreateById<Span>("mySpan");

        spanElement.Hover();

        spanElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateInnerTextIs_DoesNotThrowException_When_InnerText_Edge()
    {
        var spanElement = App.Components.CreateById<Span>("mySpan1");

        spanElement.ValidateInnerTextIs("Automate The Planet");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateInnerHtmlIs_DoesNotThrowException_When_InnerHtmlSet_Edge()
    {
        var spanElement = App.Components.CreateById<Span>("mySpan2");

        spanElement.ValidateInnerHtmlIs("<button name=\"button\">Click me</button>");
    }
}