// <copyright file="HeaderNamesService.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using Bellatrix.Utilities;
using HtmlAgilityPack;

namespace Bellatrix.Web
{
    public class HeaderNamesService
    {
        private string _xpathToNameElement;
        private Dictionary<int, string> _headerNamesIndexes;
        private List<HtmlNode> _tableRowHeaders;

        public HeaderNamesService(List<HtmlNode> tableHeaderRows, string xpathToNameElement = "")
        {
            _tableRowHeaders = tableHeaderRows;
            _xpathToNameElement = xpathToNameElement;
        }

        public List<string> ColumnHeaderNames { get; set; }

        public void SetHeaderNamesByIndex(int index, string value) => _headerNamesIndexes[index] = value;

        public IList<string> GetHeaderNames()
        {
            InitializeHeaderNames();

            return _headerNamesIndexes.Values.ToList();
        }

        public string GetNameByPosition(int position)
        {
            return _headerNamesIndexes[position];
        }

        public int? GetHeaderPosition(string header, List<IHeaderInfo> headerInfos)
        {
            SetEmptyHeadersName(headerInfos);

            if (!_headerNamesIndexes.Any(x => x.Value.EndsWith(header)))
            {
                throw new ArgumentException($"Header {header} was not found.");
            }

            if (ColumnHeaderNames != null && ColumnHeaderNames.All(x => !x.EndsWith(header)))
            {
                return null;
            }

            if (string.IsNullOrEmpty(header))
            {
                return null;
            }

            var allMatchingHeaders = _headerNamesIndexes.Where(x => x.Value.EndsWith(header));
            if (allMatchingHeaders.Count() > 1)
            {
                var exactMatchHeaders = _headerNamesIndexes.Where(x => x.Value == header);

                if (exactMatchHeaders.Count() != 1)
                {
                    Logger.LogError($"More than one Header with name ending with '{header}' was found. Returning the first one.");
                    return allMatchingHeaders.FirstOrDefault().Key;
                }

                return exactMatchHeaders.FirstOrDefault().Key;
            }
            else if (allMatchingHeaders.Any())
            {
                return allMatchingHeaders.FirstOrDefault().Key;
            }

            return null;
        }

        public string GetHeaderNameByExpression<TDto>(Expression<Func<TDto, object>> expression)
        where TDto : class
        {
            string propertyName = TypePropertiesNameResolver.GetMemberName(expression);
            var propertyInfo = typeof(TDto).GetProperties().FirstOrDefault(x => x.Name.Equals(propertyName));

            return GetHeaderNameByProperty(propertyInfo);
        }

        public string GetHeaderNameByProperty(PropertyInfo property)
        {
            var headerNameAttribute = property.GetCustomAttributes(typeof(HeaderNameAttribute)).FirstOrDefault();
            string headerName = headerNameAttribute == null ? property.Name : ((HeaderNameAttribute)headerNameAttribute).Name;

            return headerName;
        }

        private void SetEmptyHeadersName(List<IHeaderInfo> headerInfos)
        {
            var headerNameCollection = GetHeaderNames();
            var count = headerInfos.Count < headerNameCollection.Count ? headerInfos.Count : headerNameCollection.Count;

            for (int i = 0; i < count; i++)
            {
                var headerName = headerInfos[i].HeaderName;
                var collectionName = headerNameCollection[i];

                if (!collectionName.Equals(headerName) && string.IsNullOrWhiteSpace(collectionName))
                {
                    SetHeaderNamesByIndex(i, headerName);
                }
            }
        }

        private void InitializeHeaderNames()
        {
            if (_headerNamesIndexes != null)
            {
                return;
            }

            _headerNamesIndexes = new Dictionary<int, string>();
            var rowSpanPairs = new Dictionary<int, HeaderRowIndex>();
            int rowIndex = 0;
            foreach (var tableRowHeader in _tableRowHeaders)
            {
                int columnIndex = 0;
                var headerCellsCount = tableRowHeader.SelectNodes(".//th").Count;

                foreach (var currentHeader in tableRowHeader.SelectNodes(".//th"))
                {
                    string headerName;
                    while (rowSpanPairs.ContainsKey(columnIndex) && rowIndex > rowSpanPairs[columnIndex].RowIndex)
                    {
                        headerName = rowSpanPairs[columnIndex].HeaderName;
                        rowSpanPairs[columnIndex].Rowspan--;
                        int currentColSpan = rowSpanPairs[columnIndex].Colspan;

                        if (rowSpanPairs[columnIndex].Rowspan == 1)
                        {
                            rowSpanPairs.Remove(columnIndex);
                        }

                        columnIndex++;
                    }

                    if (string.IsNullOrEmpty(_xpathToNameElement))
                    {
                        headerName = currentHeader.InnerText;
                    }
                    else
                    {
                        headerName = currentHeader.SelectSingleNode(_xpathToNameElement).InnerText;
                    }

                    int colSpan = GetColSpan(currentHeader);
                    int rowSpan = GetRowSpan(currentHeader);

                    if (rowSpan > 1)
                    {
                        rowSpanPairs.Add(columnIndex, new HeaderRowIndex(headerName, rowSpan, colSpan, rowIndex));
                    }

                    AddColumnIndex(colSpan, ref columnIndex, headerName);
                }

                rowIndex++;
            }
        }

        private void AddColumnIndex(int colSpan, ref int columnIndex, string headerName)
        {
            if (colSpan == 0)
            {
                AddHeaderNameIndex(columnIndex, ref columnIndex, headerName);
                columnIndex++;
            }
            else
            {
                int initialIndex = columnIndex;
                for (int i = 0; i < colSpan; i++)
                {
                    AddHeaderNameIndex(initialIndex + i, ref columnIndex, headerName);
                    columnIndex++;
                }
            }
        }

        private void AddHeaderNameIndex(int operationalIndex, ref int columnIndex, string headerName)
        {
            if (_headerNamesIndexes.ContainsKey(operationalIndex) && _headerNamesIndexes[operationalIndex] == headerName)
            {
                return;
            }

            if (_headerNamesIndexes.ContainsKey(operationalIndex))
            {
                _headerNamesIndexes[operationalIndex] += $" {headerName}";
            }
            else
            {
                _headerNamesIndexes.Add(operationalIndex, headerName);
            }
        }

        private int GetColSpan(HtmlNode headerCell)
        {
            string colSpanText = headerCell.GetAttributeValue("colspan", null);
            return colSpanText == null ? 0 : int.Parse(colSpanText);
        }

        private int GetRowSpan(HtmlNode headerCell)
        {
            string rowSpanText = headerCell.GetAttributeValue("rowspan", null);

            return rowSpanText == null ? 0 : int.Parse(rowSpanText);
        }
    }
}