// <copyright file="TableExtensions.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web;

public static class TableExtensions
{
    public static TTable SetColumn<TTable>(this TTable table, string headerName, TTable x = null)
        where TTable : Table
    {
        if (table.ColumnHeaderNames == null)
        {
            table.ColumnHeaderNames = new List<HeaderInfo>();
        }

        table.ColumnHeaderNames.Add(new HeaderInfo(headerName));

        return table;
    }

    public static TTable SetColumns<TTable>(this TTable table, List<HeaderInfo> headerNames, TTable x = null)
        where TTable : Table
    {
        if (table.ColumnHeaderNames == null)
        {
            table.ColumnHeaderNames = new List<HeaderInfo>();
        }

        table.ColumnHeaderNames.AddRange(headerNames);

        return table;
    }
}