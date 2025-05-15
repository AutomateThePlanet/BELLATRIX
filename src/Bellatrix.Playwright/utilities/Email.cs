﻿// <copyright file="Email.cs" company="Automate The Planet Ltd.">
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

using System.Text.Json.Serialization;

namespace Bellatrix.Playwright.Utilities;

public class Email
{
    [JsonPropertyName("tpl")]
    public string Tpl { get; set; }

    [JsonPropertyName("to")]
    public string To { get; set; }

    [JsonPropertyName("subject")]
    public string Subject { get; set; }

    [JsonPropertyName("bcc")]
    public string Bcc { get; set; }

    [JsonPropertyName("html")]
    public string Html { get; set; }
}
