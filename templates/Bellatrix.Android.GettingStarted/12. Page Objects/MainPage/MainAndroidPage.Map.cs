namespace Bellatrix.Mobile.Android.GettingStarted;

public partial class MainAndroidPage
{
    // 1. All elements are placed inside the file PageName.Map so that the declarations of your elements to be in a single place.
    // It is convenient since if there is a change in some of the locators or elements types you can apply the fix only here.
    // All elements are implements as properties. Here we use the short syntax for declaring properties, but you can always use the old one.
    // App.Components property is actually a shorter version of ComponentCreateService
    public Button Transfer => App.Components.CreateByIdContaining<Button>("button");
    public CheckBox PermanentTransfer => App.Components.CreateByIdContaining<CheckBox>("check1");
    public ComboBox Items => App.Components.CreateByIdContaining<ComboBox>("spinner1");
    public Button ReturnItemAfter => App.Components.CreateByIdContaining<Button>("toggle1");
    public Label Results => App.Components.CreateByText<Label>("textColorPrimary");
    public Password Password => App.Components.CreateByIdContaining<Password>("edit2");
    public TextField UserName => App.Components.CreateByIdContaining<TextField>("edit");
    public RadioButton KeepMeLogged => App.Components.CreateByIdContaining<RadioButton>("radio2");
}