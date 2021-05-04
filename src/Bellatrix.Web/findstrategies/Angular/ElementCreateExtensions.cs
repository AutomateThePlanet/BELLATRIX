// <copyright file="ElementCreateExtensions.cs" company="Automate The Planet Ltd.">
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
    public static class ElementCreateExtensions
    {
        public static TElement CreateByNgBinding<TElement>(this Component element, string binding)
            where TElement : Component => element.Create<TElement, ByNgBinding>(new ByNgBinding(binding));

        public static TElement CreateByNgModel<TElement>(this Component element, string model)
            where TElement : Component => element.Create<TElement, ByNgModel>(new ByNgModel(model));

        public static TElement CreateByNgRepeater<TElement>(this Component element, string repeater)
            where TElement : Component => element.Create<TElement, ByNgRepeater>(new ByNgRepeater(repeater));

        public static TElement CreateByNgSelectedOption<TElement>(this Component element, string selectedOption)
            where TElement : Component => element.Create<TElement, ByNgSelectedOption>(new ByNgSelectedOption(selectedOption));

        public static ComponentsList<TElement> CreateAllByNgBinding<TElement>(this Component element, string binding)
            where TElement : Component => new ComponentsList<TElement>(new ByNgBinding(binding), element.WrappedElement);

        public static ComponentsList<TElement> CreateAllByNgModel<TElement>(this Component element, string model)
            where TElement : Component => new ComponentsList<TElement>(new ByNgModel(model), element.WrappedElement);

        public static ComponentsList<TElement> CreateAllByNgRepeater<TElement>(this Component element, string repeater)
            where TElement : Component => new ComponentsList<TElement>(new ByNgRepeater(repeater), element.WrappedElement);

        public static ComponentsList<TElement> CreateAllByNgSelectedOption<TElement>(this Component element, string selectedOption)
            where TElement : Component => new ComponentsList<TElement>(new ByNgSelectedOption(selectedOption), element.WrappedElement);
    }
}