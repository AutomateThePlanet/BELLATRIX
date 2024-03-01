// <copyright file="WeekControlValidateExtensionsExceptionMessagesTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Week Control")]
[AllureFeature("ValidateExtensions")]
public class WeekControlValidateExtensionsExceptionMessagesTests : MSTest.WebTest
{
    private string _url = ConfigurationService.GetSection<TestPagesSettings>().WeekLocalPage;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(_url);
        ////_url = App.Browser.Url.ToString();
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateWeekIsThrowsException()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        weekElement.SetWeek(2017, 7);

        try
        {
            weekElement.ValidateWeekIs("2017-W08", 200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's week should be '2017-W08' but was '2017-W07'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }
}