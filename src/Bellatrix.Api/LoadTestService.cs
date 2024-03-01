// <copyright file="LoadTestService.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Bellatrix.Api;

public class LoadTestService
{
    public void ExecuteForTime(int numberOfProcesses, int pauseBetweenStartSeconds, int secondsToBeExecuted, Action testBody)
    {
        if (numberOfProcesses <= 0)
        {
            throw new ArgumentException($"Number of processes should be a positive number.");
        }

        if (pauseBetweenStartSeconds < 0)
        {
            throw new ArgumentException($"Pause between start of processes should be a positive number.");
        }

        if (secondsToBeExecuted < 0)
        {
            throw new ArgumentException($"Seconds to be executed should be a positive number.");
        }

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        do
        {
            var loadTasks = CreateTestTasks(numberOfProcesses, pauseBetweenStartSeconds, testBody);
            Task.WaitAll(loadTasks.ToArray());
        }
        while (stopWatch.Elapsed.TotalSeconds < secondsToBeExecuted);

        stopWatch.Stop();
    }

    public void ExecuteNumberOfTimes(int numberOfProcesses, int pauseBetweenStartSeconds, int timesToBeExecuted, Action testBody)
    {
        if (numberOfProcesses <= 0)
        {
            throw new ArgumentException($"Number of processes should be a positive number.");
        }

        if (pauseBetweenStartSeconds < 0)
        {
            throw new ArgumentException($"Pause between start of processes should be a positive number.");
        }

        if (timesToBeExecuted < 0)
        {
            throw new ArgumentException($"Times to be executed should be a positive number.");
        }

        for (int i = 0; i < timesToBeExecuted; i++)
        {
            var loadTasks = CreateTestTasks(numberOfProcesses, pauseBetweenStartSeconds, testBody);
            Task.WaitAll(loadTasks.ToArray());
        }
    }

    private List<Task> CreateTestTasks(int numberOfProcesses, int pauseBetweenStartSeconds, Action testBody)
    {
        var loadTasks = new List<Task>();
        for (int i = 0; i < numberOfProcesses; i++)
        {
            if (pauseBetweenStartSeconds > 0)
            {
                Thread.Sleep(pauseBetweenStartSeconds * 1000);
            }

            loadTasks.Add(Task.Factory.StartNew(testBody));
        }

        return loadTasks;
    }
}
