// <copyright file="ControlDataHandlerResolver.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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

using Bellatrix.Playwright.Controls.Advanced.ControlDataHandlers;

namespace Bellatrix.Playwright;

public static class ControlDataHandlerResolver
{
    public static dynamic ResolveReadonlyDataHandler(Type controlType)
    {
        Type controlDataHandler = typeof(IReadonlyControlDataHandler<>);
        Type typeSpecificControlDataHandler = controlDataHandler.MakeGenericType(controlType);
        dynamic resolvedControlDataHandler = ServicesCollection.Current.Resolve(typeSpecificControlDataHandler);

        if (resolvedControlDataHandler == null)
        {
            resolvedControlDataHandler = ResolveEditableDataHandler(controlType);
        }

        if (resolvedControlDataHandler == null)
        {
            throw new Exception($"Cannot find proper IControlDataHandler for type: {controlType.Name}. Make sure it is registered in the ServiceContainer");
        }

        return resolvedControlDataHandler;
    }

    public static dynamic ResolveEditableDataHandler(Type controlType)
    {
        Type editableControlDataHandler = typeof(IEditableControlDataHandler<>);
        Type typeSpecificEditableControlDataHandler = editableControlDataHandler.MakeGenericType(controlType);
        dynamic resolvedControlDataHandler = ServicesCollection.Current.Resolve(typeSpecificEditableControlDataHandler);

        return resolvedControlDataHandler;
    }
}
