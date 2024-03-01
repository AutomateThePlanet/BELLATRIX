// <copyright file="ImageRecognitionRuntime.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Text.RegularExpressions;
using Bellatrix.ImageRecognition.Exceptions;
using Bellatrix.ImageRecognition.Interfaces;
using Bellatrix.ImageRecognition.Utilities;

namespace Bellatrix.ImageRecognition;

internal class ImageRecognitionRuntime : IImageRecognitionRuntime
{
    private readonly IImageRecognitionService _imageRecognitionService;
    private Process _process;
    private IAsyncStreamsHandler _asyncStreamsHandler;

    public ImageRecognitionRuntime(IImageRecognitionService imageRecognitionService)
    {
        _imageRecognitionService = imageRecognitionService ?? throw new ArgumentNullException(nameof(imageRecognitionService));
    }

    public void Start()
    {
        if (_process != null)
        {
            throw new InvalidOperationException("This Sikuli session has already been started");
        }

        _process = _imageRecognitionService.Start("-i");
        _asyncStreamsHandler = new AsyncStreamsHandler(_process.StandardOutput, _process.StandardError, _process.StandardInput);
        _asyncStreamsHandler.ReadUntil(Constants.SikuliReadyTimeoutSeconds, Constants.InteractiveConsoleReadyMarker);
    }

    public void Stop(bool ignoreErrors = false)
    {
        if (_process == null)
        {
            return;
        }

        _asyncStreamsHandler.WriteLine(Constants.ExitCommand);

        if (!_process.HasExited)
        {
            if (!_process.WaitForExit(500))
            {
                _process.Kill();
            }
        }

        string errors = null;
        if (!ignoreErrors)
        {
            errors = _process.StandardError.ReadToEnd();
        }

        _asyncStreamsHandler.WaitForExit();

        _asyncStreamsHandler.Dispose();
        _asyncStreamsHandler = null;
        _process.Dispose();
        _process = null;

        if (!ignoreErrors && !string.IsNullOrEmpty(errors))
        {
            throw new ImageRecognitionException("Sikuli Errors: " + errors);
        }
    }

    public void Run(string command, double? timeoutInSeconds = 1)
    {
        if (_process == null || _process.HasExited)
        {
            throw new InvalidOperationException("The Sikuli process is not running");
        }

        _asyncStreamsHandler.WriteLine(command);
        _asyncStreamsHandler.WriteLine(string.Empty);
        _asyncStreamsHandler.WriteLine(string.Empty);

        var result = _asyncStreamsHandler.ReadUntil((double)timeoutInSeconds, Constants.ErrorMarker, Constants.ResultPrefix);

        if (result.IndexOf(Constants.ErrorMarker, StringComparison.Ordinal) <= -1)
        {
            return;
        }

        result = result + Environment.NewLine + string.Join(Environment.NewLine, _asyncStreamsHandler.ReadUpToNow(0.1d));

        if (!result.Contains("FindFailed"))
        {
            throw new ImageRecognitionException(result);
        }

        var regex = new Regex("FindFailed: .+");
        throw new ImageRecognitionFindFailedException(regex.Match(result).Value);
    }

    public void Dispose()
    {
        Stop(true);
    }
}
