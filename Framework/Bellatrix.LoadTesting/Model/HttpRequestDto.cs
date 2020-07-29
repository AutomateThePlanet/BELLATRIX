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

namespace Bellatrix.LoadTesting
{
    public class HttpRequestDto
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
    }
}
