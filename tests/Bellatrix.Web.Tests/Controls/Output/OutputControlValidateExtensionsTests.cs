// <copyright file="OutputControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Output Control")]
[AllureFeature("ValidateExtensions")]
public class OutputControlValidateExtensionsTests : MSTest.WebTest
{
    private string _url = ConfigurationService.GetSection<TestPagesSettings>().OutputLocalPage;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(_url);
        ////_url = App.Browser.Url.ToString();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateDateIs_DoesNotThrowException_When_Hover_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput");

        outputComponent.Hover();

        outputComponent.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateInnerTextIs_DoesNotThrowException_When_InnerText_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput");

        outputComponent.ValidateInnerTextIs("10");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateInnerHtmlIs_DoesNotThrowException_When_InnerHtmlSet_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput1");

        outputComponent.ValidateInnerHtmlIs("<button name=\"button\">Click me</button>");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateForIsNull_DoesNotThrowException_When_ForNotSet_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput2");

        outputComponent.ValidateForIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateForIs_DoesNotThrowException_When_ForSet_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput");

        outputComponent.ValidateForIs("myOutput");
    }
}