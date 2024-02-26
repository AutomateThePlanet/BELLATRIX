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
using Bellatrix.Mobile.Controls.Android;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.Tests;

////curl -u angelov.st.anton+sauce:eaa32fdc-f200-4e53-b47f-b93553a8726e -X POST -H "Content-Type: application/octet-stream" https://saucelabs.com/rest/v1/storage/angelov.st.anton+sauce/ApiDemos.apk?overwrite=true --data-binary "D:\SourceCode\AutomateThePlanet\BellatrixTestFramework\Tests\Bellatrix.Mobile.Android.Tests\bin\Debug\netcoreapp2.0\Demos\ApiDemos.apk"
[TestClass]
[Android(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidRealDeviceName,
    ".view.ControlsMaterialDark",
    Lifecycle.RestartEveryTime)]
[AllureSuite("Services")]
[AllureFeature("ComponentCreateService")]
public class ElementCreateSingleElementTests : MSTest.AndroidTest
{
    private AndroidComponent _mainElement;

    public override void TestInit()
    {
        _mainElement = App.Components.CreateByIdContaining<AndroidComponent>("decor_content_parent");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByIdContaining_And_ElementIsOnScreen()
    {
        var button = _mainElement.CreateByIdContaining<Button>("button");

        button.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByIdContaining_And_ElementIsNotOnScreen()
    {
        var textField = _mainElement.CreateByIdContaining<TextField>("edit");

        textField.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateById_And_ElementIsOnScreen()
    {
        var button = _mainElement.CreateById<Button>("com.example.android.apis:id/button");

        button.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateById_And_ElementIsNotOnScreen()
    {
        var textField = _mainElement.CreateById<TextField>("com.example.android.apis:id/edit");

        textField.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByClass()
    {
        var checkBox = _mainElement.CreateByClass<CheckBox>("android.widget.CheckBox");

        checkBox.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByText_And_ElementIsOnScreen()
    {
        var button = _mainElement.CreateByText<Button>("BUTTON");

        button.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByText_And_ElementIsNotOnScreen()
    {
        var textField = _mainElement.CreateByText<TextField>("Text appearances");

        textField.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByTextContaining_And_ElementIsOnScreen()
    {
        var button = _mainElement.CreateByTextContaining<Button>("BUTTO");

        button.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByTextContaining_And_ElementIsNotOnScreen()
    {
        var textField = _mainElement.CreateByTextContaining<TextField>("Text appearanc");

        textField.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByAndroidUIAutomator_And_ElementIsOnScreen()
    {
        var button = _mainElement.CreateByAndroidUIAutomator<Button>("new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceId(\"com.example.android.apis:id/button\"));");

        button.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByAndroidUIAutomator_And_ElementIsNotOnScreen()
    {
        var textField = _mainElement.CreateByAndroidUIAutomator<TextField>("new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceId(\"com.example.android.apis:id/edit\"));");

        textField.ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ElementFound_When_CreateByXPath_And_ElementIsOnScreen()
    {
        var button = _mainElement.CreateByXPath<Button>("//*[@resource-id='com.example.android.apis:id/button']");

        button.ValidateIsVisible();
    }
}