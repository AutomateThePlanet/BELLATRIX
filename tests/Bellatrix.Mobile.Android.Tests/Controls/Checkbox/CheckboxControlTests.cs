// <copyright file="CheckboxControlTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Mobile.Android.Tests;

[TestClass]
[Android(Constants.AndroidNativeAppPath,
    "",
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.Controls1",
    Lifecycle.ReuseIfStarted)]
[AllureSuite("CheckBox Control")]
public class CheckboxControlTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsCheckedTrue_When_CheckBoxUncheckedAndCheckIt()
    {
        var checkBox = App.Components.CreateByIdContaining<CheckBox>("check1");

        checkBox.Check();

        Assert.IsTrue(checkBox.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void Checkbox1TextReturned_When_CheckBoxGetTextMethodCalled()
    {
        var checkBox = App.Components.CreateByIdContaining<CheckBox>("check1");

        string text = checkBox.GetText();

        Assert.AreEqual("Checkbox 1", text);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsCheckedFalse_When_CheckBoxCheckedAndUncheckIt()
    {
        var checkBox = App.Components.CreateByIdContaining<CheckBox>("check2");

        checkBox.Check();
        checkBox.Uncheck();

        Assert.IsFalse(checkBox.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsDisabledReturnsFalse_When_CheckBoxIsNotDisabled()
    {
        var checkBox = App.Components.CreateByIdContaining<CheckBox>("check2");

        Assert.AreEqual(false, checkBox.IsDisabled);
    }
}