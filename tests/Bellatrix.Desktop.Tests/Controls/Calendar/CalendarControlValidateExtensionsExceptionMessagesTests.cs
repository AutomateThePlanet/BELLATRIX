// <copyright file="CalendarControlValidateExtensionsExceptionMessagesTests.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.Tests;

[TestClass]
[App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
[AllureSuite("Calendar Control")]
[AllureFeature("ValidateExtensionsExceptionMessages")]
[AllureTag("WPF")]
public class CalendarControlValidateExtensionsExceptionMessagesTests : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void ValidateIsNotDisabled_ThrowException_When_CalendarNotDisabled()
    {
        var calendar = App.Components.CreateByAutomationId<Calendar>("calendar");

        try
        {
            calendar.ValidateIsDisabled();
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = "The control should be disabled but it was NOT.";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }
}
