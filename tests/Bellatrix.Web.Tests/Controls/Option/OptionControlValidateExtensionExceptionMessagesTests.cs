// <copyright file="OptionControlValidateExtensionExceptionMessagesTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Option Control")]
public class OptionControlValidateExtensionExceptionMessagesTests : MSTest.WebTest
{
    private string _url = ConfigurationService.GetSection<TestPagesSettings>().OptionLocalPage;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(_url);
        ////_url = App.Browser.Url.ToString();
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateIsSelectedThrowsException()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        try
        {
            selectComponent.GetAllOptions()[1].ValidateIsSelected(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control should be selected but was NOT. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateIsNotSelectedThrowsException()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        try
        {
            selectComponent.GetAllOptions()[0].ValidateIsNotSelected(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control should be not selected but it WAS. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }
}