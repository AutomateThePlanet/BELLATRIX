namespace Bellatrix.Web.GettingStarted._12._Page_Objects
{
    public partial class CheckoutPage
    {
        public Heading BillingDetailsHeading => ElementCreateService.CreateByInnerTextContaining<Heading>("Billing details");
        public Anchor ShowLogin => ElementCreateService.CreateByInnerTextContaining<Anchor>("Click here to login");
        public TextArea OrderCommentsTextArea => ElementCreateService.CreateById<TextArea>("order_comments");
        public TextField BillingFirstName => ElementCreateService.CreateById<TextField>("billing_first_name");
        public TextField BillingLastName => ElementCreateService.CreateById<TextField>("billing_last_name");
        public TextField BillingCompany => ElementCreateService.CreateById<TextField>("billing_company");
        public Select BillingCountry => ElementCreateService.CreateById<Select>("billing_country");
        public TextField BillingAddress1 => ElementCreateService.CreateById<TextField>("billing_address_1");
        public TextField BillingAddress2 => ElementCreateService.CreateById<TextField>("billing_address_2");
        public TextField BillingCity => ElementCreateService.CreateById<TextField>("billing_city");
        public Select BillingState => ElementCreateService.CreateById<Select>("billing_state").ToBeVisible().ToBeClickable();
        public TextField BillingZip => ElementCreateService.CreateById<TextField>("billing_postcode");
        public Phone BillingPhone => ElementCreateService.CreateById<Phone>("billing_phone");
        public Email BillingEmail => ElementCreateService.CreateById<Email>("billing_email");
        public CheckBox CreateAccountCheckBox => ElementCreateService.CreateById<CheckBox>("createaccount");
        public RadioButton CheckPaymentsRadioButton => ElementCreateService.CreateByAttributesContaining<RadioButton>("for", "payment_method_cheque");
    }
}