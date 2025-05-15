﻿// <copyright file="CheckBoxControlDataHandler.cs" company="Automate The Planet Ltd.">
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

using System;

namespace Bellatrix.Web.Controls.Advanced.ControlDataHandlers;

public class CheckBoxControlDataHandler : IEditableControlDataHandler<CheckBox>
{
    public dynamic GetData(CheckBox element)
    {
        try
        {
            return element.IsChecked;
        }
        catch (TimeoutException)
        {
            return null;
        }
    }

    public void SetData(CheckBox element, string data)
    {
        if (bool.TryParse(data, out bool valueToSet))
        {
            if (valueToSet)
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
                    element.Uncheck();
                }
            }
        }
        else
        {
            throw new ArgumentException($"The input string '{data}' was not recognized as valid boolean.");
        }
    }

    public void ValidateValueIs(CheckBox element, string expectedValue)
    {
        if (bool.TryParse(expectedValue, out bool expectedBool))
        {
            if (expectedBool)
            {
                element.ValidateIsChecked();
            }
            else
            {
                element.ValidateIsNotChecked();
            }
        }
        else
        {
            throw new ArgumentException($"The input string '{expectedValue}' was not recognized as valid boolean.");
        }
    }
}