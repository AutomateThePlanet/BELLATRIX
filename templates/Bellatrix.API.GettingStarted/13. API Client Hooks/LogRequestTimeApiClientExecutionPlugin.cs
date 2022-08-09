using System;
using System.Diagnostics;
using Bellatrix.Api.Events;
using Bellatrix.Api.Extensions;

namespace Bellatrix.API.GettingStarted;

public class LogRequestTimeApiClientExecutionPlugin : ApiClientExecutionPlugin
{
    private Stopwatch _requestStopwatch = Stopwatch.StartNew();

    protected override void OnMakingRequest(object sender, RequestEventArgs client) => _requestStopwatch = Stopwatch.StartNew();

    protected override void OnRequestMade(object sender, ResponseEventArgs client)
    {
        _requestStopwatch.Stop();
        Console.WriteLine($"Request made for {_requestStopwatch.ElapsedMilliseconds}");
    }
}
