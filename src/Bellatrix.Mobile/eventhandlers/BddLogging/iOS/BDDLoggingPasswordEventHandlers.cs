// <copyright file="BDDLoggingPasswordEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.EventHandlers.IOS;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.BddLogging.IOS;

public class BDDLoggingPasswordEventHandlers : PasswordEventHandlers
{
    protected override void SettingPasswordEventHandler(object sender, ComponentActionEventArgs<AppiumElement> arg) => Logger.LogInformation($"Type '{arg.ActionValue}' into {arg.Element.ComponentName} on {arg.Element.PageName}");
}