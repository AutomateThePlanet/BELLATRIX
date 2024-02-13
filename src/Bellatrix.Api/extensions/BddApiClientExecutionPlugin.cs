// <copyright file="BddApiClientExecutionPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Text;
using Bellatrix.Api.Events;

namespace Bellatrix.Api.Extensions;

public class BddApiClientExecutionPlugin : ApiClientExecutionPlugin
{
    protected override void OnRequestTimeout(object sender, ClientEventArgs client) => Logger.LogInformation("Request was not executed in the specified timeout.");

    protected override void OnMakingRequest(object sender, RequestEventArgs requestEventArgs)
    {
        var sb = new StringBuilder();
        sb.Append($"Making {requestEventArgs.Request.Method} request against resource {requestEventArgs.RequestResource}");
        if (requestEventArgs.Request.Parameters != null && requestEventArgs.Request.Parameters.Count > 0)
        {
            sb.Append(" with parameters ");
            foreach (var param in requestEventArgs.Request.Parameters)
            {
                sb.Append($"{param.Name}={param.Value} ");
            }
        }

        Logger.LogInformation(sb.ToString().TrimEnd());
    }

    protected override void OnRequestMade(object sender, ResponseEventArgs responseEventArgs) => Logger.LogInformation($"Response of request {responseEventArgs.Response.Request.Method} against resource {responseEventArgs.RequestUri} - {responseEventArgs.Response.ResponseStatus}");

    protected override void OnRequestFailed(object sender, ResponseEventArgs responseEventArgs) => Logger.LogInformation($"Request Failed {responseEventArgs.Response.Request.Method} on URL {responseEventArgs.RequestUri} - {responseEventArgs.Response.ResponseStatus} {responseEventArgs.Response.ErrorMessage}");
}