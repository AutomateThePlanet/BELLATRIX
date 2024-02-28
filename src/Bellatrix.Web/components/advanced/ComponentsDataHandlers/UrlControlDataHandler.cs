// <copyright file="UrlControlDataHandler.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Assertions;

namespace Bellatrix.Web.Controls.Advanced.ControlDataHandlers;

public class UrlControlDataHandler : IEditableControlDataHandler<Url>
{
    public dynamic GetData(Url element) => element.GetUrl();

    public void SetData(Url element, string data)
    {
        try
        {
            element.SetUrl(data);
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Exception occured while trying to set Url value: {data}", e);
        }
    }

    public void ValidateValueIs(Url element, string expectedValue) => element.ValidateUrlIs(expectedValue);
}