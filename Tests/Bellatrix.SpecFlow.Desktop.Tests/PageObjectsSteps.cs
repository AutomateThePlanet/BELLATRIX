// <copyright file="PageObjectsSteps.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Desktop.Tests
{
    [Binding]
    public class PageObjectsSteps : DesktopSteps
    {
        private MainDesktopPage _mainPage;

        [When(@"I transfer item (.*) user name (.*) password (.*)")]
        public void WhenITrasnferItem(string itemName, string userName, string password)
        {
            _mainPage = App.Create<MainDesktopPage>();
            _mainPage.TransferItem(itemName, userName, password);
        }

        [Then(@"I assert that keep me logged is checked")]
        public void AssertKeepMeLoggedChecked()
        {
            _mainPage.AssertKeepMeLoggedChecked();
        }

        [Then(@"I assert that permanent trasnfer is checked")]
        public void AssertPermanentTransferIsChecked()
        {
            _mainPage.AssertPermanentTransferIsChecked();
        }

        [Then(@"I assert that (.*) right item is selected")]
        public void AssertRightItemSelected(string itemName)
        {
            _mainPage.AssertRightItemSelected(itemName);
        }

        [Then(@"I assert that (.*) user name is set")]
        public void AssertRightUserNameSet(string userName)
        {
            _mainPage.AssertRightUserNameSet(userName);
        }
    }
}
