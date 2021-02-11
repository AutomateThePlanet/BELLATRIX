using Bellatrix.Web.Tests.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageRecognition = Bellatrix.ImageRecognition;

namespace Bellatrix.Web.Demos
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    public class ImageRecognitionTests : MSTest.WebTest
    {
        [TestMethod]
        public void AssertPrintPreview()
        {
            var gridTestPage = App.Create<GridTestPage>();
            App.NavigationService.NavigateToLocalPage(gridTestPage.Url);
            using (var screen = new ImageRecognition.Screen())
            {
                var firstOrderCell = gridTestPage.Grid.GetRow(0).GetCell(0).As<TextField>();
                firstOrderCell.SetText("BELLATRIX IS beautiful");

                screen.Click("chrome-dots-button");
                screen.Click("chrome-print-button");

                screen.ValidateIsVisible("chrome-print-preview-grid", similarity: 0.7, timeoutInSeconds: 30);

                screen.Click("print-preview-cancel-button");

                screen.ValidateIsNotVisible("chrome-print-preview-grid");
            }
        }
    }
}
