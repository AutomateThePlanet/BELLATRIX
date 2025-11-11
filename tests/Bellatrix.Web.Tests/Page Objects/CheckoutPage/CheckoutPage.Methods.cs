namespace Bellatrix.Web.Tests;

public partial class CheckoutPage : WebPage
{
    public override string Url => "https://demos.bellatrix.solutions/checkout/";

    public void FillBillingInfo(BillingInfo billingInfo)
    {
        OrderCommentsTextArea.SetText(billingInfo.OrderCommentsTextArea);
        BillingFirstName.SetText(billingInfo.FirstName);
        BillingLastName.SetText(billingInfo.LastName);
        BillingCompany.SetText(billingInfo.Company);
        BillingCountry.SelectByText(billingInfo.Country);
        BillingAddress1.SetText(billingInfo.Address1);
        BillingAddress2.SetText(billingInfo.Address2);
        BillingCity.SetText(billingInfo.City);
        BillingState.SelectByText(billingInfo.State);
        BillingZip.SetText(billingInfo.Zip);
        BillingPhone.SetPhone(billingInfo.Phone);
        BillingEmail.SetEmail(billingInfo.Email);
        if (billingInfo.ShouldCreateAccount)
        {
            CreateAccountCheckBox.Check();
        }
    }
}