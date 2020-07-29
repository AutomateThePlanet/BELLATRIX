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
using Bellatrix.Api.Extensions;
using Bellatrix.Application;
using Bellatrix.Configuration;
using Bellatrix.DynamicTestCases;
using Bellatrix.Logging;
using Bellatrix.TestWorkflowPlugins;
using Bellatrix.Trace;

namespace Bellatrix.Api
{
    public class App : BaseApp
    {
        public bool ShouldReuseRestClient { get; set; } = true;

        public LoadTestService LoadTestService => new LoadTestService();

        public IBellaLogger Logger => ServicesCollection.Current.Resolve<IBellaLogger>();

        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();

        public void AddApiClientExecutionPlugin<TExecutionExtension>()
            where TExecutionExtension : ApiClientExecutionPlugin
        {
            RegisterType<ApiClientExecutionPlugin, TExecutionExtension>(Guid.NewGuid().ToString());
        }

        public void AddTestWorkflowPlugin<TExecutionExtension>()
            where TExecutionExtension : TestWorkflowPlugin
        {
            RegisterType<TestWorkflowPlugin, TExecutionExtension>(Guid.NewGuid().ToString());
        }

        public void AddAssertionsEventHandler<TElementsEventHandler>()
            where TElementsEventHandler : AssertExtensionsEventHandlers
        {
            var elementEventHandler = (TElementsEventHandler)Activator.CreateInstance(typeof(TElementsEventHandler));
            elementEventHandler.SubscribeToAll();
        }

        public void RemoveAssertionsEventHandler<TElementsEventHandler>()
            where TElementsEventHandler : AssertExtensionsEventHandlers
        {
            var elementEventHandler = (TElementsEventHandler)Activator.CreateInstance(typeof(TElementsEventHandler));
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
                    var apiSettingsConfig = ConfigurationService.Instance.GetApiSettings();
                    if (apiSettingsConfig == null)
                    {
                        throw new ConfigurationNotFoundException("apiSettings");
                    }

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

                client.PauseBetweenFailures = Utilities.TimeSpanConverter.Convert(pauseBetweenFailures, timeUnit);
                client.MaxRetryAttempts = maxRetryAttempts;

                ServicesCollection.Current.RegisterInstance(client);
            }

            return client;
        }

        public void Initialize()
        {
            Telemetry.Instance.TrackTestExecution("API");
        }
    }
}
