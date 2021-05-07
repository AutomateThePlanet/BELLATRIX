using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    public class TextVerificationPDF : MSTest.WebTest
    {
        [TestMethod]
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

        [TestMethod]
        public void ExtractTextFromGraph()
        {
            var textSnippets = App.ComputerVision.ExtractOCRTextFromLocalFile("devPortalGraph1.PNG");
            textSnippets.ForEach(Console.WriteLine);

            List<string> expectedTextSnippets = new List<string>()
            {
                "Apr 12 01:00",
                "Apr 13 00:00",
            };

            App.ComputerVision.ValidateText("devPortalGraph1.PNG", "en", expectedTextSnippets);
        }
    }
}