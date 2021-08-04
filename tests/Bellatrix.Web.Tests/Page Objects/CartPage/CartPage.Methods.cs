using System;
using System.Linq;

namespace Bellatrix.Web.Tests
{
    public partial class CartPage : WebPage
    {
        private const string CouponSuccessfullyAdded = @"Coupon code applied successfully.";

        public override string Url => "http://demos.bellatrix.solutions/cart/";

        public void ApplyCoupon(string coupon)
        {
            int height = TotalCartTable.Size.Height;
            CouponCode.SetText(coupon);
            ApplyCouponButton.Click();
            ValidateControlExtensions.ValidatedHeightIsLargeThanMinHeight(TotalCartTable, height);
            MessageAlert.ToHasContent().ToBeVisible().WaitToBe();

            MessageAlert.ValidateInnerTextIs(CouponSuccessfullyAdded);
        }

        public void UpdateProductQuantity(int productNumber, int newQuantity)
        {
            if (productNumber > QuantityBoxes.Count())
            {
                throw new ArgumentException("There are less added items in the cart. Please specify smaller product number.");
            }

            BrowserService.WaitUntilReady();
            QuantityBoxes[productNumber - 1].SetNumber(newQuantity);
            UpdateCart.Click();
            BrowserService.WaitUntilReady();
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