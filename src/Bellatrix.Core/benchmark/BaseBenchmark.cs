// <copyright file="BaseBenchmark.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;
using System.Threading;
using Bellatrix.Plugins;

namespace Bellatrix.Benchmark;

public abstract class BaseBenchmark
{
    private static readonly List<string> TypeForAlreadyExecutedBenchmarkInits = new List<string>();
    private static ThreadLocal<Exception> _thrownException;
    private readonly PluginProvider _currentBenchmarkExecutionProvider;
    private readonly List<string> _authors = new List<string>();
    private readonly List<string> _categories = new List<string>();
    private readonly List<string> _descriptions = new List<string>();

    protected BaseBenchmark()
    {
        _currentBenchmarkExecutionProvider = new PluginProvider();
        InitializeBenchmarksExecutionBehaviorObservers(_currentBenchmarkExecutionProvider);
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

    public void CoreIterationSetup()
    {
        if (_thrownException?.Value != null)
        {
            _thrownException.Value = null;
        }

        var stackTrace = new StackTrace();
        var benchmarkMethodMemberInfo = stackTrace.GetFrame(1).GetMethod() as MethodInfo;
        var benchmarkClassType = benchmarkMethodMemberInfo?.DeclaringType;
        var categories = _categories;
        var authors = _authors;
        var descriptions = _descriptions;
        try
        {
            _currentBenchmarkExecutionProvider.PreTestInit(benchmarkMethodMemberInfo?.Name, benchmarkMethodMemberInfo, benchmarkClassType, new List<object>(), categories, authors, descriptions);
            IterationInitialize();
            _currentBenchmarkExecutionProvider.PostTestInit(benchmarkMethodMemberInfo?.Name, benchmarkMethodMemberInfo, benchmarkClassType, new List<object>(), categories, authors, descriptions);
        }
        catch (Exception ex)
        {
            _currentBenchmarkExecutionProvider.TestInitFailed(ex, benchmarkMethodMemberInfo?.Name, benchmarkMethodMemberInfo, benchmarkClassType, new List<object>(), categories, authors, descriptions);
            throw;
        }
    }

    public void CoreIterationCleanup()
    {
        var exceptionStackTrace = _thrownException?.Value?.ToString();
        var stackTrace = new StackTrace();
        var benchmarkMethodMemberInfo = stackTrace.GetFrame(1).GetMethod() as MethodInfo;
        var benchmarkClassType = benchmarkMethodMemberInfo?.DeclaringType;

        try
        {
            _currentBenchmarkExecutionProvider.PreTestCleanup(TestOutcome.Passed, benchmarkMethodMemberInfo?.Name, benchmarkMethodMemberInfo, benchmarkClassType, new List<object>(), _categories, _authors, _descriptions, string.Empty, exceptionStackTrace, _thrownException?.Value);
            IterationCleanup();
            _currentBenchmarkExecutionProvider.PostTestCleanup(TestOutcome.Passed, benchmarkMethodMemberInfo?.Name, benchmarkMethodMemberInfo, benchmarkClassType, new List<object>(), _categories, _authors, _descriptions, string.Empty, exceptionStackTrace, _thrownException?.Value);
        }
        catch (Exception ex)
        {
            _currentBenchmarkExecutionProvider.TestCleanupFailed(ex, benchmarkMethodMemberInfo?.Name, benchmarkMethodMemberInfo, benchmarkClassType, new List<object>(), _categories, _authors, _descriptions);
            throw;
        }
    }

    public void CoreExecuteActArrangePhases()
    {
        try
        {
            var stackTrace = new StackTrace();
            var benchmarkMethodMemberInfo = stackTrace.GetFrame(1).GetMethod() as MethodInfo;
            var benchmarkClassType = benchmarkMethodMemberInfo?.DeclaringType;

            if (!TypeForAlreadyExecutedBenchmarkInits.Contains(benchmarkClassType?.FullName))
            {
                TypeForAlreadyExecutedBenchmarkInits.Add(benchmarkClassType?.FullName);
                _currentBenchmarkExecutionProvider.PreTestsArrange(benchmarkClassType, new List<object>());
                BenchmarkArrange();
                _currentBenchmarkExecutionProvider.PostTestsArrange(benchmarkClassType);
                _currentBenchmarkExecutionProvider.PreTestsAct(benchmarkClassType, new List<object>());
                BenchmarkAct();
                _currentBenchmarkExecutionProvider.PostTestsAct(benchmarkClassType, new List<object>());
            }
        }
        catch (Exception ex)
        {
            _currentBenchmarkExecutionProvider.TestsArrangeFailed(ex);
        }
    }

    // Called twice - once for BenchmarkA and once for BenchmarkB. But engine will make sure that act and arrange are executed only once.
    public void CoreBenchmarkCleanup()
    {
        BenchmarkCleanup();
    }

    public virtual void BenchmarkArrange()
    {
    }

    public virtual void BenchmarkAct()
    {
    }

    public virtual void BenchmarkCleanup()
    {
    }

    public virtual void IterationInitialize()
    {
    }

    public virtual void IterationCleanup()
    {
    }

    private void InitializeBenchmarksExecutionBehaviorObservers(PluginProvider testExecutionProvider)
    {
        var observers = ServicesCollection.Current.ResolveAll<Plugin>();
        foreach (var observer in observers)
        {
            observer.Subscribe(testExecutionProvider);
        }
    }
}