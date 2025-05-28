using NUnit.Framework;
using static Bellatrix.AiValidator;
using static Bellatrix.AiAssert;

namespace Bellatrix.Mobile.IOS.GettingStarted.LLM;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class PageObjectsTests : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    public void ActionsWithoutPageObjectsFirst()
    {
        var numberOne = App.Components.CreateById<TextField>("IntegerA");
        var numberTwo = App.Components.CreateById<TextField>("IntegerB");

        var compute = App.Components.CreateByPrompt<Button>("find the compute sum button");
        var answer = App.Components.CreateByName<Label>("Answer");

        numberOne.SetText("5");
        numberTwo.SetText("6");
        compute.Click();

        ValidateByPrompt("validate that the result is 11");
        //Assert.That("11".Equals(answer.GetText()));

        // fail on purpose to show how smart AI analysis works
        //Assert.That("12".Equals(answer.GetText()));
    }
}