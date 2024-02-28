// <copyright file="ButtonControlValidateExtensionsExceptionMessagesTests.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.Tests;

[TestClass]
[IOS(Constants.IOSNativeAppPath,
    Constants.IOSAppBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.ReuseIfStarted)]
[AllureSuite("Button Control")]
[AllureFeature("ValidateExtensions")]
public class ButtonControlValidateExtensionsExceptionMessagesTests : MSTest.IOSTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    [Timeout(180000)]
    public void ValidateInnerTextIs_ThrowException_Button_When_InnerTextIsAsExpected()
    {
        try
        {
            var button = App.Components.CreateByName<Button>("ComputeSumButton");

            button.ValidateTextIs("Compute");
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = "The control's text should be 'Compute'";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [Timeout(180000)]
    public void ValidateInnerTextIsNull_ThrowException_Button_When_InnerTextIsNotNull()
    {
        try
        {
            var button = App.Components.CreateByName<Button>("ComputeSumButton");

            button.ValidateTextIsNotSet();
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = "The control's text should be null but was 'Compute Sum'";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [Timeout(180000)]
    public void ValidateIsNotDisabled_ThrowException_When_ButtonNotDisabled()
    {
        try
        {
            var button = App.Components.CreateByName<Button>("ComputeSumButton");

            button.ValidateIsDisabled();
        }
        catch (ComponentPropertyValidateException e)
        {
            string expectedExceptionMessage = "The control should be disabled but it was NOT.";
            Assert.AreEqual(true, e.Message.Contains(expectedExceptionMessage), $"Should be {expectedExceptionMessage} but was {e.Message}");
        }
    }
}
