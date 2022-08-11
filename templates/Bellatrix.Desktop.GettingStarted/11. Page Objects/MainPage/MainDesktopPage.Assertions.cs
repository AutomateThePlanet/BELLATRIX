namespace Bellatrix.Desktop.GettingStarted;

public partial class MainDesktopPage
{
    public void AssertPermanentTransferIsChecked()
    {
        App.Assert.IsTrue(PermanentTransfer.IsChecked);
    }

    public void AssertRightItemSelected(string itemName)
    {
        App.Assert.AreEqual(itemName, Items.InnerText);
    }

    public void AssertRightUserNameSet(string userName)
    {
        App.Assert.AreEqual(userName, UserName.InnerText);
    }

    public void AssertKeepMeLoggedChecked()
    {
        App.Assert.IsTrue(KeepMeLogged.IsChecked);
    }
}