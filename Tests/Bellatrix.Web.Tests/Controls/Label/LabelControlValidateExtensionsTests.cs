// <copyright file="LabelControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
    [AllureSuite("Label Control")]
    [AllureFeature("ValidateExtensions")]
    public class LabelControlValidateExtensionsTests : WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().LabelLocalPage);

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ValidateStyleIs_DoesNotThrowException_When_Hover()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel");

            labelElement.Hover();

            labelElement.ValidateStyleIs("color: red;");
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ValidateInnerTextIs_DoesNotThrowException_When_InnerTextSet()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel");

            labelElement.ValidateInnerTextIs("Hover");
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ValidateInnerHtmlIs_DoesNotThrowException_When_InnerHtmlSet()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel1");

            labelElement.ValidateInnerHtmlIs("<button name=\"button\">Click me</button>");
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ValidateForIsNull_DoesNotThrowException_When_ForNotSet()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel2");

            labelElement.ValidateForIsNull();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ValidateForIs_DoesNotThrowException_When_ForSet()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel");

            labelElement.ValidateForIs("myLabel");
        }
    }
}