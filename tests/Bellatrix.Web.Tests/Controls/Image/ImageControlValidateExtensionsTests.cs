// <copyright file="ImageControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Image Control")]
[AllureFeature("ValidateExtensions")]
public class ImageControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ImageLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateSrcIs_DoesNotThrowException_When_DefaultSrcIsSet()
    {
        var imageElement = App.Components.CreateById<Image>("myImage");

        imageElement.ValidateSrcIs("https://bellatrix.solutions/assets/uploads/2017/09/logo.png");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateSrcIsNotNull_DoesNotThrowException_When_DefaultSrcIsSet()
    {
        var imageElement = App.Components.CreateById<Image>("myImage");

        imageElement.ValidateSrcIsNotNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateHeightIsNotNull_DoesNotThrowException_When_HeightAttributeIsNotPresent()
    {
        var imageElement = App.Components.CreateById<Image>("myImage");

        imageElement.ValidateHeightIsNotNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateWidthIsNotNull_DoesNotThrowException_When_WidthAttributeIsNotPresent()
    {
        var imageElement = App.Components.CreateById<Image>("myImage");

        imageElement.ValidateWidthIsNotNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAltIs_DoesNotThrowException_When_AltAttributePresent()
    {
        var imageElement = App.Components.CreateById<Image>("myImage");

        imageElement.ValidateAltIs("MDN");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateSrcSetIs_DoesNotThrowException_When_SrcSetAttributePresent()
    {
        var imageElement = App.Components.CreateById<Image>("myImage1");

        imageElement.ValidateSrcSetIs("mdn-logo-HD.png 2x");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateSizesIs_DoesNotThrowException_When_SizesAttributePresent()
    {
        var imageElement = App.Components.CreateById<Image>("myImage2");

        imageElement.ValidateSizesIs("(min-width: 600px) 200px, 50vw");
    }
}