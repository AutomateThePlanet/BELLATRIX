// <copyright file="AndroidKeyboardServiceSteps.cs" company="Automate The Planet Ltd.">
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
using System;
using Bellatrix.SpecFlow.Mobile;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Mobile.Android
{
    [Binding]
    public class AndroidKeyboardServiceSteps : AndroidSteps
    {
        [When(@"I hide the android app keyboard")]
        public void WhenIHideKeyboard()
        {
            App.KeyboardService.HideKeyboard();
        }

        [When(@"I long press key (.*) with meta key (.*)")]
        public void WhenILongPressKeyCode(int keyCode, int metastate = -1)
        {
            App.KeyboardService.LongPressKeyCode(keyCode, metastate);
        }

        [When(@"I press key (.*) with meta key (.*)")]
        public void WhenIPressKeyCode(int keyCode, int metastate = -1)
        {
            App.KeyboardService.PressKeyCode(keyCode, metastate);
        }
    }
}
