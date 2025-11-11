using AutoFixture;
using Bellatrix.Api;
using Bellatrix.API.MSTest.Tests.Models;
using Bellatrix.Assertions;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assert = Bellatrix.Assertions.Assert;

namespace Bellatrix.API.MSTest.Tests;

// TODO: To use your own webservice change the endpoint in "baseUrl": "http://127.0.0.1:55215";
// Delete the code with TODO notes in TestInitialize.
// Uninstall Bellatrix.API.DemoApps NuGet package
[TestFixture]
[JwtAuthenticationStrategy("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiZWxsYXRyaXhVc2VyIiwianRpIjoiMDUyMzlmYjgtYTY5MS00NTZhLWE3MDMtYzFhMDEyYmMzZDU4IiwibmJmIjoxNTI0NzcyODAyLCJleHAiOjE1Mjk5NTY4MDIsImlzcyI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSIsImF1ZCI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSJ9.MZtFEtfKzu-BhfiXMSwZprPT5wGJnXTGAYzUTy0E5AA")]
public class AllRequestTypesTests : NUnit.APITest
{
    private ApiClientService _apiClientService;
    private Fixture _fixture;

    public override void TestInit()
    {
        _fixture = FixtureFactory.Create();
        _apiClientService = App.GetApiClientService();
    }

    [Test]
    public void ContentPopulated_When_GetAlbums()
    {
        var request = new RestRequest("api/Albums");

        // 1. Make a Get request but not convert the response to C# object.
        var response = _apiClientService.Get(request);

        // 1.1. The response is returned in its native format- JSON or XML.
        Assert.IsNotNull(response.Content);
    }

    [Test]
    public void DataPopulatedAsList_When_GetGenericAlbums()
    {
        var request = new RestRequest("api/Albums");

        // 2. BELLATRIX can return list of objects.
        var response = _apiClientService.Get<List<Albums>>(request);

        // 2.1. You can access the response's list through Data property.
        Assert.AreEqual(347, response.Data.Count);
    }

    [Test]
    public void DataPopulatedAsList_When_GetGenericAlbumsById()
    {
        var request = new RestRequest("api/Albums/10");

        // 3. Making GET request and return C# response object.
        var response = _apiClientService.Get<Albums>(request);

        Assert.AreEqual(10, response.Data.AlbumId);
    }

    [Test]
    public void ContentPopulated_When_GetGenericAlbumsById()
    {
        var request = new RestRequest("api/Albums/10");

        var response = _apiClientService.Get<Albums>(request);

        // 4. Even when you use the generic methods you have access to the original
        // text response through the Content property.
        Assert.IsNotNull(response.Content);
    }

    [Test]
    public void DataPopulatedAsGenres_When_PutModifiedContent()
    {
        // 5. First we create a new genre
        var newGenres = CreateUniqueGenres();

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newGenres);

        var insertedGenres = _apiClientService.Post<Genres>(request);

        var putRequest = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");

        // 5.1. After that we change the Name and create a PUT request
        string updatedName = Guid.NewGuid().ToString();
        insertedGenres.Data.Name = updatedName;

        // You need to add the changes as JSON body
        putRequest.AddJsonBody(insertedGenres.Data);

        // 5.2. Use the generic Put method to create a PUT request.
        _apiClientService.Put<Genres>(putRequest);

        request = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");

        var getUpdatedResponse = _apiClientService.Get<Genres>(request);

        Assert.IsNotNull(getUpdatedResponse.Content);
    }

    [Test]
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

        // 6. You have a non-generic version of the PUT method.
        _apiClientService.Put(putRequest);

        request = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}");

        var getUpdatedResponse = _apiClientService.Get<Genres>(request);

        Assert.IsNotNull(getUpdatedResponse.Content);
    }

    [Test]
    public void ContentPopulated_When_NewAlbumInsertedViaPost()
    {
        // 7. Create a new album.
        var newAlbum = CreateUniqueGenres();

        // 7.1. Create a POST request and add the new object as JSON body.
        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newAlbum);

        // 7.2. Use the non-generic Post method to make the POST request.
        var response = _apiClientService.Post(request);

        // 7.3. Access the native text response through Content property.
        Assert.IsNotNull(response.Content);
    }

    [Test]
    public void DataPopulatedAsGenres_When_NewAlbumInsertedViaPost()
    {
        var newAlbum = CreateUniqueGenres();

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newAlbum);

        // 8. Use the generic Post version to get a converted response to C# object.
        var response = _apiClientService.Post<Genres>(request);

        Assert.AreEqual(newAlbum.Name, response.Data.Name);
    }

    [Test]
    public void ArtistsDeleted_When_PerformDeleteRequest()
    {
        // 9. First create a new artist and post it.
        var newArtist = CreateUniqueArtists();
        var request = new RestRequest("api/Artists");
        request.AddJsonBody(newArtist);
        _apiClientService.Post<Artists>(request);

        // 9.1. Create a delete request to remove it.
        var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}");
        var response = _apiClientService.Delete(deleteRequest);

        Assert.IsTrue(response.IsSuccessful);
    }

    [Test]
    public void ArtistsDeleted_When_PerformGenericDeleteRequest()
    {
        var newArtist = CreateUniqueArtists();
        var request = new RestRequest("api/Artists");
        request.AddJsonBody(newArtist);
        _apiClientService.Post<Artists>(request);

        // 10. Use a generic version of the Delete to convert the response to C# object.
        var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}");
        var response = _apiClientService.Delete<Artists>(deleteRequest);

        Assert.IsNotNull(response.Data);
    }

    [Test]
    public async Task ArtistsDeleted_When_PerformGenericDeleteRequestAsync()
    {
        // 11. All BELLATRIX client API methods have an async version.
        // Your test should be marked as async.
        var newArtist = CreateUniqueArtists();
        var request = new RestRequest("api/Artists");
        request.AddJsonBody(newArtist);

        // 11.1. Use the PostAsync. Should use the await operator.
        await _apiClientService.PostAsync<Artists>(request);

        var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}");

        // 11.2. Use the DeleteAsync. Should use the await operator.
        var response = await _apiClientService.DeleteAsync<Artists>(deleteRequest);

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