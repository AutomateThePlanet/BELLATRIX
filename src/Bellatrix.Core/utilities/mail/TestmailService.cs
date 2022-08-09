using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.Utilities;

public class TestmailService
{
    private const string EMAIL_SERVICE_URL = "https://api.testmail.app/";
    private readonly string apiKey;
    private readonly string emailNamespace;

    public TestmailService(string apiKey, string emailNamespace)
    {
        this.apiKey = apiKey;
        this.emailNamespace = emailNamespace;
    }

    public Email GetLastSentEmail(String inboxName)
    {
        var allEmails = GetAllEmails();
        return allEmails.emails.Where(e => e.envelope_to.Contains(inboxName)).Last();
    }

    public List<Email> GetAllEmails(String inboxName)
    {
        var allEmails = GetAllEmails();

        return allEmails.emails.Where(e => e.envelope_to.Contains(inboxName)).ToList();
    }

    private Root GetAllEmails()
    {
        var client = new RestClient
        {
            BaseHost = EMAIL_SERVICE_URL
        };
        var request = new RestRequest("/api/json/");
        request.AddQueryParameter("apikey", apiKey);
        request.AddQueryParameter("namespace", emailNamespace);
        request.AddQueryParameter("pretty", "true");
        var emailsResponse = client.Get<Root>(request);
        return emailsResponse.Data;
    }
}