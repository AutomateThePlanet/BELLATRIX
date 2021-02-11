using System;
using System.Linq;

namespace Bellatrix.Web.GettingStarted._17._Elements_Snippets
{
    public partial class CartPage : AssertedNavigatablePage
    {
        private const string CouponSuccessfullyAdded = @"Coupon code applied successfully.";

        public override string Url => "http://demos.bellatrix.solutions/cart/";

        public void ApplyCoupon(string coupon)
        {
            CouponCode.SetText(coupon);
            ApplyCouponButton.Click();
            MessageAlert.ToHasContent().ToBeVisible().WaitToBe();
            MessageAlert.ValidateInnerTextIs(CouponSuccessfullyAdded);
        }

        public void UpdateProductQuantity(int productNumber, int newQuantity)
        {
            if (productNumber >= QuantityBoxes.Count())
            {
                throw new ArgumentException("There are less added items in the cart. Please specify smaller product number.");
            }

            QuantityBoxes[productNumber].SetNumber(0);
            QuantityBoxes[productNumber].SetNumber(newQuantity);
            UpdateCart.Click();
        }

        public void UpdateAllProductsQuantity(int newQuantity)
        {
            if (QuantityBoxes.Any())
            {
                throw new ArgumentException("There are no items to be updated.");
            }

            foreach (var currentQuantityBox in QuantityBoxes)
            {
                currentQuantityBox.SetNumber(0);
                currentQuantityBox.SetNumber(newQuantity);
            }

            UpdateCart.Click();
        }
    }
}