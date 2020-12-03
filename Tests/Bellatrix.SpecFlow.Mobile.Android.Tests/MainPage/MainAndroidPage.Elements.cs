// <copyright file="MainAndroidPage.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Android;

namespace Bellatrix.SpecFlow.Mobile.Android.Tests
{
    public partial class MainAndroidPage
    {
        public Button Transfer => Element.CreateByIdContaining<Button>("button");
        public CheckBox PermanentTransfer => Element.CreateByIdContaining<CheckBox>("check1");
        public ComboBox Items => Element.CreateByIdContaining<ComboBox>("spinner1");
        public Button ReturnItemAfter => Element.CreateByIdContaining<Button>("toggle1");
        public Label Results => Element.CreateByText<Label>("textColorPrimary");
        public Password Password => Element.CreateByIdContaining<Password>("edit2");
        public TextField UserName => Element.CreateByIdContaining<TextField>("edit");
        public RadioButton KeepMeLogged => Element.CreateByIdContaining<RadioButton>("radio2");
    }
}