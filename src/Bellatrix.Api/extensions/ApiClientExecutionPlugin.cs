// <copyright file="ApiClientExecutionPlugin.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Api.Events;

namespace Bellatrix.Api.Extensions;

public class ApiClientExecutionPlugin
{
    public void Subscribe(IExecutionProvider provider)
    {
        provider.OnClientInitializedEvent += OnClientInitialized;
        provider.OnRequestTimeoutEvent += OnRequestTimeout;
        provider.OnMakingRequestEvent += OnMakingRequest;
        provider.OnRequestMadeEvent += OnRequestMade;
        provider.OnRequestFailedEvent += OnRequestFailed;
    }

    public void Unsubscribe(IExecutionProvider provider)
    {
        provider.OnClientInitializedEvent -= OnClientInitialized;
        provider.OnRequestTimeoutEvent -= OnRequestTimeout;
        provider.OnMakingRequestEvent -= OnMakingRequest;
        provider.OnRequestMadeEvent -= OnRequestMade;
        provider.OnRequestFailedEvent -= OnRequestFailed;
    }

    protected virtual void OnClientInitialized(object sender, ClientEventArgs client)
    {
    }

    protected virtual void OnRequestTimeout(object sender, ClientEventArgs client)
    {
    }

    protected virtual void OnMakingRequest(object sender, RequestEventArgs client)
    {
    }

    protected virtual void OnRequestMade(object sender, ResponseEventArgs client)
    {
    }

    protected virtual void OnRequestFailed(object sender, ResponseEventArgs client)
    {
    }
}
