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
using Bellatrix.Api;
using Bellatrix.Api.Configuration;
using Bellatrix.Assertions;
using Bellatrix.CognitiveServices;
using Bellatrix.DynamicTestCases;
using Bellatrix.Plugins;
using Bellatrix.Utilities;
using Bellatrix.Web.Controls.Advanced.ControlDataHandlers;
using Bellatrix.Web.Controls.EventHandlers;
using Bellatrix.Web.Proxy;
using Bellatrix.Web.services;
using Bellatrix.Web.Services;
using OpenQA.Selenium;

namespace Bellatrix.Web
{
    public class App : IDisposable
    {
        private readonly ApiClientService _apiClientService;

        public App()
        {
            _apiClientService = GetNewApiClientService();
        }

        [Obsolete("BrowserService is deprecated use Browser property instead.")]
        public BrowserService BrowserService => ServicesCollection.Current.Resolve<BrowserService>();
        public BrowserService Browser => ServicesCollection.Current.Resolve<BrowserService>();

        [Obsolete("NavigationService is deprecated use Navigation property instead.")]
        public NavigationService NavigationService => ServicesCollection.Current.Resolve<NavigationService>();
        public NavigationService Navigation => ServicesCollection.Current.Resolve<NavigationService>();
        public DevToolsService DevTools => ServicesCollection.Current.Resolve<DevToolsService>();

        [Obsolete("DialogService is deprecated use Dialogs property instead.")]
        public DialogService DialogService => ServicesCollection.Current.Resolve<DialogService>();
        public DialogService Dialogs => ServicesCollection.Current.Resolve<DialogService>();

        [Obsolete("JavaScriptService is deprecated use JavaScript property instead.")]
        public JavaScriptService JavaScriptService => ServicesCollection.Current.Resolve<JavaScriptService>();
        public JavaScriptService JavaScript => ServicesCollection.Current.Resolve<JavaScriptService>();

        [Obsolete("InteractionsService is deprecated use Interactions property instead.")]
        public InteractionsService InteractionsService => ServicesCollection.Current.Resolve<InteractionsService>();
        public InteractionsService Interactions => ServicesCollection.Current.Resolve<InteractionsService>();

        [Obsolete("CookiesService is deprecated use Cookies property instead.")]
        public CookiesService CookiesService => ServicesCollection.Current.Resolve<CookiesService>();
        public CookiesService Cookies => ServicesCollection.Current.Resolve<CookiesService>();

        [Obsolete("ComponentCreateService is deprecated use Components property instead.")]
        public ComponentCreateService ComponentCreateService => ServicesCollection.Current.Resolve<ComponentCreateService>();
        public ComponentCreateService Components => ServicesCollection.Current.Resolve<ComponentCreateService>();

        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
        public LighthouseService Lighthouse => ServicesCollection.Current.Resolve<LighthouseService>();

        public IAssert Assert => ServicesCollection.Current.Resolve<IAssert>();

        [Obsolete("ProxyService is deprecated use Proxy property instead.")]
        public ProxyService ProxyService => ServicesCollection.Current.Resolve<ProxyService>();
        public ProxyService Proxy => ServicesCollection.Current.Resolve<ProxyService>();

        public ComputerVision ComputerVision => ServicesCollection.Current.Resolve<ComputerVision>();

        public FormRecognizer FormRecognizer => ServicesCollection.Current.Resolve<FormRecognizer>();

        public ApiClientService ApiClient
        {
            get => _apiClientService;
            init
            {
                _apiClientService = GetNewApiClientService();
            }
        }

        public ApiClientService GetNewApiClientService(string url = null, bool sharedCookies = true, int maxRetryAttempts = 1, int pauseBetweenFailures = 1, TimeUnit timeUnit = TimeUnit.Seconds)
        {
            ServicesCollection.Current.UnregisterSingleInstance<ApiClientService>();

            bool isClientRegistered = ServicesCollection.Current.IsRegistered<ApiClientService>();
            var client = ServicesCollection.Current.Resolve<ApiClientService>();

            if (!isClientRegistered || client == null)
            {
                client = new ApiClientService();
                if (string.IsNullOrEmpty(url))
                {
                    var apiSettingsConfig = ConfigurationService.GetSection<ApiSettings>();

                    client.WrappedClient.BaseUrl = new Uri(apiSettingsConfig.BaseUrl);
                }
                else
                {
                    client.WrappedClient.BaseUrl = new Uri(url);
                }

                if (sharedCookies)
                {
                    client.WrappedClient.CookieContainer = new System.Net.CookieContainer();
                }

                client.PauseBetweenFailures = TimeSpanConverter.Convert(pauseBetweenFailures, timeUnit);
                client.MaxRetryAttempts = maxRetryAttempts;

                ServicesCollection.Current.RegisterInstance(client);
            }

            return client;
        }

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

        public void AddReadonlyControlDataHandler<TComponent, TControlDataHandler>()
           where TComponent : Component
           where TControlDataHandler : IReadonlyControlDataHandler<TComponent>
        {
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<TComponent>, TControlDataHandler>();
        }

        public void AddEditableControlDataHandler<TComponent, TControlDataHandler>()
           where TComponent : Component
           where TControlDataHandler : IEditableControlDataHandler<TComponent>
        {
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<TComponent>, TControlDataHandler>();
        }

        public void AddElementEventHandler<TComponentsEventHandler>()
            where TComponentsEventHandler : ComponentEventHandlers
        {
            var elementEventHandler = (TComponentsEventHandler)Activator.CreateInstance(typeof(TComponentsEventHandler));
            elementEventHandler.SubscribeToAll();
        }

        public void RemoveElementEventHandler<TComponentsEventHandler>()
            where TComponentsEventHandler : ComponentEventHandlers
        {
            var elementEventHandler = (TComponentsEventHandler)Activator.CreateInstance(typeof(TComponentsEventHandler));
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
            DevTools?.Dispose();
            ProxyService?.Dispose();
            DisposeDriverService.DisposeAll();
            DisposeDriverService.Dispose();
            GC.SuppressFinalize(this);
        }

        public TPage Create<TPage>()
            where TPage : WebPage
        {
            TPage page = ServicesCollection.Current.Resolve<TPage>();
            return page;
        }

        public TPage GoTo<TPage>()
            where TPage : WebPage
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