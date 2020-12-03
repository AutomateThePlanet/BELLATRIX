// <copyright file="PhoneControlTestsInternetExplorer.cs" company="Automate The Planet Ltd.">
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
    [AllureSuite("Phone Control")]
    public class PhoneControlTestsInternetExplorer : WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().PhoneLocalPage);

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void PhoneSet_When_UseSetPhoneMethod_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone");

            phoneElement.SetPhone("123-4567-8901");

            Assert.AreEqual("123-4567-8901", phoneElement.GetPhone());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetPhoneReturnsCorrectPhone_When_DefaultPhoneIsSet_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone3");

            Assert.AreEqual("123-4567-8901", phoneElement.GetPhone());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone");

            Assert.AreEqual(false, phoneElement.IsAutoComplete);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone5");

            Assert.AreEqual(false, phoneElement.IsAutoComplete);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone4");

            Assert.AreEqual(true, phoneElement.IsAutoComplete);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone4");

            Assert.AreEqual(false, phoneElement.IsReadonly);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone6");

            Assert.AreEqual(true, phoneElement.IsReadonly);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone");

            var maxLength = phoneElement.MaxLength;

            Assert.IsNull(maxLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone");

            Assert.IsNull(phoneElement.MinLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetSizeReturnsDefault20_When_SizeAttributeIsNotPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone");

            // Specifies the width of an <input> element, in characters. Default value is 20
            Assert.AreEqual(20, phoneElement.Size);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone2");

            Assert.AreEqual(80, phoneElement.MaxLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone2");

            Assert.AreEqual(10, phoneElement.MinLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetSizeReturns30_When_SizeAttributeIsNotPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone2");

            Assert.AreEqual(30, phoneElement.Size);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone4");

            Assert.AreEqual(false, phoneElement.IsRequired);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone7");

            Assert.AreEqual(true, phoneElement.IsRequired);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetPlaceholder_When_PlaceholderAttributeIsSet_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone");

            Assert.AreEqual("123-4567-8901", phoneElement.Placeholder);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone1");

            Assert.IsNull(phoneElement.Placeholder);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnRed_When_Hover_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone8");

            phoneElement.Hover();

            Assert.AreEqual("color: red;", phoneElement.GetStyle());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnBlue_When_Focus_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone9");

            phoneElement.Focus();

            Assert.AreEqual("color: blue;", phoneElement.GetStyle());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone9");

            bool isDisabled = phoneElement.IsDisabled;

            Assert.IsFalse(isDisabled);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnTrue_When_DisabledAttributePresent_InternetExplorer()
        {
            var phoneElement = App.ElementCreateService.CreateById<Phone>("myPhone10");

            bool isDisabled = phoneElement.IsDisabled;

            Assert.IsTrue(isDisabled);
        }
    }
}