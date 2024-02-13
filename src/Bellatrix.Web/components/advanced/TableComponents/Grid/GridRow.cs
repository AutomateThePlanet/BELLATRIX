// <copyright file="GridRow.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Controls.Advanced.Grid;
using Bellatrix.Web.Events;

namespace Bellatrix.Web;

public class GridRow : Component, IComponentInnerHtml
{
    private Grid _parentGrid;

    public static event EventHandler<ComponentActionEventArgs> Clicking;
    public static event EventHandler<ComponentActionEventArgs> Clicked;

    public int Index { get; set; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string InnerHtml => GetInnerHtmlAttribute();

    public GridCell this[int i] => GetCells().ToList().ElementAt(i);

    public void Click()
    {
        Clicking?.Invoke(this, new ComponentActionEventArgs(this));
        WrappedElement.Click();
        Clicked?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public void SetParentGrid(Grid grid)
    {
        _parentGrid = grid;
    }

    public GridCell GetCell(int column)
    {
        return _parentGrid.GetCell(Index, column);
    }

    public GridCell GetCell(string headerName)
    {
        return _parentGrid.GetCell(headerName, Index);
    }

    public GridCell GetCell<TDto>(Expression<Func<TDto, object>> expression)
        where TDto : class
    {
        return _parentGrid.GetCell<TDto>(expression, Index);
    }

    public IEnumerable<GridCell> GetCells()
    {
        var listOfCells = new List<GridCell>();
        var rowCells = _parentGrid.TableService.GetRowCells(Index);
        for (int rowCellsIndex = 0; rowCellsIndex < rowCells.Count; rowCellsIndex++)
        {
            var rowCellXPath = rowCells[rowCellsIndex].GetXPath();
            var cell = ComponentCreateService.CreateByXpath<GridCell>(rowCellXPath);
            _parentGrid.SetCellMetaData(cell, Index, rowCellsIndex);
            listOfCells.Add(cell);
        }

        return listOfCells;
    }

    public IEnumerable<TComponent> GetCells<TComponent>()
        where TComponent : Component, new()
    {
        var listOfElements = new ComponentsList<TComponent>();
        var cells = GetCells().ToList();
        for (int columnIndex = 0; columnIndex < cells.Count; columnIndex++)
        {
            var cell = cells[columnIndex];
            TComponent element = new TComponent();
            if (cell.CellControlComponentType == null)
            {
                listOfElements.Add(cell.As<TComponent>());
            }
            else
            {
                var repo = new ComponentRepository();
                element = repo.CreateComponentWithParent(cell.CellControlBy, cell.WrappedElement, typeof(TComponent), false);
                listOfElements.Add(element);
            }
        }

        return listOfElements;
    }

    public List<TComponent> GetCells<TComponent>(Func<TComponent, bool> selector)
        where TComponent : Component, new()
    {
        return GetCells<TComponent>().Where(selector).ToList();
    }

    public TComponent GetFirstOrDefaultCell<TComponent>(Func<TComponent, bool> selector)
        where TComponent : Component, new()
    {
        return GetCells(selector).FirstOrDefault();
    }

    public T GetItem<T>(params string[] propertiesToSkip)
        where T : new()
    {
        return _parentGrid.CastRow<T>(Index, propertiesToSkip);
    }

    public void AssertRow<T>(T expectedItem, params string[] propertiesNotToCompare)
        where T : new()
    {
        var actualItem = GetItem<T>(propertiesNotToCompare);

        EntitiesAsserter.AreEqual(expectedItem, actualItem, propertiesNotToCompare);
    }

    public void AssertRow<T>(T expectedItem)
        where T : new()
    {
        var propsNotToCompare = expectedItem
                .GetType()
                .GetProperties()
                .Where(p => p.GetValue(expectedItem) == null)
                .Select(n => _parentGrid.HeaderNamesService.GetHeaderNameByProperty(n))
                .ToArray();

        AssertRow(expectedItem, propsNotToCompare);
    }

    internal void DefaultClick<TComponent>(
        TComponent element,
        EventHandler<ComponentActionEventArgs> clicking,
        EventHandler<ComponentActionEventArgs> clicked)
        where TComponent : Component
    {
        clicking?.Invoke(this, new ComponentActionEventArgs(element));

        element.ToExists().ToBeClickable().WaitToBe();
        element.WrappedElement.Click();

        clicked?.Invoke(this, new ComponentActionEventArgs(element));
    }
}