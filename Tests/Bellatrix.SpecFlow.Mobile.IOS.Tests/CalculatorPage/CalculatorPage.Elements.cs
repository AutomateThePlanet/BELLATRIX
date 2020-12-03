// <copyright file="CalculatorPage.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.IOS;

namespace Bellatrix.SpecFlow.Mobile.IOS.Tests
{
    public partial class CalculatorPage
    {
        public Button Compute => Element.CreateByName<Button>("ComputeSumButton");
        public TextField NumberOne => Element.CreateById<TextField>("IntegerA");
        public TextField NumberTwo => Element.CreateById<TextField>("IntegerB");
        public Label Answer => Element.CreateByName<Label>("Answer");
    }
}