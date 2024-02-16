// <copyright file="TestWorkflowHooksTests.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Layout;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.Tests;

[TestClass]
[AllureSuite("TestWorkflowHooks")]
[IOS(Constants.IOSNativeAppPath,
    "",
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.ReuseIfStarted)]
public class TestWorkflowHooksTests : MSTest.IOSTest
{
    // 1. One of the greatest features of BELLATRIX is test workflow hooks.
    // It gives you the possibility to execute your logic in every part of the test workflow.
    // Also, as you can read in the next chapter write plug-ins that execute code in different places of the workflow every time.
    // This is happening no matter what test framework you use- MSTest or NUnit. As you know, MSTest is not extension friendly.
    //
    // 2. BELLATRIX default Test Workflow.
    //
    // The following methods are called once for test class:
    //
    // 2.1. All plug-ins PreTestsArrange logic executes.
    // 2.2. Current class TestsArrange method executes. By default it is empty, but you can override it in each class and execute your logic.
    // This is the place where you can set up data for your tests, call internal API services, SQL scripts and so on.
    // 2.3. All plug-ins PostTestsArrange logic executes.
    // 2.4. All plug-ins PreTestsAct logic executes.
    // 2.5. Current class TestsAct method executes. By default it is empty, but you can override it in each class and execute your logic.
    // This is the place where you can execute the primary actions for your test case. This is useful if you want later include only assertions in the tests.
    // 2.6. All plug-ins PostTestsAct logic executes.
    //
    // Note: TestsArrange and TestsAct are similar to MSTest TestClassInitialize and OneTimeSetup in NUnit. We decided to split them into two methods
    // to make the code more readable and two allow customisation of the workflow.
    //
    // The following methods are called once for each test in the class:
    //
    // 2.7. All plug-ins PreTestInit logic executes.
    // 2.8. Current class TestInit method executes. By default it is empty, but you can override it in each class and execute your logic.
    // You can add some logic that is executed for each test instead of copy pasting it for each test. For example- navigating to a specific iOS screen.
    // 2.9. All plug-ins PostTestInit logic executes.
    // 2.10. All plug-ins PreTestCleanup logic executes.
    // 2.11. Current class TestCleanup method executes. By default it is empty, but you can override it in each class and execute your logic.
    // You can add some logic that is executed after each test instead of copy pasting it. For example- deleting some entity from DB.
    // 2.12. All plug-ins PostTestCleanup logic executes.
    private static TextField _textField;
    private static Button _button;

    // 3. This is one of the ways you can use TestsArrange and TestsAct.
    // You can find create all elements in the TestsArrange and create all necessary data for the tests.
    // Then in the TestsAct execute the actual tests logic but without asserting anything.
    // Then in each separate test execute single assert or Validate method. Following the best testing practices- having a single assertion in a test.
    // If you execute multiple assertions and if one of them fails, the next ones are not executed which may lead to missing some major clue about
    // a bug in your product. Anyhow, BELLATRIX allows you to write your tests the standard way of executing the primary logic in the tests or reuse
    // some of it through the usage of TestInit and TestCleanup methods.
    public override void TestsArrange()
    {
        _textField = App.Components.CreateById<TextField>("IntegerA");
        _button = App.Components.CreateByName<Button>("ComputeSumButton");
    }

    public override void TestsAct()
    {
        _textField.SetText("1");
        _button.Click();
    }

    public override void TestInit()
    {
        // Executes a logic before each test in the test class.
    }

    public override void TestCleanup()
    {
        // Executes a logic after each test in the test class.
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void ButtonIsAboveOfCheckBox()
    {
        _textField.AssertAboveOf(_button);
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void ButtonIsAboveOfCheckBox_GreaterThan20px()
    {
        _textField.AssertAboveOfGreaterThan(_button, 20);
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void ButtonIsAboveOfCheckBox_GreaterThanOrEqual20px()
    {
        _textField.AssertAboveOfGreaterThanOrEqual(_button, 20);
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void ButtonIsNearTopOfCheckBox_GreaterThan20px()
    {
        _textField.AssertNearTopOfGreaterThan(_button, 20);
    }
}