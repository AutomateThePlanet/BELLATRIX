using Bellatrix.DataGeneration.Parameters;
using RestSharp;
using System.Collections.Generic;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.OutputGenerators;
using System;

namespace Bellatrix.DataGeneration.Tests.Tests.RealWorld;

[TestFixture]
public class CountriesGraphQLTests
{
    public static List<IInputParameter> ABCGeneratedTestParameters()
    {
        return new List<IInputParameter>
        {
            new SingleSelectDataParameter(
                validOptions: new List<object> { "US", "BG", "FR" },
                invalidOptions: new List<object> { "XX", "U1", "" }),

            new SingleSelectDataParameter(
                validOptions: new List<object> { "en", "fr", "de" },
                invalidOptions: new List<object> { "zz", "123" }),

            new SingleSelectDataParameter(
                validOptions: new List<object> { "EU", "AF", "AS" },
                invalidOptions: new List<object> { "999", "X", "" })
        };
    }

    [Test]
    [ABCTestCaseSource(nameof(ABCGeneratedTestParameters), TestCaseCategory.Validation)]
    public void QueryCountry_WithLanguageAndContinentFilters_ShouldReturn200(
     string countryCode, string languageCode, string continentCode)
    {
        var client = new RestClient("https://countries.trevorblades.com/");
        var request = new RestRequest("", Method.Post);
        request.AddHeader("Content-Type", "application/json");

        var graphql = new
        {
            query = @"
                    query FilteredQuery($code: ID!, $lang: String!, $cont: String!) {
                        country(code: $code) {
                            name
                            capital
                            languages { code name }
                            continent { code name }
                        }
                    }",
            variables = new
            {
                code = countryCode.ToUpperInvariant(),
                lang = languageCode.ToLowerInvariant(),
                cont = continentCode.ToUpperInvariant()
            }
        };

        request.AddJsonBody(graphql);
        var response = client.Execute(request);

        Console.WriteLine($"→ Querying {countryCode}, Language: {languageCode}, Continent: {continentCode}");
        Console.WriteLine($"← Response: {response.StatusCode}, Body: {response.Content}");

        Assert.That((int)response.StatusCode, Is.EqualTo(200));
    }
}
