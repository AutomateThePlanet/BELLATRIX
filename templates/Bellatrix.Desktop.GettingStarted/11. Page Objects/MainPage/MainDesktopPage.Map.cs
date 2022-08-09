namespace Bellatrix.Desktop.GettingStarted;

public partial class MainDesktopPage
{
    // 1. All elements are placed inside the file PageName.Map so that the declarations of your elements to be in a single place.
    // It is convenient since if there is a change in some of the locators or elements types you can apply the fix only here.
    // All elements are implements as properties. Here we use the short syntax for declaring properties, but you can always use the old one.
    // App.Components property is actually a shorter version of ComponentCreateService
    public Button Transfer => App.Components.CreateByName<Button>("E Button");
    public CheckBox PermanentTransfer => App.Components.CreateByName<CheckBox>("BellaCheckBox");
    public ComboBox Items => App.Components.CreateByAutomationId<ComboBox>("select");
    public Button ReturnItemAfter => App.Components.CreateByName<Button>("DisappearAfterButton1");
    public Label Results => App.Components.CreateByAutomationId<Label>("ResultLabelId");
    public Password Password => App.Components.CreateByAutomationId<Password>("passwordBox");
    public TextField UserName => App.Components.CreateByAutomationId<TextField>("textBox");
    public RadioButton KeepMeLogged => App.Components.CreateByName<RadioButton>("RadioButton");
}