// <copyright file="ElementsList.cs" company="Automate The Planet Ltd.">
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

using System.Collections;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Services;

namespace Bellatrix.Playwright;

public class ComponentsList<TComponent> : IEnumerable<TComponent>
    where TComponent : Component
{
    private readonly List<TComponent> _components;

    public ComponentsList(FindStrategy by, WebElement parenTComponent)
    {
        _components = InitializeComponents(by, parenTComponent);
        WrappedBrowser = ServicesCollection.Current.Resolve<WrappedBrowser>();
    }

    public ComponentsList(FindStrategy by)
    {
        _components = InitializeComponents(by);
        WrappedBrowser = ServicesCollection.Current.Resolve<WrappedBrowser>();
    }

    public ComponentsList()
    {
        _components = new List<TComponent>();
        WrappedBrowser = ServicesCollection.Current.Resolve<WrappedBrowser>();
    }

    public ComponentsList(IEnumerable<TComponent> componentList)
    {
        _components = new List<TComponent>(componentList);
        WrappedBrowser = ServicesCollection.Current.Resolve<WrappedBrowser>();
    }

    public WrappedBrowser WrappedBrowser { get; }

    public TComponent this[int i]
    {
        get
        {
            try
            {
                return GetComponents().ElementAt(i);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new Exception($"Component at position {i} was not found.", ex);
            }
        }
    }

    public IEnumerator<TComponent> GetEnumerator() => GetComponents().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count() => _components.Count;

    public void ForEach(Action<TComponent> action)
    {
        foreach (var tableRow in this)
        {
            action(tableRow);
        }
    }

    public void Add(TComponent element)
    {
        _components.Add(element);
    }

    private IEnumerable<TComponent> GetComponents()
    {
        foreach (var component in _components)
        {
            yield return component;
        }
    }

    private List<TComponent> InitializeComponents(FindStrategy by, WebElement parenTComponent)
    {
        var list = new List<TComponent>();
        var webElements = by.Resolve(parenTComponent).All();
        foreach (var element in webElements)
        {
            list.Add(ServicesCollection.Current.Resolve<ComponentRepository>().CreateComponentWithParent<TComponent>(by, parenTComponent));
        }

        return list;
    }

    private List<TComponent> InitializeComponents(FindStrategy by)
    {
        var list = new List<TComponent>();
        var webElements = by.Resolve(WrappedBrowser.CurrentPage).All();
        foreach (var element in webElements)
        {
            list.Add(ServicesCollection.Current.Resolve<ComponentRepository>().CreateComponent<TComponent>(by));
        }

        return list;
    }

    public void AddRange(List<TComponent> currentFilteredCells)
    {
        _components.AddRange(currentFilteredCells);
    }
}