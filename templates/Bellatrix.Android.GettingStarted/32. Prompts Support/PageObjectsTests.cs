using NUnit.Framework;
using static Bellatrix.AiValidator;
using static Bellatrix.AiAssert;

namespace Bellatrix.Mobile.Android.GettingStarted.LLM;

[TestFixture]
[Android(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.Controls1",
    Lifecycle.RestartEveryTime)]
public class PageObjectsTests : NUnit.AndroidTest
{
    [Test]
    public void ActionsWithoutPageObjects_LLM()
    {
        var button = App.Components.CreateByIdContaining<Button>("button_disabled");
        button.ValidateIsDisabled();
        //var checkBox = App.Components.CreateByIdContaining<CheckBox>("check1");
        var checkBox = App.Components.CreateByPrompt<CheckBox>("find the checkbox under the disabled button");
        checkBox.Check();
        checkBox.ValidateIsChecked();
        var comboBox = App.Components.CreateByIdContaining<ComboBox>("spinner1");
        comboBox.SelectByText("Jupiter");
        comboBox.ValidateTextIs("Jupiter");

        ValidateByPrompt("validate that the spinner combobox has text Jupiter");

        var label = App.Components.CreateByText<Label>("textColorPrimary");
        label.ValidateIsVisible();
        var radioButton = App.Components.CreateByIdContaining<RadioButton>("radio2");
        radioButton.Click();

        radioButton.ValidateIsChecked(timeout: 30, sleepInterval: 2);

        // fail on purpose to show how smart AI analysis works
        //radioButton.ValidateIsNotChecked(timeout: 30, sleepInterval: 2);
    }
}