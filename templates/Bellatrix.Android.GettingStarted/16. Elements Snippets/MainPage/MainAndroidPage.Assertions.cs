namespace Bellatrix.Mobile.Android.GettingStarted.Snippets
{
    public partial class MainAndroidPage
    {
        public void AssertPermanentTransferIsChecked()
        {
            Assert.IsTrue(PermanentTransfer.IsChecked);
        }

        public void AssertRightItemSelected(string itemName)
        {
            Assert.AreEqual(itemName, Items.GetText());
        }

        public void AssertRightUserNameSet(string userName)
        {
            Assert.AreEqual(userName, UserName.GetText());
        }

        public void AssertKeepMeLoggedChecked()
        {
            Assert.IsTrue(KeepMeLogged.IsChecked);
        }
    }
}