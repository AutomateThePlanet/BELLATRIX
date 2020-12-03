// <copyright file="SearchControlTestsInternetExplorer.cs" company="Automate The Planet Ltd.">
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
    [Browser(BrowserType.InternetExplorer, BrowserBehavior.ReuseIfStarted)]
    [AllureSuite("Search Control")]
    public class SearchControlTestsInternetExplorer : WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().SearchLocalPage);

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void SearchSet_When_UseSetSearchMethod_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch");

            searchElement.SetSearch("bellatrix test framework");

            Assert.AreEqual("bellatrix test framework", searchElement.GetSearch());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetSearchReturnsCorrectSearch_When_DefaultSearchIsSet_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch3");

            Assert.AreEqual("search for stars", searchElement.GetSearch());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch");

            Assert.AreEqual(false, searchElement.IsAutoComplete);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch5");

            Assert.AreEqual(false, searchElement.IsAutoComplete);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch4");

            Assert.AreEqual(true, searchElement.IsAutoComplete);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch4");

            Assert.AreEqual(false, searchElement.IsReadonly);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch6");

            Assert.AreEqual(true, searchElement.IsReadonly);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch");

            var maxLength = searchElement.MaxLength;

            Assert.IsNull(maxLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch");

            Assert.IsNull(searchElement.MinLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetSizeReturnsDefault20_When_SizeAttributeIsNotPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch");

            // Specifies the width of an <input> element, in characters. Default value is 20
            Assert.AreEqual(20, searchElement.Size);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch2");

            Assert.AreEqual(80, searchElement.MaxLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch2");

            Assert.AreEqual(10, searchElement.MinLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetSizeReturns30_When_SizeAttributeIsNotPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch2");

            Assert.AreEqual(30, searchElement.Size);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch4");

            Assert.AreEqual(false, searchElement.IsRequired);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch7");

            Assert.AreEqual(true, searchElement.IsRequired);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetPlaceholder_When_PlaceholderAttributeIsSet_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch");

            Assert.AreEqual("your search term goes here", searchElement.Placeholder);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch1");

            Assert.IsNull(searchElement.Placeholder);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnRed_When_Hover_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch8");

            searchElement.Hover();

            Assert.AreEqual("color: red;", searchElement.GetStyle());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnBlue_When_Focus_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch9");

            searchElement.Focus();

            Assert.AreEqual("color: blue;", searchElement.GetStyle());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch9");

            bool isDisabled = searchElement.IsDisabled;

            Assert.IsFalse(isDisabled);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnTrue_When_DisabledAttributePresent_InternetExplorer()
        {
            var searchElement = App.ElementCreateService.CreateById<Search>("mySearch10");

            bool isDisabled = searchElement.IsDisabled;

            Assert.IsTrue(isDisabled);
        }
    }
}