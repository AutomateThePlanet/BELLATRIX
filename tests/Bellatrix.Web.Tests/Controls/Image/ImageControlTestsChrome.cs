// <copyright file="ImageControlTestsChrome.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
[AllureSuite("Image Control")]
public class ImageControlTestsChrome : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ImageLocalPage);

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetSrc_When_SrcAttributeIsSet_Chrome()
    {
        var imageElement = App.Components.CreateById<Image>("myImage");

        Assert.AreEqual("https://bellatrix.solutions/assets/uploads/2017/09/logo.png", imageElement.Src);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetHeight_When_HeightAttributeIsSet_Chrome()
    {
        var imageElement = App.Components.CreateById<Image>("myImage3");

        Assert.IsNotNull(imageElement.Height);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetWidth_When_WidthAttributeIsSet_Chrome()
    {
        var imageElement = App.Components.CreateById<Image>("myImage3");

        Assert.IsNotNull(imageElement.Width);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetHeight_When_HeightAttributeIsNotPresent_Chrome()
    {
        var imageElement = App.Components.CreateById<Image>("myImage");

        Assert.IsNotNull(imageElement.Height);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetWidth_When_WidthAttributeIsNotPresent_Chrome()
    {
        var imageElement = App.Components.CreateById<Image>("myImage");

        Assert.IsNotNull(imageElement.Width);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetAlt_When_AltAttributePresent_Chrome()
    {
        var imageElement = App.Components.CreateById<Image>("myImage");

        Assert.AreEqual("MDN", imageElement.Alt);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetSrcSet_When_SrcSetAttributePresent_Chrome()
    {
        var imageElement = App.Components.CreateById<Image>("myImage1");

        Assert.AreEqual("mdn-logo-HD.png 2x", imageElement.SrcSet);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetSizes_When_SizesAttributePresent_Chrome()
    {
        var imageElement = App.Components.CreateById<Image>("myImage2");

        Assert.AreEqual("(min-width: 600px) 200px, 50vw", imageElement.Sizes);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void SetClassToHovered_When_Hover_Chrome()
    {
        var imageElement = App.Components.CreateById<Image>("myImage4");

        imageElement.Hover();

        Assert.AreEqual("hovered", imageElement.CssClass);
    }
}