// <copyright file="AssertApiAssertionsTests.cs" company="Automate The Planet Ltd.">
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
using System.Net;
using System.Threading.Tasks;
using Bellatrix.Api;
using MediaStore.Demo.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.Tests;

[TestClass]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]
[AllureFeature("API Assertions")]
[AllureSuite("API Assertions")]
public class AssertApiAssertionsTests : MSTest.APITest
{
    private ApiClientService _apiClientService;

    public override void TestInit()
    {
        FixtureFactory.Create();
        _apiClientService = App.GetApiClientService();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void AssertSuccessStatusCode()
    {
        var request = new RestRequest("api/Albums");

        var response = _apiClientService.Get(request);

        response.AssertSuccessStatusCode();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void AssertStatusCodeOk()
    {
        var request = new RestRequest("api/Albums");

        var response = _apiClientService.Get(request);

        response.AssertStatusCode(HttpStatusCode.OK);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void AssertResponseHeaderServerIsEqualToKestrel()
    {
        var request = new RestRequest("api/Albums");

        var response = _apiClientService.Get(request);

        response.AssertResponseHeader("server", "Kestrel");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void AssertExecutionTimeUnderIsUnderOneSecond()
    {
        var request = new RestRequest("api/Albums");

        var response = _apiClientService.Get(request);

        response.AssertExecutionTimeUnder(1);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task AssertContentTypeJson()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request);

        response.AssertContentType("application/json; charset=utf-8");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task AssertContentContainsAudioslave()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        response.AssertContentContains("Audioslave");
    }

    [TestMethod]
    [Ignore]
    [TestCategory(Categories.API)]
    public async Task AssertContentEncodingUtf8()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        response.AssertContentEncoding("gzip");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task AssertContentEquals()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        response.AssertContentEquals("{\"albumId\":10,\"title\":\"Audioslave\",\"artistId\":8,\"artist\":null,\"tracks\":[]}");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task AssertContentNotContainsRammstein()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        response.AssertContentNotContains("Rammstein");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task AssertContentNotEqualsRammstein()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        response.AssertContentNotEquals("Rammstein");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task AssertResultEquals()
    {
        var expectedAlbum = new Albums
                            {
                                AlbumId = 10,
                            };
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        response.AssertResultEquals(expectedAlbum);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task AssertResultNotEquals()
    {
        var expectedAlbum = new Albums
                            {
                                AlbumId = 11,
                            };
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        response.AssertResultNotEquals(expectedAlbum);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task AssertCookieExists()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        response.AssertCookieExists("whoIs");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task AssertCookieWhoIsBella()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        response.AssertCookie("whoIs", "Bella");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task AssertJsonSchema()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        // http://json-schema.org/examples.html
        var expectedSchema = @"{
                                    ""title"": ""Albums"",
                                    ""type"": ""object"",
                                    ""properties"": {
                                                ""albumId"": {
                                                    ""type"": ""integer""
                                                },
                                        ""title"": {
                                                    ""type"": ""string""
                                        },
                                        ""artistId"": {
                                                    ""type"": ""integer""
                                        },
 	                                ""artist"": {
                                                    ""type"": [""object"", ""null""]
                                        },
	                                ""tracks"": {
                                                    ""type"": ""array""
                                        }
                                            },
                                    ""required"": [""albumId""]
                                  }";
        response.AssertSchema(expectedSchema);
    }
}
