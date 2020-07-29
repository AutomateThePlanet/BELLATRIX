namespace Bellatrix.SpecFlow.Desktop.MSTest.Tests
{
    public partial class MainDesktopPage
    {
        public void AssertPermanentTransferIsChecked()
        {
            Assert.IsTrue(PermanentTransfer.IsChecked);
        }

        public void AssertRightItemSelected(string itemName)
        {
            Assert.AreEqual(itemName, Items.InnerText);
        }

        public void AssertRightUserNameSet(string userName)
        {
            Assert.AreEqual(userName, UserName.InnerText);
        }

        public void AssertKeepMeLoggedChecked()
        {
            Assert.IsTrue(KeepMeLogged.IsChecked);
        }
    }
}