// <copyright file="GridExtensions.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using System.Reflection;

namespace Bellatrix.Web;

public static class GridExtensions
{
    public static TGrid SetColumn<TGrid>(this TGrid table, string headerName, TGrid x = null)
    where TGrid : Grid
    {
        if (table.ControlColumnDataCollection == null)
        {
            table.ControlColumnDataCollection = new List<IHeaderInfo>();
        }

        table.ControlColumnDataCollection.Add(new ControlColumnData(headerName));

        return table;
    }

    public static TGrid SetColumns<TGrid>(this TGrid table, List<string> headerNames, TGrid x = null)
        where TGrid : Grid
    {
        if (headerNames == null)
        {
            return table;
        }

        if (table.ControlColumnDataCollection == null)
        {
            table.ControlColumnDataCollection = new List<IHeaderInfo>();
        }

        foreach (var headerName in headerNames)
        {
            table.ControlColumnDataCollection.Add(new ControlColumnData(headerName));
        }

        return table;
    }

    public static TGrid SetColumn<TGrid>(this TGrid table, string headerName, Type elementType, TGrid x = null)
    where TGrid : Grid
    {
        if (table.ControlColumnDataCollection == null)
        {
            table.ControlColumnDataCollection = new List<IHeaderInfo>();
        }

        table.ControlColumnDataCollection.Add(new ControlColumnData(headerName, null, elementType));

        return table;
    }

    public static TGrid SetColumn<TGrid, TBy>(this TGrid table, string headerName, Type elementType, TBy controlInnerLocator, TGrid x = null)
    where TGrid : Grid
    where TBy : FindStrategy
    {
        if (table.ControlColumnDataCollection == null)
        {
            table.ControlColumnDataCollection = new List<IHeaderInfo>();
        }

        table.ControlColumnDataCollection.Add(new ControlColumnData(headerName, controlInnerLocator, elementType));

        return table;
    }

    public static Grid SetModelColumns<TGridModel>(this Grid grid)
          where TGridModel : class
    {
        grid.ControlColumnDataCollection = new List<IHeaderInfo>();
        foreach (PropertyInfo property in typeof(TGridModel).GetProperties())
        {
            var headerNameAttribute = (HeaderNameAttribute)property.GetCustomAttributes(typeof(HeaderNameAttribute), false).FirstOrDefault();
            var headerName = headerNameAttribute != null ? headerNameAttribute.Name : property.Name;

            grid.ControlColumnDataCollection.Add(new ControlColumnData(headerName));
        }

        return grid;
    }
}