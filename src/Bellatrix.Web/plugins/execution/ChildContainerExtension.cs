// <copyright file="ControlsLocalOverridesCleanExtension.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Plugins;

namespace Bellatrix.Web.Plugins.Browser;

public class ChildContainerExtension : Plugin
{
    protected override void PreTestsArrange(object sender, PluginEventArgs e)
    {
    }

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
    }
}