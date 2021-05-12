using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted
{
    [TestFixture]
    public class TextVerificationPDF : NUnit.WebTest
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

        [Test]
        [Ignore("API example purposes only. No need to run.")]
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