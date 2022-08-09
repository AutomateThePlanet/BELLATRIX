namespace Bellatrix.Mobile.IOS.GettingStarted;

public partial class CalculatorPage
{
    // 1. All elements are placed inside the file PageName.Map so that the declarations of your elements to be in a single place.
    // It is convenient since if there is a change in some of the locators or elements types you can apply the fix only here.
    // All elements are implements as properties. Here we use the short syntax for declaring properties, but you can always use the old one.
    // App.Components property is actually a shorter version of ComponentCreateService
    public Button Compute => App.Components.CreateByName<Button>("ComputeSumButton");
    public TextField NumberOne => App.Components.CreateById<TextField>("IntegerA");
    public TextField NumberTwo => App.Components.CreateById<TextField>("IntegerB");
    public Label Answer => App.Components.CreateByName<Label>("Answer");
}