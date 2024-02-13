// <copyright file="QTestDynamicTestCasesSettings.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using QASymphony.QTest;

namespace Bellatrix.DynamicTestCases.QTest;

public class QTestDynamicTestCasesSettings
{
    public bool IsEnabled { get; set; }
    public string ServiceAddress { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ProjectId { get; set; }
    public List<FieldValue> FieldValues { get; set; }

    public bool TestCaseLinking { get; set; }
}