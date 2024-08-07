﻿// <copyright file="PasswordControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Playwright.Tests.Controls;

[TestClass]
[Browser(BrowserTypes.Webkit, Lifecycle.ReuseIfStarted)]
[AllureSuite("Password Control")]
public class PasswordControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().PasswordPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void PasswordSet_When_UseSetPasswordMethod_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        passwordElement.SetPassword("bellatrix");

        Assert.AreEqual("bellatrix", passwordElement.GetPassword());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetPasswordReturnsCorrectPassword_When_DefaultPasswordIsSet_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword3");

        Assert.AreEqual("password for stars", passwordElement.GetPassword());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        Assert.AreEqual(false, passwordElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword5");

        Assert.AreEqual(false, passwordElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword4");

        Assert.AreEqual(true, passwordElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword4");

        Assert.AreEqual(false, passwordElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword6");

        Assert.AreEqual(true, passwordElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        var maxLength = passwordElement.MaxLength;

        Assert.IsNull(maxLength);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        Assert.IsNull(passwordElement.MinLength);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetSizeReturnsDefault20_When_SizeAttributeIsNotPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        // Specifies the width of an <input> element, in characters. Default value is 20
        Assert.AreEqual(20, passwordElement.Size);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword2");

        Assert.AreEqual(80, passwordElement.MaxLength);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword2");

        Assert.AreEqual(10, passwordElement.MinLength);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetSizeReturns30_When_SizeAttributeIsNotPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword2");

        Assert.AreEqual(30, passwordElement.Size);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword4");

        Assert.AreEqual(false, passwordElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword7");

        Assert.AreEqual(true, passwordElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetPlaceholder_When_PlaceholderAttributeIsSet_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        Assert.AreEqual("your password term goes here", passwordElement.Placeholder);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword1");

        Assert.IsNull(passwordElement.Placeholder);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword8");

        passwordElement.Hover();

        Assert.AreEqual("color: red;", passwordElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword9");

        passwordElement.Focus();

        Assert.AreEqual("color: blue;", passwordElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword9");

        bool isDisabled = passwordElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword10");

        bool isDisabled = passwordElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}