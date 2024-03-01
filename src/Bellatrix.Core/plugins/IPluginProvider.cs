// <copyright file="IPluginProvider.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Plugins;

public interface IPluginProvider
{
    event EventHandler<PluginEventArgs> PreTestInitEvent;

    event EventHandler<PluginEventArgs> TestInitFailedEvent;

    event EventHandler<PluginEventArgs> PostTestInitEvent;

    event EventHandler<PluginEventArgs> PreTestCleanupEvent;

    event EventHandler<PluginEventArgs> PostTestCleanupEvent;

    event EventHandler<PluginEventArgs> TestCleanupFailedEvent;

    event EventHandler<PluginEventArgs> PreTestsActEvent;

    event EventHandler<PluginEventArgs> PreTestsArrangeEvent;

    event EventHandler<PluginEventArgs> PostTestsActEvent;

    event EventHandler<PluginEventArgs> PostTestsArrangeEvent;

    event EventHandler<PluginEventArgs> PreTestsCleanupEvent;

    event EventHandler<PluginEventArgs> PostTestsCleanupEvent;

    event EventHandler<Exception> TestsCleanupFailedEvent;

    event EventHandler<Exception> TestsArrangeFailedEvent;
}