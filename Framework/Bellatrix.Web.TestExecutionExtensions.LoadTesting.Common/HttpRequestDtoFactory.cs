// <copyright file="HttpRequestDtoFactory.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.LoadTesting;
using Titanium.Web.Proxy.Http;

namespace Bellatrix.Web.TestExecutionExtensions.LoadTesting
{
    public class HttpRequestDtoFactory
    {
        public HttpRequestDto Create(Request request, HttpRequestDto previousHttpRequestDto)
        {
            var httpRequestDto = new HttpRequestDto
            {
                Url = request.Url,
                Method = request.Method,
                ContentType = request.ContentType,
                CreationTime = DateTime.Now,
            };
            if (previousHttpRequestDto != null)
            {
                httpRequestDto.MillisecondsPauseAfterPreviousRequest = (httpRequestDto.CreationTime - previousHttpRequestDto.CreationTime).Milliseconds;
            }

            foreach (var item in request.Headers)
            {
                httpRequestDto.Headers.Add(item.ToString());
            }

            return httpRequestDto;
        }
    }
}