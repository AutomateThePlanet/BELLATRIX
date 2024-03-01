// <copyright file="TabsControlTests.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.Tests;

[TestClass]
[Android(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.Tabs1",
    Lifecycle.ReuseIfStarted)]
[AllureSuite("Tabs Control")]
public class TabsControlTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void GetAllTabs_When_CallGetAllWithButtonControl()
    {
        var tabs = App.Components.CreateByIdContaining<Tabs<Button>>("tabs");

        var tabButtons = tabs.GetAll("android.widget.TextView");
        tabButtons[1].Click();

        var resultLabel = App.Components.CreateByIdContaining<Label>("view2");

        resultLabel.ValidateTextIs("tab2");
    }
}