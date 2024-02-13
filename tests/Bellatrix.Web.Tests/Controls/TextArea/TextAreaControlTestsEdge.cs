// <copyright file="TextAreaControlTestsEdge.cs" company="Automate The Planet Ltd.">
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
[AllureFeature("Edge Browser")]
public class TextAreaControlTestsEdge : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().TextAreaLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void TextSet_When_UseSetTextMethod_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        textAreaElement.SetText("aangelov@bellatrix.solutions");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        Assert.AreEqual(false, textAreaElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea5");

        Assert.AreEqual(false, textAreaElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea4");

        Assert.AreEqual(true, textAreaElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea4");

        Assert.AreEqual(false, textAreaElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea6");

        Assert.AreEqual(true, textAreaElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        var maxLength = textAreaElement.MaxLength;

        Assert.IsNull(maxLength);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        Assert.IsNull(textAreaElement.MinLength);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetRowsReturnsDefault2_When_RowsAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        // Specifies the width of an <input> element, in characters. Default value is 20
        Assert.AreEqual(2, textAreaElement.Rows);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetColsReturnsDefault20_When_ColsAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        // Specifies the width of an <input> element, in characters. Default value is 20
        Assert.AreEqual(20, textAreaElement.Cols);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea2");

        Assert.AreEqual(80, textAreaElement.MaxLength);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea2");

        Assert.AreEqual(10, textAreaElement.MinLength);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetRowsReturns5_When_RowsAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea11");

        Assert.AreEqual(5, textAreaElement.Rows);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetColsReturns5_When_ColsAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea11");

        Assert.AreEqual(50, textAreaElement.Cols);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea4");

        Assert.AreEqual(false, textAreaElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea7");

        Assert.AreEqual(true, textAreaElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetPlaceholder_When_PlaceholderAttributeIsSet_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        Assert.AreEqual("your Text term goes here", textAreaElement.Placeholder);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea1");

        Assert.IsNull(textAreaElement.Placeholder);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnRed_When_Hover_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea8");

        textAreaElement.Hover();

        Assert.AreEqual("color: red;", textAreaElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnBlue_When_Focus_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea9");

        textAreaElement.Focus();

        Assert.AreEqual("color: blue;", textAreaElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea9");

        bool isDisabled = textAreaElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnTrue_When_DisabledAttributePresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea10");

        bool isDisabled = textAreaElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetWrap_When_WrapAttributeIsSet_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea13");

        Assert.AreEqual("hard", textAreaElement.Wrap);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetWrapReturnsNull_When_WrapAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea1");

        Assert.AreEqual("soft", textAreaElement.Wrap);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetSpellCheck_When_SpellCheckAttributeIsSet_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea12");

        Assert.AreEqual("true", textAreaElement.SpellCheck);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetSpellCheckReturnsTrue_When_SpellCheckAttributeIsNotPresent_Edge()
    {
        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea1");

        Assert.AreEqual("true", textAreaElement.SpellCheck);
    }
}