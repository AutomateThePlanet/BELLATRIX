// <copyright file="App.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Reflection;
using Bellatrix.Application;
using Bellatrix.DynamicTestCases;
using Bellatrix.Logging;
using Bellatrix.Mobile.Configuration;
using Bellatrix.Mobile.PageObjects;
using Bellatrix.Mobile.Services;
using Bellatrix.TestWorkflowPlugins;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace Bellatrix.Mobile
{
    public abstract class App<TDriver, TDriverElement> : BaseApp, IDisposable
        where TDriver : AppiumDriver<TDriverElement>
        where TDriverElement : AppiumWebElement
    {
        private readonly bool _shouldStartAppiumLocalService;

        public App()
            => _shouldStartAppiumLocalService = ConfigurationService.GetSection<MobileSettings>().ShouldStartAppiumLocalService;

        public ElementWaitService<TDriver, TDriverElement> ElementWaitService => ServicesCollection.Current.Resolve<ElementWaitService<TDriver, TDriverElement>>();

        public ElementCreateService ElementCreateService => ServicesCollection.Current.Resolve<ElementCreateService>();

        public WebServicesFacade Web => ServicesCollection.Current.Resolve<WebServicesFacade>();

        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

        public abstract void Initialize();

        public void StartAppiumLocalService()
        {
            if (_shouldStartAppiumLocalService)
            {
                var args = new OptionCollector().AddArguments(GeneralOptionList.PreLaunch());
                WrappedAppiumCreateService.AppiumLocalService = new AppiumServiceBuilder().WithArguments(args).UsingAnyFreePort().Build();
                WrappedAppiumCreateService.AppiumLocalService.Start();
            }
        }

        public void StopAppiumLocalService()
        {
            if (_shouldStartAppiumLocalService)
            {
                WrappedAppiumCreateService.AppiumLocalService.Dispose();
            }
        }

        public void AddAdditionalCapability(string name, object value)
        {
            string fullClassName = DetermineTestClassFullNameAttributes();
            var dictionary = ServicesCollection.Current.Resolve<Dictionary<string, object>>($"caps-{fullClassName}") ?? new Dictionary<string, object>();
            dictionary.Add(name, value);
            ServicesCollection.Current.RegisterInstance(dictionary, $"caps-{fullClassName}");
        }

        public void AddTestWorkflowPlugin<TExecutionExtension>()
            where TExecutionExtension : TestWorkflowPlugin
        {
            RegisterType<TestWorkflowPlugin, TExecutionExtension>(Guid.NewGuid().ToString());
        }

        public abstract void Dispose();

        public TPage Create<TPage>()
            where TPage : Page
        {
            TPage page = ServicesCollection.Current.Resolve<TPage>();
            return page;
        }

        private string DetermineTestClassFullNameAttributes()
        {
            string fullClassName = string.Empty;
            var callStackTrace = new StackTrace();
            var currentAssembly = GetType().Assembly;

            foreach (var frame in callStackTrace.GetFrames())
            {
                var frameMethodInfo = frame.GetMethod() as MethodInfo;
                if (!frameMethodInfo?.ReflectedType?.Assembly.Equals(currentAssembly) == true &&
                    frameMethodInfo.Name.Equals("TestsArrange") || frameMethodInfo.Name.Equals("ScenarioInitialize"))
                {
                    fullClassName = frameMethodInfo.DeclaringType.FullName;
                    break;
                }
            }

            return fullClassName;
        }
    }
}
