using Bellatrix.Api;
using Bellatrix.API.GettingStarted.Models;
using Bellatrix.API.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.GettingStarted;

[TestClass]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]
public class ValidateSchemaTests : APITest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void AssertJsonSchema()
    {
        var request = new RestRequest("api/Albums/10");

        var response = App.GetApiClientService().Get<Albums>(request);

        // 1. The expected JSON schema.
        // http://json-schema.org/examples.html
        var expectedSchema = @"{
              ""definitions"": {},
              ""$schema"": ""http://json-schema.org/draft-07/schema#"",
              ""$id"": ""http://example.com/root.json"",
              ""type"": ""object"",
              ""title"": ""The Root Schema"",
              ""required"": [
                ""albumId"",
                ""title"",
                ""artistId"",
                ""artist"",
                ""tracks""
              ],
              ""properties"": {
                ""albumId"": {
                  ""$id"": ""#/properties/albumId"",
                  ""type"": ""integer"",
                  ""title"": ""The Albumid Schema"",
                  ""default"": 0,
                  ""examples"": [
                    10
                  ]
                },
                ""title"": {
                  ""$id"": ""#/properties/title"",
                  ""type"": ""string"",
                  ""title"": ""The Title Schema"",
                  ""default"": """",
                  ""examples"": [
                    ""Audioslave""
                  ],
                  ""pattern"": ""^(.*)$""
                },
                ""artistId"": {
                  ""$id"": ""#/properties/artistId"",
                  ""type"": ""integer"",
                  ""title"": ""The Artistid Schema"",
                  ""default"": 0,
                  ""examples"": [
                    8
                  ]
                },
                ""artist"": {
                  ""$id"": ""#/properties/artist"",
                  ""type"": ""null"",
                  ""title"": ""The Artist Schema"",
                  ""default"": null,
                  ""examples"": [
                    null
                  ]
                },
                ""tracks"": {
                  ""$id"": ""#/properties/tracks"",
                  ""type"": ""array"",
                  ""title"": ""The Tracks Schema""
                }
              }
            }";

        // 2. Use the BELLATRIX AssertSchema method to validate the schema.
        // The same method can be used for XML responses as well.
        response.AssertSchema(expectedSchema);
    }
}
