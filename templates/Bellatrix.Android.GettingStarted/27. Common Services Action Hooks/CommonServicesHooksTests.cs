using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.ReuseIfStarted)]
    public class CommonServicesHooksTests : MSTest.AndroidTest
    {
        // 1. Another way to extend BELLATRIX is to use the common services hooks. This is how the failed tests analysis works.
        // The base class for all Android elements- Element provides a few special events as well:
        // ScrollingToVisible - called before scrolling
        // ScrolledToVisible - called after scrolling
        // CreatingElement - called before creating the element
        // CreatedElement - called after the creation of the element
        // CreatingElements - called before the creation of nested element
        // CreatedElements - called after the creation of nested element
        // ReturningWrappedElement - called before searching for native WebDriver element
        //
        // To add custom logic to the element's methods you can create a class that derives from ElementEventHandlers. The override the methods you like.
        [TestMethod]
        [Ignore]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ComponentCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}