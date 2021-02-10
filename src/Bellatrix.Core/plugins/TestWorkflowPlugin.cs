// <copyright file="TestWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using System.Reflection;

namespace Bellatrix.TestWorkflowPlugins
{
    public class TestWorkflowPlugin
    {
        public void Subscribe(ITestWorkflowPluginProvider provider)
        {
            provider.PreTestInitEvent += PreTestInit;
            provider.TestInitFailedEvent += TestInitFailed;
            provider.PostTestInitEvent += PostTestInit;
            provider.PreTestCleanupEvent += PreTestCleanup;
            provider.PostTestCleanupEvent += PostTestCleanup;
            provider.TestCleanupFailedEvent += TestCleanupFailed;
            provider.PreTestsArrangeEvent += PreTestsArrange;
            provider.TestsArrangeFailedEvent += TestsArrangeFailed;
            provider.PreTestsActEvent += PreTestsAct;
            provider.PostTestsActEvent += PostTestsAct;
            provider.PostTestsArrangeEvent += PostTestsArrange;
            provider.PreTestsCleanupEvent += PreTestsCleanup;
            provider.PostTestsCleanupEvent += PostTestsCleanup;
            provider.TestsCleanupFailedEvent += TestsCleanupFailed;
        }

        public void Unsubscribe(ITestWorkflowPluginProvider provider)
        {
            provider.PreTestInitEvent -= PreTestInit;
            provider.TestInitFailedEvent -= TestInitFailed;
            provider.PostTestInitEvent -= PostTestInit;
            provider.PreTestCleanupEvent -= PreTestCleanup;
            provider.PostTestCleanupEvent -= PostTestCleanup;
            provider.TestCleanupFailedEvent -= TestCleanupFailed;
            provider.PreTestsArrangeEvent -= PreTestsArrange;
            provider.TestsArrangeFailedEvent -= TestsArrangeFailed;
            provider.PreTestsActEvent -= PreTestsAct;
            provider.PostTestsActEvent -= PostTestsAct;
            provider.PostTestsArrangeEvent -= PostTestsArrange;
            provider.PreTestsCleanupEvent -= PreTestsCleanup;
            provider.PostTestsCleanupEvent -= PostTestsCleanup;
            provider.TestsCleanupFailedEvent -= TestsCleanupFailed;
        }

        protected virtual void TestsCleanupFailed(object sender, Exception ex)
        {
        }

        protected virtual void PreTestsCleanup(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PostTestsCleanup(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PreTestInit(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void TestInitFailed(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PostTestInit(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PreTestCleanup(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PostTestCleanup(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void TestCleanupFailed(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void TestsArrangeFailed(object sender, Exception e)
        {
        }

        protected virtual void PreTestsAct(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PreTestsArrange(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PostTestsAct(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PostTestsArrange(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected List<dynamic> GetAllAttributes<TAttribute>(MemberInfo memberInfo)
        where TAttribute : Attribute
        {
            var classAttributes = GetClassAttributes<TAttribute>(memberInfo.DeclaringType);
            var methodAttributes = GetMethodAttributes<TAttribute>(memberInfo);
            var attributes = classAttributes.ToList();
            attributes.AddRange(methodAttributes);

            return attributes;
        }

        protected TAttribute GetOverridenAttribute<TAttribute>(MemberInfo memberInfo)
        where TAttribute : Attribute
        {
            var classAttribute = GetClassAttribute<TAttribute>(memberInfo.DeclaringType);
            var methodAttribute = GetMethodAttribute<TAttribute>(memberInfo);
            if (methodAttribute != null)
            {
                return methodAttribute;
            }

            return classAttribute;
        }

        protected dynamic GetClassAttribute<TAttribute>(Type currentType)
        where TAttribute : Attribute
        {
            var classAttribute = currentType.GetCustomAttribute<TAttribute>(true);

            return classAttribute;
        }

        protected dynamic GetMethodAttribute<TAttribute>(MemberInfo memberInfo)
        where TAttribute : Attribute
        {
            var methodAttribute = memberInfo?.GetCustomAttribute<TAttribute>(true);

            return methodAttribute;
        }

        protected IEnumerable<dynamic> GetClassAttributes<TAttribute>(Type currentType)
        where TAttribute : Attribute
        {
            var classAttributes = currentType.GetCustomAttributes<TAttribute>(true);

            return classAttributes;
        }

        protected IEnumerable<dynamic> GetMethodAttributes<TAttribute>(MemberInfo memberInfo)
        where TAttribute : Attribute
        {
            var methodAttributes = memberInfo?.GetCustomAttributes<TAttribute>(true);

            return methodAttributes;
        }
    }
}