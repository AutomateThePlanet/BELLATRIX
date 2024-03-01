// <copyright file="NUnitBaseTest.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Bellatrix.NUnit;
using Bellatrix.Plugins;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Bellatrix;

public class NUnitBaseTest
{
    private static readonly ThreadLocal<bool> _isConfigurationExecuted = new ThreadLocal<bool>(() => { return false; });
    private static ThreadLocal<Exception> _thrownException;
    private ServicesCollection _container;

    private PluginProvider _currentTestExecutionProvider;

    public NUnitBaseTest()
    {
        _container = ServicesCollection.Current;
        if (!_isConfigurationExecuted.Value)
        {
            Configure();
            _isConfigurationExecuted.Value = true;
        }

        AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
        {
            if (eventArgs.Exception.Source != "System.Private.CoreLib")
            {
                if (_thrownException == null)
                {
                    _thrownException = new ThreadLocal<Exception>(() => eventArgs.Exception);
                }
                else
                {
                    _thrownException.Value = eventArgs.Exception;
                }
            }
        };
    }

    public TestContext TestContext => TestContext.CurrentContext;

    public virtual void Configure()
    {
    }

    [OneTimeSetUp]
    public void OneTimeArrangeAct()
    {
        try
        {
            var testClassType = GetCurrentExecutionTestClassType();

            _container = ServicesCollection.Main.CreateChildServicesCollection(testClassType.FullName);
            _container.RegisterInstance(_container);
            _currentTestExecutionProvider = new PluginProvider();
            Initialize();
            InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
            _currentTestExecutionProvider.PreTestsArrange(testClassType, TestContext.CurrentContext.Test.Arguments.ToList());
            TestsArrange();
            _currentTestExecutionProvider.PostTestsArrange(testClassType);
            _currentTestExecutionProvider.PreTestsAct(testClassType, TestContext.CurrentContext.Test.Arguments.ToList());
            TestsAct();
            _currentTestExecutionProvider.PostTestsAct(testClassType, TestContext.CurrentContext.Test.Arguments.ToList());
        }
        catch (Exception ex)
        {
            _currentTestExecutionProvider.TestsArrangeFailed(ex);

            throw;
        }
    }

    [OneTimeTearDown]
    public void ClassCleanup()
    {
        try
        {
            var testClassType = GetCurrentExecutionTestClassType();

            ////_container = ServicesCollection.Current.FindCollection(testClassType.FullName);
            ////_container.RegisterInstance(_container);
            ////_currentTestExecutionProvider = new PluginProvider();
            ////InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);

            _currentTestExecutionProvider.PreClassCleanup(testClassType);
            TestsCleanup();
            _currentTestExecutionProvider.PostClassCleanup(testClassType);
        }
        catch (Exception ex)
        {
            _currentTestExecutionProvider.TestsCleanupFailed(ex);
            throw;
        }
    }

    [SetUp]
    public void CoreTestInit()
    {
        if (_thrownException?.Value != null)
        {
            _thrownException.Value = null;
        }

        var testClassType = GetCurrentExecutionTestClassType();

        _container = ServicesCollection.Main.FindCollection(testClassType.FullName);

        var testMethodMemberInfo = GetCurrentExecutionMethodInfo();
        var categories = GetAllTestCategories();
        var authors = GetAllAuthors();
        var descriptions = GetAllDescriptions();

        _currentTestExecutionProvider = new PluginProvider();
        InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
        try
        {
            _currentTestExecutionProvider.PreTestInit(TestContext.Test.Name, testMethodMemberInfo, testClassType, TestContext.CurrentContext.Test.Arguments.ToList(), categories, authors, descriptions);
            TestInit();
            _currentTestExecutionProvider.PostTestInit(TestContext.Test.Name, testMethodMemberInfo, testClassType, TestContext.CurrentContext.Test.Arguments.ToList(), categories, authors, descriptions);
        }
        catch (Exception ex)
        {
            _currentTestExecutionProvider.TestInitFailed(ex, TestContext.Test.Name, testMethodMemberInfo, testClassType, TestContext.CurrentContext.Test.Arguments.ToList(), categories, authors, descriptions);
            throw;
        }
    }

    [TearDown]
    public void CoreTestCleanup()
    {
        var testClassType = GetCurrentExecutionTestClassType();
        _container = ServicesCollection.Main.FindCollection(testClassType.FullName);
        var testMethodMemberInfo = GetCurrentExecutionMethodInfo();
        var categories = GetAllTestCategories();
        var authors = GetAllAuthors();
        var descriptions = GetAllDescriptions();

        _currentTestExecutionProvider = new PluginProvider();
        var cleanupExceptions = new List<Exception>();
        InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
        
        try
        {
            _currentTestExecutionProvider.PreTestCleanup((TestOutcome)TestContext.Result.Outcome.Status, TestContext.Test.Name, testMethodMemberInfo, testClassType, TestContext.CurrentContext.Test.Arguments.ToList(), categories, authors, descriptions, TestContext.Result.Message, TestContext.Result.StackTrace, _thrownException?.Value);
        }
        catch (Exception preTestCleanupException)
        {
            cleanupExceptions.Add(preTestCleanupException);
        }

        try
        {
            TestCleanup();
        }
        catch (Exception ex)
        {
            cleanupExceptions.Add(ex);
        }

        try
        {
            _currentTestExecutionProvider.PostTestCleanup((TestOutcome)TestContext.Result.Outcome.Status, TestContext.Test.FullName, testMethodMemberInfo, testClassType, TestContext.CurrentContext.Test.Arguments.ToList(), categories, authors, descriptions, TestContext.Result.Message, TestContext.Result.StackTrace, _thrownException?.Value);
        }
        catch (Exception postCleanupException)
        {
            cleanupExceptions.Add(postCleanupException);
        }

        if (cleanupExceptions.Any())
        {
            _currentTestExecutionProvider.TestCleanupFailed(cleanupExceptions.Last(), TestContext.Test.Name, testMethodMemberInfo, testClassType, TestContext.CurrentContext.Test.Arguments.ToList(), categories, authors, descriptions);
            throw new AggregateException("Test Cleanup failed with one or more errors:", cleanupExceptions);
        }
    }

    public virtual void Initialize()
    {
    }

    public virtual void TestsArrange()
    {
    }

    public virtual void TestsAct()
    {
    }

    public virtual void TestsCleanup()
    {
    }

    public virtual void TestInit()
    {
    }

    public virtual void TestCleanup()
    {
    }

    protected static bool IsDebugRun()
    {
#if DEBUG
        var isDebug = true;
#else
        bool isDebug = false;
#endif

        return isDebug;
    }

    private List<string> GetAllTestCategories()
    {
        var categories = new List<string>();
        foreach (var property in GetTestProperties(PropertyNames.Category))
        {
            categories.Add(property);
        }

        return categories;
    }

    private List<string> GetAllAuthors()
    {
        var authors = new List<string>();
        foreach (var property in GetTestProperties(PropertyNames.Author))
        {
            authors.Add(property);
        }

        return authors;
    }

    private IEnumerable<string> GetTestProperties(string name)
    {
        var list = new List<string>();
        var currentTest = (ITest)TestExecutionContext.CurrentContext?.CurrentTest;
        while (currentTest != null && currentTest?.GetType() != typeof(TestSuite) && currentTest?.ClassName != "NUnit.Framework.Internal.TestExecutionContext+AdhocContext")
        {
            if (currentTest.Properties.ContainsKey(name))
            {
                if (currentTest.Properties[name].Count > 0)
                {
                    for (var i = 0; i < currentTest.Properties[name].Count; i++)
                    {
                        list.Add(currentTest.Properties[name][i].ToString());
                    }
                }
            }

            currentTest = currentTest.Parent;
        }

        return list;
    }

    private List<string> GetAllDescriptions()
    {
        var descriptions = new List<string>();
        foreach (var property in GetTestProperties(PropertyNames.Description))
        {
            descriptions.Add(property);
        }

        return descriptions;
    }

    private void InitializeTestExecutionBehaviorObservers(PluginProvider testExecutionProvider)
    {
        var observers = ServicesCollection.Current.ResolveAll<Plugin>();
        foreach (var observer in observers)
        {
            observer.Subscribe(testExecutionProvider);
        }
    }

    private MethodInfo GetCurrentExecutionMethodInfo()
    {
        var testMethodMemberInfo = GetType().GetMethod(TestContext.CurrentContext.Test.MethodName);
        return testMethodMemberInfo;
    }

    private Type GetCurrentExecutionTestClassType()
    {
        var testClassType = GetType().Assembly.GetType(TestContext.CurrentContext.Test.ClassName);

        return testClassType;
    }
}