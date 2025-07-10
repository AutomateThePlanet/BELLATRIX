﻿// <copyright file="WeekControlDataHandler.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Globalization;

namespace Bellatrix.Playwright.Controls.Advanced.ControlDataHandlers;

public class WeekControlDataHandler : IEditableControlDataHandler<Week>
{
    public dynamic GetData(Week element) => element.GetWeek();

    public void SetData(Week element, string data)
    {
        DateTime valueToSet = DateTime.Parse(data);
        element.SetWeek(valueToSet.Year, CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(valueToSet, CalendarWeekRule.FirstDay, DayOfWeek.Monday));
    }

    public void ValidateValueIs(Week element, string expectedValue) => element.ValidateWeekIs(expectedValue);
}