﻿namespace Bellatrix.Playwright.GettingStarted;

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

    [Test]
    [Ignore("API example purposes only. No need to run.")]
    public void AssertPdfLayoutBasedOnTemplate()
    {
        App.FormRecognizer.SaveAnalyzedFileToJson("sampleinvoice.pdf", "analizedFileOutput.json");

        App.FormRecognizer.ValidateFormLayout("sampleinvoice.pdf", "sampleinvoice1.pdf");
    }

    [Test]
    [Ignore("API example purposes only. No need to run.")]
    public void AssertCellsText()
    {
        var analyzedPdf = App.FormRecognizer.Analyze("sampleinvoice.pdf");
        analyzedPdf.AssertLinesCount(48);

        analyzedPdf.GetTable().GetCell(0, 3).AssertTextEquals("OF355548 24/1/2014");
        analyzedPdf.GetTable().GetCell(5, 1).AssertTextContains("45 Acker Driv");

        analyzedPdf.GetTable().GetCell(c => c.Text.StartsWith("69 Trailsway")).AssertTextContains("0903");

        analyzedPdf.Lines[46].AssertWordsCount(9);

        analyzedPdf.Lines[40].AssertWordsEqual("TERMS", "AND", "CONDITIONS");

        analyzedPdf.Lines[40].AssertWordsContain("TERMS", "CONDITIONS");
    }

    [Test]
    ////[Ignore("API example purposes only. No need to run.")]
    public void AssertTableComponentCellsText()
    {
        App.Navigation.NavigateToLocalPage("TestPages\\Table\\table.html");

        var table = App.Components.CreateById<Table>("table1")
                                     .SetColumn("Last Name")
                                     .SetColumn("First Name")
                                     .SetColumn("Email")
                                     .SetColumn("Due")
                                     .SetColumn("Action");

        var analyzedTable = table.AIAnalyze();
        analyzedTable.AssertLinesCount(24);

        ////analyzedTable.GetTable().GetCell(1, 3).AssertTextEquals("jsmith@gmail.com");
        ////analyzedTable.GetTable().GetCell(4, 3).AssertTextContains("$50");

        ////analyzedTable.GetTable().GetCell(c => c.Text.StartsWith("jdoe@")).AssertTextContains("hotmail.com");

        analyzedTable.Lines[13].AssertWordsCount(2);

        analyzedTable.Lines[23].AssertWordsContain("tconway", "delete", "edit");
    }
}