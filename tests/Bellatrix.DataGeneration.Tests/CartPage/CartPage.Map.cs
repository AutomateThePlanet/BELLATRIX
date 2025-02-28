namespace Bellatrix.Web.GettingStarted._12._Page_Objects;

public partial class CartPage
{
    // 1. All components are placed inside the file PageName.Map so that the declarations of your elements to be in a single place.
    // It is convenient since if there is a change in some of the locators or component types you can apply the fix only here.
    // All components are implements as properties. Here we use the short syntax for declaring properties, but you can always use the old one.
    // App.Components property is actually a shorter version of ComponentCreateService
    public TextField CouponCode => App.Components.CreateById<TextField>("coupon_code");
    public Button ApplyCouponButton => App.Components.CreateByValueContaining<Button>("Apply coupon");
    public Div MessageAlert => App.Components.CreateByClassContaining<Div>("woocommerce-message");

    // 2. If you want to find multiple elements, you can use the special BELLATRIX collection ComponentsList<TComponentType>.
    // You can read more about it in the actions file.
    public ComponentsList<Number> QuantityBoxes => App.Components.CreateAllByClassContaining<Number>("input-text qty text");
    public Button UpdateCart => App.Components.CreateByValueContaining<Button>("Update cart").ToBeClickable();
    public Span TotalSpan => App.Components.CreateByXpath<Span>("//*[@class='order-total']//span");
    public Anchor ProceedToCheckout => App.Components.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
}