// <copyright file="BenchmarkTest.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Reflection;
using System.Threading;
using Bellatrix.Plugins;

namespace Bellatrix.Benchmark;

public abstract class BenchmarkTest
{
#pragma warning disable SA1306 // Field names should begin with lower-case letter
    protected static ThreadLocal<Exception> ThrownException;
#pragma warning restore SA1306 // Field names should begin with lower-case letter
    private PluginProvider _currentTestExecutionProvider;
    private List<string> _authors = new List<string>();
    private List<string> _categories = new List<string>();
    private List<string> _descriptions = new List<string>();
    private StringWriter _stringWriter = new StringWriter();

    public BenchmarkTest()
    {
        AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
        {
            if (eventArgs.Exception.Source != "System.Private.CoreLib")
            {
                if (ThrownException == null)
                {
                    ThrownException = new ThreadLocal<Exception>(() => eventArgs.Exception);
                }
                else
                {
                    ThrownException.Value = eventArgs.Exception;
                }
            }
        };
    }

    public ServicesCollection Container { get; set; }

    public abstract Type TestClassType
    {
        get;
    }

    public abstract string ShortTestClassName
    {
        get;
    }

    public void CoreTestInit(string testName)
    {
        if (ThrownException?.Value != null)
        {
            ThrownException.Value = null;
        }

        _stringWriter = new StringWriter();
        Console.SetOut(_stringWriter);

        var testClassType = TestClassType;
        var testMethodMemberInfo = GetCurrentExecutionMethodInfo(testName);
        Container = ServicesCollection.Current.FindCollection(testClassType.FullName);
        _currentTestExecutionProvider = new PluginProvider();
        InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
        var categories = _categories;
        var authors = _authors;
        var descriptions = _descriptions;
        try
        {
            _currentTestExecutionProvider.PreTestInit(testName, testMethodMemberInfo, testClassType, new List<object>(), categories, authors, descriptions);
            TestInit();
            _currentTestExecutionProvider.PostTestInit(testName, testMethodMemberInfo, testClassType, new List<object>(), categories, authors, descriptions);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            _currentTestExecutionProvider.TestInitFailed(ex, testName, testMethodMemberInfo, testClassType, new List<object>(), categories, authors, descriptions);
            throw;
        }
    }

    public void CoreTestCleanup(string testName)
    {
        string consoleOutput = _stringWriter.ToString();
        _stringWriter.Close();
        string stackTrace = ThrownException?.Value?.ToString();
        var testClassType = TestClassType;
        var testMethodMemberInfo = GetCurrentExecutionMethodInfo(testName);
        Container = ServicesCollection.Current.FindCollection(testClassType.FullName);
        _currentTestExecutionProvider = new PluginProvider();
        InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
        try
        {
            _currentTestExecutionProvider.PreTestCleanup(TestOutcome.Passed, testName, testMethodMemberInfo, testClassType, new List<object>(), _categories, _authors, _descriptions, consoleOutput, stackTrace, ThrownException?.Value);
            TestCleanup();
            _currentTestExecutionProvider.PostTestCleanup(TestOutcome.Passed, testName, testMethodMemberInfo, testClassType, new List<object>(), _categories, _authors, _descriptions, consoleOutput, stackTrace, ThrownException?.Value);
        }
        catch (Exception ex)
        {
            _currentTestExecutionProvider.TestCleanupFailed(ex, testName, testMethodMemberInfo, testClassType, new List<object>(), _categories, _authors, _descriptions);
            throw;
        }
    }

    public void OneTimeArrangeAct()
    {
        try
        {
            var testClassType = TestClassType;
            Container = ServicesCollection.Current.CreateChildServicesCollection(testClassType.FullName);
            Container.RegisterInstance(Container);
            _currentTestExecutionProvider = new PluginProvider();
            InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
            _currentTestExecutionProvider.PreTestsArrange(testClassType, new List<object>());
            TestsArrange();
            _currentTestExecutionProvider.PostTestsArrange(testClassType);
            _currentTestExecutionProvider.PreTestsAct(testClassType, new List<object>());
            TestsAct();
            _currentTestExecutionProvider.PostTestsAct(testClassType, new List<object>());
        }
        catch (Exception ex)
        {
            _currentTestExecutionProvider.TestsArrangeFailed(ex);
        }
    }

    public virtual void TestsArrange()
    {
    }

    public virtual void TestsAct()
    {
    }

    public virtual void TestInit()
    {
    }

    public virtual void TestCleanup()
    {
    }

    private void InitializeTestExecutionBehaviorObservers(PluginProvider testExecutionProvider)
    {
        var observers = ServicesCollection.Current.ResolveAll<Plugin>();
        foreach (var observer in observers)
        {
            observer.Subscribe(testExecutionProvider);
        }
    }

    private MethodInfo GetCurrentExecutionMethodInfo(string testName)
    {
        var testMethodMemberInfo = GetType().GetMethod(testName);
        return testMethodMemberInfo;
    }
}