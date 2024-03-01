// <copyright file="PluginProvider.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;

namespace Bellatrix.Plugins;

public class PluginProvider : IPluginProvider
{
    public event EventHandler<PluginEventArgs> PreTestInitEvent;

    public event EventHandler<PluginEventArgs> TestInitFailedEvent;

    public event EventHandler<PluginEventArgs> PostTestInitEvent;

    public event EventHandler<PluginEventArgs> PreTestCleanupEvent;

    public event EventHandler<PluginEventArgs> PostTestCleanupEvent;

    public event EventHandler<PluginEventArgs> TestCleanupFailedEvent;

    public event EventHandler<PluginEventArgs> PreTestsActEvent;

    public event EventHandler<PluginEventArgs> PreTestsArrangeEvent;

    public event EventHandler<PluginEventArgs> PostTestsActEvent;

    public event EventHandler<PluginEventArgs> PostTestsArrangeEvent;

    public event EventHandler<PluginEventArgs> PreTestsCleanupEvent;

    public event EventHandler<PluginEventArgs> PostTestsCleanupEvent;

    public event EventHandler<Exception> TestsCleanupFailedEvent;

    public event EventHandler<Exception> TestsArrangeFailedEvent;

    public void PreTestsArrange(Type testClassType, List<object> arguments)
    {
        RaiseClassTestEvent(PreTestsArrangeEvent, TestOutcome.Unknown, testClassType, arguments);
    }

    public void TestsArrangeFailed(Exception ex)
    {
        TestsArrangeFailedEvent.Invoke(this, ex);
    }

    public void PostTestsArrange(Type testClassType)
    {
        RaiseClassTestEvent(PostTestsArrangeEvent, TestOutcome.Unknown, testClassType);
    }

    public void PreTestsAct(Type testClassType, List<object> arguments)
    {
        RaiseClassTestEvent(PreTestsActEvent, TestOutcome.Unknown, testClassType, arguments);
    }

    public void PostTestsAct(Type testClassType, List<object> arguments)
    {
        RaiseClassTestEvent(PostTestsActEvent, TestOutcome.Unknown, testClassType, arguments);
    }

    public void PreTestInit(string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<object> arguments, List<string> categories, List<string> authors, List<string> descriptions)
    {
        RaiseTestEvent(PreTestInitEvent, TestOutcome.Unknown, testName, testMethodMemberInfo, testClassType, arguments, categories, authors, descriptions);
    }

    public void TestInitFailed(Exception ex, string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<object> arguments, List<string> categories, List<string> authors, List<string> descriptions)
    {
        RaiseTestEvent(TestInitFailedEvent, TestOutcome.Failed, testName, testMethodMemberInfo, testClassType, arguments, categories, authors, descriptions, ex.Message, ex.StackTrace);
    }

    public void PostTestInit(string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<object> arguments, List<string> categories, List<string> authors, List<string> descriptions)
    {
        RaiseTestEvent(PostTestInitEvent, TestOutcome.Unknown, testName, testMethodMemberInfo, testClassType, arguments, categories, authors, descriptions);
    }

    public void PreTestCleanup(TestOutcome testOutcome, string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<object> arguments, List<string> categories, List<string> authors, List<string> descriptions, string message, string stackTrace, Exception exception)
    {
        RaiseTestEvent(PreTestCleanupEvent, testOutcome, testName, testMethodMemberInfo, testClassType, arguments, categories, authors, descriptions, message, stackTrace, exception);
    }

    public void TestCleanupFailed(Exception ex, string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<object> arguments, List<string> categories, List<string> authors, List<string> descriptions)
    {
        RaiseTestEvent(TestCleanupFailedEvent, TestOutcome.Failed, testName, testMethodMemberInfo, testClassType, arguments, categories, authors, descriptions, ex.Message, ex.StackTrace);
    }

    public void PostTestCleanup(TestOutcome testOutcome, string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<object> arguments, List<string> categories, List<string> authors, List<string> descriptions, string message, string stackTrace, Exception exception)
    {
        RaiseTestEvent(PostTestCleanupEvent, testOutcome, testName, testMethodMemberInfo, testClassType, arguments, categories, authors, descriptions, message, stackTrace, exception);
    }

    public void PreClassCleanup(Type testClassType)
    {
        RaiseClassTestEvent(PreTestsCleanupEvent, TestOutcome.Unknown, testClassType);
    }

    public void TestsCleanupFailed(Exception ex) => TestsCleanupFailedEvent?.Invoke(this, ex);

    public void PostClassCleanup(Type testClassType)
    {
        RaiseClassTestEvent(PostTestsCleanupEvent, TestOutcome.Unknown, testClassType);
    }

    private void RaiseClassTestEvent(EventHandler<PluginEventArgs> eventHandler, TestOutcome testOutcome, Type testClassType, List<object> arguments = null)
    {
        var args = new PluginEventArgs(testOutcome, testClassType, arguments);
        eventHandler?.Invoke(this, args);
    }

    private void RaiseTestEvent(
        EventHandler<PluginEventArgs> eventHandler,
        TestOutcome testOutcome,
        string testName,
        MemberInfo testMethodMemberInfo,
        Type testClassType,
        List<object> arguments,
        List<string> categories,
        List<string> authors,
        List<string> descriptions,
        string message = null,
        string stackTrace = null,
        Exception exception = null)
    {
        var args = new PluginEventArgs(testOutcome, testName, testMethodMemberInfo, testClassType, arguments, message, stackTrace, exception, categories, authors, descriptions);
        eventHandler?.Invoke(this, args);
    }
}