﻿// <copyright file="RadioButtonControlDataHandler.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Playwright.Controls.Advanced.ControlDataHandlers;

public class RadioButtonControlDataHandler : IEditableControlDataHandler<RadioButton>
{
    public dynamic GetData(RadioButton element) => element.IsChecked;

    public void SetData(RadioButton element, string data)
    {
        if (bool.Parse(data))
        {
            if (!element.IsChecked)
            {
                element.Check();
            }
        }
        else
        {
            if (element.IsChecked)
            {
                element.Check();
            }
        }
    }

    public void ValidateValueIs(RadioButton element, string expectedValue)
    {
        if (bool.Parse(expectedValue))
        {
            element.ValidateIsChecked();
        }
        else
        {
            element.ValidateIsNotChecked();
        }
    }
}