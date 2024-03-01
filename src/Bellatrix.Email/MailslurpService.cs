// <copyright file="MailslurpService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Infrastructure.SystemFacades;
using mailslurp.Api;
using mailslurp.Client;
using mailslurp.Model;
using System;
using System.Collections.Generic;

namespace Bellatrix.Email;

public class MailslurpService
{
    private const long TIMEOUT = 30000L;
    private InboxControllerApi inboxControllerApi;

    public MailslurpService(string apiKey)
    {
        var config = new Configuration();
        config.ApiKey.Add("x-api-key", apiKey);
        inboxControllerApi = new InboxControllerApi(config);
    }

    public InboxDto CreateInbox(string name)
    {
        var inbox = inboxControllerApi.CreateInbox(name: name);

        return inbox;
    }

    public mailslurp.Model.Email WaitForLatestEmail(InboxDto inbox, DateTime? since)
    {
        var waitForControllerApi = new WaitForControllerApi();
        mailslurp.Model.Email receivedEmail = waitForControllerApi.WaitForLatestEmail(inbox.Id, TIMEOUT, false, null, since, null, 10000L);

        return receivedEmail;
    }

    public void SendEmail(InboxDto inbox, string toEmail, string subject, string templateName)
    {
        var emailBody = new EmbeddedResourcesService().FromFile(templateName);
        // send HTML body email
        var sendEmailOptions = new SendEmailOptions();
        sendEmailOptions.To = new List<string> { toEmail };
        sendEmailOptions.Subject = subject;
        sendEmailOptions.Body = emailBody;

        inboxControllerApi.SendEmail(inbox.Id, sendEmailOptions);
    }
}
