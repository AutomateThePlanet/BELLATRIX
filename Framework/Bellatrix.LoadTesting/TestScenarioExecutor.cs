// <copyright file="TestScenarioExecutor.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using Bellatrix.Api;
using Bellatrix.Api.Contracts;
using Bellatrix.LoadTesting.Model;
using Bellatrix.LoadTesting.Model.Assertions;
using Bellatrix.LoadTesting.Model.Results;
using RestSharp;

namespace Bellatrix.LoadTesting
{
    public class TestScenarioExecutor
    {
        private readonly List<LoadTestAssertionHandler> _loadTestAssertionHandlers;
        private readonly ApiClientService _apiClientService;
        private readonly Dictionary<string, string> _scenarioCookies;

        public TestScenarioExecutor(List<LoadTestAssertionHandler> loadTestAssertionHandlers, ApiClientService apiClientService)
        {
            _loadTestAssertionHandlers = loadTestAssertionHandlers;
            _apiClientService = apiClientService;
            _scenarioCookies = new Dictionary<string, string>();
        }

        public void Execute(TestScenario testScenario, TestScenarioResults testScenarioResults, bool shouldExecuteRecordedRequestPauses, List<string> ignoreUrlRequestsPatterns)
        {
            var testScenarioRunResult = new TestScenarioRunResult();
            var watch = Stopwatch.StartNew();

            foreach (var httpRequestDto in testScenario.Requests)
            {
                if (ShouldFilterRequest(httpRequestDto.Url, ignoreUrlRequestsPatterns))
                {
                    continue;
                }

                var requestResults = new RequestResults();
                try
                {
                    if (shouldExecuteRecordedRequestPauses)
                    {
                        Thread.Sleep(httpRequestDto.MillisecondsPauseAfterPreviousRequest);
                    }

                    var request = CreateRestRequest(httpRequestDto);
                    var response = _apiClientService.Execute(request);

                    UpdateCookiesCollection(response);

                    requestResults.ExecutionTime = response.ExecutionTime;
                    requestResults.StatusCode = response.StatusCode;
                    requestResults.RequestUrl = response.ResponseUri.ToString();
                    requestResults.IsSuccessful = response.IsSuccessful;
                    if (!response.IsSuccessful)
                    {
                        requestResults.ResponseContent = response.ResponseUri.ToString();
                    }

                    foreach (var loadTestAssertionHandler in _loadTestAssertionHandlers)
                    {
                        var responseAssertionResults = loadTestAssertionHandler.Execute(httpRequestDto, response);
                        requestResults.ResponseAssertionResults.AddRange(responseAssertionResults);
                    }
                }
                catch (Exception ex)
                {
                    requestResults.RequestUrl = httpRequestDto.Url;
                    requestResults.IsSuccessful = false;
                    requestResults.ResponseContent = $"{httpRequestDto.Url} {ex.Message}";
                    Console.WriteLine($"FAILED- {httpRequestDto.Url}");
                }

                testScenarioRunResult.RequestResults.Add(requestResults);
            }

            watch.Stop();

            testScenarioRunResult.ExecutionTime = watch.Elapsed;
            testScenarioResults.TimesExecuted++;
            testScenarioResults.Weight = testScenario.Weight;
            if (testScenarioRunResult.Passed)
            {
                testScenarioResults.TimesPassed++;
            }
            else
            {
                testScenarioResults.TimesFailed++;
            }

            testScenarioResults.TestScenarioRunResults.GetOrAdd(testScenarioRunResult.RunId, testScenarioRunResult);
        }

        private bool ShouldFilterRequest(string url, List<string> ignoreUrlRequestsPatterns)
        {
            bool shouldFilter = false;
            foreach (var currentPattern in ignoreUrlRequestsPatterns)
            {
                var m = Regex.Match(url, currentPattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    shouldFilter = true;
                    break;
                }
            }

            return shouldFilter;
        }

        private void UpdateCookiesCollection(IMeasuredResponse response)
        {
            foreach (var setCookieHeader in response.Headers.Where(x => x.Name.Equals("Set-Cookie")))
            {
                if (!string.IsNullOrEmpty(setCookieHeader.Value?.ToString()))
                {
                    string setCookieValue = setCookieHeader.Value.ToString();
                    string cookieSetValuePart = setCookieValue.Split(';').First().Trim();
                    string cookieName = cookieSetValuePart.Split('=').First();
                    string cookieNewValue = cookieSetValuePart.Split('=').Last();
                    string cookieExpiresPart = setCookieValue.Split(';').FirstOrDefault(x => x.Contains("expires"))?.Trim();
                    if (!string.IsNullOrEmpty(cookieExpiresPart))
                    {
                        var cookieExpirationDate = DateTime.Parse(cookieExpiresPart.Split('=').Last());

                        if (cookieExpirationDate > DateTime.Now)
                        {
                            CreateOrUpdateCookieInLocalCollection(cookieName, cookieNewValue);
                        }
                        else if (_scenarioCookies.ContainsKey(cookieName))
                        {
                            _scenarioCookies.Remove(cookieName);
                        }
                    }
                    else
                    {
                        CreateOrUpdateCookieInLocalCollection(cookieName, cookieNewValue);
                    }
                }
            }

            foreach (var responseCookie in response.Cookies)
            {
                CreateOrUpdateCookieInLocalCollection(responseCookie.Name, responseCookie.Value);
            }
        }

        private void CreateOrUpdateCookieInLocalCollection(string cookieName, string cookieNewValue)
        {
            if (!_scenarioCookies.ContainsKey(cookieName))
            {
                _scenarioCookies.Add(cookieName, cookieNewValue);
            }
            else
            {
                _scenarioCookies[cookieName] = cookieNewValue;
            }
        }

        private RestRequest CreateRestRequest(HttpRequestDto httpRequestDto)
        {
            var method = (Method)Enum.Parse(typeof(Method), httpRequestDto.Method);
            string resource = HttpUtility.UrlDecode(httpRequestDto.Url);
            var request = new RestRequest(resource, method);

            foreach (var currentHeader in httpRequestDto.Headers)
            {
                if (currentHeader.Contains("Cookie"))
                {
                    string cookiesHeader = currentHeader.Replace("Cookie:", string.Empty).TrimStart();
                    var cookiesPairs = cookiesHeader.Split(';');
                    foreach (string cookiePair in cookiesPairs)
                    {
                        var splitHeader = cookiePair.Split('=');
                        var cookieName = splitHeader[0].Trim();
                        if (_scenarioCookies.ContainsKey(cookieName))
                        {
                            request.AddCookie(cookieName, _scenarioCookies[cookieName]);
                        }
                    }
                }
                else
                {
                    var headerPairs = currentHeader.Split(':');
                    if (headerPairs.Length >= 2)
                    {
                        string headerName = headerPairs[0].Trim();
                        string headerValue = currentHeader.Replace($"{headerName}:", string.Empty).Trim();
                        if (!string.IsNullOrEmpty(headerName) && !string.IsNullOrEmpty(headerValue))
                        {
                            try
                            {
                                request.AddHeader(headerName, headerValue);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                    }
                }
            }

            if (httpRequestDto.Body != null)
            {
                var parametersPairs = httpRequestDto.Body.Split('&');
                var urlEncodedPairs = new List<KeyValuePair<string, string>>();
                foreach (var parameterPair in parametersPairs)
                {
                    string parameterName = parameterPair.Split('=').FirstOrDefault();
                    string parameterValue = parameterPair.Split('=').LastOrDefault() ?? string.Empty;
                    urlEncodedPairs.Add(new KeyValuePair<string, string>(HttpUtility.UrlDecode(parameterName), HttpUtility.UrlDecode(parameterValue)));
                }

                var formUrlEncodedContent = new FormUrlEncodedContent(urlEncodedPairs.ToArray());
                string rawBody = formUrlEncodedContent.ReadAsStringAsync().Result;
                string decodedRawBody = rawBody;
                if (httpRequestDto.Headers.Any(x => x.Contains("text/html")))
                {
                    decodedRawBody = HttpUtility.HtmlDecode(rawBody);
                }
                else
                {
                    decodedRawBody = HttpUtility.UrlDecode(rawBody);
                }

                request.AddParameter(httpRequestDto.ContentType, decodedRawBody, ParameterType.RequestBody);
            }

            return request;
        }
    }
}