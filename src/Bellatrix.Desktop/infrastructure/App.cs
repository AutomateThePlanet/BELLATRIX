﻿// <copyright file="App.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using System.IO;
using System.Linq;
using System.Reflection;
using Bellatrix;
using Bellatrix.Assertions;
using Bellatrix.AWS;
using Bellatrix.CognitiveServices;
using Bellatrix.Desktop.Configuration;
using Bellatrix.Desktop.EventHandlers;
using Bellatrix.Desktop.PageObjects;
using Bellatrix.Desktop.Services;
using Bellatrix.DynamicTestCases;
using Bellatrix.Plugins;
using Bellatrix.Utilities;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace Bellatrix.Desktop;

public class App : IDisposable
{
    // TODO: Change to be ThreadLocal.
    private static readonly bool ShouldStartLocalService = ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.ShouldStartLocalService;
    private static Process _appiumServerProcess;

    public AppService AppService => ServicesCollection.Current.Resolve<AppService>();
    public ComponentWaitService Wait => ServicesCollection.Current.Resolve<ComponentWaitService>();
    public ComponentCreateService Components => ServicesCollection.Current.Resolve<ComponentCreateService>();

    public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    public ComputerVision ComputerVision => ServicesCollection.Current.Resolve<ComputerVision>();
    public FormRecognizer FormRecognizer => ServicesCollection.Current.Resolve<FormRecognizer>();
    public AWSServicesFactory AWS => ServicesCollection.Current.Resolve<AWSServicesFactory>();
    public IAssert Assert => ServicesCollection.Current.Resolve<IAssert>();

    public static void StartAppiumServer()
    {
        if (!ShouldStartLocalService)
        {
            return;
        }

        var uri = new Uri(ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.Url);

        // Anton(06.09.2018): maybe we can kill WinAppDriver every time
        if (ProcessProvider.IsProcessWithNameRunning("WinAppDriver") || ProcessProvider.IsPortBusy(uri.Port))
        {
            return;
        }

        var winAppDriverPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Windows Application Driver");
        if (!Directory.Exists(winAppDriverPath))
        {
            throw new ArgumentException("Windows Application Driver is not installed on the machine. To use BELLATRIX Desktop libraries you need to install it first. You can download it from here: https://github.com/Microsoft/WinAppDriver/releases");
        }

        var appiumPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "npm");
        if (!Directory.Exists(appiumPath))
        {
            throw new ArgumentException("Node.js is not installed on the machine. To use BELLATRIX Desktop libraries you need to install it first. You can download it from here: https://nodejs.org/en/download");
        }

        var appiumPs1Path = Path.Combine(appiumPath, "appium.ps1");
        _appiumServerProcess = ProcessProvider.StartProcess(
            "powershell.exe",
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            $"-NoProfile -ExecutionPolicy RemoteSigned -File \"{appiumPs1Path}\" -a {uri.Host} -p {uri.Port} --allow-insecure=power_shell",
            true);

        ProcessProvider.WaitPortToGetBusy(uri.Port);
    }

    public static void StopAppiumServer()
    {
        ProcessProvider.CloseProcess(_appiumServerProcess);
    }

    public void AddAdditionalCapability(string name, object value)
    {
        string fullClassName = DetermineTestClassFullNameAttributes();
        var dictionary = ServicesCollection.Main.Resolve<Dictionary<string, object>>($"caps-{fullClassName}") ?? new Dictionary<string, object>();
        dictionary.Add(name, value);
        ServicesCollection.Main.RegisterInstance(dictionary, $"caps-{fullClassName}");
    }

    public void AddElementEventHandler<TComponentsEventHandler>()
        where TComponentsEventHandler : ComponentEventHandlers
    {
        var elementEventHandler = (TComponentsEventHandler)Activator.CreateInstance(typeof(TComponentsEventHandler));
        elementEventHandler.SubscribeToAll();
    }

    public void RemoveElementEventHandler<TComponentsEventHandler>()
        where TComponentsEventHandler : ComponentEventHandlers
    {
        var elementEventHandler = (TComponentsEventHandler)Activator.CreateInstance(typeof(TComponentsEventHandler));
        elementEventHandler.UnsubscribeToAll();
    }

    public void AddPlugin<TExecutionExtension>()
        where TExecutionExtension : Plugin
    {
        ServicesCollection.Current.RegisterType<Plugin, TExecutionExtension>(Guid.NewGuid().ToString());
    }

    public void Dispose()
    {
        DisposeDriverService.DisposeAll();
        GC.SuppressFinalize(this);
    }

    public TPage Create<TPage>()
        where TPage : DesktopPage
    {
        TPage page = ServicesCollection.Current.Resolve<TPage>();
        return page;
    }

    private string DetermineTestClassFullNameAttributes()
    {
        string fullClassName = string.Empty;
        var callStackTrace = new StackTrace();
        var currentAssembly = GetType().Assembly;

        foreach (var frame in callStackTrace.GetFrames())
        {
            var frameMethodInfo = frame.GetMethod() as MethodInfo;
            if (!frameMethodInfo?.ReflectedType?.Assembly.Equals(currentAssembly) == true &&
                frameMethodInfo.Name.Equals("TestsArrange") || frameMethodInfo.Name.Equals("ScenarioInitialize"))
            {
                fullClassName = frameMethodInfo.DeclaringType.FullName;
                break;
            }
        }

        return fullClassName;
    }
}