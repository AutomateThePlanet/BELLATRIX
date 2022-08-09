namespace Bellatrix.Mobile.Android.GettingStarted;

public partial class MainAndroidPage
{
    public void AssertPermanentTransferIsChecked()
    {
        App.Assert.IsTrue(PermanentTransfer.IsChecked);
    }

    public void AssertRightItemSelected(string itemName)
    {
        App.Assert.AreEqual(itemName, Items.GetText());
    }

    public void AssertRightUserNameSet(string userName)
    {
        App.Assert.AreEqual(userName, UserName.GetText());
    }

    public void AssertKeepMeLoggedChecked()
    {
        App.Assert.IsTrue(KeepMeLogged.IsChecked);
    }
}