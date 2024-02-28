// <copyright file="TextAreaControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("TextArea Control")]
[AllureFeature("ValidateExtensions")]
public class TextAreaControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().TextAreaLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        textAreaElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea4");

        textAreaElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea4");

        textAreaElement.ValidateIsNotReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsReadonly_DoesNotThrowException_When_ReadonlyAttributeIsPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea6");

        textAreaElement.ValidateIsReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxLengthIsNull_DoesNotThrowException_When_MaxLengthAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        textAreaElement.ValidateMaxLengthIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIsNull_DoesNotThrowException_When_MinLengthAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        textAreaElement.ValidateMinLengthIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIsNull_DoesNotThrowException_When_RowsAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        textAreaElement.ValidateRowsIs(2);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateColsIs_DoesNotThrowException_When_ColsAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        textAreaElement.ValidateColsIs(20);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxLengthIs_DoesNotThrowException_When_MaxLengthAttributeIsPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea2");

        textAreaElement.ValidateMaxLengthIs(80);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIs_DoesNotThrowException_When_MinLengthAttributeIsPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea2");

        textAreaElement.ValidateMinLengthIs(10);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateRowsIs_DoesNotThrowException_When_RowsAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea11");

        textAreaElement.ValidateRowsIs(5);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea4");

        textAreaElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea7");

        textAreaElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIs_DoesNotThrowException_When_PlaceholderAttributeIsSet_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        textAreaElement.ValidatePlaceholderIs("your Text term goes here");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIsNull_DoesNotThrowException_When_PlaceholderAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea1");

        textAreaElement.ValidatePlaceholderIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea8");

        textAreaElement.Hover();

        textAreaElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea9");

        textAreaElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea10");

        textAreaElement.ValidateIsDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateWrapIs_DoesNotThrowException_When_WrapAttributeIsSet_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea13");

        textAreaElement.ValidateWrapIs("hard");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateSpellCheckIs_DoesNotThrowException_When_SpellCheckAttributeIsSet_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea12");

        textAreaElement.ValidateSpellCheckIs("true");
    }
}