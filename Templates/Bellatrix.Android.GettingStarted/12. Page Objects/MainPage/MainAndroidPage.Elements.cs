namespace Bellatrix.Mobile.Android.GettingStarted
{
    public partial class MainAndroidPage
    {
        // 1. All elements are placed inside the file PageName.Elements so that the declarations of your elements to be in a single place.
        // It is convenient since if there is a change in some of the locators or elements types you can apply the fix only here.
        // All elements are implements as properties. Here we use the short syntax for declaring properties, but you can always use the old one.
        // Elements property is actually a shorter version of ElementCreateService
        public Button Transfer => Element.CreateByIdContaining<Button>("button");
        public CheckBox PermanentTransfer => Element.CreateByIdContaining<CheckBox>("check1");
        public ComboBox Items => Element.CreateByIdContaining<ComboBox>("spinner1");
        public Button ReturnItemAfter => Element.CreateByIdContaining<Button>("toggle1");
        public Label Results => Element.CreateByText<Label>("textColorPrimary");
        public Password Password => Element.CreateByIdContaining<Password>("edit2");
        public TextField UserName => Element.CreateByIdContaining<TextField>("edit");
        public RadioButton KeepMeLogged => Element.CreateByIdContaining<RadioButton>("radio2");
    }
}