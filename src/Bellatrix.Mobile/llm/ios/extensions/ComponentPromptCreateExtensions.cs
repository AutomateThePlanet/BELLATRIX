// <copyright file="ComponentPromptCreateExtensions.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
// <note>This file is part of an academic research project exploring autonomous test agents using LLMs and Semantic Kernel.
// The architecture and agent logic are original contributions by Anton Angelov, forming the foundation for a PhD dissertation.
// Please cite or credit appropriately if reusing in academic or commercial work.</note>

using Bellatrix.Mobile.Controls.Core;

namespace Bellatrix.Mobile.LLM.Extensions.Android;

public static class ComponentPromptCreateExtensions
{
    public static TComponent CreateByIdEndingWith<TComponent>(this Component element, string instruction)
        where TComponent : Component => element.Create<TComponent, FindByPrompt>(new FindByPrompt(instruction));

    public static ComponentsList<TComponent> CreateAllByPrompt<TComponent>(this Component component, string instruction, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindByPrompt(instruction), null, shouldCacheFoundElements);
}