using Bellatrix.API.GettingStarted.Models;
using Bellatrix.API.MSTest;
using Bellatrix.Plugins.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.GettingStarted;

[TestClass]

// 1. BELLATRIX provides an easy way to authenticate through the usage of few attributes.
// 1.1. Bellow we use JwtToken authentication. The attribute accepts your text tocken. https://tools.ietf.org/html/draft-ietf-oauth-json-web-token
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]

// 2. Other authentication strategy attributes:
// HttpBasicAuthenticationStrategy - user and password
// NtlmAuthenticationStrategy - authenticate with the credentials of the currently logged in user, or impersonate a user
// OAuth2AuthorizationRequestHeaderAuthenticationStrategy - OAuth 2 authenticator using the authorization request header field.
// OAuth2UriQueryParameterAuthenticationStrategy - OAuth 2 authenticator using URI query parameter.
// SimpleAuthenticationStrategy - userKey, user, passwordKey, password
public class AuthenticationTests : APITest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void GetAlbumById()
    {
        var request = new RestRequest("api/Albums/10");

        var client = App.GetApiClientService();

        var response = client.Get<Albums>(request);

        Assert.AreEqual(10, response.Data.AlbumId);
    }
}
