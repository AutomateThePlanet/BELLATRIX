// <copyright file="BaseTest.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Bellatrix.Infrastructure;
using Bellatrix.MSTest;
using Bellatrix.TestWorkflowPlugins;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix
{
    [TestClass]
    public class MSTestBaseTest
    {
        public IServicesCollection Container;
        protected MSTestExecutionContext ЕxecutionContext;
        private static readonly List<string> TypeForAlreadyExecutedClassInits = new List<string>();
        private TestWorkflowPluginProvider _currentTestExecutionProvider;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void CoreTestInit()
        {
            if (ЕxecutionContext == null)
            {
                ЕxecutionContext = new MSTestExecutionContext(TestContext);
            }

            ServicesCollection.Main.RegisterInstance(ЕxecutionContext.SharedData.ContainsKey("isParallelRun"), "isParallelRun");
            if (ЕxecutionContext.SharedData.ContainsKey("ExecutionDirectory"))
            {
                ServicesCollection.Main.RegisterInstance((string)ЕxecutionContext.SharedData["ExecutionDirectory"], "ExecutionDirectory");
            }

            var testMethodMemberInfo = GetCurrentExecutionMethodInfo(ЕxecutionContext);
            var testClassType = GetCurrentExecutionTestClassType(ЕxecutionContext);
            ExecuteActArrangePhases();

            Container = ServicesCollection.Current.FindCollection(testClassType.FullName);
            Container.RegisterInstance<ExecutionContext>(ЕxecutionContext);
            _currentTestExecutionProvider = new TestWorkflowPluginProvider();
            InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);

            var categories = GetAllTestCategories(testMethodMemberInfo);
            var authors = GetAllAuthors(testMethodMemberInfo);
            var descriptions = GetAllDescriptions(testMethodMemberInfo);
            try
            {
                Initialize();
                _currentTestExecutionProvider.PreTestInit(ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions);
                TestInit();
                _currentTestExecutionProvider.PostTestInit(ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions);
            }
            catch (Exception ex)
            {
                _currentTestExecutionProvider.TestInitFailed(ex, ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions);
                throw;
            }
        }

        [TestCleanup]
        public void CoreTestCleanup()
        {
            if (ЕxecutionContext == null)
            {
                ЕxecutionContext = new MSTestExecutionContext(TestContext);
            }

            var testMethodMemberInfo = GetCurrentExecutionMethodInfo(ЕxecutionContext);
            var testClassType = GetCurrentExecutionTestClassType(ЕxecutionContext);
            var categories = GetAllTestCategories(testMethodMemberInfo);
            var authors = GetAllAuthors(testMethodMemberInfo);
            var descriptions = GetAllDescriptions(testMethodMemberInfo);

            Container = ServicesCollection.Current.FindCollection(testClassType.FullName);
            Container.RegisterInstance<ExecutionContext>(ЕxecutionContext);
            _currentTestExecutionProvider = new TestWorkflowPluginProvider();
            InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);

            try
            {
                _currentTestExecutionProvider.PreTestCleanup((TestOutcome)ЕxecutionContext.TestOutcome, ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions, ЕxecutionContext.ExceptionMessage, ЕxecutionContext.ExceptionStackTrace, ЕxecutionContext.Exception);
                TestCleanup();
                _currentTestExecutionProvider.PostTestCleanup((TestOutcome)ЕxecutionContext.TestOutcome, ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions, ЕxecutionContext.ExceptionMessage, ЕxecutionContext.ExceptionStackTrace, ЕxecutionContext.Exception);
            }
            catch (Exception ex)
            {
                _currentTestExecutionProvider.TestCleanupFailed(ex, ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions);
                throw;
            }
        }

        [ClassCleanup]
        public void CoreClassCleanup()
        {
            if (ЕxecutionContext == null)
            {
                ЕxecutionContext = new MSTestExecutionContext(TestContext);
            }

            var testClassType = GetCurrentExecutionTestClassType(ЕxecutionContext);

            Container = ServicesCollection.Current.FindCollection(testClassType.FullName);
            Container.RegisterInstance<ExecutionContext>(ЕxecutionContext);
            _currentTestExecutionProvider = new TestWorkflowPluginProvider();
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

        private void InitializeTestExecutionBehaviorObservers(TestWorkflowPluginProvider testExecutionProvider)
        {
            var observers = ServicesCollection.Current.ResolveAll<TestWorkflowPlugin>();
            foreach (var observer in observers)
            {
                observer.Subscribe(testExecutionProvider);
            }
        }

        private void ExecuteActArrangePhases()
        {
            try
            {
                var testClassType = GetCurrentExecutionTestClassType(ЕxecutionContext);
                if (!TypeForAlreadyExecutedClassInits.Contains(ЕxecutionContext.TestClassName))
                {
                    Container = ServicesCollection.Current.CreateChildServicesCollection(testClassType.FullName);
                    Container.RegisterInstance(Container);
                    Container.RegisterInstance<ExecutionContext>(ЕxecutionContext);
                    _currentTestExecutionProvider = new TestWorkflowPluginProvider();
                    InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
                    TypeForAlreadyExecutedClassInits.Add(ЕxecutionContext.TestClassName);
                    _currentTestExecutionProvider.PreTestsArrange(testClassType);
                    Initialize();
                    TestsArrange();
                    _currentTestExecutionProvider.PostTestsArrange(testClassType);
                    _currentTestExecutionProvider.PreTestsAct(testClassType);
                    TestsAct();
                    _currentTestExecutionProvider.PostTestsAct(testClassType);
                }
            }
            catch (Exception ex)
            {
                _currentTestExecutionProvider.TestsArrangeFailed(ex);
            }
        }

        private MethodInfo GetCurrentExecutionMethodInfo(MSTestExecutionContext executionContext)
        {
            var memberInfo = GetCurrentExecutionTestClassType(executionContext).GetMethod(executionContext.TestName);
            return memberInfo;
        }

        private Type GetCurrentExecutionTestClassType(MSTestExecutionContext executionContext)
        {
            var testClassType = GetType().Assembly.GetType(executionContext.TestClassName);

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
}