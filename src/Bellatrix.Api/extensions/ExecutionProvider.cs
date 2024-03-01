// <copyright file="ExecutionProvider.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Api.Events;
using RestSharp;

namespace Bellatrix.Api.Extensions;

public class ExecutionProvider : IExecutionProvider
{
    public event EventHandler<ClientEventArgs> OnClientInitializedEvent;
    public event EventHandler<ClientEventArgs> OnRequestTimeoutEvent;
    public event EventHandler<RequestEventArgs> OnMakingRequestEvent;
    public event EventHandler<ResponseEventArgs> OnRequestMadeEvent;
    public event EventHandler<ResponseEventArgs> OnRequestFailedEvent;

    public void OnClientInitialized(IRestClient client) => OnClientInitializedEvent?.Invoke(this, new ClientEventArgs(client));
    public void OnRequestTimeout(IRestClient client) => OnRequestTimeoutEvent?.Invoke(this, new ClientEventArgs(client));
    public void OnMakingRequest(RestRequest request, string requestUri) => OnMakingRequestEvent?.Invoke(this, new RequestEventArgs(request, requestUri));
    public void OnRequestMade(RestResponse response, string requestUri) => OnRequestMadeEvent?.Invoke(this, new ResponseEventArgs(response, requestUri));
    public void OnRequestFailed(RestResponse response, string requestUri) => OnRequestFailedEvent?.Invoke(this, new ResponseEventArgs(response, requestUri));
}
