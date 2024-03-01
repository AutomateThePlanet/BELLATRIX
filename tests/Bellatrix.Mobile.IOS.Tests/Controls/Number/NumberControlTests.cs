// <copyright file="NumberControlTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Mobile.IOS.Tests;

[TestClass]
[IOS(Constants.IOSNativeAppPath,
    Constants.IOSAppBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
[AllureSuite("Number Control")]
public class NumberControlTests : MSTest.IOSTest
{
    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void NineSet_When_CallSetTextMethod()
    {
        var number = App.Components.CreateById<Number>("IntegerA");

        number.SetNumber(9);

        Assert.AreEqual(9, number.GetNumber());
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void IsDisabledReturnsFalse_When_NumberIsNotDisabled()
    {
        var number = App.Components.CreateById<Number>("IntegerA");

        Assert.AreEqual(false, number.IsDisabled);
    }
}
