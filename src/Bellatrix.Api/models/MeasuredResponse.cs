// <copyright file="MeasuredResponse.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;

namespace Bellatrix.Api;

public class MeasuredResponse
{
    private readonly RestResponse _restResponse;

    public MeasuredResponse(RestResponse restResponse, TimeSpan executionTime)
    {
        _restResponse = restResponse;
        ExecutionTime = executionTime;
    }

    public TimeSpan ExecutionTime { get; set; }
#pragma warning disable CS0618 // Type or member is obsolete
    public List<Cookie> Cookies => _restResponse.Cookies.ToList();
#pragma warning restore CS0618 // Type or member is obsolete
    public bool IsSuccessful => _restResponse.IsSuccessful;
#pragma warning disable CS0618 // Type or member is obsolete
    public IReadOnlyCollection<HeaderParameter> Headers => _restResponse.Headers;
#pragma warning restore CS0618 // Type or member is obsolete
    public RestRequest Request { get => _restResponse.Request; set => _restResponse.Request = value; }
    public string ContentType { get => _restResponse.ContentType; set => _restResponse.ContentType = value; }
    public long ContentLength { get => _restResponse.Content.Length; set => _restResponse.ContentLength = value; }
    public ICollection<string> ContentEncoding { get => _restResponse.ContentEncoding; set => _restResponse.ContentEncoding = value; }
    public string Content { get => _restResponse.Content; set => _restResponse.Content = value; }
    public HttpStatusCode StatusCode { get => _restResponse.StatusCode; set => _restResponse.StatusCode = value; }
    public string StatusDescription { get => _restResponse.StatusDescription; set => _restResponse.StatusDescription = value; }
    public byte[] RawBytes { get => _restResponse.RawBytes; set => _restResponse.RawBytes = value; }
    public Uri ResponseUri { get => _restResponse.ResponseUri; set => _restResponse.ResponseUri = value; }
    public string Server { get => _restResponse.Server; set => _restResponse.Server = value; }
    public ResponseStatus ResponseStatus { get => _restResponse.ResponseStatus; set => _restResponse.ResponseStatus = value; }
    public string ErrorMessage { get => _restResponse.ErrorMessage; set => _restResponse.ErrorMessage = value; }
    public Exception ErrorException { get => _restResponse.ErrorException; set => _restResponse.ErrorException = value; }
}
