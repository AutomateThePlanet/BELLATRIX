// <copyright file="FullPageScreenshotsOnFailTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web.Tests
{
    [TestClass]
    [ScreenshotOnFail(true)]
    [Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    [Browser(OS.OSX, BrowserType.Safari, Lifecycle.ReuseIfStarted)]
    public class FullPageScreenshotsOnFailTests : MSTest.WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");
            var promotionsLink = App.ElementCreateService.CreateByLinkText<Anchor>("Promotions");
            promotionsLink.Click();
        }

        [TestMethod]
        [ScreenshotOnFail(false)]
        [TestCategory(Categories.CI)]
        public void BlogPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            var blogLink = App.ElementCreateService.CreateByLinkText<Anchor>("Blog");

            blogLink.Click();
        }
    }
}