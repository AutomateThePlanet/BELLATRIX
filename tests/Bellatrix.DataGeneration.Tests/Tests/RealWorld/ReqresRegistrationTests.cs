using Bellatrix.DataGeneration;
using Bellatrix.DataGeneration.Parameters;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.OutputGenerators;
using System;

namespace Bellatrix.DataGeneration.Tests.Tests.RealWorld;

[TestFixture]
public class ReqresRegistrationTests
{
    public static List<IInputParameter> ABCGeneratedTestParameters()
    {
        return new List<IInputParameter>
        {
            new EmailDataParameter(minBoundary: 10, maxBoundary: 20),
            new PasswordDataParameter(minBoundary: 8, maxBoundary: 16)
        };
    }

    [Test]
    [ABCTestCaseSource(nameof(ABCGeneratedTestParameters), TestCaseCategory.Validation)]
    public void RegisterUser_WithGeneratedEmailAndPassword(string email, string password)
    {
        var client = new RestClient("https://reqres.in");
        var request = new RestRequest("/api/register", Method.Post);
        request.AddJsonBody(new
        {
            email = email,
            password = password
        });

        var response = client.Execute(request);

        Console.WriteLine($"→ Attempted registration with Email: {email}, Password: {password}");
        Console.WriteLine($"← Response: {response.StatusCode}, Content: {response.Content}");

        // Expect 200 for known valid email or 400 otherwise (per Reqres.in behavior)
        Assert.That((int)response.StatusCode, Is.EqualTo(200).Or.EqualTo(400));
    }
}
