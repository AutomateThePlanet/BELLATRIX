// <copyright file="ShadowDomTests.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Web.Components;
using Bellatrix.Web.Tests.Controls.Table;
using Microsoft.TeamFoundation.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls.ShadowDom;

[TestClass]
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
public class ShadowDomTests : MSTest.WebTest
{
    public override void TestInit()
    {
        base.TestInit();

        var url = ConfigurationService.GetSection<TestPagesSettings>().ShadowDOMPage;
        App.Navigation.NavigateToLocalPage(url);
    }

    [TestMethod]
    public void CreatingShadowRootComponent()
    {
        var shadowRoot = App.Components.CreateById<ShadowRoot>("basicShadowHost");

        Assert.IsFalse(shadowRoot.InnerHtml.IsNullOrEmpty());
    }

    [TestMethod]
    public void FindingElementWithCss_Basic()
    {
        var shadowRoot = App.Components.CreateById<ShadowRoot>("basicShadowHost");
        var select = shadowRoot.CreateByCss<Select>("[name='select']");

        select.SelectByText("Is");

        Assert.AreEqual("Is", select.GetSelected().InnerText);
    }

    [TestMethod]
    public void FindingElementWithXpath_Basic()
    {
        var shadowRoot = App.Components.CreateById<ShadowRoot>("basicShadowHost");
        var select = shadowRoot.CreateByXpath<Select>(".//select[@name='select']");

        select.SelectByText("Is");

        Assert.AreEqual("Is", select.GetSelected().InnerText);
    }

    [TestMethod]
    public void DirectlyFindingElementInNestedShadowRoot_WithXpath()
    {
        var shadowRoot = App.Components.CreateById<ShadowRoot>("complexShadowHost");

        var firstEditAnchor = shadowRoot.CreateByXpath<Anchor>(".//a[@href='#edit']");
        Assert.AreEqual("edit", firstEditAnchor.InnerText);
    }

    [TestMethod]
    public void DirectlyFindingElementInNestedShadowRoot_WithCss()
    {
        var shadowRoot = App.Components.CreateById<ShadowRoot>("complexShadowHost");

        var firstEditAnchor = shadowRoot.CreateByCss<Anchor>("[href='#edit']");
        Assert.AreEqual("edit", firstEditAnchor.InnerText);
    }

    [TestMethod]
    public void FindingElementByAnotherElementInNestedShadowRoot_WithXpath()
    {
        var shadowRoot = App.Components.CreateById<ShadowRoot>("complexShadowHost");

        var table = shadowRoot.CreateByXpath<Grid>(".//table[@id='shadowTable']")
            .SetModelColumns<TableData>()
            .SetColumn("Action");

        var row = table.GetFirstOrDefaultRow<GridCell>(cell => cell.InnerText.Equals("jsmith@gmail.com"));

        var edit = table.GetColumn("Action")[row.Index].CreateByXpath<Anchor>(".//a[@href='#edit']");

        Assert.AreEqual("edit", edit.InnerText);
    }

    [TestMethod]
    public void FindingElementByAnotherElementInNestedShadowRoot_WithCss()
    {
        var shadowRoot = App.Components.CreateById<ShadowRoot>("complexShadowHost");

        var table = shadowRoot.CreateByCss<Grid>("[id='shadowTable']")
            .SetModelColumns<TableData>()
            .SetColumn("Action");

        var row = table.GetFirstOrDefaultRow<GridCell>(cell => cell.InnerText.Equals("jsmith@gmail.com"));

        var edit = table.GetColumn("Action")[row.Index].CreateByCss<Anchor>("[href='#edit']");
        Assert.AreEqual("edit", edit.InnerText);
    }
}