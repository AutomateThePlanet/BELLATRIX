using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted.LLM;

[TestFixture]
public class PageObjectsTests : NUnit.WebTest
{
    [Test]
    public void PurchaseRocketWithPageObjects_LLM()
    {
        var homePage = App.GoTo<HomePage>();
        homePage.FilterProducts(ProductFilter.Popularity);
        homePage.AddProductById(28);
        homePage.ViewCartButton.Click();

        var cartPage = App.Create<CartPage>();
        cartPage.ApplyCoupon("happybirthday");
        ////cartPage.UpdateProductQuantity(1, 2);
        ////cartPage.AssertTotalPrice("114.00");
        cartPage.ProceedToCheckout.Click();

        var billingInfo = new BillingInfo
                                  {
                                      FirstName = "In",
                                      LastName = "Deepthought",
                                      Company = "Automate The Planet Ltd.",
                                      Country = "Bulgaria",
                                      Address1 = "bul. Yerusalim 5",
                                      Address2 = "bul. Yerusalim 6",
                                      City = "Sofia",
                                      State = "Sofia-Grad",
                                      Zip = "1000",
                                      Phone = "+00359894646464",
                                      Email = "info@bellatrix.solutions",
                                      ShouldCreateAccount = true,
                                      OrderCommentsTextArea = "cool product",
                                  };

        var checkoutPage = App.Create<CheckoutPage>();
        checkoutPage.FillBillingInfo(billingInfo);
        checkoutPage.CheckPaymentsRadioButton.Click();
    }
}