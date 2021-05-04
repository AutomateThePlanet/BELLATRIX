// <copyright file="ElementWaitService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Contracts.Services;
using Bellatrix.Desktop.Events;
using Bellatrix.Desktop.Locators;
using Bellatrix.Desktop.Untils;

namespace Bellatrix.Desktop.Services
{
    public class ElementWaitService : IElementWaitService
    {
        public static event EventHandler<ElementNotFulfillingWaitConditionEventArgs> OnElementNotFulfillingWaitConditionEvent;

        public void Wait<TUntil, TElement>(TElement element, TUntil until)
            where TUntil : WaitStrategy
            where TElement : Component
        {
            try
            {
                WaitInternal(element.By, until);
            }
            catch (Exception ex)
            {
                OnElementNotFulfillingWaitConditionEvent?.Invoke(this, new ElementNotFulfillingWaitConditionEventArgs(ex));
                throw;
            }
        }

        public void WaitInternal<TUntil, TBy>(TBy by, TUntil until)
            where TUntil : WaitStrategy
            where TBy : FindStrategy => until?.WaitUntil(@by);
    }
}