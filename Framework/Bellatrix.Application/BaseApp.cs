// <copyright file="BaseApp.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.TestWorkflowPlugins;
using Bellatrix.Trace;

namespace Bellatrix.Application
{
    public class BaseApp : IDisposable
    {
        public void RegisterType<TFrom, TTo>(string name = null)
            where TTo : TFrom
        {
            ValidateServiceContainer();
            if (string.IsNullOrEmpty(name))
            {
                ServicesCollection.Current.RegisterType<TFrom, TTo>();
            }
            else
            {
                ServicesCollection.Current.RegisterType<TFrom, TTo>(name);
            }
        }

        public void RegisterInstance<TFrom>(TFrom instance)
        {
            ValidateServiceContainer();
            ServicesCollection.Current.RegisterInstance(instance);
        }

        public TFrom Resolve<TFrom>()
        {
            ValidateServiceContainer();
            return ServicesCollection.Current.Resolve<TFrom>();
        }

        private void ValidateServiceContainer()
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException("The default container for the BaseApp is not configured.\n The first method you need to call is 'BaseApp.Use{IoCFramework}Container();'\nFor example, if you have installed Unity IoC projects call 'BaseApp.UseUnityContainer();'.");
            }
        }

        public void Dispose()
        {
            Telemetry.Instance.Flush();
            GC.SuppressFinalize(this);
        }
    }
}