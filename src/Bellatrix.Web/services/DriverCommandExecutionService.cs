// <copyright file="DriverCommandExecutionService.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Web;

public class DriverCommandExecutionService : WebService
{
    public DriverCommandExecutionService(IWebDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public void InitializeSendCommand(IWebDriver driver)
    {
        ////var remoteDriver = driver as RemoteWebDriver;
        ////var commandInfo = new CommandInfo(
        ////    CommandInfo.PostCommand,
        ////    $"/session/{remoteDriver.SessionId}/chromium/send_command_and_get_result");
        ////WebDriverCommandExecutor.TryAddCommand(remoteDriver, "sendCommand", commandInfo);

        ////ServicesCollection.Current.RegisterInstance(this);
    }

    public void SendCommandForFileDownload(string downloadPath)
    {
        try
        {
            SendCommand(
                WrappedDriver,
                "Page.setDownloadBehavior",
                new Dictionary<string, object>()
                {
                    ["behavior"] = "allow",
                    ["downloadPath"] = downloadPath,
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Command for file downloаd failed: " + ex.Message);
        }
    }

    public void SendCommandForHardRefresh()
    {
        try
        {
            SendCommand(
                WrappedDriver,
                "Page.reload",
                new Dictionary<string, object>()
                {
                    ["ignoreCache"] = true,
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Command for Hard refresh failed: " + ex.Message);
        }
    }

    private Response SendCommand(IWebDriver driver, string cmd, object parameters = null)
    {
        var commandParams = new Dictionary<string, object> { { "cmd", cmd }, { "params", parameters ?? new object() } };
        var response = WebDriverCommandExecutor.Execute((RemoteWebDriver)driver, "sendCommand", commandParams);
        if (response.Status != WebDriverResult.Success)
        {
            Console.WriteLine("Error occured: " + cmd);
        }

        return response;
    }

    internal static class WebDriverCommandExecutor
    {
        public static bool TryAddCommand(RemoteWebDriver driver, string commandName, CommandInfo commandInfo)
        {
            var commandExecutor = (ICommandExecutor)driver.GetType().GetProperty("CommandExecutor", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(driver);
            if (commandExecutor == null)
            {
                throw new WebDriverException("Webdriver doesn't contain 'CommandExecutor' property.");
            }

            return commandExecutor.TryAddCommand(commandName, commandInfo);
        }

        public static Response Execute(RemoteWebDriver driver, string driverCommandToExecute, Dictionary<string, object> parameters)
        {
            var executeMethod = driver.GetType().GetMethod("Execute", BindingFlags.Instance | BindingFlags.NonPublic);
            if (executeMethod == null)
            {
                throw new WebDriverException("Webdriver doesn't contain 'Execute' method.");
            }

            var response = executeMethod.Invoke(driver, new object[] { driverCommandToExecute, parameters }) as Response;
            if (response == null)
            {
                throw new WebDriverException(
                    "Unexpected failure executing command; response was not in the proper format.");
            }

            return response;
        }
    }
}