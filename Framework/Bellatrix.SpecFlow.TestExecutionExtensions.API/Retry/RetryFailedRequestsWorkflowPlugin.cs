// <copyright file="RetryFailedRequestsWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Api;
using Bellatrix.SpecFlow.TestExecutionExtensions.API.Retry;
using Bellatrix.SpecFlow.TestWorkflowPlugins;
using Bellatrix.Utilities;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.TestExecutionExtensions.API
{
    [Binding]
    public class RetryFailedRequestsWorkflowPlugin : TestWorkflowPlugin
    {
        private int _maxRetryAttempts;
        private int _pauseBetweenFailures;
        private TimeUnit _timeUnit;

        [Given(@"I set max retry attempts to (.*)")]
        public void GivenSetMaxRetryAttempts(int maxRetryAttempts)
        {
            _maxRetryAttempts = maxRetryAttempts;
        }

        [Given(@"I pause between failures (.*) seconds")]
        public void GivenPauseBetweenFailuresSeconds(int pauseBetweenFailures)
        {
            _pauseBetweenFailures = pauseBetweenFailures;
            _timeUnit = TimeUnit.Seconds;
        }

        [Given(@"I pause between failures (.*) minutes")]
        public void GivenPauseBetweenFailuresMinutes(int pauseBetweenFailures)
        {
            _pauseBetweenFailures = pauseBetweenFailures;
            _timeUnit = TimeUnit.Minutes;
        }

        [Given(@"I pause between failures (.*) milliseconds")]
        public void GivenPauseBetweenFailuresMilliseconds(int pauseBetweenFailures)
        {
            _pauseBetweenFailures = pauseBetweenFailures;
            _timeUnit = TimeUnit.Milliseconds;
        }

        protected override void PostBeforeScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            var retryFailedRequestsInfo = new RetryFailedRequestsInfo()
            {
                MaxRetryAttempts = _maxRetryAttempts,
                PauseBetweenFailures = _pauseBetweenFailures,
                TimeUnit = _timeUnit,
            };

            var client = ServicesCollection.Current.Resolve<ApiClientService>();
            client.PauseBetweenFailures = TimeSpanConverter.Convert(retryFailedRequestsInfo.PauseBetweenFailures, retryFailedRequestsInfo.TimeUnit);
            client.MaxRetryAttempts = retryFailedRequestsInfo.MaxRetryAttempts;
        }
    }
}