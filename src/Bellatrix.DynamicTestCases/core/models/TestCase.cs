// <copyright file="TestCase.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Collections.Generic;
using System.Text;

namespace Bellatrix.DynamicTestCases;

public class TestCase
{
    public TestCase(string id, string name, string description, string precondition)
    {
        Id = id;
        Name = name;
        Description = description;
        Precondition = precondition;
        TestSteps = new List<TestStep>();
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Precondition { get; set; }
    public IList<TestStep> TestSteps { get; set; }
}
