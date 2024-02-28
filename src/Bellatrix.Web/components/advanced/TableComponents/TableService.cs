// <copyright file="TableService.cs" company="Automate The Planet Ltd.">
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

using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Bellatrix.Web;

public class TableService
{
    private const string RowsXPathLocator = "//tr[descendant::td]";
    private string _tableXPath;
    private HtmlDocument _htmlDoc;

    public TableService(string html)
    {
        _htmlDoc = new HtmlDocument
        {
            OptionFixNestedTags = true,
            OptionOutputAsXml = true,
            BackwardCompatibility = false,
            OptionWriteEmptyNodes = false,
        };
        _htmlDoc.LoadHtml(html);
    }

    public TableService(string html, string tableXpath)
        : this(html)
    {
        _tableXPath = tableXpath;
    }

    public virtual HtmlNode Table
    {
        get
        {
            if (string.IsNullOrEmpty(_tableXPath))
            {
                // By default we use root element for table
                _tableXPath = "//*";
            }

            return _htmlDoc.DocumentNode.SelectSingleNode(_tableXPath);
        }
    }

    public virtual List<HtmlNode> Headers => Table.SelectNodes("//th").ToList();

    public virtual List<HtmlNode> HeaderRows => Table
                                        .SelectNodes("//tr[descendant::th]")?
                                        .Where(a => a.GetAttributeValue("style", null) != "display:none")
                                        .ToList();

    public virtual List<HtmlNode> Rows => Table.SelectNodes(RowsXPathLocator).ToList();

    public virtual HtmlNode Footer => Table.SelectSingleNode("//tfoot");

    public HtmlNode GetRow(int row)
    {
        return Rows[row];
    }

    public HtmlNode GetCell(int row, int column)
    {
        return GetRowCells(row)[column];
    }

    public virtual List<HtmlNode> GetCells()
    {
        var listOfNodes = new List<HtmlNode>();

        for (int i = 0; i < Rows.Count; i++)
        {
            listOfNodes.AddRange(Rows[i].Descendants("td"));
        }

        return listOfNodes;
    }

    public virtual List<HtmlNode> GetRowCells(int rowIndex)
    {
        return Table.SelectNodes(Rows[rowIndex].XPath + "//td").ToList();
    }
}
