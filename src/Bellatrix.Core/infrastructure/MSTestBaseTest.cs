// <copyright file="MSTestBaseTest.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Bellatrix.Plugins;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix;

[TestClass]
public class MSTestBaseTest
{
    private ServicesCollection _container;
    private static readonly List<string> TypeForAlreadyExecutedClassInits = new List<string>();
    private static readonly ThreadLocal<bool> _isConfigurationExecuted = new ThreadLocal<bool>(() => { return false; });
    private static ThreadLocal<Exception> _thrownException;
    private PluginProvider _currentTestExecutionProvider;

    public MSTestBaseTest()
    {
        _currentTestExecutionProvider = new PluginProvider();
        if (!_isConfigurationExecuted.Value)
        {
            Configure();
            _isConfigurationExecuted.Value = true;
        }

        InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
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

    public TestContext TestContext { get; set; }

    public virtual void Configure()
    {
    }

    [TestInitialize]
    public void CoreTestInit()
    {
        ServicesCollection.Current.RegisterInstance(TestContext);
        var testMethodMemberInfo = GetCurrentExecutionMethodInfo();
        var testClassType = GetCurrentExecutionTestClassType();
        ExecuteActArrangePhases();

        _container = ServicesCollection.Current.FindCollection(testClassType.FullName);
        _currentTestExecutionProvider = new PluginProvider();
        InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);

        var categories = GetAllTestCategories(testMethodMemberInfo);
        var authors = GetAllAuthors(testMethodMemberInfo);
        var descriptions = GetAllDescriptions(testMethodMemberInfo);
        try
        {
            Initialize();
            _currentTestExecutionProvider.PreTestInit(TestContext.TestName, testMethodMemberInfo, testClassType, new List<object>(), categories, authors, descriptions);
            TestInit();
            _currentTestExecutionProvider.PostTestInit(TestContext.TestName, testMethodMemberInfo, testClassType, new List<object>(), categories, authors, descriptions);
        }
        catch (Exception ex)
        {
            _currentTestExecutionProvider.TestInitFailed(ex, TestContext.TestName, testMethodMemberInfo, testClassType, new List<object>(), categories, authors, descriptions);
            throw;
        }
    }

    [TestCleanup]
    public void CoreTestCleanup()
    {
        var testMethodMemberInfo = GetCurrentExecutionMethodInfo();
        var testClassType = GetCurrentExecutionTestClassType();
        var categories = GetAllTestCategories(testMethodMemberInfo);
        var authors = GetAllAuthors(testMethodMemberInfo);
        var descriptions = GetAllDescriptions(testMethodMemberInfo);

        _container = ServicesCollection.Current.FindCollection(testClassType.FullName);
        _currentTestExecutionProvider = new PluginProvider();
        InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);

        try
        {
            _currentTestExecutionProvider.PreTestCleanup((TestOutcome)TestContext.CurrentTestOutcome, TestContext.TestName, testMethodMemberInfo, testClassType, new List<object>(), categories, authors, descriptions, _thrownException?.Value?.Message, _thrownException?.Value?.StackTrace, _thrownException?.Value);
            TestCleanup();
            _currentTestExecutionProvider.PostTestCleanup((TestOutcome)TestContext.CurrentTestOutcome, TestContext.TestName, testMethodMemberInfo, testClassType, new List<object>(), categories, authors, descriptions, _thrownException?.Value?.Message, _thrownException?.Value?.StackTrace, _thrownException?.Value);
        }
        catch (Exception ex)
        {
            _currentTestExecutionProvider.TestCleanupFailed(ex, TestContext.TestName, testMethodMemberInfo, testClassType, new List<object>(), categories, authors, descriptions);
            throw;
        }
    }

    [ClassCleanup]
    public void CoreClassCleanup()
    {
        var testClassType = GetCurrentExecutionTestClassType();

        _container = ServicesCollection.Current.FindCollection(testClassType.FullName);
        _currentTestExecutionProvider = new PluginProvider();
        InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);

        try
        {
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

    // ReSharper disable once MemberCanBeProtected.Global
    public virtual void Initialize()
    {
    }

    // ReSharper disable once MemberCanBeProtected.Global
    public virtual void TestsArrange()
    {
    }

    // ReSharper disable once MemberCanBeProtected.Global
    public virtual void TestsAct()
    {
    }

    public virtual void TestInit()
    {
    }

    public virtual void TestCleanup()
    {
    }

    public virtual void TestsCleanup()
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

    private List<string> GetAllTestCategories(MemberInfo memberInfo)
    {
        var categories = new List<string>();
        var testCategoryAttributes = GetAllAttributes<TestCategoryAttribute>(memberInfo);
        foreach (var testCategoryAttribute in testCategoryAttributes)
        {
            categories.AddRange(testCategoryAttribute.TestCategories);
        }

        return categories;
    }

    private List<string> GetAllAuthors(MemberInfo memberInfo)
    {
        var authors = new List<string>();
        var ownerAttributes = GetAllAttributes<OwnerAttribute>(memberInfo);
        foreach (var ownerAttribute in ownerAttributes)
        {
            authors.Add(ownerAttribute.Owner);
        }

        return authors;
    }

    private List<string> GetAllDescriptions(MemberInfo memberInfo)
    {
        var descriptions = new List<string>();
        var descriptionAttributes = GetAllAttributes<DescriptionAttribute>(memberInfo);
        foreach (var descriptionAttribute in descriptionAttributes)
        {
            descriptions.Add(descriptionAttribute.Description);
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

    private void ExecuteActArrangePhases()
    {
        try
        {
            var testClassType = GetCurrentExecutionTestClassType();
            if (!TypeForAlreadyExecutedClassInits.Contains(TestContext.FullyQualifiedTestClassName))
            {
                _container = ServicesCollection.Current.CreateChildServicesCollection(testClassType.FullName);
                _container.RegisterInstance(_container);
                _currentTestExecutionProvider = new PluginProvider();
                InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
                TypeForAlreadyExecutedClassInits.Add(TestContext.FullyQualifiedTestClassName);
                _currentTestExecutionProvider.PreTestsArrange(testClassType, new List<object>());
                Initialize();
                TestsArrange();
                _currentTestExecutionProvider.PostTestsArrange(testClassType);
                _currentTestExecutionProvider.PreTestsAct(testClassType, new List<object>());
                TestsAct();
                _currentTestExecutionProvider.PostTestsAct(testClassType, new List<object>());
            }
        }
        catch (Exception ex)
        {
            _currentTestExecutionProvider.TestsArrangeFailed(ex);
        }
    }

    private MethodInfo GetCurrentExecutionMethodInfo()
    {
        var memberInfo = GetType().GetMethod(TestContext.TestName);
        return memberInfo;
    }

    private Type GetCurrentExecutionTestClassType()
    {
        string className = TestContext.FullyQualifiedTestClassName;
        var testClassType = GetType().Assembly.GetType(className);

        return testClassType;
    }

    private List<TAttribute> GetAllAttributes<TAttribute>(MemberInfo memberInfo)
        where TAttribute : Attribute
    {
        var allureClassAttributes = GetClassAttributes<TAttribute>(memberInfo.DeclaringType);
        var allureMethodAttributes = GetMethodAttributes<TAttribute>(memberInfo);
        var allAllureAttributes = allureClassAttributes.ToList();
        allAllureAttributes.AddRange(allureMethodAttributes);

        return allAllureAttributes;
    }

    private IEnumerable<TAttribute> GetClassAttributes<TAttribute>(Type currentType)
        where TAttribute : Attribute
    {
        var classAttributes = currentType.GetCustomAttributes<TAttribute>(true);

        return classAttributes;
    }

    private IEnumerable<TAttribute> GetMethodAttributes<TAttribute>(MemberInfo memberInfo)
        where TAttribute : Attribute
    {
        var methodAttributes = memberInfo.GetCustomAttributes<TAttribute>(true);

        return methodAttributes;
    }
}