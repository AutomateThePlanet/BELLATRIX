// <copyright file="ComponentWaitService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Events;
using Bellatrix.Web.Untils;

namespace Bellatrix.Web.Waits;

public class ComponentWaitService
{
    public static event EventHandler<ElementNotFulfillingWaitConditionEventArgs> OnElementNotFulfillingWaitConditionEvent;

    public void Wait<TUntil, TComponent>(TComponent element, TUntil until)
        where TUntil : WaitStrategy
        where TComponent : Component
    {
        try
        {
            if (element.ParentWrappedElement == null)
            {
                WaitInternal(element.By, until);
            }
            else
            {
                var elementRepository = new ComponentRepository();
                Component parenTComponent = elementRepository.CreateComponentThatIsFound<Component>(element.By, element.ParentWrappedElement, true);
                WaitInternal(element.By, until, parenTComponent);
            }
        }
        catch (Exception ex)
        {
            OnElementNotFulfillingWaitConditionEvent?.Invoke(this, new ElementNotFulfillingWaitConditionEventArgs(ex));
            throw;
        }
    }

    internal void WaitInternal<TUntil, TBy>(TBy by, TUntil until)
        where TUntil : WaitStrategy
        where TBy : FindStrategy => until?.WaitUntil(@by);

    internal void WaitInternal<TUntil, TBy>(TBy by, TUntil until, Component parent)
        where TUntil : WaitStrategy
        where TBy : FindStrategy => until?.WaitUntil(@by, parent);
}