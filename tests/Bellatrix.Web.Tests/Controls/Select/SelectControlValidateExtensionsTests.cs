// <copyright file="SelectControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Select Control")]
[AllureFeature("ValidateExtensions")]
public class SelectControlValidateExtensionsTests : MSTest.WebTest
{
    private string _url = ConfigurationService.GetSection<TestPagesSettings>().SelectLocalPage;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(_url);
        ////_url = App.Browser.Url.ToString();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateValueIs_DoesNotThrowException_When_UseSelectByTextMethod_Edge()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        selectComponent.SelectByText("Awesome");

        selectComponent.GetSelected().ValidateValueIs("bella2");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateValueIs_DoesNotThrowException_When_Hover_Edge()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect1");

        selectComponent.Hover();

        selectComponent.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent_Edge()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        selectComponent.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent_Edge()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect3");

        selectComponent.ValidateIsDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributePresent_Edge()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect4");

        selectComponent.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotRequired_DoesNotThrowException_When_RequiredAttributeNotPresent_Edge()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        selectComponent.ValidateIsNotRequired();
    }
}