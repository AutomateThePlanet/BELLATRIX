// <copyright file="ImageControlValidateExtensionsExceptionMessagesTests.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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

namespace Bellatrix.Web.Tests.Controls
{
    [TestClass]
    [Browser(BrowserType.Edge, BrowserBehavior.ReuseIfStarted)]
    [AllureSuite("Image Control")]
    [AllureFeature("ValidateExtensions")]
    public class ImageControlValidateExtensionsExceptionMessagesTests : WebTest
    {
        private string _url = ConfigurationService.GetSection<TestPagesSettings>().ImageLocalPage;

        public override void TestInit()
        {
            App.NavigationService.NavigateToLocalPage(_url);
            ////_url = App.BrowserService.Url.ToString();
        }

        [TestMethod]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void CorrectExceptionMessageSet_When_ValidateSrcIsThrowsException()
        {
            var imageElement = App.ElementCreateService.CreateById<Image>("myImage");

            try
            {
                imageElement.ValidateSrcIs("https://bellatrix.solutions/assets/uploads/2017/09/logo.png1", 200, 50);
            }
            catch (ElementPropertyValidateException e)
            {
                string expectedExceptionMessage = $"The control's src should be 'https://bellatrix.solutions/assets/uploads/2017/09/logo.png1' but was 'https://bellatrix.solutions/assets/uploads/2017/09/logo.png'. The test failed on URL:";
                Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
            }
        }

        [TestMethod]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void CorrectExceptionMessageSet_When_ValidateSrcIsNotNullThrowsException()
        {
            var imageElement = App.ElementCreateService.CreateById<Image>("myImage");

            try
            {
                imageElement.ValidateSrcIsNotNull(200, 50);
            }
            catch (ElementPropertyValidateException e)
            {
                string expectedExceptionMessage = $"The control's src shouldn't be null but was. The test failed on URL:";
                Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
            }
        }

        [TestMethod]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void CorrectExceptionMessageSet_When_ValidateSrcIsNullThrowsException()
        {
            var imageElement = App.ElementCreateService.CreateById<Image>("myImage");

            try
            {
                imageElement.ValidateSrcIsNull(200, 50);
            }
            catch (ElementPropertyValidateException e)
            {
                string expectedExceptionMessage = $"The control's src should be null but was 'https://bellatrix.solutions/assets/uploads/2017/09/logo.png'. The test failed on URL:";
                Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
            }
        }

        [TestMethod]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void CorrectExceptionMessageSet_When_ValidateAltIsThrowsException()
        {
            var imageElement = App.ElementCreateService.CreateById<Image>("myImage");

            try
            {
                imageElement.ValidateAltIs("MDN1", 200, 50);
            }
            catch (ElementPropertyValidateException e)
            {
                string expectedExceptionMessage = $"The control's alt should be 'MDN1' but was 'MDN'. The test failed on URL:";
                Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
            }
        }

        [TestMethod]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void CorrectExceptionMessageSet_When_ValidateAltIsNullThrowsException()
        {
            var imageElement = App.ElementCreateService.CreateById<Image>("myImage");

            try
            {
                imageElement.ValidateAltIsNull(200, 50);
            }
            catch (ElementPropertyValidateException e)
            {
                string expectedExceptionMessage = $"The control's alt should be null but was 'MDN'. The test failed on URL:";
                Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
            }
        }

        [TestMethod]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void CorrectExceptionMessageSet_When_ValidateSrcSetIsThrowsException()
        {
            var imageElement = App.ElementCreateService.CreateById<Image>("myImage1");

            try
            {
                imageElement.ValidateSrcSetIs("mdn-logo-HD.png 2", 200, 50);
            }
            catch (ElementPropertyValidateException e)
            {
                string expectedExceptionMessage = $"The control's srcset should be 'mdn-logo-HD.png 2' but was 'mdn-logo-HD.png 2x'. The test failed on URL:";
                Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
            }
        }

        [TestMethod]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void CorrectExceptionMessageSet_When_ValidateSrcSetIsNullThrowsException()
        {
            var imageElement = App.ElementCreateService.CreateById<Image>("myImage2");

            try
            {
                imageElement.ValidateSrcSetIsNull(200, 50);
            }
            catch (ElementPropertyValidateException e)
            {
                string expectedExceptionMessage = $"The control's srcset should be null but was 'clock-demo-thumb-200.png 200w,clock-demo-thumb-400.png 400w'. The test failed on URL:";
                Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
            }
        }

        [TestMethod]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void CorrectExceptionMessageSet_When_ValidateSizesIsThrowsException()
        {
            var imageElement = App.ElementCreateService.CreateById<Image>("myImage2");

            try
            {
                imageElement.ValidateSizesIs("min", 200, 50);
            }
            catch (ElementPropertyValidateException e)
            {
                string expectedExceptionMessage = $"The control's sizes should be 'min' but was '(min-width: 600px) 200px, 50vw'. The test failed on URL:";
                Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
            }
        }

        [TestMethod]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void CorrectExceptionMessageSet_When_ValidateSizesIsNullThrowsException()
        {
            var imageElement = App.ElementCreateService.CreateById<Image>("myImage2");

            try
            {
                imageElement.ValidateSizesIsNull(200, 50);
            }
            catch (ElementPropertyValidateException e)
            {
                string expectedExceptionMessage = $"The control's sizes should be null but was '(min-width: 600px) 200px, 50vw'. The test failed on URL:";
                Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
            }
        }
    }
}