// <copyright file="Grid.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Web;
using Bellatrix.Assertions;
using Bellatrix.Infrastructure;
using Bellatrix.Infrastructure.SystemFacades;
using Bellatrix.Utilities;
using Bellatrix.Web;
using Bellatrix.Web.Controls.Advanced.ControlDataHandlers;
using Bellatrix.Web.Controls.Advanced.Grid;
using HtmlAgilityPack;

namespace Bellatrix.Web
{
    public class Grid : Component
    {
        private const string RowsXPathLocator = "//tr[descendant::td]";
        private TableService _tableService;

        static Grid()
        {
        }

        public virtual TableService TableService
        {
            get
            {
                if (_tableService == null)
                {
                    WaitUntilPopulated();
                    var innerHtml = GetAttribute("innerHTML");
                    _tableService = new TableService(innerHtml);
                }

                return _tableService;
            }
        }

        public virtual HeaderNamesService HeaderNamesService => new HeaderNamesService(TableService.HeaderRows);

        public List<IHeaderInfo> ControlColumnDataCollection { get; set; }

        public virtual ComponentsList<Button> ColumnHeaders
        {
            get
            {
                var list = new ComponentsList<Button>();
                foreach (HtmlNode node in TableService.Headers)
                {
                    var element = this.CreateByXpath<Button>(node.GetXPath());
                    list.Add(element);
                }

                return list;
            }
        }

        public virtual ComponentsList<TableHeaderRow> TableHeaderRows
        {
            get
            {
                var list = new ComponentsList<TableHeaderRow>();
                foreach (HtmlNode header in TableService.HeaderRows)
                {
                    var element = this.CreateByXpath<TableHeaderRow>(header.GetXPath());
                    list.Add(element);
                }

                return list;
            }
        }

        public virtual void WaitUntilPopulated()
        {
            Bellatrix.Utilities.Wait.ForConditionUntilTimeout(
                () =>
                {
                    var rows = ElementCreateService.CreateAllByXpath<Label>(RowsXPathLocator);
                    return rows != null && rows.Any();
                },
                totalRunTimeoutMilliseconds: 3000,
                sleepTimeMilliseconds: 500);
        }

        public IList<string> GetHeaderNames()
        {
            return HeaderNamesService.GetHeaderNames();
        }

        public void ForEachHeader(Action<Button> action)
        {
            foreach (var header in ColumnHeaders)
            {
                action(header);
            }
        }

        public int RowsCount => TableService.Rows.Count;

        public GridRow GetRow(int rowIndex)
        {
            string xpath = $".{TableService.GetRow(rowIndex).GetXPath()}";
            GridRow row = this.CreateByXpath<GridRow>(xpath);
            row.SetParentGrid(this);
            row.Index = rowIndex;
            return row;
        }

        public virtual IEnumerable<GridRow> GetRows()
        {
            for (int rowIndex = 0; rowIndex < TableService.Rows.Count; rowIndex++)
            {
                GridRow row = GetRow(rowIndex);
                yield return row;
            }
        }

        public virtual ComponentsList<GridRow> GetRows<TElement>(Func<TElement, bool> selector)
            where TElement : Component, new()
        {
            return GetRows().Where(r => r.GetCells<TElement>(selector).Any()).ToElementList();
        }

        public GridRow GetFirstOrDefaultRow<TElement>(Func<TElement, bool> selector)
            where TElement : Component, new()
        {
            return GetRows(selector).FirstOrDefault();
        }

        public void ForEachRow(Action<GridRow> action)
        {
            foreach (var gridRow in TableService.Rows)
            {
                string xpath = gridRow.GetXPath();
                action(this.CreateByXpath<GridRow>(xpath));
            }
        }

        public GridCell GetCell(int row, int column)
        {
            string innerXpath = TableService.GetCell(row, column).GetXPath();
            string outerXpath = GetCurrentElementXPath();
            string fullXpath = outerXpath + innerXpath;
            GridCell cell = ElementCreateService.CreateByXpath<GridCell>(fullXpath);
            SetCellMetaData(cell, row, column);
            return cell;
        }

        public GridCell GetCell(string header, int row)
        {
            int? position = HeaderNamesService.GetHeaderPosition(header, ControlColumnDataCollection.AsEnumerable<IHeaderInfo>().ToList());
            if (position == null)
            {
                return null;
            }

            return GetCell(row, (int)position);
        }

        public GridCell GetCell<TDto>(Expression<Func<TDto, object>> expression, int row)
            where TDto : class
        {
            string headerName = HeaderNamesService.GetHeaderNameByExpression<TDto>(expression);
            int? position = HeaderNamesService.GetHeaderPosition(headerName, ControlColumnDataCollection);
            if (position == null)
            {
                return null;
            }

            return GetCell(row, (int)position);
        }

        public void ForEachCell(Action<GridCell> action)
        {
            string outerXPath = GetCurrentElementXPath();
            foreach (var gridCell in TableService.GetCells())
            {
                string fullXPath = outerXPath + gridCell.GetXPath();
                action(this.CreateByXpath<GridCell>(fullXPath));
            }
        }

        public ComponentsList<TElement> GetCells<TElement>(Func<TElement, bool> selector)
            where TElement : Component, new()
        {
            var filteredCells = new List<TElement>();
            foreach (var gridRow in GetRows())
            {
                var currentFilteredCells = gridRow.GetCells<TElement>(selector);
                filteredCells.AddRange(currentFilteredCells);
            }

            return filteredCells.ToElementList();
        }

        public TElement GetFirstOrDefaultCell<TElement>(Func<TElement, bool> selector)
            where TElement : Component, new()
        {
            return GetCells<TElement>(selector).FirstOrDefault();
        }

        public TElement GetLastOrDefaultCell<TElement>(Func<TElement, bool> selector)
            where TElement : Component, new()
        {
            return GetCells<TElement>(selector).LastOrDefault();
        }

        public string GetGridColumnNameByIndex(int index) => HeaderNamesService.GetHeaderNames()[index];

        public int GetGridColumnIndexByName(string columnName)
        {
            var coll = HeaderNamesService.GetHeaderNames();
            return coll.IndexOf(columnName);
        }

        public IList<GridCell> GetColumn(int column)
        {
            var list = new List<GridCell>();

            for (int row = 0; row < RowsCount; row++)
            {
                list.Add(GetCell(row, column));
            }

            return list;
        }

        public IList<GridCell> GetColumn(string header)
        {
            int? position = HeaderNamesService.GetHeaderPosition(header, ControlColumnDataCollection.AsEnumerable<IHeaderInfo>().ToList());
            if (position == null)
            {
                return new List<GridCell>();
            }

            return GetColumn((int)position);
        }

        public void AssertTable<TRowObject>(List<TRowObject> expectedEntities, params string[] propertiesNotToCompare)
            where TRowObject : new()
        {
            ScrollToVisible();
            Assert.AreEqual(expectedEntities.Count, RowsCount, $"Expected rows count {expectedEntities.Count} but rows was {RowsCount}");

            for (int i = 0; i < RowsCount; i++)
            {
                var entity = CastRow<TRowObject>(i, propertiesNotToCompare);
                EntitiesAsserter.AreEqual(expectedEntities[i], entity, propertiesNotToCompare);
            }
        }

        public virtual void AssertIsEmpty()
        {
            ScrollToVisible();
            Assert.AreEqual(0, RowsCount, $"Grid should be empty, but has {RowsCount} rows");
        }

        public virtual void AssertIsNotEmpty()
        {
            ScrollToVisible();
            Assert.IsTrue(TableService.Rows.Any(), $"Grid has no rows");
        }

        public virtual void AssertRowsCount(int expectedRowsCount)
        {
            ScrollToVisible();
            Assert.AreEqual(expectedRowsCount, RowsCount, $"Grid should have {expectedRowsCount} rows, but has {RowsCount} rows");
        }

        public IList<TRowObject> GetItems<TRowObject>()
            where TRowObject : new()
        {
            ScrollToVisible();
            return GetItems<TRowObject, GridRow>(GetRows().ToElementList());
        }

        public TRowObject CastRow<TRowObject>(int rowIndex, params string[] propertiesToSkip)
            where TRowObject : new()
        {
            var cells = TableService.GetRowCells(rowIndex);

            if (cells.Count != ControlColumnDataCollection.Count)
            {
                // Compare headers to determine why the cells count is different
                var actual = TableService.Headers.Select(c => c.InnerText.Trim(" 0".ToCharArray())).ToList();
                var expected = ControlColumnDataCollection.Select(c => c.HeaderName).ToList();
                CollectionAssert.AreEqual(expected, actual, $"Expected: {expected.Stringify()}\r\nActual: {actual.Stringify()}");
            }

            var dto = new TRowObject();
            var properties = dto.GetType().GetProperties().ToList();
            foreach (var propertyInfo in properties)
            {
                string headerName = HeaderNamesService.GetHeaderNameByProperty(propertyInfo);

                if (propertiesToSkip.Contains(headerName))
                {
                    continue;
                }

                int? headerPosition = HeaderNamesService.GetHeaderPosition(headerName, ControlColumnDataCollection.AsEnumerable<IHeaderInfo>().ToList());
                if (headerPosition == null)
                {
                    continue;
                }

                var controlData = GetControlDataByProperty(propertyInfo);
                if (controlData != null && controlData.ElementType != null && controlData.ElementType.IsSubclassOf(typeof(Component)))
                {
                    var repo = new ElementRepository();
                    var xpath = $".{cells[(int)headerPosition].GetXPath()}";
                    var tableCell = this.CreateByXpath<TableCell>(xpath);
                    dynamic elementValue;
                    if (controlData.By == null)
                    {
                        controlData.By = new FindXpathStrategy(xpath);
                        elementValue = CastCell(repo, controlData, tableCell);
                        controlData.By = null;
                    }
                    else
                    {
                        elementValue = CastCell(repo, controlData, tableCell);
                    }

                    var elementType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                    var newValue = elementValue == null ? default : Convert.ChangeType(elementValue, elementType);

                    elementValue = Convert.ChangeType(newValue, propertyInfo.PropertyType);
                    propertyInfo.SetValue(dto, elementValue);
                }
                else
                {
                    string htmlNodeValue = HttpUtility.HtmlDecode(TableService.GetRowCells(rowIndex)[(int)headerPosition].InnerText).Trim();
                    var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                    object elementValue;
                    if (type == typeof(DateTime) || type == typeof(DateTime?))
                    {
                        DateTime dateTime;
                        DateTime.TryParse(htmlNodeValue, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime);
                        elementValue = (DateTime?)dateTime;
                    }
                    else
                    {
                        elementValue = string.IsNullOrEmpty(htmlNodeValue) ? default : Convert.ChangeType(htmlNodeValue, type, CultureInfo.InvariantCulture);
                    }

                    propertyInfo.SetValue(dto, elementValue);
                }
            }

            return dto;
        }

        internal void SetCellMetaData(GridCell cell, int row, int column)
        {
            cell.Row = row;
            cell.Column = column;
            var headerName = ControlColumnDataCollection[column].HeaderName;
            var controlData = ControlColumnDataCollection.GetControlColumnDataByHeaderName(headerName.Trim());
            cell.CellControlBy = controlData.By;
            cell.CellControlElementType = controlData.ElementType;
        }

        protected virtual IList<TRowObject> GetItems<TRowObject, TRow>(ComponentsList<TRow> rows)
            where TRowObject : new()
            where TRow : GridRow, new()
        {
            var list = new List<TRowObject>();

            foreach (var row in rows)
            {
                var obj = CastRow<TRowObject>(row.Index);
                list.Add(obj);
            }

            return list;
        }

        private ControlColumnData GetControlDataByProperty(PropertyInfo property)
        {
            Attribute headerNameAttribute = property.GetCustomAttributes(typeof(HeaderNameAttribute)).FirstOrDefault();
            IHeaderInfo headerInfo;

            if (headerNameAttribute != null)
            {
                headerInfo = ControlColumnDataCollection.FirstOrDefault(h => h.HeaderName == ((HeaderNameAttribute)headerNameAttribute).Name);
            }
            else
            {
                headerInfo = ControlColumnDataCollection.FirstOrDefault(h => h.HeaderName == property.Name);
            }

            return headerInfo as ControlColumnData;
        }

        private dynamic CastCell(ElementRepository repo, ControlColumnData controlData, TableCell tableCell)
        {
            var element = repo.CreateElementWithParent(controlData.By, tableCell.WrappedElement, controlData.ElementType, false);

            // Resolve the appropriate Readonly Control Data Handler
            dynamic controlDataHandler = ControlDataHandlerResolver.ResolveReadonlyDataHandler(element.GetType());

            if (controlDataHandler == null)
            {
                throw new Exception($"Cannot find proper IReadonlyControlDataHandler for type: {element.GetType().Name}. Make sure it is registered in the ServiceContainer");
            }

            dynamic elementValue = controlDataHandler.GetData(element);

            return elementValue;
        }

        private string GetCurrentElementXPath()
        {
            string jsScriptText =
                @"function createXPathFromElement(elm) {
                    var allNodes = document.getElementsByTagName('*');
                    for (var segs = []; elm && elm.nodeType === 1; elm = elm.parentNode) {
                        if (elm.hasAttribute('id')) {
                            var uniqueIdCount = 0;
                            for (var n = 0; n < allNodes.length; n++) {
                                if (allNodes[n].hasAttribute('id') && allNodes[n].id === elm.id) uniqueIdCount++;
                                if (uniqueIdCount > 1) break;
                            };
                            if (uniqueIdCount === 1) {
                                segs.unshift('//*[@id=\'' + elm.getAttribute('id') + '\']');
                                return segs.join('/');
                            }
                            else {
                                segs.unshift('//' + elm.localName.toLowerCase() + '[@id=\'' + elm.getAttribute('id') + '\']');
                            }
                        }
                        else {
                            for (i = 1, sib = elm.previousSibling; sib; sib = sib.previousSibling) {
                                if (sib.localName === elm.localName) i++;
                            };
                            segs.unshift(elm.localName.toLowerCase() + '[' + i + ']');
                        };
                    };
                    return segs.length ? '/' + segs.join('/') : null;
                };
                return createXPathFromElement(arguments[0])";

            return JavaScriptService.Execute(jsScriptText, this);
        }
    }
}