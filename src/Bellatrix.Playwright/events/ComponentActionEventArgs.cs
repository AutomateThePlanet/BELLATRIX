﻿// <copyright file="ComponentActionEventArgs.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Playwright.Contracts;

namespace Bellatrix.Playwright.Events;

public class ComponentActionEventArgs
{
    public ComponentActionEventArgs(IComponent element) => Element = element;

    public ComponentActionEventArgs(IComponent element, string actionValue)
        : this(element) => ActionValue = actionValue;

    public IComponent Element { get; }
    public string ActionValue { get; }
}