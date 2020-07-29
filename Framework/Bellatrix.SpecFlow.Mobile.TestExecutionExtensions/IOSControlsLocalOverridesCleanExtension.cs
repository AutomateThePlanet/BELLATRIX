// <copyright file="IOSControlsLocalOverridesCleanExtension.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using Bellatrix.Mobile.Controls.IOS;
using Bellatrix.Mobile.IOS;
using Bellatrix.SpecFlow.TestWorkflowPlugins;

namespace Bellatrix.SpecFlow.Mobile.TestExecutionExtensions
{
    public class IOSControlsLocalOverridesCleanExtension : TestWorkflowPlugin
    {
        protected override void PostAfterScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            Element.ClearLocalOverrides();
            Button.ClearLocalOverrides();
            RadioButton.ClearLocalOverrides();
            CheckBox.ClearLocalOverrides();
            ToggleButton.ClearLocalOverrides();
            TextField.ClearLocalOverrides();
            ComboBox.ClearLocalOverrides();
            Password.ClearLocalOverrides();
            Label.ClearLocalOverrides();
            ImageButton.ClearLocalOverrides();
            Image.ClearLocalOverrides();
            Progress.ClearLocalOverrides();
            Number.ClearLocalOverrides();
            SeekBar.ClearLocalOverrides();
            RadioGroup.ClearLocalOverrides();
        }
    }
}
