// <copyright file="AndroidDeviceService.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
using Bellatrix.Mobile.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Services.Android;

public class AndroidDeviceService : DeviceService<AndroidDriver, AppiumElement>
{
    public AndroidDeviceService(AndroidDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public bool IsLocked
    {
        get
        {
            bool result;
            try
            {
                result = WrappedAppiumDriver.IsLocked();
            }
            catch (InvalidCastException ex) when (ex.Message.Contains("Unable to cast object of type 'System.String' to type 'System.Boolean"))
            {
                throw new AppiumEngineException(ex);
            }

            return result;
        }
    }

    public ConnectionType ConnectionType
    {
        get
        {
            // airplane_mode_on: "1" = ON, "0" = OFF
            var airplane = ShellGet("settings", "get global airplane_mode_on").Trim();
            if (airplane == "1") return ConnectionType.AirplaneMode;

            // These return "enabled"/"disabled" on most devices
            var wifi = ShellGet("svc", "wifi").Trim().ToLowerInvariant();
            var data = ShellGet("svc", "data").Trim().ToLowerInvariant();

            bool wifiOn = wifi.Contains("enabled");
            bool dataOn = data.Contains("enabled");

            if (wifiOn && dataOn) return ConnectionType.AllNetworkOn;
            if (wifiOn) return ConnectionType.WifiOnly;
            if (dataOn) return ConnectionType.DataOnly;

            return ConnectionType.None;
        }
        set
        {
            try { WrappedAppiumDriver.HideKeyboard(); } catch { /* ignore */ }

            switch (value)
            {
                case ConnectionType.AirplaneMode:
                    // Turn airplane mode on; (broadcast helps some OEMs apply it)
                    Shell("settings", "put global airplane_mode_on 1");
                    Shell("am", "broadcast -a android.intent.action.AIRPLANE_MODE --ez state true");
                    // Typically disables radios, but OEMs vary
                    break;

                case ConnectionType.None:
                    Shell("settings", "put global airplane_mode_on 0");
                    Shell("am", "broadcast -a android.intent.action.AIRPLANE_MODE --ez state false");
                    Shell("svc", "wifi disable");
                    Shell("svc", "data disable");
                    break;

                case ConnectionType.WifiOnly:
                    Shell("settings", "put global airplane_mode_on 0");
                    Shell("am", "broadcast -a android.intent.action.AIRPLANE_MODE --ez state false");
                    Shell("svc", "wifi enable");
                    Shell("svc", "data disable");
                    break;

                case ConnectionType.DataOnly:
                    Shell("settings", "put global airplane_mode_on 0");
                    Shell("am", "broadcast -a android.intent.action.AIRPLANE_MODE --ez state false");
                    Shell("svc", "wifi disable");
                    Shell("svc", "data enable");
                    break;

                case ConnectionType.AllNetworkOn:
                    Shell("settings", "put global airplane_mode_on 0");
                    Shell("am", "broadcast -a android.intent.action.AIRPLANE_MODE --ez state false");
                    Shell("svc", "wifi enable");
                    Shell("svc", "data enable");
                    break;
            }
        }
    }

    public Dictionary<string, object> Settings { get => WrappedAppiumDriver.Settings; set => WrappedAppiumDriver.Settings = value; }
    public void Lock() => WrappedAppiumDriver.Lock();
    public void Unlock() => WrappedAppiumDriver.Lock();
    public void TurnOnLocationService() => WrappedAppiumDriver.ToggleLocationServices();
    public void OpenNotifications() => WrappedAppiumDriver.OpenNotifications();
    public void SetSetting(string setting, object value) => WrappedAppiumDriver.SetSetting(setting, value);

    private void Shell(string command, string args)
    {
        var p = new Dictionary<string, object>
        {
            ["command"] = command,
            ["args"] = args.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            ["includeStderr"] = true,
            ["timeout"] = 20000
        };

        ((IJavaScriptExecutor)WrappedAppiumDriver).ExecuteScript("mobile: shell", p);
    }

    private string ShellGet(string command, string args)
    {
        var p = new Dictionary<string, object>
        {
            ["command"] = command,
            ["args"] = args.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            ["includeStderr"] = true,
            ["timeout"] = 20000
        };

        var result = ((IJavaScriptExecutor)WrappedAppiumDriver).ExecuteScript("mobile: shell", p);

        // Appium returns a dictionary with "stdout"/"stderr" on many setups
        if (result is IDictionary<string, object> dict && dict.TryGetValue("stdout", out var stdout))
            return stdout?.ToString() ?? string.Empty;

        return result?.ToString() ?? string.Empty;
    }
}
