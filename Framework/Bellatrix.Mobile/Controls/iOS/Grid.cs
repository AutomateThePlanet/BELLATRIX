// <copyright file="Grid.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Controls.IOS;
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Locators.IOS;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS
{
    public class Grid<TElement> : Element
            where TElement : Element<IOSDriver<IOSElement>, IOSElement>
    {
        public static Func<Grid<TElement>, string, ElementsList<TElement, ByClassName, IOSDriver<IOSElement>, IOSElement>> OverrideGetAllGlobally;

        public static Func<Grid<TElement>, string, ElementsList<TElement, ByClassName, IOSDriver<IOSElement>, IOSElement>> OverrideGetAllLocally;

        public static new void ClearLocalOverrides()
        {
            OverrideGetAllLocally = null;
        }

        public ElementsList<TElement, ByClassName, IOSDriver<IOSElement>, IOSElement> GetAll(string searchClass)
        {
            var action = InitializeFunction(this, OverrideGetAllGlobally, OverrideGetAllLocally, DefaultGetAllOptions);
            return action(searchClass);
        }

        protected virtual ElementsList<TElement, ByClassName, IOSDriver<IOSElement>, IOSElement> DefaultGetAllOptions(Grid<TElement> tabs, string searchClass)
        {
            var elements = tabs.CreateAllByClass<TElement>(searchClass);

            return elements;
        }
    }
}