using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    public class TextVerificationPDF : MSTest.DesktopTest
    {
        [TestMethod]
        [Ignore]
        public void MakeTextExtractionFromPDF()
        {
            var textSnippets = App.ComputerVision.ExtractOCRTextFromLocalFile("sampleinvoice.pdf");
            textSnippets.ForEach(Console.WriteLine);

            List<string> expectedTextSnippets = new List<string>()
            {
                "69653 1st Point, 45 Acker Driv",
                "Subtotal",
                "$84.00",
                "Total",
                "$136.00",
            };

            App.ComputerVision.ValidateText("sampleinvoice.pdf", "en", expectedTextSnippets);
        }
    }
}