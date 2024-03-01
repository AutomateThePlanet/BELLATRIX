// <copyright file="FooterService.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;
using System.Web;
using Bellatrix.Utilities;
using HtmlAgilityPack;

namespace Bellatrix.Web;

public class FooterService
{
    private string _xpathToNameElement;
    private HtmlNode _tableFooter;
    private List<List<string>> _tableFooterRowsValuesWithIndex;

    public FooterService(HtmlNode tableFooter, string xpathToNameElement = "")
    {
        _tableFooter = tableFooter;
        _xpathToNameElement = xpathToNameElement;
    }

    public virtual List<HtmlNode> Rows => _tableFooter
                        .SelectNodes("//tr[ancestor::tfoot]")?
                        .Where(a => a.GetAttributeValue("style", null) != "display:none")
                        .ToList() ?? new List<HtmlNode>();
    public List<List<string>> GetFooterRowsData()
    {
        InitializeFooterRows();

        return _tableFooterRowsValuesWithIndex;
    }

    public HtmlNode GetFooterRowByName(string footerName)
    {
        return Rows.FirstOrDefault(row => row.SelectSingleNode($".//td[.='{footerName}']") != null);
    }

    public HtmlNode GetFooterRowByPosition(int position)
    {
        return Rows[position];
    }

    public HtmlNode GetFooterRowCellByName(string footerName, int cellIndex)
    {
        var row = Rows.FirstOrDefault(row => row.InnerText == footerName);
        return row.SelectNodes(".//td")[cellIndex];
    }

    public HtmlNode GetFooterRowCellByPosition(int position, int cellIndex)
    {
        var row = Rows[position];
        return row.SelectNodes(".//td")[cellIndex];
    }

    public List<string> GetFooterRowDataByName(string footerName)
    {
        var footerRow = GetFooterRowsData().FirstOrDefault(row => row[0] == footerName);
        if (footerRow == null)
        {
            footerRow = GetFooterRowsData().FirstOrDefault(row => row[0].StartsWith(footerName));
        }

        return footerRow;
    }

    public List<string> GetFooterRowDataByPosition(int position)
    {
        return GetFooterRowsData()[position];
    }

    public string GetFooterValueForIndex(int footerPosition, int index)
    {
        var footerRow = GetFooterRowDataByPosition(footerPosition);
        return footerRow[index];
    }

    public string GetFooterValueForIndex(string footerName, int index)
    {
        var footerRow = GetFooterRowDataByName(footerName);
        return footerRow[index];
    }

    private void InitializeFooterRows()
    {
        if (_tableFooterRowsValuesWithIndex != null)
        {
            return;
        }

        _tableFooterRowsValuesWithIndex = new List<List<string>>();
        int rowIndex = 0;
        foreach (var tableFooterRow in Rows)
        {
            int columnIndex = 0;
            var footerCellsCount = tableFooterRow.SelectNodes(".//td").Count;

            foreach (var currentCell in tableFooterRow.SelectNodes(".//td"))
            {
                string cellValue;

                if (string.IsNullOrEmpty(_xpathToNameElement))
                {
                    cellValue = HttpUtility.HtmlDecode(currentCell.InnerText);
                }
                else
                {
                    cellValue = HttpUtility.HtmlDecode(currentCell.SelectSingleNode(_xpathToNameElement).InnerText);
                }

                int colSpan = GetColSpan(currentCell);

                AddColumnIndex(rowIndex, colSpan, ref columnIndex, cellValue);
            }

            rowIndex++;
        }
    }

    private void AddColumnIndex(int currentRow, int colSpan, ref int columnIndex, string cellValue)
    {
        AddFooterCellIndex(currentRow, columnIndex, ref columnIndex, cellValue);
        if (colSpan == 0)
        {
            columnIndex++;
        }
        else
        {
            int initialIndex = columnIndex;
            for (int i = 1; i < colSpan; i++)
            {
                AddFooterCellIndex(currentRow, initialIndex + i, ref columnIndex, string.Empty);
                columnIndex++;
            }

            columnIndex++;
        }
    }

#pragma warning disable IDE0060 // Remove unused parameter
    private void AddFooterCellIndex(int currentRow, int operationalIndex, ref int columnIndex, string cellValue)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        if (_tableFooterRowsValuesWithIndex.Count > currentRow)
        {
            _tableFooterRowsValuesWithIndex[currentRow].Add(cellValue);
        }
        else
        {
            _tableFooterRowsValuesWithIndex.Add(new List<string>() { cellValue });
        }
    }

    private int GetColSpan(HtmlNode headerCell)
    {
        string colSpanText = headerCell.GetAttributeValue("colspan", null);
        return colSpanText == null ? 0 : int.Parse(colSpanText);
    }
}