// <copyright file="TestWorkflowPluginProvider.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bellatrix.TestWorkflowPlugins
{
    public class TestWorkflowPluginProvider : ITestWorkflowPluginProvider
    {
        public event EventHandler<TestWorkflowPluginEventArgs> PreTestInitEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> TestInitFailedEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PostTestInitEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PreTestCleanupEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PostTestCleanupEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> TestCleanupFailedEvent;

        public event EventHandler<Exception> ClassInitFailedEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> TestsArrangeFailedEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PreTestsActEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PreTestsArrangeEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PostTestsActEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PostTestsArrangeEvent;

        public void PreTestsAct(string testName, MemberInfo testMethodMemberInfo, Type testClassType)
            => RaiseTestEvent(PreTestsActEvent, TestOutcome.Unknown, testName, testMethodMemberInfo, testClassType);

        public void PreTestsArrange(string testName, MemberInfo testMethodMemberInfo, Type testClassType)
            => RaiseTestEvent(PreTestsArrangeEvent, TestOutcome.Unknown, testName, testMethodMemberInfo, testClassType);

        public void PreTestInit(string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<string> categories, List<string> authors, List<string> descriptions)
            => RaiseTestEvent(PreTestInitEvent, TestOutcome.Unknown, testName, testMethodMemberInfo, testClassType, categories, authors, descriptions);

        public void TestInitFailed(Exception ex, string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<string> categories, List<string> authors, List<string> descriptions, string message, string stackTrace)
            => RaiseTestEvent(TestInitFailedEvent, ex, testName, testMethodMemberInfo, testClassType, categories, authors, descriptions, message, stackTrace);

        public void PostTestInit(string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<string> categories, List<string> authors, List<string> descriptions)
            => RaiseTestEvent(PostTestInitEvent, TestOutcome.Unknown, testName, testMethodMemberInfo, testClassType, categories, authors, descriptions);

        public void PreTestCleanup(TestOutcome testOutcome, string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<string> categories, List<string> authors, List<string> descriptions, string message, string stackTrace, Exception exception)
            => RaiseTestEvent(PreTestCleanupEvent, testOutcome, testName, testMethodMemberInfo, testClassType, categories, authors, descriptions, message, stackTrace, exception);

        public void PostTestCleanup(TestOutcome testOutcome, string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<string> categories, List<string> authors, List<string> descriptions, string message, string stackTrace, Exception exception)
            => RaiseTestEvent(PostTestCleanupEvent, testOutcome, testName, testMethodMemberInfo, testClassType, categories, authors, descriptions, message, stackTrace, exception);

        public void TestCleanupFailed(Exception ex, string testName, MemberInfo testMethodMemberInfo, Type testClassType, List<string> categories, List<string> authors, List<string> descriptions, string message, string stackTrace)
            => RaiseTestEvent(TestCleanupFailedEvent, ex, testName, testMethodMemberInfo, testClassType, categories, authors, descriptions, message, stackTrace);

        public void OnClassInitFailed(Exception ex) => ClassInitFailedEvent?.Invoke(this, ex);

        public void TestsArrangeFailed(Exception ex, string testName, MethodInfo testMethodMemberInfo, Type testClassType, List<string> categories, List<string> authors, List<string> descriptions, string message, string stackTrace)
            => RaiseTestEvent(TestsArrangeFailedEvent, ex, testName, testMethodMemberInfo, testClassType, categories, authors, descriptions, message, stackTrace);

        public void PostTestsAct(string testName, MemberInfo testMethodMemberInfo, Type testClassType)
            => RaiseTestEvent(PostTestsActEvent, TestOutcome.Unknown, testName, testMethodMemberInfo, testClassType);

        public void PostTestsArrange(string testName, MemberInfo testMethodMemberInfo, Type testClassType)
            => RaiseTestEvent(PostTestsArrangeEvent, TestOutcome.Unknown, testName, testMethodMemberInfo, testClassType);

        private void RaiseTestEvent(
            EventHandler<TestWorkflowPluginEventArgs> eventHandler,
            Exception exception,
            string testName,
            MemberInfo testMethodMemberInfo,
            Type testClassType,
            List<string> categories,
            List<string> authors,
            List<string> descriptions,
            string message = null,
            string stackTrace = null)
        {
            var args = new TestWorkflowPluginEventArgs(exception, testName, testMethodMemberInfo, testClassType, message, stackTrace, categories, authors, descriptions);
            eventHandler?.Invoke(this, args);
        }

        private void RaiseTestEvent(
            EventHandler<TestWorkflowPluginEventArgs> eventHandler,
            TestOutcome testOutcome,
            string testName,
            MemberInfo testMethodMemberInfo,
            Type testClassType,
            List<string> categories,
            List<string> authors,
            List<string> descriptions,
            string message = null,
            string stackTrace = null,
            Exception exception = null)
        {
            var args = new TestWorkflowPluginEventArgs(testOutcome, testName, testMethodMemberInfo, testClassType, message, stackTrace, exception, categories, authors, descriptions);
            eventHandler?.Invoke(this, args);
        }

        private void RaiseTestEvent(
            EventHandler<TestWorkflowPluginEventArgs> eventHandler,
            TestOutcome testOutcome,
            string testName,
            MemberInfo testMethodMemberInfo,
            Type testClassType,
            string message = null,
            string stackTrace = null)
        {
            var args = new TestWorkflowPluginEventArgs(testOutcome, testName, testMethodMemberInfo, testClassType, message, stackTrace);
            eventHandler?.Invoke(this, args);
        }
    }
}