using Bellatrix.Desktop;

namespace Bellatrix.SpecFlow.Desktop.MSTest.Tests
{
    public partial class MainDesktopPage
    {
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