namespace Bellatrix.Web.Tests
{
    public partial class CartPage
    {
        public TextField CouponCode => ElementCreateService.CreateById<TextField>("coupon_code");
        public Button ApplyCouponButton => ElementCreateService.CreateByValueContaining<Button>("Apply coupon");
        public Div MessageAlert => ElementCreateService.CreateByClassContaining<Div>("woocommerce-message");
        public ComponentsList<Number> QuantityBoxes => ElementCreateService.CreateAllByClassContaining<Number>("input-text qty text");
        public Button UpdateCart => ElementCreateService.CreateByValueContaining<Button>("Update cart").ToBeClickable();
        public Span TotalSpan => ElementCreateService.CreateByXpath<Span>("//*[@class='order-total']//span");
        public Anchor ProceedToCheckout => ElementCreateService.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
    }
}