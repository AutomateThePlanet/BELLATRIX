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

        var url = ConfigurationService.GetSection<TestPagesSettings>().ShadowDOMLocalPage;
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