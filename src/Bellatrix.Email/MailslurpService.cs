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
