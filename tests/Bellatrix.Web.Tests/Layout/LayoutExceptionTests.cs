// <copyright file="LayoutExceptionTests.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using Bellatrix.Layout;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bellatrix.Web.Tests.Controls
{
    [TestClass]
    [AllureSuite("Layout")]
    public class LayoutExceptionTests
    {
        private Mock<ILayoutElement> _firstElement;
        private Mock<ILayoutElement> _secondElement;
        private Mock<ILayoutElement> _thirdlement;

        [TestInitialize]
        public void TestInit()
        {
            _firstElement = new Mock<ILayoutElement>();
            _firstElement.Setup(x => x.Location).Returns(new Point(0, 10));
            _firstElement.Setup(x => x.Size).Returns(new Size(20, 30));
            _firstElement.Setup(x => x.ElementName).Returns("1stE");
            _secondElement = new Mock<ILayoutElement>();
            _secondElement.Setup(x => x.Location).Returns(new Point(20, 40));
            _secondElement.Setup(x => x.Size).Returns(new Size(20, 30));
            _secondElement.Setup(x => x.ElementName).Returns("2ndE");
            _thirdlement = new Mock<ILayoutElement>();
            _thirdlement.Setup(x => x.Location).Returns(new Point(10, 40));
            _thirdlement.Setup(x => x.Size).Returns(new Size(20, 30));
            _thirdlement.Setup(x => x.ElementName).Returns("3dE");
        }

        [TestMethod]
        [TestCategory(Categories.Layout)]
        public void ThrowLayoutAssertFailedException_WhenNotAlignedVerticallyCentered()
        {
            try
            {
                LayoutAssert.AssertAlignedVerticallyCentered(_firstElement.Object, _thirdlement.Object);
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
                LayoutAssert.AssertAlignedVerticallyRight(_firstElement.Object, _thirdlement.Object);
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
                LayoutAssert.AssertAlignedVerticallyLeft(_firstElement.Object, _thirdlement.Object);
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
                LayoutAssert.AssertAlignedVerticallyAll(_firstElement.Object, _thirdlement.Object);
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
                LayoutAssert.AssertAlignedHorizontallyCentered(_firstElement.Object, _thirdlement.Object);
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
                LayoutAssert.AssertAlignedHorizontallyBottom(_firstElement.Object, _thirdlement.Object);
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
                LayoutAssert.AssertAlignedHorizontallyTop(_firstElement.Object, _thirdlement.Object);
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
                LayoutAssert.AssertAlignedHorizontallyAll(_firstElement.Object, _thirdlement.Object);
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
                _firstElement.Object.AssertTopInsideOf(_secondElement.Object);
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
                _firstElement.Object.AssertTopInsideOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertTopInsideOfBetween(_secondElement.Object, 100, 200);
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
                _firstElement.Object.AssertTopInsideOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertTopInsideOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertTopInsideOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertTopInsideOfLessThanOrEqual(_secondElement.Object, -70);
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
                _firstElement.Object.AssertTopInsideOfApproximate(_secondElement.Object, 100, -5);
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
                _firstElement.Object.AssertRightOf(_secondElement.Object);
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
                _firstElement.Object.AssertRightOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertRightOfBetween(_secondElement.Object, 100, 200);
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
                _firstElement.Object.AssertRightOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertRightOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertRightOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertRightOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertRightOfApproximate(_secondElement.Object, 100, -5);
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
                _firstElement.Object.AssertRightInsideOf(_secondElement.Object);
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
                _firstElement.Object.AssertRightInsideOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertRightInsideOfBetween(_secondElement.Object, 100, 200);
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
                _firstElement.Object.AssertRightInsideOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertRightInsideOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertRightInsideOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertRightInsideOfLessThanOrEqual(_secondElement.Object, -70);
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
                _firstElement.Object.AssertRightInsideOfApproximate(_secondElement.Object, 100, -5);
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
                _firstElement.Object.AssertNearTopOf(_secondElement.Object);
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
                _firstElement.Object.AssertNearTopOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearTopOfBetween(_secondElement.Object, 100, 200);
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
                _firstElement.Object.AssertNearTopOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearTopOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearTopOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertNearTopOfLessThanOrEqual(_secondElement.Object, -70);
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
                _firstElement.Object.AssertNearTopOfApproximate(_secondElement.Object, 100, -5);
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
                _firstElement.Object.AssertNearRightOf(_secondElement.Object);
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
                _firstElement.Object.AssertNearRightOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearRightOfBetween(_secondElement.Object, 100, 200);
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
                _firstElement.Object.AssertNearRightOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearRightOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearRightOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertNearRightOfLessThanOrEqual(_secondElement.Object, -70);
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
                _firstElement.Object.AssertNearRightOfApproximate(_secondElement.Object, 100, -5);
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
                _firstElement.Object.AssertNearLeftOf(_secondElement.Object);
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
                _firstElement.Object.AssertNearLeftOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearLeftOfBetween(_secondElement.Object, 100, 200);
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
                _firstElement.Object.AssertNearLeftOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearLeftOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearLeftOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertNearLeftOfLessThanOrEqual(_secondElement.Object, -70);
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
                _firstElement.Object.AssertNearLeftOfApproximate(_secondElement.Object, 100, -5);
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
                _firstElement.Object.AssertNearBottomOf(_secondElement.Object);
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
                _firstElement.Object.AssertNearBottomOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearBottomOfBetween(_secondElement.Object, 100, 200);
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
                _firstElement.Object.AssertNearBottomOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearBottomOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertNearBottomOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertNearBottomOfLessThanOrEqual(_secondElement.Object, -70);
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
                _firstElement.Object.AssertNearBottomOfApproximate(_secondElement.Object, 100, -5);
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
                _firstElement.Object.AssertLeftOf(_secondElement.Object);
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
                _firstElement.Object.AssertLeftOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertLeftOfBetween(_secondElement.Object, 100, 200);
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
                _firstElement.Object.AssertLeftOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertLeftOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertLeftOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertLeftOfLessThanOrEqual(_secondElement.Object, -70);
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
                _firstElement.Object.AssertLeftOfApproximate(_secondElement.Object, 100, -5);
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
                _firstElement.Object.AssertLeftInsideOf(_secondElement.Object);
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
                _firstElement.Object.AssertLeftInsideOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertLeftInsideOfBetween(_secondElement.Object, 100, 200);
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
                _firstElement.Object.AssertLeftInsideOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertLeftInsideOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertLeftInsideOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertLeftInsideOfLessThanOrEqual(_secondElement.Object, -70);
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
                _firstElement.Object.AssertLeftInsideOfApproximate(_secondElement.Object, 100, -5);
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
                _firstElement.Object.AssertWidth(100);
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
                _firstElement.Object.AssertWidthBetween(100, 200);
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
                _firstElement.Object.AssertWidthGreaterThan(100);
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
                _firstElement.Object.AssertWidthGreaterThanOrEqual(100);
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
                _firstElement.Object.AssertWidthLessThan(-70);
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
                _firstElement.Object.AssertWidthLessThanOrEqual(-70);
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
                _firstElement.Object.AssertWidthApproximate(_secondElement.Object, -5);
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
                _firstElement.Object.AssertHeight(100);
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
                _firstElement.Object.AssertHeightBetween(100, 200);
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
                _firstElement.Object.AssertHeightGreaterThan(100);
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
                _firstElement.Object.AssertHeightGreaterThanOrEqual(100);
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
                _firstElement.Object.AssertHeightLessThan(-10);
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
                _firstElement.Object.AssertHeightLessThanOrEqual(-10);
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
                _firstElement.Object.AssertHeightApproximate(_secondElement.Object, -5);
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
                _firstElement.Object.AssertBottomInsideOf(_secondElement.Object);
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
                _firstElement.Object.AssertBottomInsideOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertBottomInsideOfBetween(_secondElement.Object, -70, 0);
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
                _firstElement.Object.AssertBottomInsideOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertBottomInsideOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertBottomInsideOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertBottomInsideOfLessThanOrEqual(_secondElement.Object, -70);
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
                _firstElement.Object.AssertBottomInsideOfApproximate(_secondElement.Object, -70, 5);
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
                _firstElement.Object.AssertBelowOf(_secondElement.Object);
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
                _firstElement.Object.AssertBelowOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertBelowOfBetween(_secondElement.Object, 0, 100);
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
                _firstElement.Object.AssertBelowOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertBelowOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertBelowOfLessThan(_secondElement.Object, -70);
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
                _firstElement.Object.AssertBelowOfLessThanOrEqual(_secondElement.Object, -70);
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
                _firstElement.Object.AssertBelowOfApproximate(_secondElement.Object, 100, 5);
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
                _firstElement.Object.AssertAboveOf(_secondElement.Object);
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
                _firstElement.Object.AssertAboveOf(_secondElement.Object, 100);
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
                _firstElement.Object.AssertAboveOfBetween(_secondElement.Object, 10, 100);
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
                _firstElement.Object.AssertAboveOfGreaterThan(_secondElement.Object, 100);
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
                _firstElement.Object.AssertAboveOfGreaterThanOrEqual(_secondElement.Object, 100);
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
                _firstElement.Object.AssertAboveOfLessThan(_secondElement.Object, -10);
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
                _firstElement.Object.AssertAboveOfLessThanOrEqual(_secondElement.Object, -10);
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
                _firstElement.Object.AssertAboveOfApproximate(_secondElement.Object, 100, 5);
            }
            catch (LayoutAssertFailedException e)
            {
                Assert.AreEqual(FormatExceptionMessage("1stE should be <= 5% of 100 px above of 2ndE but was 0 px."), e.Message);
            }
        }

        private string FormatExceptionMessage(string exceptionMessage) => $"\n\n{new string('#', 40)}\n\n{exceptionMessage}\n\n{new string('#', 40)}\n";
    }
}