using Bellatrix.Mobile.PageObjects;

namespace Bellatrix.Mobile.IOS.GettingStarted;

// 1. All BELLATRIX page objects are implemented as partial classes which means that you have separate files for different parts of it- actions, elements, assertions
// but at the end, they are all built into a single type. This makes the maintainability and readability of these classes much better. Also, you can easier locate what you need.
//
// You can always create BELLATRIX page objects yourself inheriting the IOSPage class
// We advise you to follow the convention with partial classes, but you are always free to put all pieces in a single file.
public partial class CalculatorPage : IOSPage
{
    // 2. These elements are always used together when an item is transferred. There are many test cases where you need to transfer different items and so on.
    // This way you reuse the code instead of copy-paste it. If there is a change in the way how the item is transferred, change the workflow only here.
    // Even single line of code is changed in your tests.
    public void Sum(int firstNumber, int secondNumber)
    {
        NumberOne.SetText(firstNumber.ToString());
        NumberTwo.SetText(secondNumber.ToString());
        Compute.Click();
    }
}