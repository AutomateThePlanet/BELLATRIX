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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using Bellatrix.Api.Configuration;
using Bellatrix.Api.Extensions;
using Bellatrix.DynamicTestCases;
using Bellatrix.Plugins;
using Bellatrix.Settings;
using Bellatrix.Utilities;

namespace Bellatrix.Api;

public class App
{
    private readonly ApiClientService _apiClientService;

    public App()
    {
        _apiClientService = GetApiClientService();
    }

    public ApiClientService ApiClient
    {
        get => _apiClientService;
        init
        {
            _apiClientService = GetApiClientService();
        }
    }

    public bool ShouldReuseRestClient { get; set; } = true;

    public LoadTestService LoadTestService => new LoadTestService();

    public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

    public void AddApiClientExecutionPlugin<TExecutionExtension>()
        where TExecutionExtension : ApiClientExecutionPlugin
    {
        ServicesCollection.Current.RegisterType<ApiClientExecutionPlugin, TExecutionExtension>(Guid.NewGuid().ToString());
    }

    public void AddPlugin<TExecutionExtension>()
        where TExecutionExtension : Plugin
    {
        ServicesCollection.Current.RegisterType<Plugin, TExecutionExtension>(Guid.NewGuid().ToString());
    }

    public void AddAssertionsEventHandler<TComponentsEventHandler>()
        where TComponentsEventHandler : AssertExtensionsEventHandlers
    {
        var elementEventHandler = (TComponentsEventHandler)Activator.CreateInstance(typeof(TComponentsEventHandler));
        elementEventHandler.SubscribeToAll();
    }

    public void RemoveAssertionsEventHandler<TComponentsEventHandler>()
        where TComponentsEventHandler : AssertExtensionsEventHandlers
    {
        var elementEventHandler = (TComponentsEventHandler)Activator.CreateInstance(typeof(TComponentsEventHandler));
        elementEventHandler.UnsubscribeToAll();
    }

    public ApiClientService GetApiClientService(string url = null, bool sharedCookies = true, int maxRetryAttempts = 1, int pauseBetweenFailures = 1, TimeUnit timeUnit = TimeUnit.Seconds)
    {
        if (!ShouldReuseRestClient)
        {
            ServicesCollection.Current.UnregisterSingleInstance<ApiClientService>();
        }

        bool isClientRegistered = ServicesCollection.Current.IsRegistered<ApiClientService>();
        var client = ServicesCollection.Current.Resolve<ApiClientService>();

        if (!isClientRegistered || client == null)
        {
            client = new ApiClientService();
            if (string.IsNullOrEmpty(url))
            {
                var apiSettingsConfig = ConfigurationService.GetSection<ApiSettings>();
                if (apiSettingsConfig == null)
                {
                    throw new SettingsNotFoundException("apiSettings");
                }

                ////client.WrappedClient.AddDefaultUrlSegment(new Uri(apiSettingsConfig.BaseUrl));
            }
            else
            {
                ////client.WrappedClient.BaseUrl = new Uri(url);
            }

            ////if (sharedCookies)
            ////{
            ////    client.WrappedClient.CookieContainer = new System.Net.CookieContainer();
            ////}

            client.PauseBetweenFailures = TimeSpanConverter.Convert(pauseBetweenFailures, timeUnit);
            client.MaxRetryAttempts = maxRetryAttempts;

            ServicesCollection.Current.RegisterInstance(client);
        }

        return client;
    }
}