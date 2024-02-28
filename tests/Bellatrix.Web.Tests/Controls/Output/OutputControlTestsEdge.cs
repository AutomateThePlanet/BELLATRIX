// <copyright file="OutputControlTestsEdge.cs" company="Automate The Planet Ltd.">
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
[AllureFeature("Edge Browser")]
public class OutputControlTestsEdge : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().OutputLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnRed_When_Hover_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput");

        outputComponent.Hover();

        Assert.AreEqual("color: red;", outputComponent.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void Return10_When_InnerText_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput");

        Assert.AreEqual("10", outputComponent.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnNull_When_InnerTextNotSet_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput2");

        Assert.IsNotNull(outputComponent.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnNull_When_InnerHtmlNotSet_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput2");

        Assert.IsNotNull(outputComponent.InnerHtml);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnButtonHtml_When_InnerHtmlSet_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput1");

        Assert.AreEqual("<button name=\"button\">Click me</button>", outputComponent.InnerHtml);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnNull_When_ForNotSet_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput2");

        Assert.IsNull(outputComponent.For);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnOutputFor_When_ForSet_Edge()
    {
        var outputComponent = App.Components.CreateById<Output>("myOutput");

        Assert.AreEqual("myOutput", outputComponent.For);
    }
}