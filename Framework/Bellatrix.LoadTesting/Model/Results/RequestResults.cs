// <copyright file="RequestResults.cs" company="Automate The Planet Ltd.">
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
using System.Net;

namespace Bellatrix.LoadTesting.Model.Results
{
    public class RequestResults
    {
        public RequestResults() => ResponseAssertionResults = new List<ResponseAssertionResults>();

        public TimeSpan ExecutionTime { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string RequestUrl { get; set; }
        public string ResponseContent { get; set; }
        public bool IsSuccessful { get; set; }
        public List<ResponseAssertionResults> ResponseAssertionResults { get; set; }
    }
}
