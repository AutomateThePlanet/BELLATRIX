namespace Bellatrix.Web.GettingStarted._12._Page_Objects
{
    public partial class CartPage
    {
        // 1. All elements are placed inside the file PageName.Elements so that the declarations of your elements to be in a single place.
        // It is convenient since if there is a change in some of the locators or elements types you can apply the fix only here.
        // All elements are implements as properties. Here we use the short syntax for declaring properties, but you can always use the old one.
        // Elements property is actually a shorter version of ElementCreateService
        public TextField CouponCode => Element.CreateById<TextField>("coupon_code");
        public Button ApplyCouponButton => Element.CreateByValueContaining<Button>("Apply coupon");
        public Div MessageAlert => Element.CreateByClassContaining<Div>("woocommerce-message");

        // 2. If you want to find multiple elements, you can use the special BELLATRIX collection ElementsList<TElementType>.
        // You can read more about it in the actions file.
        public ElementsList<Number> QuantityBoxes => Element.CreateAllByClassContaining<Number>("input-text qty text");
        public Button UpdateCart => Element.CreateByValueContaining<Button>("Update cart").ToBeClickable();
        public Span TotalSpan => Element.CreateByXpath<Span>("//*[@class='order-total']//span");
        public Anchor ProceedToCheckout => Element.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
    }
}