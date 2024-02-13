// <copyright file="ApiClientServiceNotAsyncRequestTests.cs" company="Automate The Planet Ltd.">
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
using AutoFixture;
using Bellatrix.Api;
using MediaStore.Demo.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.Tests;

[TestClass]
[ExecutionTimeUnder(10)]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]
[AllureFeature("Not Async Requests")]
[AllureSuite("Not Async Requests")]
public class ApiClientServiceNotAsyncRequestTests : MSTest.APITest
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
    public void ContentPopulated_When_GetAlbums()
    {
        var request = new RestRequest("api/Albums");

        var response = _apiClientService.Get(request);

        Assert.IsNotNull(response.Content);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void DataPopulatedAsList_When_GetGenericAlbums()
    {
        var request = new RestRequest("api/Albums");

        var response = _apiClientService.Get<List<Albums>>(request);

        Assert.AreEqual(347, response.Data.Count);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void DataPopulatedAsList_When_GetGenericAlbumsById()
    {
        var request = new RestRequest("api/Albums/10");

        var response = _apiClientService.Get<Albums>(request);

        Assert.AreEqual(10, response.Data.AlbumId);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void ContentPopulated_When_GetGenericAlbumsById()
    {
        var request = new RestRequest("api/Albums/10");

        var response = _apiClientService.Get<Albums>(request);

        Assert.IsNotNull(response.Content);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void DataPopulatedAsGenres_When_PutModifiedContent()
    {
        var newGenres = CreateUniqueGenres();

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newGenres);

        var insertedGenres = _apiClientService.Post<Genres>(request);

        var putRequest = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");
        string updatedName = Guid.NewGuid().ToString();
        insertedGenres.Data.Name = updatedName;
        putRequest.AddJsonBody(insertedGenres.Data);

        _apiClientService.Put<Genres>(putRequest);

        request = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");

        var getUpdatedResponse = _apiClientService.Get<Genres>(request);

        Assert.IsNotNull(getUpdatedResponse.Content);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void ContentPopulated_When_PutModifiedContent()
    {
        var newGenres = CreateUniqueGenres();

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newGenres);

        var insertedGenres = _apiClientService.Post<Genres>(request);

        var putRequest = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");
        string updatedName = Guid.NewGuid().ToString();
        insertedGenres.Data.Name = updatedName;
        putRequest.AddJsonBody(insertedGenres.Data);

        _apiClientService.Put(putRequest);

        request = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");

        var getUpdatedResponse = _apiClientService.Get<Genres>(request);

        Assert.IsNotNull(getUpdatedResponse.Content);
    }

    // TODO: put w/o body?
    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void ContentPopulated_When_NewAlbumInsertedViaPost()
    {
        var newAlbum = CreateUniqueGenres();

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newAlbum);

        var response = _apiClientService.Post(request);

        Assert.IsNotNull(response.Content);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void DataPopulatedAsGenres_When_NewAlbumInsertedViaPost()
    {
        var newAlbum = CreateUniqueGenres();

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newAlbum);

        var response = _apiClientService.Post<Genres>(request);

        Assert.AreEqual(newAlbum.Name, response.Data.Name);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void ArtistsDeleted_When_PerformDeleteRequest()
    {
        var newArtist = CreateUniqueArtists();
        var request = new RestRequest("api/Artists");
        request.AddJsonBody(newArtist);
        _apiClientService.Post<Artists>(request);

        var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}");
        var response = _apiClientService.Delete(deleteRequest);

        Assert.IsTrue(response.IsSuccessful);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.API)]
    public void ArtistsDeleted_When_PerformGenericDeleteRequest()
    {
        var newArtist = CreateUniqueArtists();
        var request = new RestRequest("api/Artists");
        request.AddJsonBody(newArtist);
        _apiClientService.Post<Artists>(request);

        var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}");
        var response = _apiClientService.Delete<Artists>(deleteRequest);

        Assert.IsNotNull(response.Data);
    }

    private Artists CreateUniqueArtists()
    {
        var getResponse = _apiClientService.Get<List<Artists>>(new RestRequest("api/Artists"));
        var newArtists = new Artists
                         {
                             Name = _fixture.Create<string>(),
                             ArtistId = getResponse.Data.OrderBy(x => x.ArtistId).Last().ArtistId + 1,
                         };
        return newArtists;
    }

    private Genres CreateUniqueGenres()
    {
        var getResponse = _apiClientService.Get<List<Genres>>(new RestRequest("api/Genres"));
        var newAlbum = new Genres
                       {
                           Name = _fixture.Create<string>(),
                           GenreId = getResponse.Data.OrderBy(x => x.GenreId).Last().GenreId + 1,
                       };
        return newAlbum;
    }
}
