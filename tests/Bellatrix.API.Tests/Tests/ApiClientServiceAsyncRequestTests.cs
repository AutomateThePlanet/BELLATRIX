// <copyright file="ApiClientServiceAsyncRequestTests.cs" company="Automate The Planet Ltd.">
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
using System.Threading.Tasks;
using AutoFixture;
using Bellatrix.Api;
using MediaStore.Demo.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.Tests;

[TestClass]
[RetryFailedRequests(3, 200, TimeUnit.Milliseconds)]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]
[AllureFeature("Async Requests")]
[AllureSuite("Async Requests")]
public class ApiClientServiceAsyncRequestTests : MSTest.APITest
{
    private ApiClientService _apiClientService;
    private Fixture _fixture;

    public override void TestInit()
    {
        _fixture = FixtureFactory.Create();
        _apiClientService = App.GetApiClientService();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task ContentPopulated_When_GetAsyncAlbums()
    {
        var request = new RestRequest("api/Albums");

        var response = await _apiClientService.GetAsync(request).ConfigureAwait(false);

        Assert.IsNotNull(response.Content);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task DataPopulatedAsList_When_GetAsyncGenericAlbums()
    {
        var request = new RestRequest("api/Albums");

        var response = await _apiClientService.GetAsync<List<Albums>>(request).ConfigureAwait(false);

        Assert.AreEqual(347, response.Data.Count);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task DataPopulatedAsList_When_GetAsyncGenericAlbumsById()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        Assert.AreEqual(10, response.Data.AlbumId);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task ContentPopulated_When_GetAsyncGenericAlbumsById()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _apiClientService.GetAsync<Albums>(request).ConfigureAwait(false);

        Assert.IsNotNull(response.Content);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task DataPopulatedAsGenres_When_PutModifiedContent()
    {
        var newGenres = await CreateUniqueGeneresAsync().ConfigureAwait(false);

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newGenres);

        var insertedGenres = await _apiClientService.PostAsync<Genres>(request).ConfigureAwait(false);

        var putRequest = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");
        string updatedName = Guid.NewGuid().ToString();
        insertedGenres.Data.Name = updatedName;
        putRequest.AddJsonBody(insertedGenres.Data);

        await _apiClientService.PutAsync<Genres>(putRequest).ConfigureAwait(false);

        request = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");

        var getUpdatedResponse = await _apiClientService.GetAsync<Genres>(request).ConfigureAwait(false);

        Assert.IsNotNull(getUpdatedResponse.Content);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task ContentPopulated_When_PutModifiedContent()
    {
        var newGenres = await CreateUniqueGeneresAsync().ConfigureAwait(false);

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newGenres);

        var insertedGenres = await _apiClientService.PostAsync<Genres>(request).ConfigureAwait(false);

        var putRequest = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");
        string updatedName = Guid.NewGuid().ToString();
        insertedGenres.Data.Name = updatedName;
        putRequest.AddJsonBody(insertedGenres.Data);

        await _apiClientService.PutAsync(putRequest).ConfigureAwait(false);

        request = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");

        var getUpdatedResponse = await _apiClientService.GetAsync<Genres>(request).ConfigureAwait(false);

        Assert.IsNotNull(getUpdatedResponse.Content);
    }

    // TODO: put w/o body?
    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task ContentPopulated_When_NewAlbumInsertedViaPost()
    {
        var newAlbum = await CreateUniqueGeneresAsync().ConfigureAwait(false);

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newAlbum);

        var response = await _apiClientService.PostAsync(request).ConfigureAwait(false);

        Assert.IsNotNull(response.Content);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task DataPopulatedAsGenres_When_NewAlbumInsertedViaPost()
    {
        var newGenres = await CreateUniqueGeneresAsync().ConfigureAwait(false);

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newGenres);

        var response = await _apiClientService.PostAsync<Genres>(request).ConfigureAwait(false);

        Assert.AreEqual(newGenres.Name, response.Data.Name);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task ArtistsDeleted_When_PerformDeleteRequest()
    {
        var newArtist = await CreateUniqueArtistsAsync().ConfigureAwait(false);
        var request = new RestRequest("api/Artists");
        request.AddJsonBody(newArtist);
        await _apiClientService.PostAsync<Artists>(request).ConfigureAwait(false);

        var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}");
        var response = await _apiClientService.DeleteAsync(deleteRequest).ConfigureAwait(false);

        Assert.IsTrue(response.IsSuccessful);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public async Task ArtistsDeleted_When_PerformGenericDeleteRequest()
    {
        var newArtist = await CreateUniqueArtistsAsync().ConfigureAwait(false);
        var request = new RestRequest("api/Artists");
        request.AddJsonBody(newArtist);
        await _apiClientService.PostAsync<Artists>(request).ConfigureAwait(false);

        var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}");
        var response = await _apiClientService.DeleteAsync<Artists>(deleteRequest).ConfigureAwait(false);

        Assert.IsNotNull(response.Data);
    }

    private async Task<Artists> CreateUniqueArtistsAsync()
    {
        var getResponse = await _apiClientService.GetAsync<List<Artists>>(new RestRequest("api/Artists")).ConfigureAwait(false);
        var newArtists = new Artists
                         {
                             Name = _fixture.Create<string>(),
                             ArtistId = getResponse.Data.OrderBy(x => x.ArtistId).Last().ArtistId + 1,
                         };
        return newArtists;
    }

    private async Task<Genres> CreateUniqueGeneresAsync()
    {
        var getResponse = await _apiClientService.GetAsync<List<Genres>>(new RestRequest("api/Genres")).ConfigureAwait(false);
        var newAlbum = new Genres
                       {
                           Name = _fixture.Create<string>(),
                           GenreId = getResponse.Data.OrderBy(x => x.GenreId).Last().GenreId + 1,
                       };
        return newAlbum;
    }
}
