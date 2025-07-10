﻿// <copyright file="LabelControlValidateExtensionsExceptionMessagesTests.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Playwright.Tests.Controls;

[TestClass]
[Browser(BrowserTypes.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Label Control")]
[AllureFeature("ValidateExtensions")]
public class LabelControlValidateExtensionsExceptionMessagesTests : MSTest.WebTest
{
    private string _url = ConfigurationService.GetSection<TestPagesSettings>().LabelPage;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(_url);
        ////_url = App.Browser.Url.ToString();
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateForIsNullThrowsException()
    {
        var labelElement = App.Components.CreateById<Label>("myLabel");

        try
        {
            labelElement.ValidateForIsNull(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's for should be null but was 'myLabel'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateForIsThrowsException()
    {
        var labelElement = App.Components.CreateById<Label>("myLabel");

        try
        {
            labelElement.ValidateForIs("myLabel1", 200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's for should be 'myLabel1' but was 'myLabel'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }
}