using NUnit.Framework;
using BA = Bellatrix.Assertions;
using static Bellatrix.AiValidator;
using static Bellatrix.AiAssert;
using Bellatrix.LLM;
namespace Bellatrix.Web.GettingStarted.LLM;

[TestFixture]
public class PageObjectsTests : NUnit.WebTest
{
    [Test]
    public void PurchaseRocketWithPageObjects_LLM()
    {
        LocatorCacheService.RemoveAllLocators(); // Clear the cache to ensure fresh locator prompts
        var homePage = App.GoTo<HomePage>();
        homePage.FilterProducts(ProductFilter.Popularity);
        //homePage.AddProductById(28);

        //var product = App.Components.CreateByAttributesContaining<Anchor>("data-product_id", productId.ToString()).ToBeClickable();
        //product.Click();
        //ViewCartButton.ValidateIsVisible();
        var product = App.Components.CreateByPrompt<Anchor>("find Add to cart anchor under 'Falcon 9' item");
        product.Click();
        
        //AssertByPrompt("validate that view cart button is visible");
        ValidateByPrompt("validate that view cart button is visible");

        var product2 = App.Components.CreateByPrompt<Anchor>("find Add to cart anchor under 'Saturn V' item");
        product2.Click();

        homePage.ViewCartButton.Click();

        var cartPage = App.Create<CartPage>();
        cartPage.ApplyCoupon("happybirthday");

        
        var product2Quantity = App.Components.CreateByPrompt<Number>("find 'Saturn V' quantity number input", false);
        product2Quantity.SetNumber(2);
        cartPage.UpdateCart.Click();

        //AssertByPrompt("validate that the total is 294.67€ euro and the vat is 9.67€ euro and -5.00€ coupon applied, subtotal is 290.00€");
        ValidateByPrompt("validate that the total is 294.67€ euro and the vat is 9.67€ euro and -5.00€ coupon applied, subtotal is 290.00€");


        var deleteLink = App.Components.CreateByPrompt<Anchor>("find 'Saturn V' remove anchor");
        deleteLink.Click();

        var checkoutLink = App.Components.CreateByPrompt<Anchor>("find the checkout link in the main navigation");
        checkoutLink.Click();

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

        var checkPaymentsRadioButton = App.Components.CreateByPrompt<RadioButton>("find Check payments radio button under Your Order info");
        checkPaymentsRadioButton.Click();

        //AssertByPrompt("validate that the total is 54 euro and the vat is 9 euro and -5 coupon applied and exactly 1 item added");
        //AssertByPrompt("validate no additional items added");
        ValidateByPrompt("validate that the total is 54 euro and the vat is 9 euro and -5 coupon applied and exactly 1 item added");
        ValidateByPrompt("validate no additional items added");
    }
}