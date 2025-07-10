﻿// <copyright file="PhoneControlTestsChrome.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
[AllureSuite("Phone Control")]
public class PhoneControlTestsChrome : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().PhonePage);

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void PhoneSet_When_UseSetPhoneMethod_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        phoneElement.SetPhone("123-4567-8901");

        Assert.AreEqual("123-4567-8901", phoneElement.GetPhone());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetPhoneReturnsCorrectPhone_When_DefaultPhoneIsSet_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone3");

        Assert.AreEqual("123-4567-8901", phoneElement.GetPhone());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        Assert.AreEqual(false, phoneElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone5");

        Assert.AreEqual(false, phoneElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone4");

        Assert.AreEqual(true, phoneElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone4");

        Assert.AreEqual(false, phoneElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone6");

        Assert.AreEqual(true, phoneElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        var maxLength = phoneElement.MaxLength;

        Assert.IsNull(maxLength);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        Assert.IsNull(phoneElement.MinLength);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetSizeReturnsDefault20_When_SizeAttributeIsNotPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        // Specifies the width of an <input> element, in characters. Default value is 20
        Assert.AreEqual(20, phoneElement.Size);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone2");

        Assert.AreEqual(80, phoneElement.MaxLength);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone2");

        Assert.AreEqual(10, phoneElement.MinLength);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetSizeReturns30_When_SizeAttributeIsNotPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone2");

        Assert.AreEqual(30, phoneElement.Size);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone4");

        Assert.AreEqual(false, phoneElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone7");

        Assert.AreEqual(true, phoneElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetPlaceholder_When_PlaceholderAttributeIsSet_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        Assert.AreEqual("123-4567-8901", phoneElement.Placeholder);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone1");

        Assert.IsNull(phoneElement.Placeholder);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone8");

        phoneElement.Hover();

        Assert.AreEqual("color: red;", phoneElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone9");

        phoneElement.Focus();

        Assert.AreEqual("color: blue;", phoneElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone9");

        bool isDisabled = phoneElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Chrome()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone10");

        bool isDisabled = phoneElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}