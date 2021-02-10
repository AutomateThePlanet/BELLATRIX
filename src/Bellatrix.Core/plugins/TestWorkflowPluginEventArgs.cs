// <copyright file="TestWorkflowPluginEventArgs.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
    public class TestWorkflowPluginEventArgs : EventArgs
    {
        public TestWorkflowPluginEventArgs()
        {
        }

        public TestWorkflowPluginEventArgs(TestOutcome testOutcome,
            string testName,
            MemberInfo testMethodMemberInfo,
            Type testClassType,
            string consoleOutputMessage,
            string consoleOutputStackTrace,
            Exception exception,
            List<string> categories,
            List<string> authors,
            List<string> descriptions)
            : this(testOutcome,
                testClassType,
                consoleOutputMessage,
                consoleOutputStackTrace)
        {
            TestMethodMemberInfo = testMethodMemberInfo;
            TestName = testName;
            Exception = exception;
            Categories = categories;
            Authors = authors;
            Descriptions = descriptions;
        }

        public TestWorkflowPluginEventArgs(
            TestOutcome testOutcome,
            Type testClassType,
            string consoleOutputMessage = null,
            string consoleOutputStackTrace = null)
        {
            TestOutcome = testOutcome;
            TestClassType = testClassType;
            TestClassName = testClassType.FullName;
            TestFullName = $"{TestClassName}.{TestName}";
            ConsoleOutputMessage = consoleOutputMessage;
            ConsoleOutputStackTrace = consoleOutputStackTrace;
            Container = ServicesCollection.Current.FindCollection(testClassType.FullName);
        }

        public ServicesCollection Container { get; set; }

        public Exception Exception { get; }

        public MemberInfo TestMethodMemberInfo { get; }

        public Type TestClassType { get; }

        public TestOutcome TestOutcome { get; }

        public string TestName { get; }

        public string TestClassName { get; }

        public string TestFullName { get; }

        public string ConsoleOutputMessage { get; }

        public string ConsoleOutputStackTrace { get; }

        public List<string> Categories { get; }

        public List<string> Authors { get; }

        public List<string> Descriptions { get; }
    }
}