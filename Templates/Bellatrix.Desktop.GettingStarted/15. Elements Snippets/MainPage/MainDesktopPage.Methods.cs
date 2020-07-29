using Bellatrix.Desktop.PageObjects;

namespace Bellatrix.Desktop.GettingStarted.Snippets
{
    public partial class MainDesktopPage : Page
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