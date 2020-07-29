using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Desktop.MSTest.Tests
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
