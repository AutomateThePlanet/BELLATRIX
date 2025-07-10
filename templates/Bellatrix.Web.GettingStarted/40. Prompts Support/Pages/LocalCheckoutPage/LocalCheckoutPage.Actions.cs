namespace Bellatrix.Web.GettingStarted;

public partial class LocalCheckoutPage : WebPage
{
    public override string Url => "TestPages\\Checkout\\index.html";

    public void FillBillingInfo(ClientInfo info)
    {
        FirstName.SetText(info.FirstName);
        LastName.SetText(info.LastName);
        Username.SetText(info.Username);
        Email.SetText(info.Email);
        Address1.SetText(info.Address1);
        Address2.SetText(info.Address2);
        Country.SelectByIndex(info.CountryIndex);
        State.SelectByIndex(info.StateIndex);
        Zip.SetText(info.Zip);
        CardName.SetText(info.CardName);
        CardNumber.SetText(info.CardNumber);
        CardExpiration.SetText(info.CardExpiration);
        CardCVV.SetText(info.CardCVV);
        DebitCardButton.Click();
    }

    public void SubmitOrder() => CheckoutButton.Click();
}