// <copyright file="GridCell.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Diagnostics;
using Bellatrix.Playwright.Contracts;
using Bellatrix.Playwright.Events;

namespace Bellatrix.Playwright;

public class GridCell : Component, IComponentInnerText, IComponentInnerHtml
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> Clicking;
    public static event EventHandler<ComponentActionEventArgs> Clicked;

    public void Hover()
    {
        Hover(Hovering, Hovered);
    }

    public void Click()
    {
        DefaultClick(Clicking, Clicked);
    }

    public Type CellControlComponentType { get; set; }

    public dynamic CellControlBy { get; set; }

    public int Column { get; set; }

    public int Row { get; set; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string InnerText => GetInnerText();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string InnerHtml => GetInnerHtmlAttribute();

    public override TComponent As<TComponent>()
    {
        CellControlComponentType = typeof(TComponent);
        if (CellControlBy == null)
        {
            object instance = Activator.CreateInstance(CellControlComponentType);
            var byProperty = CellControlComponentType.GetProperty("By");
            byProperty?.SetValue(instance, By, null);

            var wrappedElementProperty = CellControlComponentType.GetProperty("WrappedElement");

            wrappedElementProperty?.SetValue(instance, WrappedElement, null);

            return instance as TComponent;
        }
        else
        {
            return Create(CellControlBy, typeof(TComponent));
        }
    }

    public dynamic As()
    {
        if (CellControlComponentType == null)
        {
            CellControlComponentType = typeof(Label);
        }

        if (CellControlBy == null)
        {
            object instance = Activator.CreateInstance(CellControlComponentType);
            var byProperty = CellControlComponentType.GetProperty("By");
            byProperty?.SetValue(instance, By, null);

            var wrappedElementProperty = CellControlComponentType.GetProperty("WrappedElement");

            wrappedElementProperty?.SetValue(instance, WrappedElement, null);

            return instance;
        }
        else
        {
            return Create(CellControlBy, CellControlComponentType);
        }
    }
}