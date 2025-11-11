namespace Bellatrix.Playwright.Tests;

public partial class CartPage
{
    public void AssertTotalPrice(string price)
    {
        TotalSpan.ValidateInnerTextIs($"{price}€", 15000);
    }
}