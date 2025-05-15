﻿// <copyright file="EmailControlTestsFirefox.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Playwright.Tests.Controls;

[TestClass]
[Browser(BrowserTypes.Firefox, Lifecycle.ReuseIfStarted)]
[AllureSuite("Email Control")]
public class EmailControlTestsFirefox : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().EmailPage);

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void EmailSet_When_UseSetEmailMethod_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail");

        emailElement.SetEmail("aangelov@bellatrix.solutions");

        Assert.AreEqual("aangelov@bellatrix.solutions", emailElement.GetEmail());
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetEmailReturnsCorrectEmail_When_DefaultEmailIsSet_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail3");

        Assert.AreEqual("aangelov@bellatrix.solutions", emailElement.GetEmail());
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail");

        Assert.AreEqual(false, emailElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail5");

        Assert.AreEqual(false, emailElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail4");

        Assert.AreEqual(true, emailElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail4");

        Assert.AreEqual(false, emailElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail6");

        Assert.AreEqual(true, emailElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail");

        var maxLength = emailElement.MaxLength;

        Assert.IsNull(maxLength);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail");

        Assert.IsNull(emailElement.MinLength);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetSizeReturnsDefault20_When_SizeAttributeIsNotPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail");

        // Specifies the width of an <input> element, in characters. Default value is 20
        Assert.AreEqual(20, emailElement.Size);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail2");

        Assert.AreEqual(80, emailElement.MaxLength);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail2");

        Assert.AreEqual(10, emailElement.MinLength);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetSizeReturns30_When_SizeAttributeIsNotPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail2");

        Assert.AreEqual(30, emailElement.Size);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail4");

        Assert.AreEqual(false, emailElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail7");

        Assert.AreEqual(true, emailElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetPlaceholder_When_PlaceholderAttributeIsSet_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail");

        Assert.AreEqual("your email term goes here", emailElement.Placeholder);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail1");

        Assert.IsNull(emailElement.Placeholder);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail8");

        emailElement.Hover();

        emailElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail9");

        emailElement.Focus();

        emailElement.ValidateStyleIs("color: blue;");
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail9");

        bool isDisabled = emailElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Firefox()
    {
        var emailElement = App.Components.CreateById<Email>("myEmail10");

        bool isDisabled = emailElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}