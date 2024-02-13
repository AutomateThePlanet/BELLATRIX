// <copyright file="ComponentExtensions.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web.Services.Angular;

public static class ComponentExtensions
{
    private static JavaScriptService _javaScriptService;
    private static BrowserService _browserService;

    public static object Evaluate<TComponent>(this TComponent element, string expression)
        where TComponent : Component
    {
        _javaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
        _browserService = ServicesCollection.Current.Resolve<BrowserService>();
        _browserService.WaitForAngular();

        return _javaScriptService.Execute(AngularClientSideScripts.Evaluate, element, expression);
    }
}
