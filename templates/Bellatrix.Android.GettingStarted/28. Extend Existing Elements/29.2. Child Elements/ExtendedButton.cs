namespace Bellatrix.Mobile.Android.GettingStarted;

// 1. The second way of extending an existing element is to create a child element.
// Inherit the element you want to extend. In this case, a new method is added to the standard Button element.
// Next in your tests, use the ExtendedButton instead of regular Button to have access to this method.
//
// 2. The same strategy can be used to create a completely new element that BELLATRIX does not provide.
// You need to extend the 'Element' as a base class.
public class ExtendedButton : Button
{
    public void SubmitButtonWithScroll()
    {
         this.ToExists().ToBeClickable().WaitToBe();
         ScrollToVisible(ScrollDirection.Down);
         Click();
    }
}