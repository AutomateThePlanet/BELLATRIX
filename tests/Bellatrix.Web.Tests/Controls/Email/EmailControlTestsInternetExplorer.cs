// <copyright file="EmailControlTestsInternetExplorer.cs" company="Automate The Planet Ltd.">
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
    [Browser(BrowserType.InternetExplorer, Lifecycle.ReuseIfStarted)]
    [AllureSuite("Email Control")]
    public class EmailControlTestsInternetExplorer : MSTest.WebTest
    {
        public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().EmailLocalPage);

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void EmailSet_When_UseSetEmailMethod_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail");

            emailElement.SetEmail("aangelov@bellatrix.solutions");

            Assert.AreEqual("aangelov@bellatrix.solutions", emailElement.GetEmail());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetEmailReturnsCorrectEmail_When_DefaultEmailIsSet_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail3");

            Assert.AreEqual("aangelov@bellatrix.solutions", emailElement.GetEmail());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail");

            Assert.AreEqual(false, emailElement.IsAutoComplete);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail5");

            Assert.AreEqual(false, emailElement.IsAutoComplete);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail4");

            Assert.AreEqual(true, emailElement.IsAutoComplete);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail4");

            Assert.AreEqual(false, emailElement.IsReadonly);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail6");

            Assert.AreEqual(true, emailElement.IsReadonly);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail");

            var maxLength = emailElement.MaxLength;

            Assert.IsNull(maxLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail");

            Assert.IsNull(emailElement.MinLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetSizeReturnsDefault20_When_SizeAttributeIsNotPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail");

            // Specifies the width of an <input> element, in characters. Default value is 20
            Assert.AreEqual(20, emailElement.Size);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail2");

            Assert.AreEqual(80, emailElement.MaxLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail2");

            Assert.AreEqual(10, emailElement.MinLength);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetSizeReturns30_When_SizeAttributeIsNotPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail2");

            Assert.AreEqual(30, emailElement.Size);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail4");

            Assert.AreEqual(false, emailElement.IsRequired);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail7");

            Assert.AreEqual(true, emailElement.IsRequired);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetPlaceholder_When_PlaceholderAttributeIsSet_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail");

            Assert.AreEqual("your email term goes here", emailElement.Placeholder);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail1");

            Assert.IsNull(emailElement.Placeholder);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnRed_When_Hover_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail8");

            emailElement.Hover();

            Assert.AreEqual("color: red;", emailElement.GetStyle());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnBlue_When_Focus_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail9");

            emailElement.Focus();

            Assert.AreEqual("color: blue;", emailElement.GetStyle());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail9");

            bool isDisabled = emailElement.IsDisabled;

            Assert.IsFalse(isDisabled);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnTrue_When_DisabledAttributePresent_InternetExplorer()
        {
            var emailElement = App.Components.CreateById<Email>("myEmail10");

            bool isDisabled = emailElement.IsDisabled;

            Assert.IsTrue(isDisabled);
        }
    }
}