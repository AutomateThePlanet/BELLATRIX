// <copyright file="HttpRequestDto.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bellatrix.Web.TestExecutionExtensions.LoadTesting;
using Titanium.Web.Proxy.Http;

namespace Bellatrix.Web.LoadTesting
{
    public class HttpRequestDto : IEquatable<HttpRequestDto>
    {
        public HttpRequestDto()
        {
            Headers = new List<string>();
            RequestModifications = new List<RequestModification>();
            ResponseAssertions = new List<ResponseAssertion>();
        }

        public string Url { get; set; }
        public string Method { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public List<string> Headers { get; set; }
        public string Body { get; set; }
        public DateTime CreationTime { get; set; }
        public int MillisecondsPauseAfterPreviousRequest { get; set; }
        public List<RequestModification> RequestModifications { get; set; }
        public List<ResponseAssertion> ResponseAssertions { get; set; }

        public bool Equals(HttpRequestDto other)
        {
            if (Url != other.Url)
            {
                return false;
            }

            if (Method != other.Method)
            {
                return false;
            }

            if (ContentType != other.ContentType)
            {
                return false;
            }

            if (ContentLength != other.ContentLength)
            {
                return false;
            }

            if (Body != other.Body)
            {
                return false;
            }

            if (Headers?.Count != other.Headers?.Count)
            {
                return false;
            }

            if (Headers?.Count == other.Headers?.Count)
            {
                if (Headers != null && other.Headers != null)
                {
                    foreach (var header in Headers)
                    {
                        if (!other.Headers.Contains(header))
                        {
                            return false;
                        }
                    }
                }
            }

            if (Math.Abs((CreationTime - other.CreationTime).TotalMilliseconds) > 200)
            {
                return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as HttpRequestDto);
        }
    }
}