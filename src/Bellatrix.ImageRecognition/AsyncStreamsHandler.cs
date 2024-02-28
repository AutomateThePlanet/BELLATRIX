// <copyright file="AsyncStreamsHandler.cs" company="Automate The Planet Ltd.">
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
// <author>Ventsislav Ivanov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bellatrix.ImageRecognition.Interfaces;

namespace Bellatrix.ImageRecognition;

internal class AsyncStreamsHandler : IAsyncStreamsHandler
{
    private readonly TextReader _stdout;
    private readonly TextReader _stderr;
    private readonly TextWriter _stdin;
    private readonly Task _readStderrTask;
    private readonly Task _readStdoutTask;
    private readonly BlockingCollection<string> _pendingOutputLines = new BlockingCollection<string>();

    public AsyncStreamsHandler(TextReader stdout, TextReader stderr, TextWriter stdin)
    {
        _stdout = stdout;
        _stderr = stderr;
        _stdin = stdin;

        _readStdoutTask = Task.Factory.StartNew(ReadStdout, TaskCreationOptions.LongRunning);
        _readStderrTask = Task.Factory.StartNew(ReadStderr, TaskCreationOptions.LongRunning);
    }

    public virtual string ReadUntil(double timeoutInSeconds, params string[] expectedStrings)
    {
        while (true)
        {
            string line;
            if (timeoutInSeconds > 0)
            {
                var timeout = TimeSpan.FromSeconds(timeoutInSeconds);
                if (!_pendingOutputLines.TryTake(out line, timeout))
                {
                    throw new TimeoutException($"No result in allowed time: {timeout}");
                }
            }
            else
            {
                line = _pendingOutputLines.Take();
            }

            if (expectedStrings.Any(s => line.IndexOf(s, StringComparison.Ordinal) > -1))
            {
                return line;
            }
        }
    }

    public IEnumerable<string> ReadUpToNow(double timeoutInSeconds)
    {
        while (true)
        {
            if (_pendingOutputLines.TryTake(out var line, TimeSpan.FromSeconds(timeoutInSeconds)))
            {
                yield return line;
            }
            else
            {
                yield break;
            }
        }
    }

    public void WriteLine(string command)
    {
        _stdin.WriteLine(command);
    }

    public void WaitForExit()
    {
        _readStdoutTask.Wait();
        _readStderrTask.Wait();
    }

    public void Dispose()
    {
        _readStdoutTask?.Dispose();
        _readStderrTask?.Dispose();
    }

    private void ReadStdout()
    {
        ReadStd(_stdout);
    }

    private void ReadStderr()
    {
        ReadStd(_stderr, "[error] ");
    }

    private void ReadStd(TextReader output, string prefix = null)
    {
        string line;
        while ((line = output.ReadLine()) != null)
        {
            if (prefix != null)
            {
                line = prefix + line;
            }

            Debug.WriteLine(line);
            _pendingOutputLines.Add(line);
        }
    }
}
