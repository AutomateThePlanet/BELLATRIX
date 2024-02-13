// <copyright file="ElementCreateExtensions.cs" company="Automate The Planet Ltd.">
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
namespace Bellatrix.Web.Angular;

public static class ElementCreateExtensions
{
    public static TComponent CreateByNgBinding<TComponent>(this Component element, string binding)
        where TComponent : Component => element.Create<TComponent, ByNgBinding>(new ByNgBinding(binding));

    public static TComponent CreateByNgModel<TComponent>(this Component element, string model)
        where TComponent : Component => element.Create<TComponent, ByNgModel>(new ByNgModel(model));

    public static TComponent CreateByNgRepeater<TComponent>(this Component element, string repeater)
        where TComponent : Component => element.Create<TComponent, ByNgRepeater>(new ByNgRepeater(repeater));

    public static TComponent CreateByNgSelectedOption<TComponent>(this Component element, string selectedOption)
        where TComponent : Component => element.Create<TComponent, ByNgSelectedOption>(new ByNgSelectedOption(selectedOption));

    public static ComponentsList<TComponent> CreateAllByNgBinding<TComponent>(this Component element, string binding)
        where TComponent : Component => new ComponentsList<TComponent>(new ByNgBinding(binding), element.WrappedElement);

    public static ComponentsList<TComponent> CreateAllByNgModel<TComponent>(this Component element, string model)
        where TComponent : Component => new ComponentsList<TComponent>(new ByNgModel(model), element.WrappedElement);

    public static ComponentsList<TComponent> CreateAllByNgRepeater<TComponent>(this Component element, string repeater)
        where TComponent : Component => new ComponentsList<TComponent>(new ByNgRepeater(repeater), element.WrappedElement);

    public static ComponentsList<TComponent> CreateAllByNgSelectedOption<TComponent>(this Component element, string selectedOption)
        where TComponent : Component => new ComponentsList<TComponent>(new ByNgSelectedOption(selectedOption), element.WrappedElement);
}