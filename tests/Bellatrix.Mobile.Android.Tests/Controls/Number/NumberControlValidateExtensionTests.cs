﻿// <copyright file="NumberControlValidateExtensionTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Mobile.Android.Tests;

[TestClass]
[Android(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.NumberPickerActivity",
    Lifecycle.ReuseIfStarted)]
[AllureSuite("Number Control")]
[AllureFeature("ValidateExtensions")]
public class NumberControlValidateExtensionTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateNumberIs_DoesNotThrowException_When_NumberIsSet()
    {
        var number = App.Components.CreateByClass<Number>("android.widget.NumberPicker");

        number.SetNumber(9);

        number.ValidateNumberIs(9);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateIsDisabled_DoesNotThrowException_When_NumberIsNotDisabled()
    {
        var number = App.Components.CreateByClass<Number>("android.widget.NumberPicker");

        number.ValidateIsNotDisabled();
    }
}