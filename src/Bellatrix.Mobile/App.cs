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
using System.Reflection;
using Bellatrix.Assertions;
using Bellatrix.AWS;
using Bellatrix.CognitiveServices;
using Bellatrix.DynamicTestCases;
using Bellatrix.Mobile.Configuration;
using Bellatrix.Mobile.PageObjects;
using Bellatrix.Mobile.Services;
using Bellatrix.Plugins;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace Bellatrix.Mobile;

public abstract class App<TDriver, TDriverElement> : IDisposable
    where TDriver : AppiumDriver
    where TDriverElement : AppiumElement
{
    private static bool _shouldStartAppiumLocalService;

    public App()
    {
        _shouldStartAppiumLocalService = ConfigurationService.GetSection<MobileSettings>().ExecutionSettings.ShouldStartLocalService;
    }

    public ComponentWaitService<TDriver, TDriverElement> Wait => ServicesCollection.Current.Resolve<ComponentWaitService<TDriver, TDriverElement>>();
    public ComponentCreateService Components => ServicesCollection.Current.Resolve<ComponentCreateService>();
    public WebServicesFacade Web => ServicesCollection.Current.Resolve<WebServicesFacade>();
    public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    public FormRecognizer FormRecognizer => ServicesCollection.Current.Resolve<FormRecognizer>();
    public ComputerVision ComputerVision => ServicesCollection.Current.Resolve<ComputerVision>();
    public AWSServicesFactory AWS => ServicesCollection.Current.Resolve<AWSServicesFactory>();
    public IAssert Assert => ServicesCollection.Current.Resolve<IAssert>();

    public static void StartAppiumLocalService()
    {
        if (_shouldStartAppiumLocalService)
        {
            var args = new OptionCollector().AddArguments(GeneralOptionList.PreLaunch());
            WrappedAppiumCreateService.AppiumLocalService = new AppiumServiceBuilder().WithArguments(args).UsingAnyFreePort().Build();
            WrappedAppiumCreateService.AppiumLocalService.Start();
        }
    }

    public void StopAppiumLocalService()
    {
        if (_shouldStartAppiumLocalService)
        {
            WrappedAppiumCreateService.AppiumLocalService.Dispose();
        }
    }

    public void AddAdditionalAppiumOption(string name, object value)
    {
        string fullClassName = DetermineTestClassFullNameAttributes();
        var dictionary = ServicesCollection.Current.Resolve<Dictionary<string, object>>($"caps-{fullClassName}") ?? new Dictionary<string, object>();
        dictionary.Add(name, value);
        ServicesCollection.Current.RegisterInstance(dictionary, $"caps-{fullClassName}");
    }

    public void AddPlugin<TExecutionExtension>()
        where TExecutionExtension : Plugin
    {
        ServicesCollection.Current.RegisterType<Plugin, TExecutionExtension>(Guid.NewGuid().ToString());
    }

    public abstract void Dispose();

    public TPage Create<TPage>()
        where TPage : MobilePage
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
