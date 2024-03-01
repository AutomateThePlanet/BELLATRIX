// <copyright file="TableFooter.cs" company="Automate The Planet Ltd.">
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
// <author>Teodor Nikolov</author>
// <site>https://bellatrix.solutions/</site>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Bellatrix.Assertions;
using Bellatrix.Web.Contracts;

namespace Bellatrix.Web;

public class TableFooter : Component, IComponentInnerHtml
{
    private Table _parentTable;
    private HeaderNamesService _headerNamesService;
    private FooterService _footerService;

    public int Index { get; set; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string InnerHtml => GetInnerHtmlAttribute();

    public void SetParentTable(Table table)
    {
        _parentTable = table;
        _headerNamesService = new HeaderNamesService(_parentTable.TableService.HeaderRows);
        _footerService = new FooterService(_parentTable.TableService.Footer);
    }

    public TableRow GetRowByPosition(int position)
    {
        var footerRow = this.CreateByXpath<TableRow>(_footerService.GetFooterRowByPosition(position).GetXPath());
        footerRow.SetParentTable(_parentTable);
        footerRow.Index = position;

        return footerRow;
    }

    public TableRow GetRowByName(string footerName)
    {
        var node = _footerService.GetFooterRowByName(footerName);
        int position = _footerService.Rows.IndexOf(node);
        var footerRow = this.CreateByXpath<TableRow>(node.GetXPath());
        footerRow.SetParentTable(_parentTable);
        footerRow.Index = position;

        return footerRow;
    }

    public TableCell GetCell(string footerName, string headerName)
    {
        int? headerPosition = _headerNamesService.GetHeaderPosition(headerName, _parentTable.ColumnHeaderNames.AsEnumerable<IHeaderInfo>().ToList());
        if (headerPosition == null)
        {
            return null;
        }

        var node = _footerService.GetFooterRowCellByName(footerName, (int)headerPosition);
        var footerCell = this.CreateByXpath<TableCell>(node.GetXPath());
        footerCell.Row = _footerService.Rows.IndexOf(node);
        footerCell.Column = (int)headerPosition;

        return footerCell;
    }

    public TableCell GetCell(int position, string headerName)
    {
        int? headerPosition = _headerNamesService.GetHeaderPosition(headerName, _parentTable.ColumnHeaderNames.AsEnumerable<IHeaderInfo>().ToList());
        if (headerPosition == null)
        {
            return null;
        }

        var node = _footerService.GetFooterRowCellByPosition(position, (int)headerPosition);
        var footerCell = this.CreateByXpath<TableCell>(node.GetXPath());
        footerCell.Row = position;
        footerCell.Column = (int)headerPosition;

        return footerCell;
    }
}