namespace Bellatrix.Web.Tests
{
    public partial class CartPage
    {
        public TextField CouponCode => ComponentCreateService.CreateById<TextField>("coupon_code");
        public Button ApplyCouponButton => ComponentCreateService.CreateByValueContaining<Button>("Apply coupon");
        public Div MessageAlert => ComponentCreateService.CreateByClassContaining<Div>("woocommerce-message");
        public ComponentsList<Number> QuantityBoxes => ComponentCreateService.CreateAllByClassContaining<Number>("input-text qty text");
        public Button UpdateCart => ComponentCreateService.CreateByValueContaining<Button>("Update cart").ToBeClickable();
        public Span TotalSpan => ComponentCreateService.CreateByXpath<Span>("//*[@class='order-total']//span");
        public Anchor ProceedToCheckout => ComponentCreateService.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
        public Div TotalCartTable => ComponentCreateService.CreateByClassContaining<Div>("cart_totals");
    }
}