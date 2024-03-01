// <copyright file="ApiLoadTests.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.Tests;

[TestClass]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]
[AllureFeature("Load Testing")]
[AllureSuite("Load Testing")]
public class ApiLoadTests : MSTest.APITest
{
    private ApiClientService _apiClientService;

    public override void TestInit()
    {
        FixtureFactory.Create();
        _apiClientService = App.GetApiClientService();
    }

    [TestMethod]
    [TestCategory(Categories.API)]
    public void LoadTest_ExecuteForTime()
    {
        var request = new RestRequest("api/Albums");

        App.LoadTestService.ExecuteForTime(10, 1, 30, () =>
        {
            var response = _apiClientService.Get(request);

            response.AssertSuccessStatusCode();
            response.AssertExecutionTimeUnder(1);
        });
    }

    [TestMethod]
    [TestCategory(Categories.API)]
    public void LoadTest_ExecuteNumberOfTimes()
    {
        var request = new RestRequest("api/Albums");

        App.LoadTestService.ExecuteNumberOfTimes(5, 1, 5, () =>
        {
            var response = _apiClientService.Get(request);

            response.AssertSuccessStatusCode();
            response.AssertExecutionTimeUnder(1);
        });
    }
}
