// <copyright file="TestmailService.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.Email;

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
        var client = new RestClient(EMAIL_SERVICE_URL);
        var request = new RestRequest("/api/json/");
        request.AddQueryParameter("apikey", apiKey);
        request.AddQueryParameter("namespace", emailNamespace);
        request.AddQueryParameter("pretty", "true");
        var emailsResponse = client.GetAsync<Root>(request).Result;
        return emailsResponse;
    }
}