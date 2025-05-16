// <copyright file="LocatorMapperSkill.cs" company="Automate The Planet Ltd.">
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
using Microsoft.SemanticKernel;

namespace Bellatrix.Web.LLM.Plugins;
public class LocatorMapperSkill
{
    [KernelFunction]
    public string MatchPromptToKnownLocator(string pageSummary, string instruction)
    {
        return $"""
You are an AI assistant that maps user instructions to known UI locators.

User wants to: {instruction}

From the list of known locators below, pick the one that best matches and return its exact locator in the format:
locator_strategy=locator_value

Known Locators:
{pageSummary}

Examples:
For login button → return xpath=//button[@id='login']
For search input → return id=input-search

Return ONLY one valid locator in that format. If no match is found, return Unknown.
""";
    }


}
