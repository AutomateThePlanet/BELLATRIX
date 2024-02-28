// <copyright file="RadioGroupControlTests.cs" company="Automate The Planet Ltd.">
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
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.RadioGroup1",
    Lifecycle.ReuseIfStarted)]
[AllureSuite("RadioGroup Control")]
public class RadioGroupControlTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void Return5RadioButtons_When_CallGetAllMethod()
    {
        var radioGroup = App.Components.CreateByIdContaining<RadioGroup>("menu");

        var radioButtons = radioGroup.GetAll();

        Assert.AreEqual(5, radioButtons.Count());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ClickDinnerRadioButton_When_CallClickByText()
    {
        var radioGroup = App.Components.CreateByIdContaining<RadioGroup>("menu");

        radioGroup.ClickByText("Dinner");
        var clickedRadioButton = radioGroup.GetChecked();

        clickedRadioButton.ValidateTextIs("Dinner");
        clickedRadioButton.ValidateIsChecked();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ClickLunchRadioButton_When_CallClickByIndex()
    {
        var radioGroup = App.Components.CreateByIdContaining<RadioGroup>("menu");

        radioGroup.ClickByIndex(2);
        var clickedRadioButton = radioGroup.GetChecked();

        clickedRadioButton.ValidateTextIs("Lunch");
        clickedRadioButton.ValidateIsChecked();
    }
}