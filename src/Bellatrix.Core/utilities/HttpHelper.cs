// <copyright file="HttpHelper.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Bellatrix.Utilities;

public sealed class HttpHelper
{
    public static string Get(string uri, string authorizationHeader = null, string authorizationType = "Basic") => SendRequest(HttpMethod.Get, uri, authorizationHeader, authorizationType, null, null);

    public static string Post(
        string uri,
        string authorizationHeader = null,
        string authorizationType = "Basic",
        string content = null,
        string contentType = @"application/json") => SendRequest(HttpMethod.Post, uri, authorizationHeader, authorizationType, content, contentType);

    public static string Put(
        string uri,
        string authorizationHeader = null,
        string authorizationType = "Basic",
        string content = null,
        string contentType = @"application/x-www-form-urlencoded") => SendRequest(HttpMethod.Put, uri, authorizationHeader, authorizationType, content, contentType);

    public static string Delete(
        string uri,
        string authorizationHeader = null,
        string authorizationType = "Basic",
        string content = null,
        string contentType = @"application/x-www-form-urlencoded") => SendRequest(HttpMethod.Delete, uri, authorizationHeader, authorizationType, content, contentType);

    private static string SendRequest(
        HttpMethod requestType,
        string uri,
        string authorizationHeader,
        string authorizationType,
        string content,
        string contentType)
    {
        var httpRequestMessage = new HttpRequestMessage(requestType, new Uri(uri));

        if (authorizationHeader != null &&
            authorizationType != null)
        {
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(
                authorizationType, authorizationHeader);
        }

        if (content != null)
        {
            httpRequestMessage.Content = new StringContent(content, Encoding.UTF8, contentType);
        }

        using (var httpClient = new HttpClient())
        {
            httpClient.Timeout = new TimeSpan(0, 3, 0);
            var response = httpClient.SendAsync(
                httpRequestMessage, HttpCompletionOption.ResponseHeadersRead).
                Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new Exception($"{uri.Split('?').First()} returns ServiceUnavailable.");
            }

            return responseContent;
        }
    }
}