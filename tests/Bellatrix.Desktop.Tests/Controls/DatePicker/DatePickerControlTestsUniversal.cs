// <copyright file="DatePickerControlTestsUniversal.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Desktop.Tests;

[TestClass]
[App(Constants.UniversalAppPath, Lifecycle.RestartEveryTime)]
[AllureSuite("DatePicker Control")]
[AllureTag("Universal")]
public class DatePickerControlTestsUniversal : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_DateHovered_Universal()
    {
        var datePicker = App.Components.CreateByAutomationId<Date>("datePicker");

        datePicker.Hover();

        var label = App.Components.CreateByAutomationId<Label>("resultTextBlock");
        Assert.AreEqual("edatepickerHovered", label.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsFalse_When_DatePickerIsNotDisabled_Universal()
    {
        var datePicker = App.Components.CreateByAutomationId<Date>("datePicker");

        Assert.AreEqual(false, datePicker.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsTrue_When_DatePickerIsDisabled_Universal()
    {
        var datePicker = App.Components.CreateByAutomationId<Date>("disabledDatePicker");

        Assert.AreEqual(true, datePicker.IsDisabled);
    }
}
