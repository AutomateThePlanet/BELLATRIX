// <copyright file="ComponentPromptRepositoryExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.LLM.Android;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android;

public static class ComponentPromptRepositoryExtensions
{
    public static TComponent CreateByPrompt<TComponent>(this ComponentCreateService repo, string instruction)
    where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindByPrompt, AndroidDriver, AppiumElement>(new FindByPrompt(instruction));
    public static ComponentsList<TComponent, FindByPrompt, AndroidDriver, AppiumElement> CreateAllById<TComponent>(this ComponentCreateService repo, string instruction)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindByPrompt, AndroidDriver, AppiumElement>(new FindByPrompt(instruction), null);

}