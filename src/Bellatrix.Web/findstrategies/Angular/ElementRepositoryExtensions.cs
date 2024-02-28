// <copyright file="ElementRepositoryExtensions.cs" company="Automate The Planet Ltd.">
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

public static class ElementRepositoryExtensions
{
    public static TComponent CreateByNgBinding<TComponent>(this ComponentCreateService repository, string binding, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, ByNgBinding>(new ByNgBinding(binding), shouldCacheElement);

    public static TComponent CreateByNgModel<TComponent>(this ComponentCreateService repository, string model, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, ByNgModel>(new ByNgModel(model), shouldCacheElement);

    public static TComponent CreateByNgRepeater<TComponent>(this ComponentCreateService repository, string repeater, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, ByNgRepeater>(new ByNgRepeater(repeater), shouldCacheElement);

    public static TComponent CreateByNgSelectedOption<TComponent>(this ComponentCreateService repository, string selectedOption, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, ByNgSelectedOption>(new ByNgSelectedOption(selectedOption), shouldCacheElement);

    public static ComponentsList<TComponent> CreateAllByNgBinding<TComponent>(this ComponentCreateService repository, string binding, bool shouldCacheElement = false)
        where TComponent : Component => new ComponentsList<TComponent>(new ByNgBinding(binding), null, shouldCacheElement);

    public static ComponentsList<TComponent> CreateAllByNgModel<TComponent>(this ComponentCreateService repository, string model, bool shouldCacheElement = false)
        where TComponent : Component => new ComponentsList<TComponent>(new ByNgModel(model), null, shouldCacheElement);

    public static ComponentsList<TComponent> CreateAllByNgRepeater<TComponent>(this ComponentCreateService repository, string repeater, bool shouldCacheElement = false)
        where TComponent : Component => new ComponentsList<TComponent>(new ByNgRepeater(repeater), null, shouldCacheElement);

    public static ComponentsList<TComponent> CreateAllByNgSelectedOption<TComponent>(this ComponentCreateService repository, string selectedOption, bool shouldCacheElement = false)
        where TComponent : Component => new ComponentsList<TComponent>(new ByNgSelectedOption(selectedOption), null, shouldCacheElement);
}