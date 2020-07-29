// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using Bellatrix.Application;
using Bellatrix.ExceptionAnalysation;
using Bellatrix.ExceptionAnalysation.Contracts;
using Bellatrix.Web.ExceptionAnalysation;
using Bellatrix.Web.Waits;

namespace Bellatrix.Web
{
    public static class AppRegistrationExtensions
    {
        public static BaseApp UseExceptionAnalysation(this BaseApp baseApp)
        {
            baseApp.RegisterType<IEnumerable<IExceptionAnalysationHandler>, IExceptionAnalysationHandler[]>();
            baseApp.RegisterType<IExceptionAnalyser, ExceptionAnalyser>();

            baseApp.RegisterType<IExceptionAnalysationHandler, FileNotFoundExceptionHandler>(Guid.NewGuid().ToString());
            baseApp.RegisterType<IExceptionAnalysationHandler, ServiceUnavailableExceptionHandler>(Guid.NewGuid().ToString());
            baseApp.RegisterType<IExceptionAnalysationHandler, UnhandledServerExceptionHandler>(Guid.NewGuid().ToString());

            ElementWaitService.OnElementNotFulfillingWaitConditionEvent += ExceptionAnalysationEventHandlers.OnElementNotFulfillingWaitCondition;
            NavigationService.UrlNotNavigatedEvent += ExceptionAnalysationEventHandlers.UrlNotNavigated;

            return baseApp;
        }

        public static void AddExceptionHandler<TExceptionHandler>(this BaseApp baseApp)
            where TExceptionHandler : class, IExceptionAnalysationHandler
        {
            baseApp.RegisterType<IExceptionAnalysationHandler, TExceptionHandler>(Guid.NewGuid().ToString());
        }
    }
}