// <copyright file="Wait.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Diagnostics;
using System.Threading;

namespace Bellatrix.Utilities;

public sealed class Wait
{
    public static void Until(
        Func<bool> condition,
        int timeoutInSeconds = 10,
        string exceptionMessage = "Timeout exceeded.",
        int retryRateDelay = 50)
    {
        var start = DateTime.Now;
        while (!condition())
        {
            var now = DateTime.Now;
            var totalSeconds = (now - start).TotalSeconds;
            if (totalSeconds >= timeoutInSeconds)
            {
                throw new TimeoutException(exceptionMessage + $" Elapsed time: {totalSeconds} s.");
            }

            Thread.Sleep(retryRateDelay);
        }
    }

    public static void Until<T>(
        Func<T, bool> condition, T expectedIntParam, int timeoutInSeconds = 10, int retryRateDelay = 50)
    {
        var start = DateTime.Now;
        while (!condition(expectedIntParam))
        {
            var now = DateTime.Now;
            var totalSeconds = (now - start).TotalSeconds;
            if (totalSeconds >= timeoutInSeconds)
            {
                throw new TimeoutException("Timeout exceeded.");
            }

            Thread.Sleep(retryRateDelay);
        }
    }

    public static void Until(
        Func<int, Guid, bool> condition, int expectedIntParam, Guid expectedStrParam, int timeoutInSeconds = 10)
    {
        var start = DateTime.Now;
        while (!condition(expectedIntParam, expectedStrParam))
        {
            var now = DateTime.Now;
            var totalSeconds = (now - start).TotalSeconds;
            if (totalSeconds >= timeoutInSeconds)
            {
                throw new TimeoutException("Timeout exceeded.");
            }

            Thread.Sleep(50);
        }
    }

    public static void UntilOk(Func<string> condition, int timeoutInSeconds = 10, bool debugFail = false)
    {
        var start = DateTime.Now;

        var functionResult = condition();
        while (!functionResult.Equals("ok", StringComparison.InvariantCultureIgnoreCase))
        {
            var now = DateTime.Now;
            if ((now - start).TotalSeconds >= timeoutInSeconds)
            {
                if ((debugFail && Debugger.IsAttached) || !Debugger.IsAttached)
                {
                    throw new TimeoutException(functionResult);
                }
            }

            Thread.Sleep(50);
            functionResult = condition();
        }
    }

    public static void ForConditionUntilTimeout(
        Func<bool> condition,
        int totalRunTimeoutMilliseconds = 5000,
        Action onTimeout = null,
        int sleepTimeMilliseconds = 2000)
    {
        var startTime = DateTime.UtcNow;
        var timeout = startTime + TimeSpan.FromMilliseconds(totalRunTimeoutMilliseconds);

        while (true)
        {
            var conditionFinished = condition();
            if (conditionFinished)
            {
                break;
            }

            if (DateTime.UtcNow >= timeout)
            {
                onTimeout?.Invoke();

                break;
            }

            Thread.Sleep(sleepTimeMilliseconds);
        }
    }

    public static void Until(
        Func<bool> condition,
        TimeSpan timeout,
        TimeSpan retryRateDelay,
        string exceptionMessage)
    {
        var start = DateTime.Now;
        while (!condition())
        {
            var now = DateTime.Now;
            var elapsedTime = now - start;

            if (elapsedTime >= timeout)
            {
                throw new TimeoutException(exceptionMessage);
            }

            Thread.Sleep(retryRateDelay);
        }
    }
}