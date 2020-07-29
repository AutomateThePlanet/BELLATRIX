using Bellatrix.ExceptionAnalysation;
using Bellatrix.Web.GettingStarted._16._Troubleshooting__Exception_Analysation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, BrowserBehavior.ReuseIfStarted)]
    [Browser(OS.OSX, BrowserType.Chrome, BrowserBehavior.ReuseIfStarted)]
    public class FailedTestsAnalysisTests : WebTest
    {
        // 1. Exception analysis or failed tests analysis is a BELLATRIX feature that can provide you meaningful information why your tests failed,
        // instead of viewing some native ugly exception messages like not found elements and so on.
        // Here is how the system works. Always when some of your tests fail BELLATRIX goes through so-called global ExceptionHandlers
        // if some of their rules match a beatified message is displayed. BELLATRIX comes with few global handlers such as file not found,
        // generic .NET exception page, service unavailable. Also, we have created for your convenience a few base classes that you can derive from
        // to create your global exception handlers. For example below we have created OppsExceptionHandler.
        // It derives from the base class CustomHtmlExceptionHandler. If you navigate to the class, you see that we have specified the beatified message,
        // and more importantly what text should BELLATRIX search on the failed web page. If the text is located, your message is displayed.
        // Once the page is created, we use the AddExceptionHandler to register the global exception handler.
        // Usually we call this method once per test run so you can do it in the AssemblyInitialize method located in the TestInialize.cs
        public override void TestsArrange()
        {
            App.AddExceptionHandler<OppsExceptionHandler>();

            // 2. Another class you can derive from is OppsUrlExceptionHandler instead of searching in the HTML source of the failed web page,
            // this one checks parts of the URL which you specify in an override property.
            App.AddExceptionHandler<OppsUrlExceptionHandler>();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void NavigateToNonExistingWebPage()
        {
            Assert.ThrowsException<AnalyzedTestException>(() =>
                {
                    App.NavigationService.Navigate("http://demos.bellatrix.solutions/welco1");

                    // If we execute the test, without the added custom exception handlers, the button click fails
                    // since the button is not be located.
                    var couponButton = App.ElementCreateService.CreateById<Button>("couponBtn");
                    couponButton.Click();
                });

            // 3. If the custom handlers above are present, this is how the exception is printed:
            //
            // Message: Test method Bellatrix.Web.GettingStarted.ExceptionAnalysationTests.NavigateToNonExistingWebPage threw exception:
            // Bellatrix.ExceptionAnalysation.AnalyzedTestException:
            //
            // ########################################
            //
            //             The test navigated to a page that was not present. Maybe someone deleted it?
            //
            // ########################################
            //     ---> OpenQA.Selenium.WebDriverTimeoutException: Timed out after 30 seconds

            // 4. When the exception handlers are not present the exception is:
            //
            // Message: Test method Bellatrix.Web.GettingStarted.ExceptionAnalysationTests.NavigateToNonExistingWebPage threw exception:
            // System.TimeoutException:
            //
            // The element with Name = control (ID = couponBtn) Locator couponBtn was not found on the page or didn't fulfill the specified conditions.

            // As you can see the first message is more meaningful, and you directly know what is the exact problem even without rerunning the failed test.

            // 5. You can create your custom exception handlers without deriving from our base classes. Implement the IExceptionAnalysationHandler interface.
        }
    }
}