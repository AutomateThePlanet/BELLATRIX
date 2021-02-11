namespace Bellatrix.Web.Tests
{
    public partial class CartPage
    {
        public TextField CouponCode => Element.CreateById<TextField>("coupon_code");
        public Button ApplyCouponButton => Element.CreateByValueContaining<Button>("Apply coupon");
        public Div MessageAlert => Element.CreateByClassContaining<Div>("woocommerce-message");
        public ElementsList<Number> QuantityBoxes => Element.CreateAllByClassContaining<Number>("input-text qty text");
        public Button UpdateCart => Element.CreateByValueContaining<Button>("Update cart").ToBeClickable();
        public Span TotalSpan => Element.CreateByXpath<Span>("//*[@class='order-total']//span");
        public Anchor ProceedToCheckout => Element.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
    }
}