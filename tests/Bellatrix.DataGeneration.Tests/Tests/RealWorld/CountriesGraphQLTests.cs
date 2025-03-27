using RestSharp;
using System.Collections.Generic;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.OutputGenerators;
using System;

namespace Bellatrix.DataGeneration.Tests.Tests.RealWorld;

[TestFixture]
public class CountriesGraphQLTests
{
    public static List<IInputParameter> ABCGeneratedTestParameters() =>
    TestInputComposer
        .Start()
        .AddSingleSelect(s => s
            .Valid("US")
            .Valid("BG")
            .Valid("FR")
            .Invalid("XX").WithoutMessage()
            .Invalid("U1").WithoutMessage()
            .Invalid("").WithoutMessage())
        .AddSingleSelect(s => s
            .Valid("en")
            .Valid("fr")
            .Valid("de")
            .Invalid("zz").WithoutMessage()
            .Invalid("123").WithoutMessage())
        .AddSingleSelect(s => s
            .Valid("EU")
            .Valid("AF")
            .Valid("AS")
            .Invalid("999").WithoutMessage()
            .Invalid("X").WithoutMessage()
            .Invalid("").WithoutMessage())
        .Build();

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
