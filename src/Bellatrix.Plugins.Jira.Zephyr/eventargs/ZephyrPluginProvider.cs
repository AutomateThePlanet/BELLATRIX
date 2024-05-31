// <copyright file="ZephyrPluginProvider.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Plugins.Jira.Zephyr.Data;

namespace Bellatrix.Plugins.Jira.Zephyr.Eventargs;

public class ZephyrPluginProvider
{
    public event EventHandler<ZephyrCyclePluginEventArgs> ZephyrCycleCreatedEvent;
    public event EventHandler<ZephyrExecutionPluginEventArgs> ZephyrTestCaseExecutionFailedEvent;
    public event EventHandler<ZephyrExecutionPluginEventArgs> ZephyrTestCaseExecutedEvent;
    public event EventHandler<ZephyrCyclePluginEventArgs> ZephyrCycleStatusUpdateFailedEvent;

    public void ZephyrCycleCreated(PluginEventArgs e, ZephyrTestCycle cycle) => RaiseCycleEvent(ZephyrCycleCreatedEvent, e, cycle);

    public void ZephyrTestCaseExecutionFailed(PluginEventArgs e, ZephyrTestCase testCase) => RaiseExecutionEvent(ZephyrTestCaseExecutionFailedEvent, e, testCase);

    public void ZephyrTestCaseExecuted(PluginEventArgs e, ZephyrTestCase testCase) => RaiseExecutionEvent(ZephyrTestCaseExecutedEvent, e, testCase);

    public void ZephyrCycleStatusUpdateFailed(PluginEventArgs e, ZephyrTestCycle cycle) => RaiseCycleEvent(ZephyrCycleStatusUpdateFailedEvent, e, cycle);

    private void RaiseCycleEvent(EventHandler<ZephyrCyclePluginEventArgs> eventHandler, PluginEventArgs e, ZephyrTestCycle cycle) => eventHandler?.Invoke(this, new ZephyrCyclePluginEventArgs(e, cycle));

    private void RaiseExecutionEvent(EventHandler<ZephyrExecutionPluginEventArgs> eventHandler, PluginEventArgs e, ZephyrTestCase testCase) => eventHandler?.Invoke(this, new ZephyrExecutionPluginEventArgs(e, testCase));
}
