using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;
using Bellatrix.Assertions;

namespace Bellatrix.CognitiveServices.services
{
    public class AssertedFormPage
    {
        public static event EventHandler<string> FormPageAsserted;
        private readonly FormPage _formPage;

        public AssertedFormPage(FormPageCollection formPages, int pageNumber)
        {
            _formPage = formPages[pageNumber];
            Lines = _formPage.Lines.Select(x => new AssertedTableFormLine(x)).ToList();
        }

        public List<AssertedTableFormLine> Lines { get; set; }

        public AssertedFormTable GetTable(int index = 0)
        {
            return new AssertedFormTable(_formPage.Tables[index]);
        }

        public void AssertLinesCount(int expectedLinesCount)
        {
            FormPageAsserted?.Invoke(this, $"Assert page lines' count = {expectedLinesCount}");
            Assert.AreEqual(expectedLinesCount, _formPage.Lines.Count, $"Page lines' count != {expectedLinesCount}");
        }
    }
}
