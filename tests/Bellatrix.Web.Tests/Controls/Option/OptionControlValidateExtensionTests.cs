// <copyright file="OptionControlValidateExtensionTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Option Control")]
public class OptionControlValidateExtensionTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().OptionLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateInnerTextIs_DoesNotThrowException_When_UseGetInnerTextMethod()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        selectComponent.GetSelected().ValidateInnerTextIs("Bellatrix");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateValueIs_DoesNotThrowException_When_UseGetValueMethod()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect2");

        selectComponent.GetSelected().ValidateValueIs("bella2");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsSelected_DoesNotThrowException_When_OptionSelectedAndCallGetIsSelectedMethod()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        selectComponent.GetAllOptions()[0].ValidateIsSelected();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotSelected_DoesNotThrowException_When_OptionNotSelectedAndCallGetIsSelectedMethod()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        selectComponent.GetAllOptions()[1].ValidateIsNotSelected();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        selectComponent.GetSelected().ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributeIsPresent()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect4");

        selectComponent.GetAllOptions()[1].ValidateIsDisabled();
    }
}