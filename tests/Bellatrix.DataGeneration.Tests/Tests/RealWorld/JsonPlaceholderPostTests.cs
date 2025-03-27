using Bellatrix.DataGeneration.Parameters;
using RestSharp;
using System.Collections.Generic;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.OutputGenerators;
using System;

namespace Bellatrix.DataGeneration.Tests.Tests.RealWorld;

[TestFixture]
public class JsonPlaceholderPostTests
{
    //public static List<IInputParameter> ABCGeneratedTestParameters()
    //{
    //    return new List<IInputParameter>
    //    {
    //        new TextDataParameter(minBoundary: 5, maxBoundary: 100),            // title
    //        new TextDataParameter(minBoundary: 20, maxBoundary: 500),           // body
    //        new IntegerDataParameter(minBoundary: 1, maxBoundary: 100),         // userId
    //        new UsernameDataParameter(minBoundary: 6, maxBoundary: 15),         // authorUsername
    //        new EmailDataParameter(minBoundary: 10, maxBoundary: 30),           // authorEmail
    //        new BooleanDataParameter(),                                         // isPublished
    //        new DateTimeDataParameter(minBoundary: DateTime.UtcNow.AddDays(-30), maxBoundary: DateTime.UtcNow) // publishDate
    //    };
    //}
    public static List<IInputParameter> ABCGeneratedTestParameters() =>
    TestInputComposer
        .Start()
        .AddText(5, 100)                        // title
        .AddText(20, 500)                       // body
        .AddInteger(1, 100)                     // userId
        .AddUsername(6, 15)                     // authorUsername
        .AddEmail(10, 30)                       // authorEmail
        .AddBoolean()                           // isPublished
        .AddDateTime(
            DateTime.UtcNow.AddDays(-30),
            DateTime.UtcNow)                    // publishDate
        .Build();

    [Test]
    [ABCTestCaseSource(nameof(ABCGeneratedTestParameters), TestCaseCategory.Validation)]
    public void CreateFullPost_WithGeneratedMetadata_ShouldSucceed(
        string title,
        string body,
        string userId,
        string authorUsername,
        string authorEmail,
        string isPublished,
        string publishDate)
    {
        var client = new RestClient("https://jsonplaceholder.typicode.com");
        var request = new RestRequest("/posts", Method.Post);
        request.AddJsonBody(new
        {
            title = title,
            body = body,
            userId = int.Parse(userId),
            metadata = new
            {
                username = authorUsername,
                email = authorEmail,
                isPublished = bool.Parse(isPublished),
                publishDate = DateTime.Parse(publishDate)
            }
        });

        var response = client.Execute(request);

        Console.WriteLine($"→ Posting data: {title} by {authorUsername} on {publishDate}");
        Console.WriteLine($"← Response: {response.StatusCode}, Body: {response.Content}");

        Assert.That((int)response.StatusCode, Is.EqualTo(201));
    }

}
