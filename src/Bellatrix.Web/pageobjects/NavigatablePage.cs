// <copyright file="NavigatablePage.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web;

[Obsolete("Please refactor your pages to use the new WebPage base class which combies the old 4 base classes.")]
public abstract class NavigatablePage : Page
{
    protected NavigatablePage() => NavigationService = ServicesCollection.Current.Resolve<NavigationService>();

    protected NavigationService NavigationService { get; set; }

    public abstract string Url { get; }

    public virtual void Open() => NavigationService.Navigate(new Uri(Url));
}