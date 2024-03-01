// <copyright file="Table.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web;

public class Table : Component
{
    private HeaderNamesService _headerNamesService;
    private FooterService _footerService;
    private List<TableRow> _rows;
    private TableService _tableService;

    public Table()
    {
    }

    public TableService TableService
    {
        get
        {
            if (_tableService == null)
            {
                var innerHtml = GetAttribute("innerHTML");
                _tableService = new TableService(innerHtml);
            }

            return _tableService;
        }
    }

    protected HeaderNamesService HeaderNamesService
    {
        get
        {
            if (_headerNamesService == null)
            {
                _headerNamesService = new HeaderNamesService(TableService.HeaderRows);
            }

            return _headerNamesService;
        }
    }

    protected FooterService FooterService
    {
        get
        {
            if (_footerService == null)
            {
                _footerService = new FooterService(TableService.Footer);
            }

            return _footerService;
        }
    }

    public TableFooter Footer => this.CreateByXpath<TableFooter>(TableService.Footer.GetXPath());

    public int RowsCount => GetRows().ToElementList().Count();

    public List<HeaderInfo> ColumnHeaderNames { get; set; }

    public override Type ComponentType => GetType();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual List<Label> ColumnHeaders => this.CreateAllByTag<Label>("th", true).ToList();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual List<TableHeaderRow> TableHeaderRows => this.CreateAllByXpath<TableHeaderRow>(".//tr[descendant::th]").ToList();

    public IEnumerable<TableRow> GetRows()
    {
        InitializeRows();
        foreach (var tableRow in _rows)
        {
            yield return tableRow;
        }
    }

    public TableRow GetRow(int row)
    {
        InitializeRows();
        return _rows[row];
    }

    public void ForEachRow(Action<TableRow> action)
    {
        foreach (var tableRow in GetRows())
        {
            action(tableRow);
        }
    }

    public void ForEachCell(Action<TableCell> action)
    {
        foreach (var tableRow in GetRows())
        {
            foreach (var rowCell in tableRow.GetCells())
            {
                action(rowCell);
            }
        }
    }

    public void ForEachHeader(Action<Label> action)
    {
        foreach (var header in ColumnHeaders)
        {
            action(header);
        }
    }

    public IList<TableCell> GetColumn(int column)
    {
        var tableCells = (from row in GetRows() select row.GetCell(column)).ToList();
        int rowNumber = 0;
        foreach (var tableCell in tableCells)
        {
            tableCell.Column = column;
            tableCell.Row = rowNumber++;
        }

        return tableCells;
    }

    public IList<TableCell> GetColumn(string header)
    {
        int? position = HeaderNamesService.GetHeaderPosition(header, ColumnHeaderNames.AsEnumerable<IHeaderInfo>().ToList());
        if (position == null)
        {
            return new List<TableCell>();
        }

        return GetColumn((int)position);
    }

    public TableCell GetCell(string header, int row)
    {
        int? position = HeaderNamesService.GetHeaderPosition(header, ColumnHeaderNames.AsEnumerable<IHeaderInfo>().ToList());
        if (position == null)
        {
            return null;
        }

        return GetCell((int)position, row);
    }

    public TableCell GetCell<TDto>(Expression<Func<TDto, object>> expression, int row)
        where TDto : class
    {
        string headerName = HeaderNamesService.GetHeaderNameByExpression<TDto>(expression);
        int? position = HeaderNamesService.GetHeaderPosition(headerName, ColumnHeaderNames.AsEnumerable<IHeaderInfo>().ToList());
        if (position == null)
        {
            return null;
        }

        return GetCell((int)position, row);
    }

    public TableCell GetCell(int column, int row)
    {
        TableCell cell = GetRow(row).GetCell(column);
        cell.Row = row;
        cell.Column = column;

        return cell;
    }

    public List<TableCell> GetCells(Func<TableCell, bool> selector)
    {
        var filteredCells = new List<TableCell>();
        foreach (var tableRow in GetRows())
        {
            var currentFilteredCells = tableRow.GetCells(selector);
            filteredCells.AddRange(currentFilteredCells);
        }

        return filteredCells.ToList();
    }

    public TableCell GetFirstOrDefaultCell(Func<TableCell, bool> selector)
    {
        return GetCells(selector).FirstOrDefault();
    }

    public List<TableRow> GetRows(Func<TableCell, bool> selector)
    {
        return GetRows().Where(r => r.GetCells(selector).Any()).ToList();
    }

    public TableRow GetFirstOrDefaultRow(Func<TableCell, bool> selector)
    {
        return GetRows(selector).FirstOrDefault();
    }

    public void AssertTable<T>(List<T> expectedEntities)
        where T : new()
    {
        AssertTable<T, TableRow>(GetRows().ToList(), expectedEntities);
    }

    public IList<string> GetHeaderNames()
    {
        return HeaderNamesService.GetHeaderNames();
    }

    public IList<T> GetItems<T>()
        where T : new()
    {
        return GetItems<T, TableRow>(GetRows().ToList());
    }

    protected void AssertTable<T, TRow>(List<TRow> rows, List<T> expectedEntities)
        where T : new()
        where TRow : TableRow
    {
        var actualEntities = GetItems<T, TRow>(rows);
        if (actualEntities.Count != expectedEntities.Count)
        {
            throw new ArgumentException($"The current table rows count {actualEntities.Count} is different than the specified expected values {expectedEntities.Count}.");
        }

        for (int i = 0; i < expectedEntities.Count; i++)
        {
            EntitiesAsserter.AreEqual(expectedEntities[i], actualEntities[i]);
        }
    }

    protected IList<T> GetItems<T, TRow>(List<TRow> rows)
        where T : new()
        where TRow : TableRow
    {
        return rows.Where(r => r.GetCells().Any()).Select(CastRow<T>).ToList();
    }

    internal T CastRow<T>(TableRow row)
        where T : new()
    {
        var props = typeof(T).GetProperties();
        var castRow = new T();
        foreach (var propertyInfo in props)
        {
            if (row.GetCells().Any())
            {
                if (propertyInfo.CanWrite)
                {
                    string headerName = HeaderNamesService.GetHeaderNameByProperty(propertyInfo);
                    int? headerPosition = HeaderNamesService.GetHeaderPosition(headerName, ColumnHeaderNames.AsEnumerable<IHeaderInfo>().ToList());

                    // Will skip properties that are not part of the meta data.
                    if (headerPosition != null)
                    {
                        dynamic elementValue = row.GetCells().ToElementList()[(int)headerPosition].InnerText;
                        propertyInfo.SetValue(castRow, elementValue, null);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Cannot cast grid data to C# object. Most probably your C# class properties don't have setters.");
                }
            }
        }

        return castRow;
    }

    private void InitializeRows()
    {
        if (_rows == null || !_rows.Any())
        {
            _rows = this.CreateAllByXpath<TableRow>("./tr[descendant::td]|./tbody/tr[descendant::td]", true).ToList();

            int rowNumber = 0;
            foreach (var gridRow in _rows)
            {
                if (this.CreateAllByXpath<TableRow>("./tr[descendant::th]", true).ToElementList().Any())
                {
                    gridRow.SetParentTable(this);
                }

                gridRow.Index = rowNumber++;
            }
        }
    }
}