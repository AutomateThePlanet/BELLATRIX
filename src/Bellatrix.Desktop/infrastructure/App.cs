// <copyright file="App.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Desktop;

public class App : IDisposable
{
    // TODO: Change to be ThreadLocal.
    private static bool _shouldStartLocalService;
    private static Process _winAppDriverProcess;

    public App()
    {
        _shouldStartLocalService = ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.ShouldStartLocalService;
    }

    public AppService AppService => ServicesCollection.Current.Resolve<AppService>();
    public ComponentWaitService Wait => ServicesCollection.Current.Resolve<ComponentWaitService>();
    public ComponentCreateService Components => ServicesCollection.Current.Resolve<ComponentCreateService>();

    public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    public ComputerVision ComputerVision => ServicesCollection.Current.Resolve<ComputerVision>();
    public FormRecognizer FormRecognizer => ServicesCollection.Current.Resolve<FormRecognizer>();
    public AWSServicesFactory AWS => ServicesCollection.Current.Resolve<AWSServicesFactory>();
    public IAssert Assert => ServicesCollection.Current.Resolve<IAssert>();

    public static void StartWinAppDriver()
    {
        if (_shouldStartLocalService)
        {
            int port = int.Parse(ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.Url.Split(':').Last());

            // Anton(06.09.2018): maybe we can kill WinAppDriver every time
            if (ProcessProvider.IsProcessWithNameRunning("WinAppDriver") || ProcessProvider.IsPortBusy(port))
            {
                return;
            }

            string winAppDriverPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Windows Application Driver");
            if (!Directory.Exists(winAppDriverPath))
            {
                throw new ArgumentException("Windows Application Driver is not installed on the machine. To use BELLATRIX Desktop libraries you need to install it first. You can download it from here: https://github.com/Microsoft/WinAppDriver/releases");
            }

            string winAppDriverExePath = Path.Combine(winAppDriverPath, "WinAppDriver.exe");
            _winAppDriverProcess = ProcessProvider.StartProcess(winAppDriverExePath, winAppDriverPath, $"{ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.Url}", true);
            ProcessProvider.WaitPortToGetBusy(port);
        }
    }

    public static void StopWinAppDriver()
    {
        if (_shouldStartLocalService)
        {
            if (ProcessProvider.IsProcessWithNameRunning("WinAppDriver"))
            {
                ProcessProvider.CloseProcess(_winAppDriverProcess);
            }
        }
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
