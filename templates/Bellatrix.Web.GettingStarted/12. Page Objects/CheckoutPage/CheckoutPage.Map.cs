namespace Bellatrix.Web.GettingStarted._12._Page_Objects;

public partial class CheckoutPage
{
    public Heading BillingDetailsHeading => App.Components.CreateByInnerTextContaining<Heading>("Billing details");
    public Anchor ShowLogin => App.Components.CreateByInnerTextContaining<Anchor>("Click here to login");
    public TextArea OrderCommentsTextArea => App.Components.CreateById<TextArea>("order_comments");
    public TextField BillingFirstName => App.Components.CreateById<TextField>("billing_first_name");
    public TextField BillingLastName => App.Components.CreateById<TextField>("billing_last_name");
    public TextField BillingCompany => App.Components.CreateById<TextField>("billing_company");
    public Select BillingCountry => App.Components.CreateById<Select>("billing_country");
    public TextField BillingAddress1 => App.Components.CreateById<TextField>("billing_address_1");
    public TextField BillingAddress2 => App.Components.CreateById<TextField>("billing_address_2");
    public TextField BillingCity => App.Components.CreateById<TextField>("billing_city");
    public Select BillingState => App.Components.CreateById<Select>("billing_state").ToBeVisible().ToBeClickable();
    public TextField BillingZip => App.Components.CreateById<TextField>("billing_postcode");
    public Phone BillingPhone => App.Components.CreateById<Phone>("billing_phone");
    public Email BillingEmail => App.Components.CreateById<Email>("billing_email");
    public CheckBox CreateAccountCheckBox => App.Components.CreateById<CheckBox>("createaccount");
    public RadioButton CheckPaymentsRadioButton => App.Components.CreateByAttributesContaining<RadioButton>("for", "payment_method_cheque");
}