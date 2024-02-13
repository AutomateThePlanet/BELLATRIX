using Bellatrix.Web.Tests.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageRecognition = Bellatrix.ImageRecognition;

namespace Bellatrix.Web.Demos;

[TestClass]
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
public class ImageRecognitionTests : MSTest.WebTest
{
    [TestMethod]
    public void AssertPrintPreview()
    {
        var gridTestPage = App.Create<GridTestPage>();
        App.Navigation.NavigateToLocalPage(gridTestPage.Url);
        using var screen = new ImageRecognition.Screen();
        var firstOrderCell = gridTestPage.Grid.GetRow(0).GetCell(0).As<TextField>();
        firstOrderCell.SetText("BELLATRIX IS beautiful");
        // <image url="$(ProjectDir)Image Recognition\Images\chrome-dots-button.png" />
        screen.Click("chrome-dots-button");

        // <image url="$(ProjectDir)Image Recognition\Images\chrome-print-button.png" />
        screen.Click("chrome-print-button");

        // <image url="$(ProjectDir)Image Recognition\Images\chrome-print-preview-grid.png" scale="1.0" />

        screen.ValidateIsVisible("chrome-print-preview-grid", similarity: 0.5, timeoutInSeconds: 30);

        // <image url="$(ProjectDir)Image Recognition\Images\print-preview-cancel-button.png" scale="1.0" />

        screen.Click("print-preview-cancel-button");

        // <image url="$(ProjectDir)Image Recognition\Images\chrome-print-preview-grid.png" scale="1.0" />

        screen.ValidateIsNotVisible("chrome-print-preview-grid");
    }
}