// <copyright file="MainDesktopPage.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop;

namespace Bellatrix.SpecFlow.Desktop.Tests
{
    public partial class MainDesktopPage
    {
        public Button Transfer => Element.CreateByName<Button>("E Button");
        public CheckBox PermanentTransfer => Element.CreateByName<CheckBox>("BellaCheckBox");
        public ComboBox Items => Element.CreateByAutomationId<ComboBox>("select");
        public Button ReturnItemAfter => Element.CreateByName<Button>("DisappearAfterButton1");
        public Label Results => Element.CreateByAutomationId<Label>("ResultLabelId");
        public Password Password => Element.CreateByAutomationId<Password>("passwordBox");
        public TextField UserName => Element.CreateByAutomationId<TextField>("textBox");
        public RadioButton KeepMeLogged => Element.CreateByName<RadioButton>("RadioButton");
    }
}