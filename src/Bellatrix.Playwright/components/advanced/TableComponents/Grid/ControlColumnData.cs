﻿// <copyright file="ControlColumnData.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Playwright;

public class ControlColumnData : IHeaderInfo
{
    public ControlColumnData(string headerName, int order = 0)
    {
        HeaderName = headerName;
        Order = order;
    }

    public ControlColumnData(string headerName, dynamic by, Type elementType, int order = 0)
        : this(headerName)
    {
        By = by;
        ComponentType = elementType;
        Order = order;
    }

    public string HeaderName { get; set; }
    public int Order { get; set; }
    public dynamic By { get; set; }
    public Type ComponentType { get; set; }
}