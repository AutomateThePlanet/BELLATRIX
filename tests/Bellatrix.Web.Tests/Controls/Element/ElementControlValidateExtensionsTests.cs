// <copyright file="ElementControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web.Tests.Controls.Element;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Element Control")]
[AllureFeature("ValidateExtensions")]
public class ElementControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ElementLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsVisible_DoesNotThrowException_When_ElementIsPresent()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        urlElement.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotVisible_DoesNotThrowException_When_ElementIsHidden()
    {
        var urlElement = App.Components.CreateById<Url>("myURL11");

        urlElement.ValidateIsNotVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateCssClassIs_DoesNotThrowException_When_SetAttributeChangesAttributeValue()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        urlElement.SetAttribute("class", "myTestClass1");

        urlElement.ValidateCssClassIs("myTestClass1");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateCssClassIsNull_DoesNotThrowException_When_ClassAttributeIsNotPresent()
    {
        var urlElement = App.Components.CreateById<Url>("myURL1");

        urlElement.ValidateCssClassIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_InsideAnotherElementAndIsPresent()
    {
        var wrapperDiv = App.Components.CreateById<Div>("myURL10Wrapper");

        var urlElement = wrapperDiv.CreateById<Url>("myURL10");

        urlElement.ValidateIsDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateTitleIs_DoesNotThrowException_When_TitleAttributeIsPresent()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL13");

        element.ValidateTitleIs("bellatrix.solutions");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateTitleIsNotNull_DoesNotThrowException_When_TitleAttributeIsPresent()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL13");

        element.ValidateTitleIsNotNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateTitleIsNull_DoesNotThrowException_When_TitleAttributeIsNotPresent()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL12");

        element.ValidateTitleIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateTabIndexIs_DoesNotThrowException_When_TabIndexAttributeIsPresent()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL14");

        element.ValidateTabIndexIs("1");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateTabIndexIs_DoesNotThrowException_When_StyleAttributeIsPresent()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL16");

        element.ValidateStyleIs("color: green;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIsNull_DoesNotThrowException_When_StyleAttributeIsNotPresent()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL12");

        element.ValidateStyleIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateDirIs_DoesNotThrowException_When_DirAttributeIsPresent()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL19");

        element.ValidateDirIs("rtl");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateDirIsNull_DoesNotThrowException_When_DirAttributeIsNotPresent()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL12");

        element.ValidateDirIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateDirIsNull_DoesNotThrowException_When_LangAttributeIsPresent()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL20");

        element.ValidateLangIs("en");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateLangIsNull_DoesNotThrowException_When_LangAttributeIsNotPresent()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL12");

        element.ValidateLangIsNull();
    }
}