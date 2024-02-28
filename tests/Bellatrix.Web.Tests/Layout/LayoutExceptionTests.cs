// <copyright file="LayoutExceptionTests.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using Bellatrix.Layout;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[AllureSuite("Layout")]
public class LayoutExceptionTests
{
    private Mock<ILayoutComponent> _firstComponent;
    private Mock<ILayoutComponent> _secondComponent;
    private Mock<ILayoutComponent> _thirdComponent;

    [TestInitialize]
    public void TestInit()
    {
        _firstComponent = new Mock<ILayoutComponent>();
        _firstComponent.Setup(x => x.Location).Returns(new Point(0, 10));
        _firstComponent.Setup(x => x.Size).Returns(new Size(20, 30));
        _firstComponent.Setup(x => x.ComponentName).Returns("1stE");
        _secondComponent = new Mock<ILayoutComponent>();
        _secondComponent.Setup(x => x.Location).Returns(new Point(20, 40));
        _secondComponent.Setup(x => x.Size).Returns(new Size(20, 30));
        _secondComponent.Setup(x => x.ComponentName).Returns("2ndE");
        _thirdComponent = new Mock<ILayoutComponent>();
        _thirdComponent.Setup(x => x.Location).Returns(new Point(10, 40));
        _thirdComponent.Setup(x => x.Size).Returns(new Size(20, 30));
        _thirdComponent.Setup(x => x.ComponentName).Returns("3dE");
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenNotAlignedVerticallyCentered()
    {
        try
        {
            LayoutAssert.AssertAlignedVerticallyCentered(_firstComponent.Object, _thirdComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be aligned centered vertically 3dE Y = 10 px but was 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenNotAlignedVerticallyRight()
    {
        try
        {
            LayoutAssert.AssertAlignedVerticallyRight(_firstComponent.Object, _thirdComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be aligned right vertically 3dE Y = 20 px but was 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenNotAlignedVerticallyLeft()
    {
        try
        {
            LayoutAssert.AssertAlignedVerticallyLeft(_firstComponent.Object, _thirdComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be aligned left vertically 3dE Y = 0 px but was 10 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenNotAlignedVerticallyAll()
    {
        try
        {
            LayoutAssert.AssertAlignedVerticallyAll(_firstComponent.Object, _thirdComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be aligned left vertically 3dE Y = 0 px but was 10 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenNotAlignedHorizontallyCentered()
    {
        try
        {
            LayoutAssert.AssertAlignedHorizontallyCentered(_firstComponent.Object, _thirdComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be aligned centered horizontally 3dE Y = 25 px but was 55 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenNotAlignedHorizontallyBottom()
    {
        try
        {
            LayoutAssert.AssertAlignedHorizontallyBottom(_firstComponent.Object, _thirdComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be aligned bottom horizontally 3dE Y = 40 px but was 70 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenNotAlignedHorizontallyTop()
    {
        try
        {
            LayoutAssert.AssertAlignedHorizontallyTop(_firstComponent.Object, _thirdComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be aligned top horizontally 3dE Y = 10 px but was 40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenNotAlignedHorizontallyAll()
    {
        try
        {
            LayoutAssert.AssertAlignedHorizontallyAll(_firstComponent.Object, _thirdComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be aligned top horizontally 3dE Y = 10 px but was 40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotTopInsideOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertTopInsideOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE top padding but was -30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotTopInsideOf()
    {
        try
        {
            _firstComponent.Object.AssertTopInsideOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE top padding = 100 px but was -30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotTopInsideOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertTopInsideOfBetween(_secondComponent.Object, 100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE top padding between 100-200 px but was -30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotTopInsideOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertTopInsideOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE top padding > 100 px but was -30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotTopInsideOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertTopInsideOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE top padding >= 100 px but was -30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotTopInsideOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertTopInsideOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE top padding < -70 px but was -30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotTopInsideOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertTopInsideOfLessThanOrEqual(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE top padding <= -70 px but was -30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotTopInsideOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertTopInsideOfApproximate(_secondComponent.Object, 100, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -5% of 100 px top padding of 2ndE but was -30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertRightOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightOf()
    {
        try
        {
            _firstComponent.Object.AssertRightOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be 100 px right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertRightOfBetween(_secondComponent.Object, 100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be between 100-200 px right from 2ndE, but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertRightOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be > 100 px right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertRightOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be >= 100 px right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertRightOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be < -70 px right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertRightOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be < -70 px right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertRightOfApproximate(_secondComponent.Object, 100, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -5% of 100 px right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightInsideOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertRightInsideOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE right padding but was 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightInsideOf()
    {
        try
        {
            _firstComponent.Object.AssertRightInsideOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE right padding = 100 px but was 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightInsideOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertRightInsideOfBetween(_secondComponent.Object, 100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE right padding between 100-200 px but was 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightInsideOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertRightInsideOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE right padding > 100 px but was 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightInsideOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertRightInsideOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE right padding >= 100 px but was 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightInsideOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertRightInsideOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE right padding < -70 px but was 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightInsideOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertRightInsideOfLessThanOrEqual(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE right padding <= -70 px but was 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotRightInsideOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertRightInsideOfApproximate(_secondComponent.Object, 100, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -5% of 100 px right padding of 2ndE but was 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearTopOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertNearTopOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be near top of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearTopOf()
    {
        try
        {
            _firstComponent.Object.AssertNearTopOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be 100 px near top of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearTopOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertNearTopOfBetween(_secondComponent.Object, 100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be between 100-200 px near top of 2ndE, but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearTopOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertNearTopOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be > 100 px near top of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearTopOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertNearTopOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be >= 100 px near top of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearTopOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertNearTopOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be < -70 px near top of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearTopOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertNearTopOfLessThanOrEqual(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -70 px near top of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearTopOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertNearTopOfApproximate(_secondComponent.Object, 100, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -5% of 100 px near top of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearRightOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertNearRightOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be near right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearRightOf()
    {
        try
        {
            _firstComponent.Object.AssertNearRightOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be 100 px near right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearRightOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertNearRightOfBetween(_secondComponent.Object, 100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be between 100-200 px near right from 2ndE, but 0."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearRightOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertNearRightOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be > 100 px near right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearRightOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertNearRightOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be >= 100 px near right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearRightOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertNearRightOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be < -70 px near right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearRightOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertNearRightOfLessThanOrEqual(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -70 px near right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearRightOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertNearRightOfApproximate(_secondComponent.Object, 100, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -5% of 100 px near right from 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearLeftOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertNearLeftOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearLeftOf()
    {
        try
        {
            _firstComponent.Object.AssertNearLeftOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be 100 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearLeftOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertNearLeftOfBetween(_secondComponent.Object, 100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be between 100-200 px left from 2ndE, but -40."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearLeftOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertNearLeftOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be > 100 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearLeftOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertNearLeftOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be >= 100 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearLeftOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertNearLeftOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be < -70 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearLeftOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertNearLeftOfLessThanOrEqual(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -70 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearLeftOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertNearLeftOfApproximate(_secondComponent.Object, 100, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -5% of 100 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearBottomOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertNearBottomOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearBottomOf()
    {
        try
        {
            _firstComponent.Object.AssertNearBottomOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be 100 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearBottomOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertNearBottomOfBetween(_secondComponent.Object, 100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be between 100-200 px below of 2ndE, but -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearBottomOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertNearBottomOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be > 100 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearBottomOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertNearBottomOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be >= 100 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearBottomOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertNearBottomOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be < -70 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearBottomOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertNearBottomOfLessThanOrEqual(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -70 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotNearBottomOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertNearBottomOfApproximate(_secondComponent.Object, 100, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -5% of 100 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertLeftOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftOf()
    {
        try
        {
            _firstComponent.Object.AssertLeftOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be 100 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertLeftOfBetween(_secondComponent.Object, 100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be between 100-200 px left from 2ndE, but -40."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertLeftOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be > 100 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertLeftOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be >= 100 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertLeftOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be < -70 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertLeftOfLessThanOrEqual(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -70 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertLeftOfApproximate(_secondComponent.Object, 100, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -5% of 100 px left from 2ndE but was -40 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftInsideOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertLeftInsideOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE left padding but was -20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftInsideOf()
    {
        try
        {
            _firstComponent.Object.AssertLeftInsideOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE left padding = 100 px but was -20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftInsideOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertLeftInsideOfBetween(_secondComponent.Object, 100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE left padding between 100-200 px but was -20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftInsideOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertLeftInsideOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE left padding > 100 px but was -20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftInsideOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertLeftInsideOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE left padding >= 100 px but was -20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftInsideOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertLeftInsideOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE left padding < -70 px but was -20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftInsideOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertLeftInsideOfLessThanOrEqual(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE left padding <= -70 px but was -20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotLeftInsideOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertLeftInsideOfApproximate(_secondComponent.Object, 100, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -5% of 100 px left padding of 2ndE but was -20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementWidthtNot()
    {
        try
        {
            _firstComponent.Object.AssertWidth(100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The width of 1stE was not 100 px, but 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementWidthtNotBetween()
    {
        try
        {
            _firstComponent.Object.AssertWidthBetween(100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The width of 1stE was not between 100 and 200 px, but 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementWidthtNotGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertWidthGreaterThan(100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The width of 1stE was not > 100 px, but 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementWidthtNotGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertWidthGreaterThanOrEqual(100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The width of 1stE was not >= 100 px, but 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementWidthtNotLessThan()
    {
        try
        {
            _firstComponent.Object.AssertWidthLessThan(-70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The width of 1stE was not < -70 px, but 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementWidthtNotLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertWidthLessThanOrEqual(-70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The width of 1stE was not <= -70 px, but 20 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementWidthtNotApproximate()
    {
        try
        {
            _firstComponent.Object.AssertWidthApproximate(_secondComponent.Object, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The width % difference between 1stE and 2ndE was greater than -5%, it was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementHeightNot()
    {
        try
        {
            _firstComponent.Object.AssertHeight(100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The height of 1stE was not 100 px, but 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementHeightNotBetween()
    {
        try
        {
            _firstComponent.Object.AssertHeightBetween(100, 200);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The height of 1stE was not between 100 and 200 px, but 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementHeightNotGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertHeightGreaterThan(100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The height of 1stE was not > 100 px, but 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementHeightNotGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertHeightGreaterThanOrEqual(100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The height of 1stE was not >= 100 px, but 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementHeightNotLessThan()
    {
        try
        {
            _firstComponent.Object.AssertHeightLessThan(-10);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The height of 1stE was not < -10 px, but 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementHeightNotLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertHeightLessThanOrEqual(-10);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The height of 1stE was not <= -10 px, but 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementHeightNotApproximateToSecondElement()
    {
        try
        {
            _firstComponent.Object.AssertHeightApproximate(_secondComponent.Object, -5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("The height % difference between 1stE and 2ndE was greater than -5%, it was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBottomInsideOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertBottomInsideOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE bottom padding but was 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBottomInsideOf()
    {
        try
        {
            _firstComponent.Object.AssertBottomInsideOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE bottom padding = 100 px but was 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBottomInsideOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertBottomInsideOfBetween(_secondComponent.Object, -70, 0);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE bottom padding between -70-0 px but was 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBottomInsideOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertBottomInsideOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE bottom padding > 100 px but was 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBottomInsideOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertBottomInsideOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE bottom padding >= 100 px but was 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBottomInsideOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertBottomInsideOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE bottom padding < -70 px but was 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBottomInsideOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertBottomInsideOfLessThanOrEqual(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be inside of 2ndE bottom padding <= -70 px but was 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBottomInsideOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertBottomInsideOfApproximate(_secondComponent.Object, -70, 5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= 5% of -70 px bottom padding of 2ndE but was 30 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBelowOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertBelowOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBelowOf()
    {
        try
        {
            _firstComponent.Object.AssertBelowOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be 100 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBelowOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertBelowOfBetween(_secondComponent.Object, 0, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be between 0-100 px below of 2ndE, but -60."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBelowOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertBelowOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be > 100 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBelowOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertBelowOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be >= 100 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBelowOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertBelowOfLessThan(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be < -70 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBelowOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertBelowOfLessThanOrEqual(_secondComponent.Object, -70);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -70 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotBelowOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertBelowOfApproximate(_secondComponent.Object, 100, 5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= 5% of 100 px below of 2ndE but was -60 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotAboveOfNoExpectedValue()
    {
        try
        {
            _firstComponent.Object.AssertAboveOf(_secondComponent.Object);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be above of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotAboveOf()
    {
        try
        {
            _firstComponent.Object.AssertAboveOf(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be 100 px above of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotAboveOfBetween()
    {
        try
        {
            _firstComponent.Object.AssertAboveOfBetween(_secondComponent.Object, 10, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be between 10-100 px above of 2ndE, but 0."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotAboveOfGreaterThan()
    {
        try
        {
            _firstComponent.Object.AssertAboveOfGreaterThan(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be > 100 px above of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotAboveOfGreaterThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertAboveOfGreaterThanOrEqual(_secondComponent.Object, 100);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be >= 100 px above of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotAboveOfLessThan()
    {
        try
        {
            _firstComponent.Object.AssertAboveOfLessThan(_secondComponent.Object, -10);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be < -10 px above of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotAboveOfLessThanOrEqual()
    {
        try
        {
            _firstComponent.Object.AssertAboveOfLessThanOrEqual(_secondComponent.Object, -10);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= -10 px above of 2ndE but was 0 px."), e.Message);
        }
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    public void ThrowLayoutAssertFailedException_WhenElementNotAboveOfApproximate()
    {
        try
        {
            _firstComponent.Object.AssertAboveOfApproximate(_secondComponent.Object, 100, 5);
        }
        catch (LayoutAssertFailedException e)
        {
            Assert.AreEqual(FormatExceptionMessage("1stE should be <= 5% of 100 px above of 2ndE but was 0 px."), e.Message);
        }
    }

    private string FormatExceptionMessage(string exceptionMessage) => $"\n\n{new string('#', 40)}\n\n{exceptionMessage}\n\n{new string('#', 40)}\n";
}