namespace Bellatrix.Web.Tests;

public partial class CartPage
{
    public TextField CouponCode => App.Components.CreateById<TextField>("coupon_code");
    public Button ApplyCouponButton => App.Components.CreateByValueContaining<Button>("Apply coupon");
    public Div MessageAlert => App.Components.CreateByClassContaining<Div>("woocommerce-message");
    public ComponentsList<Number> QuantityBoxes => App.Components.CreateAllByClassContaining<Number>("input-text qty text");
    public Button UpdateCart => App.Components.CreateByValueContaining<Button>("Update cart").ToBeClickable();
    public Span TotalSpan => App.Components.CreateByXpath<Span>("//*[@class='order-total']//span");
    public Anchor ProceedToCheckout => App.Components.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
}