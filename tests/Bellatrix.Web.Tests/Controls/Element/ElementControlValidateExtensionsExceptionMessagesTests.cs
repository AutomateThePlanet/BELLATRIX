// <copyright file="ElementControlValidateExtensionsExceptionMessagesTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web.Tests.Controls.Element;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Element Control")]
[AllureFeature("ValidateExtensions")]
public class ElementControlValidateExtensionsExceptionMessagesTests : MSTest.WebTest
{
    private string _url = ConfigurationService.GetSection<TestPagesSettings>().ElementLocalPage;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(_url);
        ////_url = App.Browser.Url.ToString();
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateIsVisibleThrowsException()
    {
        var urlElement = App.Components.CreateById<Url>("myURL11");

        try
        {
            urlElement.ValidateIsVisible(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control should be visible but was NOT. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateIsNotVisibleThrowsException()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        try
        {
            urlElement.ValidateIsNotVisible(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control should be NOT visible but was NOT. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateCssClassIsThrowsException()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        urlElement.SetAttribute("class", "myTestClass1");

        try
        {
            urlElement.ValidateCssClassIs("myTestClass2", 200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's CSS class should be 'myTestClass2' but was 'myTestClass1'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateCssClassIsNullThrowsException()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        try
        {
            urlElement.ValidateCssClassIsNull(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's CSS class should be null but was 'myTestClass'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateTitleIsThrowsException()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL13");

        try
        {
            element.ValidateTitleIs("bellatrix.solutions1", 200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's title should be 'bellatrix.solutions1' but was 'bellatrix.solutions'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateTitleIsNotNullThrowsException()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL11");

        try
        {
            element.ValidateTitleIsNotNull(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's title shouldn't be null but was. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateTitleIsNullThrowsException()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL13");

        try
        {
            element.ValidateTitleIsNull(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's title should be null but was 'bellatrix.solutions'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateTabIndexIsThrowsException()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL14");

        try
        {
            element.ValidateTabIndexIs("2", 200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's tabindex should be '2' but was '1'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateStyleIsNullThrowsException()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL16");

        try
        {
            element.ValidateStyleIsNull(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's style should be null but was 'color: green;'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateDirIsThrowsException()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL19");

        try
        {
            element.ValidateDirIs("rtl1", 200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's dir should be 'rtl1' but was 'rtl'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateDirIsNullThrowsException()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL19");

        try
        {
            element.ValidateDirIsNull(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's dir should be null but was 'rtl'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateLangIsThrowsException()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL20");

        try
        {
            element.ValidateLangIs("bg", 200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's lang should be 'bg' but was 'en'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CorrectExceptionMessageSet_When_ValidateLangIsNullThrowsException()
    {
        var element = App.Components.CreateById<Bellatrix.Web.Component>("myURL20");

        try
        {
            element.ValidateLangIsNull(200, 50);
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = $"The control's lang should be null but was 'en'. The test failed on URL:";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }
}