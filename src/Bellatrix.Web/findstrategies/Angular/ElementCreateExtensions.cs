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
        public static TElement CreateByNgBinding<TElement>(this Element element, string binding)
            where TElement : Element => element.Create<TElement, ByNgBinding>(new ByNgBinding(binding));

        public static TElement CreateByNgModel<TElement>(this Element element, string model)
            where TElement : Element => element.Create<TElement, ByNgModel>(new ByNgModel(model));

        public static TElement CreateByNgRepeater<TElement>(this Element element, string repeater)
            where TElement : Element => element.Create<TElement, ByNgRepeater>(new ByNgRepeater(repeater));

        public static TElement CreateByNgSelectedOption<TElement>(this Element element, string selectedOption)
            where TElement : Element => element.Create<TElement, ByNgSelectedOption>(new ByNgSelectedOption(selectedOption));

        public static ElementsList<TElement> CreateAllByNgBinding<TElement>(this Element element, string binding)
            where TElement : Element => new ElementsList<TElement>(new ByNgBinding(binding), element.WrappedElement);

        public static ElementsList<TElement> CreateAllByNgModel<TElement>(this Element element, string model)
            where TElement : Element => new ElementsList<TElement>(new ByNgModel(model), element.WrappedElement);

        public static ElementsList<TElement> CreateAllByNgRepeater<TElement>(this Element element, string repeater)
            where TElement : Element => new ElementsList<TElement>(new ByNgRepeater(repeater), element.WrappedElement);

        public static ElementsList<TElement> CreateAllByNgSelectedOption<TElement>(this Element element, string selectedOption)
            where TElement : Element => new ElementsList<TElement>(new ByNgSelectedOption(selectedOption), element.WrappedElement);
    }
}