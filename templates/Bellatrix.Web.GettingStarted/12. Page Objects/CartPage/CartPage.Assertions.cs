namespace Bellatrix.Web.GettingStarted._12._Page_Objects;

public partial class CartPage
{
    public void AssertTotalPrice(string price)
    {
        // 1. With this Assert, reuse the formatting of the currency and the timeout.
        // Also, since the method is called from the page it makes your tests a little bit more readable.
        // If there is a change what needs to be checked --> for example, not span but different element you can change it in a single place.
        TotalSpan.ValidateInnerTextIs($"{price}€", 15000);
    }
}