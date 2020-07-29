using Bellatrix.Desktop;
using Bellatrix.Desktop.PageObjects;

namespace Bellatrix.SpecFlow.Desktop.MSTest.Tests
{
    public partial class MainDesktopPage : AssertedPage
    {
        public void TransferItem(string itemToBeTransfered, string userName, string password)
        {
            PermanentTransfer.Check();
            Items.SelectByText(itemToBeTransfered);
            ReturnItemAfter.ToNotExists().WaitToBe();
            UserName.SetText(userName);
            Password.SetPassword(password);
            KeepMeLogged.Click();
            Transfer.Click();
        }
    }
}