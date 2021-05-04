namespace Bellatrix.Web.GettingStarted._12._Page_Objects
{
    public partial class CheckoutPage
    {
        public Heading BillingDetailsHeading => ComponentCreateService.CreateByInnerTextContaining<Heading>("Billing details");
        public Anchor ShowLogin => ComponentCreateService.CreateByInnerTextContaining<Anchor>("Click here to login");
        public TextArea OrderCommentsTextArea => ComponentCreateService.CreateById<TextArea>("order_comments");
        public TextField BillingFirstName => ComponentCreateService.CreateById<TextField>("billing_first_name");
        public TextField BillingLastName => ComponentCreateService.CreateById<TextField>("billing_last_name");
        public TextField BillingCompany => ComponentCreateService.CreateById<TextField>("billing_company");
        public Select BillingCountry => ComponentCreateService.CreateById<Select>("billing_country");
        public TextField BillingAddress1 => ComponentCreateService.CreateById<TextField>("billing_address_1");
        public TextField BillingAddress2 => ComponentCreateService.CreateById<TextField>("billing_address_2");
        public TextField BillingCity => ComponentCreateService.CreateById<TextField>("billing_city");
        public Select BillingState => ComponentCreateService.CreateById<Select>("billing_state").ToBeVisible().ToBeClickable();
        public TextField BillingZip => ComponentCreateService.CreateById<TextField>("billing_postcode");
        public Phone BillingPhone => ComponentCreateService.CreateById<Phone>("billing_phone");
        public Email BillingEmail => ComponentCreateService.CreateById<Email>("billing_email");
        public CheckBox CreateAccountCheckBox => ComponentCreateService.CreateById<CheckBox>("createaccount");
        public RadioButton CheckPaymentsRadioButton => ComponentCreateService.CreateByAttributesContaining<RadioButton>("for", "payment_method_cheque");
    }
}