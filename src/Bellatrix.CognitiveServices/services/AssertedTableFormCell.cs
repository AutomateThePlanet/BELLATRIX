using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;
using Bellatrix.Assertions;

namespace Bellatrix.CognitiveServices.services
{
    public class AssertedTableFormCell
    {
        public static event EventHandler<string> TableFormCellAsserted;
        private readonly FormTableCell _formTableCell;

        public AssertedTableFormCell(FormTableCell formTableCell)
        {
            _formTableCell = formTableCell;
        }

        public int Row => _formTableCell.RowIndex;
        public int Column => _formTableCell.ColumnIndex;
        public string Text => _formTableCell.Text;
        public FieldBoundingBox BoundingBox => _formTableCell.BoundingBox;
        public FormTableCell WrappedFormTableCell => _formTableCell;

        public void AssertTextEquals(string expectedText)
        {
            TableFormCellAsserted?.Invoke(this, $"Assert Cell[{Row},{Column}] Text = {expectedText}");
            Assert.AreEqual(expectedText, Text, $"Cell[{Row},{Column}] Text != {expectedText}");
        }

        public void AssertTextContains(string expectedText)
        {
            TableFormCellAsserted?.Invoke(this, $"Assert Cell[{Row},{Column}] Text contains {expectedText}");
            Assert.IsTrue(Text.Contains(expectedText), $"Cell[{Row},{Column}] Text didn't contain {expectedText}");
        }

        public void AssertTextNotContains(string expectedText)
        {
            TableFormCellAsserted?.Invoke(this, $"Assert Cell[{Row},{Column}] Text doesn't contain {expectedText}");
            Assert.IsFalse(Text.Contains(expectedText), $"Cell[{Row},{Column}] Text contained {expectedText}");
        }
    }
}
