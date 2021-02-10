// <copyright file="ElementRepositoryExtensions.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web.Angular
{
    public static class ElementRepositoryExtensions
    {
        public static TElement CreateByNgBinding<TElement>(this ElementCreateService repository, string binding, bool shouldCacheElement = false)
            where TElement : Element => repository.Create<TElement, ByNgBinding>(new ByNgBinding(binding), shouldCacheElement);

        public static TElement CreateByNgModel<TElement>(this ElementCreateService repository, string model, bool shouldCacheElement = false)
            where TElement : Element => repository.Create<TElement, ByNgModel>(new ByNgModel(model), shouldCacheElement);

        public static TElement CreateByNgRepeater<TElement>(this ElementCreateService repository, string repeater, bool shouldCacheElement = false)
            where TElement : Element => repository.Create<TElement, ByNgRepeater>(new ByNgRepeater(repeater), shouldCacheElement);

        public static TElement CreateByNgSelectedOption<TElement>(this ElementCreateService repository, string selectedOption, bool shouldCacheElement = false)
            where TElement : Element => repository.Create<TElement, ByNgSelectedOption>(new ByNgSelectedOption(selectedOption), shouldCacheElement);

        public static ElementsList<TElement> CreateAllByNgBinding<TElement>(this ElementCreateService repository, string binding, bool shouldCacheElement = false)
            where TElement : Element => new ElementsList<TElement>(new ByNgBinding(binding), null, shouldCacheElement);

        public static ElementsList<TElement> CreateAllByNgModel<TElement>(this ElementCreateService repository, string model, bool shouldCacheElement = false)
            where TElement : Element => new ElementsList<TElement>(new ByNgModel(model), null, shouldCacheElement);

        public static ElementsList<TElement> CreateAllByNgRepeater<TElement>(this ElementCreateService repository, string repeater, bool shouldCacheElement = false)
            where TElement : Element => new ElementsList<TElement>(new ByNgRepeater(repeater), null, shouldCacheElement);

        public static ElementsList<TElement> CreateAllByNgSelectedOption<TElement>(this ElementCreateService repository, string selectedOption, bool shouldCacheElement = false)
            where TElement : Element => new ElementsList<TElement>(new ByNgSelectedOption(selectedOption), null, shouldCacheElement);
    }
}