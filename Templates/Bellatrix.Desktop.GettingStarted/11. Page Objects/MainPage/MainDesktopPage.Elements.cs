namespace Bellatrix.Desktop.GettingStarted
{
    public partial class MainDesktopPage
    {
        // 1. All elements are placed inside the file PageName.Elements so that the declarations of your elements to be in a single place.
        // It is convenient since if there is a change in some of the locators or elements types you can apply the fix only here.
        // All elements are implements as properties. Here we use the short syntax for declaring properties, but you can always use the old one.
        // Elements property is actually a shorter version of ElementCreateService
        public Button Transfer => Element.CreateByName<Button>("E Button");
        public CheckBox PermanentTransfer => Element.CreateByName<CheckBox>("BellaCheckBox");
        public ComboBox Items => Element.CreateByAutomationId<ComboBox>("select");
        public Button ReturnItemAfter => Element.CreateByName<Button>("DisappearAfterButton1");
        public Label Results => Element.CreateByAutomationId<Label>("ResultLabelId");
        public Password Password => Element.CreateByAutomationId<Password>("passwordBox");
        public TextField UserName => Element.CreateByAutomationId<TextField>("textBox");
        public RadioButton KeepMeLogged => Element.CreateByName<RadioButton>("RadioButton");
    }
}