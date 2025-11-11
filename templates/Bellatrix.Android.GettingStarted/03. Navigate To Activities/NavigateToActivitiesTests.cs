using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

// Depending on the types of tests you want to write there are a couple of ways to navigate to а specific activity.
// If you use the Android attribute the first time the app is started it navigates to the specified activity.
//
// Please notice that we don't use the Android attribute. A default device/selenium grid can be specified in the testFrameworkSettings.json file
// under the executionSettings section. There you can specify default lifecycle, version, grid URL, and arguments.
// You can still use the Android attribute on top of classes or tests to override the default behavior.
[TestFixture]
public class NavigateToActivitiesTests : NUnit.AndroidTest
{
    // In later chapters, there are more details about the different test workflow hooks. Find here two of them.
    //
    // 1. If you reuse your app and want to navigate once to a specific activity. You can use the TestsAct method.
    // It executes once for all tests in the class.
    public override void TestsAct() => App.AppService.StartActivity(Constants.AndroidNativeAppId, ".view.Controls1");

    // 2. If you need each test to navigate each time to the same activity, you can use the TestInit method.
    public override void TestInit() => App.AppService.StartActivity(Constants.AndroidNativeAppId, ".view.Controls1");

    [Test]
    [Category(Categories.CI)]
    public void PromotionsPageOpened_When_PromotionsButtonClicked()
    {
        // 3. You can always navigate in each separate tests, but if all of them open the same activity, you can use the above techniques for code reuse.
        App.AppService.StartActivity(Constants.AndroidNativeAppId, ".view.Controls1");

        // Use the element creation service to create an instance of the button. There are much more details about this process in the next sections.
        var button = App.Components.CreateByIdContaining<Button>("button");

        button.Click();
    }
}