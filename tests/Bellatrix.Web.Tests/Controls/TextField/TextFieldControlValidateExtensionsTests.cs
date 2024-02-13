// <copyright file="TextFieldControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("TextField Control")]
[AllureFeature("ValidateExtensions")]
public class TextFieldControlValidateExtensionsTests : MSTest.WebTest
{
    private string _url = ConfigurationService.GetSection<TestPagesSettings>().TextFieldLocalPage;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(_url);
        ////_url = App.Browser.Url.ToString();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText");

        textFieldElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText4");

        Assert.AreEqual(true, textFieldElement.IsAutoComplete);
        textFieldElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsNotPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText4");

        textFieldElement.ValidateIsNotReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsReadonly_DoesNotThrowException_When_ReadonlyAttributeIsPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText6");

        textFieldElement.ValidateIsReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxLengthIsNull_DoesNotThrowException_When_MaxLengthAttributeIsNotPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText");

        textFieldElement.ValidateMaxLengthIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIsNull_DoesNotThrowException_When_MinLengthAttributeIsNotPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText");

        textFieldElement.ValidateMinLengthIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateSizeIs_DoesNotThrowException_When_SizeAttributeIsNotPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText");

        textFieldElement.ValidateSizeIs(20);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxLengthIs_DoesNotThrowException_When_MaxLengthAttributeIsPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText2");

        textFieldElement.ValidateMaxLengthIs(80);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIs_DoesNotThrowException_When_MinLengthAttributeIsPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText2");

        textFieldElement.ValidateMinLengthIs(10);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText4");

        textFieldElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText7");

        textFieldElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIs_DoesNotThrowException_When_PlaceholderAttributeIsSet()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText");

        textFieldElement.ValidatePlaceholderIs("your Text term goes here");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIsNull_DoesNotThrowException_When_PlaceholderAttributeIsNotPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText1");

        textFieldElement.ValidatePlaceholderIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText8");

        textFieldElement.Hover();

        textFieldElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText9");

        textFieldElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var textFieldElement = App.Components.CreateById<TextField>("myText10");

        textFieldElement.ValidateIsDisabled();
    }
}