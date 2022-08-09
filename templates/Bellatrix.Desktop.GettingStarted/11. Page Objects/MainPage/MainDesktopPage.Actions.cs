using Bellatrix.Desktop.PageObjects;

namespace Bellatrix.Desktop.GettingStarted;

// 1. All BELLATRIX page objects are implemented as partial classes which means that you have separate files for different parts of it- actions, elements, assertions
// but at the end, they are all built into a single type. This makes the maintainability and readability of these classes much better. Also, you can easier locate what you need.
//
// You can always create BELLATRIX page objects yourself inheriting DesktopPage class
// We advise you to follow the convention with partial classes, but you are always free to put all pieces in a single file.
public partial class MainDesktopPage : DesktopPage
{
    // 2. These elements are always used together when an item is transferred. There are many test cases where you need to transfer different items and so on.
    // This way you reuse the code instead of copy-paste it. If there is a change in the way how the item is transferred, change the workflow only here.
    // Even single line of code is changed in your tests.
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