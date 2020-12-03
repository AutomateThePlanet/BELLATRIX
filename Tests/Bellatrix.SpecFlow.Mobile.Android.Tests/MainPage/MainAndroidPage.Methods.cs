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
using Bellatrix.Mobile.PageObjects.Android;
using Bellatrix.Mobile.Services.Android;

namespace Bellatrix.SpecFlow.Mobile.Android.Tests
{
    public partial class MainAndroidPage : AssertedNavigatablePage
    {
        private AndroidKeyboardService _keyboardService;

        public MainAndroidPage(AndroidKeyboardService androidKeyboardService) => _keyboardService = androidKeyboardService;

        public void TransferItem(string itemToBeTransferred, string userName, string password)
        {
            _keyboardService.HideKeyboard();
            PermanentTransfer.Check();
            Items.SelectByText(itemToBeTransferred);
            ReturnItemAfter.ToExists().WaitToBe();
            UserName.SetText(userName);
            Password.SetPassword(password);
            KeepMeLogged.Click();
            Transfer.Click();
        }

        protected override string ActivityName => ".view.Controls1";
        protected override string PackageName => Constants.AndroidNativeAppAppExamplePackage;
    }
}