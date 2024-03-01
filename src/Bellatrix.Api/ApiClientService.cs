// <copyright file="ApiClientService.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Bellatrix.Api.Configuration;
using Bellatrix.Api.Extensions;
using Bellatrix.Api.Model;
using Polly;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace Bellatrix.Api;

public class ApiClientService
{
    private readonly ExecutionProvider _executionProvider;
    private readonly ApiSettings _apiSettings = ConfigurationService.GetSection<ApiSettings>();

    // TODO: is this going to be accessible in the service container?
    public ApiClientService(string baseUrl = null)
    {
        _executionProvider = new ExecutionProvider();
        InitializeExecutionExtensions(_executionProvider);

        var options = new RestClientOptions
        {
            BaseUrl = baseUrl is not null ? new Uri(baseUrl) : null,
            FollowRedirects = true
        };

        _executionProvider.OnClientInitialized(WrappedClient);
        var authenticator = ServicesCollection.Current.Resolve<IAuthenticator>();
        if (authenticator != null)
        {
            options.Authenticator = authenticator;
        }
        WrappedClient = new RestClient(
            options,
            configureSerialization: s => s.UseNewtonsoftJson()
            );
        ////WrappedClient.AddHandler("application/json", () => NewtonsoftJsonSerializer.Default);
        ////WrappedClient.AddHandler("text/json", () => NewtonsoftJsonSerializer.Default);
        ////WrappedClient.AddHandler("text/x-json", () => NewtonsoftJsonSerializer.Default);
        ////WrappedClient.AddHandler("text/javascript", () => NewtonsoftJsonSerializer.Default);
        ////WrappedClient.AddHandler("*+json", () => NewtonsoftJsonSerializer.Default);

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

    ////public Uri BaseUrl
    ////{
    ////    get => WrappedClient.Ur.AbsoluteUri;
    ////    set => WrappedClient.BaseUrl = new Uri(value);
    ////}

    public TimeSpan PauseBetweenFailures { get; set; }

    public IRestClient WrappedClient { get; set; }

    public MeasuredResponse Execute(RestRequest request) => ExecuteMeasuredRequest(request, request.Method);

    public MeasuredResponse Get(RestRequest request) => ExecuteMeasuredRequest(request, Method.Get);

    public MeasuredResponse<TReturnType> Get<TReturnType>(RestRequest request, bool shouldReturnResponseOnFailure = false)
        where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.Get, shouldReturnResponseOnFailure);

    public async Task<MeasuredResponse> GetAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        => await ExecuteMeasuredRequestAsync(request, Method.Get, cancellationTokenSource).ConfigureAwait(false);

    public async Task<MeasuredResponse<TReturnType>> GetAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Get, cancellationTokenSource).ConfigureAwait(false);

    public MeasuredResponse Put(RestRequest request) => ExecuteMeasuredRequest(request, Method.Put);

    public MeasuredResponse<TReturnType> Put<TReturnType>(RestRequest request, bool shouldReturnResponseOnFailure = false)
        where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.Put, shouldReturnResponseOnFailure);

    public async Task<MeasuredResponse> PutAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        => await ExecuteMeasuredRequestAsync(request, Method.Put, cancellationTokenSource).ConfigureAwait(false);

    public async Task<MeasuredResponse<TReturnType>> PutAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Put, cancellationTokenSource).ConfigureAwait(false);

    public MeasuredResponse Post(RestRequest request) => ExecuteMeasuredRequest(request, Method.Post);

    public MeasuredResponse<TReturnType> Post<TReturnType>(RestRequest request, bool shouldReturnResponseOnFailure = false)
        where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.Post, shouldReturnResponseOnFailure);

    public async Task<MeasuredResponse> PostAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        => await ExecuteMeasuredRequestAsync(request, Method.Post, cancellationTokenSource).ConfigureAwait(false);

    public async Task<MeasuredResponse<TReturnType>> PostAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Post, cancellationTokenSource).ConfigureAwait(false);

    public MeasuredResponse Delete(RestRequest request) => ExecuteMeasuredRequest(request, Method.Delete);

    public MeasuredResponse<TReturnType> Delete<TReturnType>(RestRequest request, bool shouldReturnResponseOnFailure = false)
        where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.Delete, shouldReturnResponseOnFailure);

    public async Task<MeasuredResponse> DeleteAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        => await ExecuteMeasuredRequestAsync(request, Method.Delete, cancellationTokenSource).ConfigureAwait(false);

    public async Task<MeasuredResponse<TReturnType>> DeleteAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Delete, cancellationTokenSource).ConfigureAwait(false);

    public MeasuredResponse Copy(RestRequest request) => ExecuteMeasuredRequest(request, Method.Copy);

    public MeasuredResponse<TReturnType> Copy<TReturnType>(RestRequest request)
        where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.Copy);

    public async Task<MeasuredResponse> CopyAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        => await ExecuteMeasuredRequestAsync(request, Method.Copy, cancellationTokenSource).ConfigureAwait(false);

    public async Task<MeasuredResponse<TReturnType>> CopyAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Copy, cancellationTokenSource).ConfigureAwait(false);

    public MeasuredResponse Head(RestRequest request) => ExecuteMeasuredRequest(request, Method.Head);

    public MeasuredResponse<TReturnType> Head<TReturnType>(RestRequest request)
        where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.Head);

    public async Task<MeasuredResponse> HeadAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        => await ExecuteMeasuredRequestAsync(request, Method.Head, cancellationTokenSource).ConfigureAwait(false);

    public async Task<MeasuredResponse<TReturnType>> HeadAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Head, cancellationTokenSource).ConfigureAwait(false);

    public MeasuredResponse Merge(RestRequest request) => ExecuteMeasuredRequest(request, Method.Merge);

    public MeasuredResponse<TReturnType> Merge<TReturnType>(RestRequest request)
        where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.Merge);

    public async Task<MeasuredResponse> MergeAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        => await ExecuteMeasuredRequestAsync(request, Method.Merge, cancellationTokenSource).ConfigureAwait(false);

    public async Task<MeasuredResponse<TReturnType>> MergeAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Merge, cancellationTokenSource).ConfigureAwait(false);

    public MeasuredResponse Options(RestRequest request) => ExecuteMeasuredRequest(request, Method.Options);

    public MeasuredResponse<TReturnType> Options<TReturnType>(RestRequest request)
        where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.Options);

    public async Task<MeasuredResponse> OptionsAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        => await ExecuteMeasuredRequestAsync(request, Method.Options, cancellationTokenSource).ConfigureAwait(false);

    public async Task<MeasuredResponse<TReturnType>> OptionsAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Options, cancellationTokenSource).ConfigureAwait(false);

    public MeasuredResponse Patch(RestRequest request) => ExecuteMeasuredRequest(request, Method.Patch);

    public MeasuredResponse<TReturnType> Patch<TReturnType>(RestRequest request)
        where TReturnType : new() => ExecuteMeasuredRequest<TReturnType>(request, Method.Patch);

    public async Task<MeasuredResponse> PatchAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        => await ExecuteMeasuredRequestAsync(request, Method.Patch, cancellationTokenSource).ConfigureAwait(false);

    public async Task<MeasuredResponse<TReturnType>> PatchAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : new() => await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Patch, cancellationTokenSource).ConfigureAwait(false);

    private async Task<MeasuredResponse<TReturnType>> ExecuteMeasuredRequestAsync<TReturnType>(RestRequest request, Method method, CancellationTokenSource cancellationTokenSource = null)
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

            var response = await WrappedClient.ExecuteAsync<TReturnType>(request, cancellationTokenSource.Token).ConfigureAwait(false);

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

    private async Task<MeasuredResponse> ExecuteMeasuredRequestAsync(RestRequest request, Method method, CancellationTokenSource cancellationTokenSource = null)
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

            var response = await WrappedClient.ExecuteAsync(request, cancellationTokenSource.Token).ConfigureAwait(false);

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

    private MeasuredResponse<TReturnType> ExecuteMeasuredRequest<TReturnType>(RestRequest request, Method method, bool shouldReturnReponseOnFailure = false)
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

            var response = WrappedClient.ExecuteAsync<TReturnType>(request).Result;

            _executionProvider.OnRequestMade(response, request.Resource);

            watch.Stop();
            measuredResponse = new MeasuredResponse<TReturnType>(response, watch.Elapsed);

            if (!measuredResponse.IsSuccessful && !shouldReturnReponseOnFailure)
            {
                throw new NotSuccessfulRequestException($"Failed on URL= {measuredResponse.ResponseUri} {Environment.NewLine} {measuredResponse.StatusCode} {Environment.NewLine} {measuredResponse.Content}. Elapsed Time: {measuredResponse.ExecutionTime.ToString()}");
            }
        });

        return measuredResponse;
    }

    private MeasuredResponse ExecuteMeasuredRequest(RestRequest request, Method method)
    {
        var retryPolicy = Policy.Handle<NotSuccessfulRequestException>().WaitAndRetry(MaxRetryAttempts, i => PauseBetweenFailures);

        var measuredResponse = default(MeasuredResponse);

        retryPolicy.Execute(() =>
        {
            var watch = Stopwatch.StartNew();

            request.Method = method;
            SetJsonContent(request);

            _executionProvider.OnMakingRequest(request, request.Resource);

            var response = WrappedClient.ExecuteAsync(request).Result;

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

    private void SetJsonContent(RestRequest request, object obj = null)
    {
        //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
        if (obj != null)
        {
            request.AddJsonBody(obj);
        }
    }
}