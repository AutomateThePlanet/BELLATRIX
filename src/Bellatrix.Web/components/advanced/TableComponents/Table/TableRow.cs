// <copyright file="TableRow.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Bellatrix.Assertions;
using Bellatrix.Web.Contracts;

namespace Bellatrix.Web;

public class TableRow : Component, IComponentInnerHtml
{
    private Table _parentTable;
    private HeaderNamesService _headerNamesService;

    protected virtual List<TableCell> TableCells => this.CreateAllByXpath<TableCell>("./td", true).ToList();

    public int Index { get; set; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string InnerHtml => GetInnerHtmlAttribute();

    public TableCell this[int i] => GetCells().ToList().ElementAt(i);

    public void SetParentTable(Table table)
    {
        _parentTable = table;
        _headerNamesService = new HeaderNamesService(_parentTable.TableService.HeaderRows);
    }

    public TableCell GetCell(int column)
    {
        if (TableCells.Count() <= column)
        {
            return null;
        }

        TableCell tableCell = TableCells[column];
        tableCell.Column = column;
        tableCell.Row = Index;

        return tableCell;
    }

    public TableCell GetCell(string headerName)
    {
        int? position = _headerNamesService.GetHeaderPosition(headerName, _parentTable.ColumnHeaderNames.AsEnumerable<IHeaderInfo>().ToList());
        if (position == null)
        {
            return null;
        }

        return GetCell((int)position);
    }

    public TableCell GetCell<TDto>(Expression<Func<TDto, object>> expression)
        where TDto : class
    {
        string headerName = _headerNamesService.GetHeaderNameByExpression(expression);
        return GetCell(headerName);
    }

    public IEnumerable<TableCell> GetCells()
    {
        int columnNumber = 0;
        foreach (var tableCell in TableCells)
        {
            tableCell.Row = Index;
            tableCell.Column = columnNumber++;

            yield return tableCell;
        }
    }

    public List<TableCell> GetCells(Func<TableCell, bool> selector)
    {
        return GetCells().Where(selector).ToList();
    }

    public TableCell GetFirstOrDefaultCell(Func<TableCell, bool> selector)
    {
        return GetCells(selector).FirstOrDefault();
    }

    public T GetItem<T>()
        where T : new()
    {
        return _parentTable.CastRow<T>(this);
    }

    public void AssertRow<T>(T expectedItem)
        where T : new()
    {
        var actualItem = GetItem<T>();

        EntitiesAsserter.AreEqual(expectedItem, actualItem);
    }
}