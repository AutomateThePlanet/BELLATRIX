// <copyright file="App.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Azure.AI.FormRecognizer;
using Bellatrix.Api;
using Bellatrix.Api.Configuration;
using Bellatrix.Assertions;
using Bellatrix.AWS;
using Bellatrix.DynamicTestCases;
using Bellatrix.Playwright.Controls.Advanced.ControlDataHandlers;
using Bellatrix.Playwright.Controls.EventHandlers;
using Bellatrix.Playwright.Proxy;
using Bellatrix.Playwright.Services;
using Bellatrix.Plugins;
using Bellatrix.Utilities;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;

namespace Bellatrix.Playwright;

public class App : IDisposable
{
    private readonly ApiClientService _apiClientService;

    public App()
    {
        _apiClientService = GetNewApiClientService();
    }

    public BrowserService Browser => ServicesCollection.Current.Resolve<BrowserService>();
    public NavigationService Navigation => ServicesCollection.Current.Resolve<NavigationService>();
    public DevToolsService DevTools => ServicesCollection.Current.Resolve<DevToolsService>();
    public DialogService Dialogs => ServicesCollection.Current.Resolve<DialogService>();
    public JavaScriptService JavaScript => ServicesCollection.Current.Resolve<JavaScriptService>();
    public InteractionsService Interactions => ServicesCollection.Current.Resolve<InteractionsService>();
    public CookiesService Cookies => ServicesCollection.Current.Resolve<CookiesService>();
    public ComponentCreateService Components => ServicesCollection.Current.Resolve<ComponentCreateService>();
    public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    public LighthouseService Lighthouse => ServicesCollection.Current.Resolve<LighthouseService>();
    public IAssert Assert => ServicesCollection.Current.Resolve<IAssert>();
    public ProxyService Proxy => ServicesCollection.Current.Resolve<ProxyService>();

    public ComputerVisionClient ComputerVision => ServicesCollection.Current.Resolve<ComputerVisionClient>();

    public FormRecognizerClient FormRecognizer => ServicesCollection.Current.Resolve<FormRecognizerClient>();
    public AWSServicesFactory AWS => ServicesCollection.Current.Resolve<AWSServicesFactory>();

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

                client.BaseUrl = new Uri(apiSettingsConfig.BaseUrl);
            }
            else
            {
                client.BaseUrl = new Uri(url);
            }

            if (sharedCookies)
            {
                client.CookieContainer = new System.Net.CookieContainer();
            }

            client.PauseBetweenFailures = TimeSpanConverter.Convert(pauseBetweenFailures, timeUnit);
            client.MaxRetryAttempts = maxRetryAttempts;

            ServicesCollection.Current.RegisterInstance(client);
        }

        return client;
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
        Proxy?.Start();
    }

    public void Dispose()
    {
        DisposeBrowserService.DisposeAll();
        DisposeBrowserService.Dispose();
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
}