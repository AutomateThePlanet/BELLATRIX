// <copyright file="WaitStrategyComponentsExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Untils;

namespace Bellatrix.Desktop;

public static class WaitStrategyComponentsExtensions
{
    public static TComponentType ToExists<TComponentType>(this TComponentType element, int? timeoutInterval = null, int? sleepInterval = null)
        where TComponentType : Component
    {
        var until = new WaitToExistStrategy(timeoutInterval, sleepInterval);
        element.EnsureState(until);
        return element;
    }

    public static TComponentType ToNotExists<TComponentType>(this TComponentType element, int? timeoutInterval = null, int? sleepInterval = null)
       where TComponentType : Component
    {
        var until = new WaitNotExistStrategy(timeoutInterval, sleepInterval);
        element.EnsureState(until);
        return element;
    }

    public static TComponentType ToBeVisible<TComponentType>(this TComponentType element, int? timeoutInterval = null, int? sleepInterval = null)
      where TComponentType : Component
    {
        var until = new WaitToBeVisibleStrategy(timeoutInterval, sleepInterval);
        element.EnsureState(until);
        return element;
    }

    public static TComponentType ToNotBeVisible<TComponentType>(this TComponentType element, int? timeoutInterval = null, int? sleepInterval = null)
     where TComponentType : Component
    {
        var until = new WaitNotBeVisibleStrategy(timeoutInterval, sleepInterval);
        element.EnsureState(until);
        return element;
    }

    public static TComponentType ToBeClickable<TComponentType>(this TComponentType element, int? timeoutInterval = null, int? sleepInterval = null)
     where TComponentType : Component
    {
        var until = new WaitToBeClickable(timeoutInterval, sleepInterval);
        element.EnsureState(until);
        return element;
    }

    public static TComponentType ToHasContent<TComponentType>(this TComponentType element, int? timeoutInterval = null, int? sleepInterval = null)
     where TComponentType : Component
    {
        var until = new WaitToHaveContentStrategy(timeoutInterval, sleepInterval);
        element.EnsureState(until);
        return element;
    }
}
