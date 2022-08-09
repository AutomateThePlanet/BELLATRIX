using Bellatrix.Core.utilities.mail.model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.Utilities;

public class TestmailService
{
    private const string EMAIL_SERVICE_URL = "https://api.testmail.app/";
    private string apiKey;
    private string emailNamespace;

    public TestmailService(string apiKey, string emailNamespace)
    {
        this.apiKey = apiKey;
        this.emailNamespace = emailNamespace;
    }

    public Email getLastSentEmail(String inboxName)
    {
        var allEmails = getAllEmails();
        return allEmails.emails.Where(e => e.envelope_to.Contains(inboxName)).Last();
    }

    public List<Email> getAllEmails(String inboxName)
    {
        var allEmails = getAllEmails();

        return allEmails.emails.Where(e => e.envelope_to.Contains(inboxName)).ToList();
    }

    private Root getAllEmails()
    {
        var client = new RestClient();
        client.BaseHost = EMAIL_SERVICE_URL;
        var request = new RestRequest("/api/json/");
        request.AddQueryParameter("apikey", apiKey);
        request.AddQueryParameter("namespace", emailNamespace);
        request.AddQueryParameter("pretty", "true");
        var emailsResponse = client.Get<Root>(request);
        return emailsResponse.Data;
    }
}