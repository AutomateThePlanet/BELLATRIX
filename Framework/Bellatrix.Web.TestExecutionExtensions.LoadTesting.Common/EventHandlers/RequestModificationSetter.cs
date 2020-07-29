// <copyright file="RequestModificationSetter.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using Bellatrix.Web.Events;

namespace Bellatrix.Web.TestExecutionExtensions.LoadTesting
{
    public static class RequestModificationSetter
    {
        public static void SetRequestModification(ElementActionEventArgs arg)
        {
            var loadTestingWorkflowPluginContext = ServicesCollection.Current.Resolve<LoadTestingWorkflowPluginContext>();
            if (loadTestingWorkflowPluginContext.IsLoadTestingEnabled &&
                !string.IsNullOrEmpty(loadTestingWorkflowPluginContext.CurrentTestName) &&
                loadTestingWorkflowPluginContext.HttpRequestsPerTest != null &&
                loadTestingWorkflowPluginContext.HttpRequestsPerTest.ContainsKey(loadTestingWorkflowPluginContext.CurrentTestName))
            {
                var currentHttpRequests =
                    loadTestingWorkflowPluginContext.HttpRequestsPerTest[loadTestingWorkflowPluginContext.CurrentTestName];
                var htmlRequest = currentHttpRequests.LastOrDefault(x => x.Headers.Any(y => y.Contains("text/html")));
                htmlRequest?.RequestModifications.Add(new RequestModification(arg.ActionValue));
            }
        }
    }
}
