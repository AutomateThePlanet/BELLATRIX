// <copyright file="ElementCreateSingleElementTests.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Controls.IOS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.Tests;

////curl -u angelov.st.anton+sauce:eaa32fdc-f200-4e53-b47f-b93553a8726e -X POST -H "Content-Type: application/octet-stream" https://saucelabs.com/rest/v1/storage/angelov.st.anton+sauce/ApiDemos.apk?overwrite=true --data-binary "D:\SourceCode\AutomateThePlanet\BellatrixTestFramework\Tests\Bellatrix.Mobile.Android.Tests\bin\Debug\netcoreapp2.0\Demos\ApiDemos.apk"
[TestClass]
[IOS(Constants.IOSNativeAppPath,
    Constants.IOSAppBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
[AllureSuite("Services")]
[AllureFeature("ComponentCreateService")]
public class ElementCreateSingleElementTests : MSTest.IOSTest
{
    private IOSComponent _mainElement;

    public override void TestInit()
    {
        _mainElement = App.Components.CreateByIOSNsPredicate<IOSComponent>("type == \"XCUIElementTypeApplication\" AND name == \"TestApp\"");
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateById_And_ElementIsOnScreen()
    {
        var textFields = _mainElement.CreateById<TextField>("IntegerA");

        textFields.ValidateIsVisible();
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByClass()
    {
        var textFields = _mainElement.CreateByClass<TextField>("XCUIElementTypeTextField");

        textFields.ValidateIsNotDisabled();
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByValueContaining_And_ElementIsOnScreen()
    {
        var labels = _mainElement.CreateByValueContaining<Label>("SumLabel");

        labels.ValidateIsVisible();
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByXPath_And_ElementIsOnScreen()
    {
        var button = _mainElement.CreateByXPath<Button>("//XCUIElementTypeButton[@name=\"ComputeSumButton\"]");

        button.ValidateIsVisible();
    }
}
