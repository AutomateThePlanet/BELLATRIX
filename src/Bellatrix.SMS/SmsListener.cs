// <copyright file="SmsListener.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Twilio.Rest.Api.V2010.Account;

namespace Bellatrix.SMS;

public class SmsListener
{
    public static event EventHandler<SmsEventArgs> MessageReceived;

    private readonly string phoneNumber;
    private readonly List<MessageResource> messages = new List<MessageResource>();
    private CancellationTokenSource cancellationTokenSource;
    private DateTime start;

    public SmsListener(string phoneNumber = null)
    {
        this.phoneNumber = phoneNumber;
    }

    public List<MessageResource> GetMessages()
    {
        return new List<MessageResource>(messages);
    }

    public MessageResource GetLastMessage()
    {
        return messages.LastOrDefault();
    }

    public void Listen()
    {
        start = DateTime.UtcNow;
        cancellationTokenSource = new CancellationTokenSource();
        Task.Run(() => CheckForMessages(cancellationTokenSource.Token));
    }

    public void StopListening()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }

    private async Task CheckForMessages(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var messageReader = MessageResource.ReadAsync(
                dateSentAfter: start,
                from: phoneNumber,
                limit: 1
            );

            var foundMessages = await messageReader;

            if (foundMessages.Any())
            {
                var message = foundMessages.First();
                messages.Add(message);
                MessageReceived?.Invoke(this, new SmsEventArgs(this, message));
                start = DateTime.UtcNow;
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);
        }
    }
}
