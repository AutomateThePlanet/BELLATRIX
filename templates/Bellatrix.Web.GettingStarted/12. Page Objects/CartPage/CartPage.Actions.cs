using System;
using System.Linq;

namespace Bellatrix.Web.GettingStarted._12._Page_Objects;

// 1. All BELLATRIX page objects are implemented as partial classes which means that you have separate files for different parts of it- actions, elements, assertions
// but at the end, they are all built into a single type. This makes the maintainability and readability of these classes much better. Also, you can easier locate what you need.
//
// We advise you to follow the convention with partial classes, but you are always free to put all pieces in a single file.
public partial class CartPage : WebPage
{
    private const string CouponSuccessfullyAdded = @"Coupon code applied successfully.";

    // 2. Overriding the Url property that comes from the base page object you can later you the Open method to go to the page.
    public override string Url => "https://demos.bellatrix.solutions/cart/";

    // 3. These elements are always used together when coupon is applied. There are many test cases where you need to apply different coupons and so on.
    // This way you reuse the code instead of copy-paste it. If there is a change in the way how the coupon is applied, change the workflow only here.
    // Even single line of code is changed in your tests.
    public void ApplyCoupon(string coupon)
    {
        CouponCode.SetText(coupon);
        ApplyCouponButton.Click();
        MessageAlert.ToHasContent().ToBeVisible().WaitToBe();

        // Usually, it is not entirely correct to make assertions inside action methods. However, Validate methods are just waiting for something to happen.
        MessageAlert.ValidateInnerTextIs(CouponSuccessfullyAdded);
    }

    // 4. Another method that we can add here is the one for updating the quantity of a product.
    // This is an excellent place to put validations in your code. Here we make sure that the specified number of products that we want to update exists.
    public void UpdateProductQuantity(int productNumber, int newQuantity)
    {
        if (productNumber > QuantityBoxes.Count())
        {
            throw new ArgumentException("There are less added items in the cart. Please specify smaller product number.");
        }

        // 5. CreateAll method returns a special BELLATRIX collection called ElementsList<TComponentType> in this case ElementList<Number>
        // The collection has a couple of useful methods- Count, implements index which we use here.
        App.Browser.WaitForAjax();
        QuantityBoxes[productNumber - 1].SetNumber(newQuantity);
        UpdateCart.Click();
        App.Browser.WaitForAjax();
    }

    public void UpdateAllProductsQuantity(int newQuantity)
    {
        if (QuantityBoxes.Any())
        {
            throw new ArgumentException("There are no items to be updated.");
        }

        // 6. Also, you can use ComponentsList<T> directly in foreach statements since it implements IEnumerator interface.
        foreach (var currentQuantityBox in QuantityBoxes)
        {
            currentQuantityBox.SetNumber(0);
            currentQuantityBox.SetNumber(newQuantity);
        }

        UpdateCart.Click();
    }
}