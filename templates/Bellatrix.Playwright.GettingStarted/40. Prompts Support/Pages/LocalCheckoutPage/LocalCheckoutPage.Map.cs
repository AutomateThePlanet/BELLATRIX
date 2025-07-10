using System;

namespace Bellatrix.Playwright.GettingStarted;
public partial class LocalCheckoutPage : WebPage
{
    public TextField FirstName => App.Components.CreateById<TextField>("firstName");
    public TextField LastName => App.Components.CreateById<TextField>("lastName");
    public TextField Username => App.Components.CreateById<TextField>("username");
    public TextField Email => App.Components.CreateById<TextField>("email");
    public TextField Address1 => App.Components.CreateById<TextField>("address1");
    public TextField Address2 => App.Components.CreateById<TextField>("address2");
    public Select Country => App.Components.CreateById<Select>("country");
    public Select State => App.Components.CreateById<Select>("state");
    public TextField Zip => App.Components.CreateById<TextField>("zip");
    public TextField CardName => App.Components.CreateById<TextField>("card-name");
    public TextField CardNumber => App.Components.CreateById<TextField>("card-number");
    public TextField CardExpiration => App.Components.CreateById<TextField>("card-expiration");
    public TextField CardCVV => App.Components.CreateById<TextField>("card-cvv");
    public RadioButton DebitCardButton => App.Components.CreateById<RadioButton>("debit");
    public Button CheckoutButton => App.Components.CreateByInnerTextContaining<Button>("Checkout");
}
