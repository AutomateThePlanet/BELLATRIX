// <copyright file="VisualRegressionTrackerSettings.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.VRT;

public class VisualRegressionTrackerSettings
{
    public string? ApiUrl { get; set; }
    public string? ApiKey { get; set; }
    public string? Project {  get; set; }
    public string? Branch { get; set; }
    public bool EnableSoftAssert { get; set; } = false;
    public string? CiBuildId { get; set; }
}
