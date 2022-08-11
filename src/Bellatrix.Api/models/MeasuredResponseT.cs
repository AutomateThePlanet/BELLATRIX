// <copyright file="MeasuredResponseT.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using System.Net;
using Bellatrix.Api.Contracts;
using RestSharp;

namespace Bellatrix.Api;

public class MeasuredResponse<TReturnType> : IMeasuredResponse<TReturnType>
    where TReturnType : new()
{
    private readonly IRestResponse<TReturnType> _restResponse;

    public MeasuredResponse(IRestResponse<TReturnType> restResponse, TimeSpan executionTime)
    {
        _restResponse = restResponse;
        ExecutionTime = executionTime;
    }

    public TimeSpan ExecutionTime { get; set; }
#pragma warning disable CS0618 // Type or member is obsolete
    public IList<RestResponseCookie> Cookies => _restResponse.Cookies;
#pragma warning restore CS0618 // Type or member is obsolete
    public bool IsSuccessful => _restResponse.IsSuccessful;
#pragma warning disable CS0618 // Type or member is obsolete
    public IList<Parameter> Headers => _restResponse.Headers;
#pragma warning restore CS0618 // Type or member is obsolete
    public IRestRequest Request { get => _restResponse.Request; set => _restResponse.Request = value; }
    public string ContentType { get => _restResponse.ContentType; set => _restResponse.ContentType = value; }
    public long ContentLength { get => _restResponse.ContentLength; set => _restResponse.ContentLength = value; }
    public string ContentEncoding { get => _restResponse.ContentEncoding; set => _restResponse.ContentEncoding = value; }
    public string Content { get => _restResponse.Content; set => _restResponse.Content = value; }
    public HttpStatusCode StatusCode { get => _restResponse.StatusCode; set => _restResponse.StatusCode = value; }
    public string StatusDescription { get => _restResponse.StatusDescription; set => _restResponse.StatusDescription = value; }
    public byte[] RawBytes { get => _restResponse.RawBytes; set => _restResponse.RawBytes = value; }
    public Uri ResponseUri { get => _restResponse.ResponseUri; set => _restResponse.ResponseUri = value; }
    public string Server { get => _restResponse.Server; set => _restResponse.Server = value; }
    public ResponseStatus ResponseStatus { get => _restResponse.ResponseStatus; set => _restResponse.ResponseStatus = value; }
    public string ErrorMessage { get => _restResponse.ErrorMessage; set => _restResponse.ErrorMessage = value; }
    public Exception ErrorException { get => _restResponse.ErrorException; set => _restResponse.ErrorException = value; }
    public Version ProtocolVersion { get => _restResponse.ProtocolVersion; set => _restResponse.ProtocolVersion = value; }
    public TReturnType Data { get => _restResponse.Data; set => _restResponse.Data = value; }
}
