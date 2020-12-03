// <copyright file="ApiClientService.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Bellatrix.Api.Configuration;
using Bellatrix.Api.Contracts;
using Bellatrix.Api.Extensions;
using Bellatrix.Api.Model;
using Polly;
using RestSharp;
using RestSharp.Authenticators;

namespace Bellatrix.Api
{
    public class ApiClientService
    {
        private readonly ExecutionProvider _executionProvider;
        private readonly ApiSettings _apiSettings = ConfigurationService.GetSection<ApiSettings>();

        // TODO: is this going to be accessible in the service container?
        public ApiClientService()
        {
            _executionProvider = new ExecutionProvider();
            InitializeExecutionExtensions(_executionProvider);

            WrappedClient = new RestClient();
            WrappedClient.AddHandler("application/json", () => NewtonsoftJsonSerializer.Default);
            WrappedClient.AddHandler("text/json", () => NewtonsoftJsonSerializer.Default);
            WrappedClient.AddHandler("text/x-json", () => NewtonsoftJsonSerializer.Default);
            WrappedClient.AddHandler("text/javascript", () => NewtonsoftJsonSerializer.Default);
            WrappedClient.AddHandler("*+json", () => NewtonsoftJsonSerializer.Default);

            _executionProvider.OnClientInitialized(WrappedClient);
            var authenticator = ServicesCollection.Current.Resolve<IAuthenticator>();
            if (authenticator != null)
            {
                WrappedClient.Authenticator = authenticator;
            }

            WrappedClient.FollowRedirects = true;

            if (_apiSettings != null)
            {
                MaxRetryAttempts = _apiSettings.MaxRetryAttempts;
                PauseBetweenFailures = Utilities.TimeSpanConverter.Convert(_apiSettings.PauseBetweenFailures, _apiSettings.TimeUnit);

                int timeoutSeconds = _apiSettings.ClientTimeoutSeconds;
                Policy.Timeout(timeoutSeconds, onTimeout: (context, timespan, task) =>
                {
                    task.ContinueWith(t =>
                    {
                        if (t.IsFaulted)
                        {
                            _executionProvider.OnRequestTimeout(WrappedClient);
                        }
                    });
                });
            }
            else
            {
                throw new ArgumentNullException("The API section in testFrameworkSettings.json is missing.");
            }
        }

        public int MaxRetryAttempts { get; set; }

        public TimeSpan PauseBetweenFailures { get; set; }

        public IRestClient WrappedClient { get; set; }

        public IMeasuredResponse Execute(IRestRequest request) => ExecuteMeasuredRequest(request, request.Method);

        public IMeasuredResponse Get(IRestRequest request) => ExecuteMeasuredRequest(request, Method.GET);

        public IMeasuredResponse<TReturnType> Get<TReturnType>(IRestRequest request)
            where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.GET);

        public async Task<IMeasuredResponse> GetAsync(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            => await ExecuteMeasuredRequestAsync(request, Method.GET, cancellationTokenSource).ConfigureAwait(false);

        public async Task<IMeasuredResponse<TReturnType>> GetAsync<TReturnType>(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.GET, cancellationTokenSource).ConfigureAwait(false);

        public IMeasuredResponse Put(IRestRequest request) => ExecuteMeasuredRequest(request, Method.PUT);

        public IMeasuredResponse<TReturnType> Put<TReturnType>(IRestRequest request)
            where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.PUT);

        public async Task<IMeasuredResponse> PutAsync(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            => await ExecuteMeasuredRequestAsync(request, Method.PUT, cancellationTokenSource).ConfigureAwait(false);

        public async Task<IMeasuredResponse<TReturnType>> PutAsync<TReturnType>(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.PUT, cancellationTokenSource).ConfigureAwait(false);

        public IMeasuredResponse Post(IRestRequest request) => ExecuteMeasuredRequest(request, Method.POST);

        public IMeasuredResponse<TReturnType> Post<TReturnType>(IRestRequest request)
            where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.POST);

        public async Task<IMeasuredResponse> PostAsync(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            => await ExecuteMeasuredRequestAsync(request, Method.POST, cancellationTokenSource).ConfigureAwait(false);

        public async Task<IMeasuredResponse<TReturnType>> PostAsync<TReturnType>(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.POST, cancellationTokenSource).ConfigureAwait(false);

        public IMeasuredResponse Delete(IRestRequest request) => ExecuteMeasuredRequest(request, Method.DELETE);

        public IMeasuredResponse<TReturnType> Delete<TReturnType>(IRestRequest request)
            where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.DELETE);

        public async Task<IMeasuredResponse> DeleteAsync(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            => await ExecuteMeasuredRequestAsync(request, Method.DELETE, cancellationTokenSource).ConfigureAwait(false);

        public async Task<IMeasuredResponse<TReturnType>> DeleteAsync<TReturnType>(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.DELETE, cancellationTokenSource).ConfigureAwait(false);

        public IMeasuredResponse Copy(IRestRequest request) => ExecuteMeasuredRequest(request, Method.COPY);

        public IMeasuredResponse<TReturnType> Copy<TReturnType>(IRestRequest request)
            where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.COPY);

        public async Task<IMeasuredResponse> CopyAsync(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            => await ExecuteMeasuredRequestAsync(request, Method.COPY, cancellationTokenSource).ConfigureAwait(false);

        public async Task<IMeasuredResponse<TReturnType>> CopyAsync<TReturnType>(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.COPY, cancellationTokenSource).ConfigureAwait(false);

        public IMeasuredResponse Head(IRestRequest request) => ExecuteMeasuredRequest(request, Method.HEAD);

        public IMeasuredResponse<TReturnType> Head<TReturnType>(IRestRequest request)
            where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.HEAD);

        public async Task<IMeasuredResponse> HeadAsync(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            => await ExecuteMeasuredRequestAsync(request, Method.HEAD, cancellationTokenSource).ConfigureAwait(false);

        public async Task<IMeasuredResponse<TReturnType>> HeadAsync<TReturnType>(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.HEAD, cancellationTokenSource).ConfigureAwait(false);

        public IMeasuredResponse Merge(IRestRequest request) => ExecuteMeasuredRequest(request, Method.MERGE);

        public IMeasuredResponse<TReturnType> Merge<TReturnType>(IRestRequest request)
            where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.MERGE);

        public async Task<IMeasuredResponse> MergeAsync(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            => await ExecuteMeasuredRequestAsync(request, Method.MERGE, cancellationTokenSource).ConfigureAwait(false);

        public async Task<IMeasuredResponse<TReturnType>> MergeAsync<TReturnType>(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.MERGE, cancellationTokenSource).ConfigureAwait(false);

        public IMeasuredResponse Options(IRestRequest request) => ExecuteMeasuredRequest(request, Method.OPTIONS);

        public IMeasuredResponse<TReturnType> Options<TReturnType>(IRestRequest request)
            where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.OPTIONS);

        public async Task<IMeasuredResponse> OptionsAsync(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            => await ExecuteMeasuredRequestAsync(request, Method.OPTIONS, cancellationTokenSource).ConfigureAwait(false);

        public async Task<IMeasuredResponse<TReturnType>> OptionsAsync<TReturnType>(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.OPTIONS, cancellationTokenSource).ConfigureAwait(false);

        public IMeasuredResponse Patch(IRestRequest request) => ExecuteMeasuredRequest(request, Method.PATCH);

        public IMeasuredResponse<TReturnType> Patch<TReturnType>(IRestRequest request)
            where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.PATCH);

        public async Task<IMeasuredResponse> PatchAsync(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            => await ExecuteMeasuredRequestAsync(request, Method.PATCH, cancellationTokenSource).ConfigureAwait(false);

        public async Task<IMeasuredResponse<TReturnType>> PatchAsync<TReturnType>(IRestRequest request, CancellationTokenSource cancellationTokenSource = null)
            where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.PATCH, cancellationTokenSource).ConfigureAwait(false);

        private async Task<IMeasuredResponse<TReturnType>> ExecuteMeasuredRequestAsync<TReturnType>(IRestRequest request, Method method, CancellationTokenSource cancellationTokenSource = null)
          where TReturnType : new()
        {
            if (cancellationTokenSource == null)
            {
                cancellationTokenSource = new CancellationTokenSource();
            }

            var retryPolicy = Policy.Handle<NotSuccessfulRequestException>().WaitAndRetryAsync(MaxRetryAttempts, i => PauseBetweenFailures);

            var measuredResponse = default(MeasuredResponse<TReturnType>);

            await retryPolicy.ExecuteAsync(async () =>
            {
                var watch = Stopwatch.StartNew();

                request.Method = method;
                SetJsonContent(request);

                _executionProvider.OnMakingRequest(request, request.Resource);

                var response = await WrappedClient.ExecuteTaskAsync<TReturnType>(request, cancellationTokenSource.Token).ConfigureAwait(false);

                _executionProvider.OnRequestMade(response, request.Resource);

                watch.Stop();
                measuredResponse = new MeasuredResponse<TReturnType>(response, watch.Elapsed);

                if (!measuredResponse.IsSuccessful)
                {
                    _executionProvider.OnRequestFailed(response, request.Resource);
                    throw new NotSuccessfulRequestException();
                }
            }).ConfigureAwait(false);

            return measuredResponse;
        }

        private async Task<IMeasuredResponse> ExecuteMeasuredRequestAsync(IRestRequest request, Method method, CancellationTokenSource cancellationTokenSource = null)
        {
            if (cancellationTokenSource == null)
            {
                cancellationTokenSource = new CancellationTokenSource();
            }

            var retryPolicy = Policy.Handle<NotSuccessfulRequestException>().WaitAndRetryAsync(MaxRetryAttempts, i => PauseBetweenFailures);

            var measuredResponse = default(MeasuredResponse);

            await retryPolicy.ExecuteAsync(async () =>
            {
                var watch = Stopwatch.StartNew();

                request.Method = method;
                SetJsonContent(request);

                _executionProvider.OnMakingRequest(request, request.Resource);

                var response = await WrappedClient.ExecuteTaskAsync(request, cancellationTokenSource.Token).ConfigureAwait(false);

                _executionProvider.OnRequestMade(response, request.Resource);

                watch.Stop();
                measuredResponse = new MeasuredResponse(response, watch.Elapsed);

                if (!measuredResponse.IsSuccessful)
                {
                    throw new NotSuccessfulRequestException();
                }
            }).ConfigureAwait(false);

            return measuredResponse;
        }

        private IMeasuredResponse<TReturnType> ExecuteMeasuredRequest<TReturnType>(IRestRequest request, Method method)
            where TReturnType : new()
        {
            var retryPolicy = Policy.Handle<NotSuccessfulRequestException>().WaitAndRetry(MaxRetryAttempts, i => PauseBetweenFailures);

            var measuredResponse = default(MeasuredResponse<TReturnType>);

            retryPolicy.Execute(() =>
            {
                var watch = Stopwatch.StartNew();

                request.Method = method;
                SetJsonContent(request);

                _executionProvider.OnMakingRequest(request, request.Resource);

                var response = WrappedClient.Execute<TReturnType>(request);

                _executionProvider.OnRequestMade(response, request.Resource);

                watch.Stop();
                measuredResponse = new MeasuredResponse<TReturnType>(response, watch.Elapsed);

                if (!measuredResponse.IsSuccessful)
                {
                    throw new NotSuccessfulRequestException();
                }
            });

            return measuredResponse;
        }

        private IMeasuredResponse ExecuteMeasuredRequest(IRestRequest request, Method method)
        {
            var retryPolicy = Policy.Handle<NotSuccessfulRequestException>().WaitAndRetry(MaxRetryAttempts, i => PauseBetweenFailures);

            var measuredResponse = default(MeasuredResponse);

            retryPolicy.Execute(() =>
            {
                var watch = Stopwatch.StartNew();

                request.Method = method;
                SetJsonContent(request);

                _executionProvider.OnMakingRequest(request, request.Resource);

                var response = WrappedClient.Execute(request);

                _executionProvider.OnRequestMade(response, request.Resource);

                watch.Stop();
                measuredResponse = new MeasuredResponse(response, watch.Elapsed);

                if (!measuredResponse.IsSuccessful)
                {
                    throw new NotSuccessfulRequestException($"Failed on URL= {measuredResponse.ResponseUri} {Environment.NewLine} {measuredResponse.StatusCode} {Environment.NewLine} {measuredResponse.Content}. Elapsed Time: {measuredResponse.ExecutionTime.ToString()}");
                }
            });

            return measuredResponse;
        }

        private void InitializeExecutionExtensions(ExecutionProvider executionProvider)
        {
            var observers = ServicesCollection.Current.ResolveAll<ApiClientExecutionPlugin>();
            foreach (var observer in observers)
            {
                observer.Subscribe(executionProvider);
            }
        }

        private void SetJsonContent(IRestRequest request, object obj = null)
        {
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            if (obj != null)
            {
                request.AddJsonBody(obj);
            }
        }
    }
}