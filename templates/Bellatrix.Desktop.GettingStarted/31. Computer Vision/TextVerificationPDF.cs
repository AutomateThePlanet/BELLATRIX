using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestFixture]
    public class TextVerificationPDF : MSTest.DesktopTest
    {
        [Test]
        [Ignore("API example purposes only. No need to run.")]
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