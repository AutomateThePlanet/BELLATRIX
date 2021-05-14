using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;

namespace Bellatrix.CognitiveServices.services
{
    public class AssertedFormTable
    {
        private readonly FormTable _formTable;

        public AssertedFormTable(FormTable formTable)
        {
            _formTable = formTable;
        }

        public AssertedTableFormCell GetCell(Func<AssertedTableFormCell, bool> predicate)
        {
            return GetCells().First(predicate);
        }

        public IEnumerable<AssertedTableFormCell> GetCells(Func<AssertedTableFormCell, bool> predicate)
        {
            return GetCells().Where(predicate);
        }

        public AssertedTableFormCell GetCell(int row, int column)
        {
            return GetCells().First(c => c.Row.Equals(row) && c.Column.Equals(column));
        }

        public IEnumerable<AssertedTableFormCell> GetCells()
        {
            var listOfCells = new List<AssertedTableFormCell>();
            foreach (FormTableCell cell in _formTable.Cells)
            {
                listOfCells.Add(new AssertedTableFormCell(cell));
            }

            return listOfCells;
        }
    }
}
