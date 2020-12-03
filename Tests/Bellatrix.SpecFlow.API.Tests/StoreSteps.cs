using System;
using System.Collections.Generic;
using System.Text;
using Bellatrix.Api.SpecFlow;
using Bellatrix.Assertions;
using RestSharp;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.API.Tests
{
    [Binding]
    public class StoreSteps : APISteps
    {
        private long _actualAlbumId;

        [When(@"I get album by ID = (.*)")]
        public void WhenIGetAlbumById(int albumId)
        {
            var request = new RestRequest($"api/Albums/{albumId}");

            var client = App.GetApiClientService();

            var response = client.Get<Albums>(request);

            _actualAlbumId = response.Data.AlbumId;
        }

        [Then(@"I assert album ID = (.*)")]
        public void ThenIAssertAlbumId(int expectedAlbumId)
        {
            Assert.AreEqual(expectedAlbumId, _actualAlbumId);
        }
    }
}
