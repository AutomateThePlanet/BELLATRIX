// <copyright file="KeyboardServiceTests.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;
using BA = Bellatrix.Assertions;

namespace Bellatrix.Mobile.Android.Tests
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".app.CustomTitle",
        Lifecycle.ReuseIfStarted)]
    [AllureSuite("Services")]
    [AllureFeature("KeyboardService")]
    public class KeyboardServiceTests : MSTest.AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void TestHideKeyBoard()
        {
            var textField = App.ElementCreateService.CreateByIdContaining<TextField>("left_text_edit");
            textField.SetText(string.Empty);

            App.KeyboardService.HideKeyboard();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PressKeyCodeTest()
        {
            App.KeyboardService.PressKeyCode(AndroidKeyCode.Home);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PressKeyCodeWithMetastateTest()
        {
            App.KeyboardService.PressKeyCode(AndroidKeyCode.Space, AndroidKeyMetastate.Meta_Shift_On);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void LongPressKeyCodeTest()
        {
            App.KeyboardService.LongPressKeyCode(AndroidKeyCode.Home);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void LongPressKeyCodeWithMetastateTest()
        {
            App.KeyboardService.LongPressKeyCode(AndroidKeyCode.Space, AndroidKeyMetastate.Meta_Shift_On);
        }
    }
}
