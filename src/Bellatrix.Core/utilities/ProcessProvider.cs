// <copyright file="ProcessProvider.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Bellatrix.Utilities;

public static class ProcessProvider
{
    public static DateTime GetStartTimeOfCurrentProcess()
    {
        var process = Process.GetCurrentProcess();
        return process.StartTime;
    }

    public static void Start(string fileName, string arguments)
    {
        Process.Start(fileName, arguments);
    }

    public static void StartCLIProcess(string arguments)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start("cmd.exe", $"/c {arguments}");
        }
        else
        {
            Process.Start("/bin/bash", $"-c {arguments}");
        }
    }

    public static int StartCLIProcessAndWaitToFinish(
       string workingDir,
       string arguments,
       bool useShellExecute,
       int timeoutMinutes,
       Action<string> standardOutputCallback = null,
       Action<string> errorOutputCallback = null)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return StartProcessAndWaitToFinish("cmd.exe", workingDir, $"/c {arguments}", useShellExecute, timeoutMinutes, standardOutputCallback, errorOutputCallback);
        }
        else
        {
            return StartProcessAndWaitToFinish("/bin/bash", workingDir, $"-c {arguments}", useShellExecute, timeoutMinutes, standardOutputCallback, errorOutputCallback);
        }
    }

    public static void CloseProcess(Process process)
    {
        process?.CloseMainWindow();
        process?.Close();
        process?.Dispose();
    }

    public static Process GetCurrentProcess() => Process.GetCurrentProcess();

    public static string GetEntryProcessApplicationPath()
    {
        // If we will write tests some day, create separate interfaces for below classes. But I think there is no need.
        string codeBase = Assembly.GetExecutingAssembly().Location;
        UriBuilder uri = new UriBuilder(codeBase);
        string path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path);
    }

    /// <summary>Starts a new process with the specified file name and arguments.</summary>
    /// <param name="fileName">The name of the file to start.</param>
    /// <param name="workingDir">The working directory.</param>
    /// <param name="arguments">The arguments to pass to the started application.</param>
    /// <param name="useShellExecute">Use shell execute.</param>
    /// <param name="timeoutMinutes">The time that the program will wait for the process to finish in minutes.</param>
    /// <param name="standardOutputCallback">A callback function that is called when data is written to the standard output.</param>
    /// <param name="errorOutputCallback">A callback function that is called when data is written to the error output.</param>
    /// <returns>The exit code of the process.</returns>
    /// <remarks>Both standardOutputCallback and errorOutputCallback can be null. In that case, the specified data is not captured.
    /// The single argument to both these callback functions is the data that was written to the specified output.</remarks>
    public static int StartProcessAndWaitToFinish(
        string fileName,
        string workingDir,
        string arguments,
        bool useShellExecute,
        int timeoutMinutes,
        Action<string> standardOutputCallback = null,
        Action<string> errorOutputCallback = null)
    {
        var redirectStandard = standardOutputCallback != null;
        var redirectError = errorOutputCallback != null;

        var startInfo = InitializeProcessStartInfo(fileName, workingDir, arguments, useShellExecute, redirectStandard, redirectError);

        var process = InitializeProcess(standardOutputCallback, errorOutputCallback, redirectStandard, redirectError, startInfo);

        using (process)
        {
            process.Start();

            ExecutePostProcessStartActions(redirectStandard, redirectError, process);

            WaitForProcessToFinish(timeoutMinutes, process);

            var exitCode = DetermineFinalProcessExitCode(process);

            return exitCode;
        }
    }

    public static void WaitForProcessToFinish(int timeoutMinutes, Process process)
    {
        if (timeoutMinutes != 0)
        {
            var timeoutMilliseconds = TimeSpan.FromMinutes(timeoutMinutes).TotalMilliseconds;
            process?.WaitForExit((int)timeoutMilliseconds);
        }
        else
        {
            process?.WaitForExit();
        }
    }

    public static bool IsPortBusy(int port)
    {
        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
        if (tcpConnInfoArray.Any(x => x.Port == port))
        {
            return false;
        }

        return false;
    }

    public static bool IsProcessWithNameRunning(string processName)
    {
        return Process.GetProcesses().Any(p => p.ProcessName.ToLower().Contains(processName.ToLower()));
    }

    public static void WaitPortToGetBusy(int port, int timeoutInMilliseconds = 30000)
    {
        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        do
        {
            var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
            if (tcpConnInfoArray.Any(x => x.Port == port))
            {
                break;
            }
            else
            {
                Thread.Sleep(100);
                timeoutInMilliseconds -= 100;
            }
        }
        while (timeoutInMilliseconds > 0);
    }

    public static Process StartProcess(
        string fileName,
        string workingDir,
        string arguments,
        bool useShellExecute,
        Action<string> standardOutputCallback = null,
        Action<string> errorOutputCallback = null)
    {
        // TO FIX- Use shell and write about padding exception to the guy from Abel.
        // maybe the error is because we throw it in assembly init
        // try to throw exception in the tests...
        var redirectStandard = standardOutputCallback != null;
        var redirectError = errorOutputCallback != null;

        var startInfo = InitializeProcessStartInfo(fileName, workingDir, arguments, useShellExecute, redirectStandard, redirectError);

        var process = InitializeProcess(standardOutputCallback, errorOutputCallback, redirectStandard, redirectError, startInfo);

        process.Start();

        ExecutePostProcessStartActions(redirectStandard, redirectError, process);

        return process;
    }

    public static Process InitializeProcess(
     string fileName,
     string workingDir,
     string arguments,
     bool useShellExecute,
     Action<string> standardOutputCallback = null,
     Action<string> errorOutputCallback = null)
    {
        var redirectStandard = standardOutputCallback != null;
        var redirectError = errorOutputCallback != null;

        var startInfo = InitializeProcessStartInfo(fileName, workingDir, arguments, useShellExecute, redirectStandard, redirectError);

        var process = InitializeProcess(standardOutputCallback, errorOutputCallback, redirectStandard, redirectError, startInfo);

        return process;
    }

    public static Process StartProcess(Process process, Action<string> standardOutputCallback = null, Action<string> errorOutputCallback = null)
    {
        var redirectStandard = standardOutputCallback != null;
        var redirectError = errorOutputCallback != null;

        process.Start();

        ExecutePostProcessStartActions(redirectStandard, redirectError, process);

        return process;
    }

    public static string GetExecutingAssemblyFolder()
    {
        string codeBase = Assembly.GetExecutingAssembly().Location;
        var uri = new UriBuilder(codeBase);
        string path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path);
    }

    private static ProcessStartInfo InitializeProcessStartInfo(string fileName, string workingDir, string arguments, bool useShellExecute, bool redirectStandard, bool redirectError)
    {
        var startInfo = new ProcessStartInfo(fileName, arguments)
        {
            UseShellExecute = useShellExecute,
            CreateNoWindow = false,
        };

        //// UseShellExecute has to be false if standard or error output is redirected
        if (!string.IsNullOrEmpty(workingDir))
        {
            startInfo.WorkingDirectory = workingDir;
        }

        if (redirectStandard || redirectError)
        {
            if (redirectStandard)
            {
                startInfo.RedirectStandardOutput = true;
            }

            if (redirectError)
            {
                startInfo.RedirectStandardError = true;
            }
        }

        return startInfo;
    }

    private static Process InitializeProcess(
        Action<string> standardOutputCallback,
        Action<string> errorOutputCallback,
        bool redirectStandard,
        bool redirectError,
        ProcessStartInfo startInfo)
    {
        var process = new Process
        {
            StartInfo = startInfo,
        };
        if (redirectStandard || redirectError)
        {
            // the events are only raised if this property is set to true
            process.EnableRaisingEvents = true;

            if (redirectStandard)
            {
                process.OutputDataReceived += (_, e) => standardOutputCallback(e.Data);
            }

            if (redirectError)
            {
                process.ErrorDataReceived += (_, e) => errorOutputCallback(e.Data);
            }
        }

        return process;
    }

    private static void ExecutePostProcessStartActions(bool redirectStandard, bool redirectError, Process process)
    {
        // capturing standard output only starts if you call BeginOutputReadLine
        if (redirectStandard)
        {
            process.BeginOutputReadLine();
        }

        // capturing error output only starts if you call BeginErrorReadLine
        if (redirectError)
        {
            process.BeginErrorReadLine();
        }
    }

    private static int DetermineFinalProcessExitCode(Process process)
    {
        int exitCode;
        try
        {
            if (!process.HasExited)
            {
                exitCode = -2;
            }
            else
            {
                exitCode = process.ExitCode;
            }
        }
        catch (InvalidOperationException)
        {
            exitCode = -1;
        }

        return exitCode;
    }
}