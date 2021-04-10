// <copyright file="App.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Reflection;

using Bellatrix.DynamicTestCases;
using Bellatrix.ImageRecognition.ComputerVision;
using Bellatrix.Plugins;
using Bellatrix.Web.Controls.Advanced.ControlDataHandlers;
using Bellatrix.Web.Controls.EventHandlers;
using Bellatrix.Web.Proxy;
using Bellatrix.Web.Services;
using OpenQA.Selenium;

namespace Bellatrix.Web
{
    public class App : IDisposable
    {
        public BrowserService BrowserService => ServicesCollection.Current.Resolve<BrowserService>();

        public NavigationService NavigationService => ServicesCollection.Current.Resolve<NavigationService>();

        public DialogService DialogService => ServicesCollection.Current.Resolve<DialogService>();

        public JavaScriptService JavaScriptService => ServicesCollection.Current.Resolve<JavaScriptService>();

        public InteractionsService InteractionsService => ServicesCollection.Current.Resolve<InteractionsService>();

        public CookiesService CookieService => ServicesCollection.Current.Resolve<CookiesService>();

        public ElementCreateService ElementCreateService => ServicesCollection.Current.Resolve<ElementCreateService>();

        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

        public ProxyService ProxyService => ServicesCollection.Current.Resolve<ProxyService>();

        public ComputerVision ComputerVision => ServicesCollection.Current.Resolve<ComputerVision>();

        public void AddWebDriverOptions<TDriverOptions>(TDriverOptions options)
            where TDriverOptions : DriverOptions
        {
            string fullClassName = DetermineTestClassFullNameAttributes();
            ServicesCollection.Current.RegisterInstance(options, fullClassName);
        }

        public void AddWebDriverBrowserProfile<TBrowserProfile>(TBrowserProfile profile)
            where TBrowserProfile : class
        {
            string fullClassName = DetermineTestClassFullNameAttributes();
            ServicesCollection.Current.RegisterInstance(profile, fullClassName);
        }

        public void AddAdditionalCapability(string name, object value)
        {
            string fullClassName = DetermineTestClassFullNameAttributes();
            var dictionary = ServicesCollection.Current.Resolve<Dictionary<string, object>>($"caps-{fullClassName}") ?? new Dictionary<string, object>();
            dictionary.Add(name, value);
            ServicesCollection.Current.RegisterInstance(dictionary, $"caps-{fullClassName}");
        }

        public void AddReadonlyControlDataHandler<TElement, TControlDataHandler>()
           where TElement : Element
           where TControlDataHandler : IReadonlyControlDataHandler<TElement>
        {
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<TElement>, TControlDataHandler>();
        }

        public void AddEditableControlDataHandler<TElement, TControlDataHandler>()
           where TElement : Element
           where TControlDataHandler : IEditableControlDataHandler<TElement>
        {
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<TElement>, TControlDataHandler>();
        }

        public void AddElementEventHandler<TElementsEventHandler>()
            where TElementsEventHandler : ElementEventHandlers
        {
            var elementEventHandler = (TElementsEventHandler)Activator.CreateInstance(typeof(TElementsEventHandler));
            elementEventHandler.SubscribeToAll();
        }

        public void RemoveElementEventHandler<TElementsEventHandler>()
            where TElementsEventHandler : ElementEventHandlers
        {
            var elementEventHandler = (TElementsEventHandler)Activator.CreateInstance(typeof(TElementsEventHandler));
            elementEventHandler.UnsubscribeToAll();
        }

        public void AddPlugin<TExecutionExtension>()
            where TExecutionExtension : Plugin
        {
            ServicesCollection.Current.RegisterType<Plugin, TExecutionExtension>(Guid.NewGuid().ToString());
        }

        public void Initialize()
        {
            var proxyService = new ProxyService();
            ServicesCollection.Current.RegisterInstance(proxyService);
            proxyService?.Start();
        }

        public void Dispose()
        {
            ProxyService?.Dispose();
            DisposeDriverService.DisposeAll();
            DisposeDriverService.Dispose();
            GC.SuppressFinalize(this);
        }

        public TPage Create<TPage>()
            where TPage : Page
        {
            TPage page = ServicesCollection.Current.Resolve<TPage>();
            return page;
        }

        public TPage GoTo<TPage>()
            where TPage : NavigatablePage
        {
            TPage page = ServicesCollection.Current.Resolve<TPage>();
            page.Open();
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
                    (frameMethodInfo.Name.Equals("TestsArrange") || frameMethodInfo.Name.Equals("ScenarioInitialize")))
                {
                    fullClassName = frameMethodInfo.DeclaringType.FullName;
                    break;
                }
            }

            return fullClassName;
        }
    }
}