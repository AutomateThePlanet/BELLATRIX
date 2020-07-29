// <copyright file="LoadTestSettings.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;

namespace Bellatrix.LoadTesting.Model
{
    public class LoadTestSettings
    {
        public LoadTestSettings()
        {
            IgnoreUrlRequestsPatterns = new List<string>();
            TestScenariosNotToBeExecutedPatterns = new List<string>();
            TestScenariosToBeExecutedPatterns = new List<string>();
            TestScenariosWeights = new Dictionary<string, int>();
        }

        public bool ShouldExecuteRecordedRequestPauses { get; set; } = true;
        public MixtureMode MixtureMode { get; set; } = MixtureMode.Equal;
        public LoadTestType LoadTestType { get; set; } = LoadTestType.ExecuteForTime;
        public int NumberOfProcesses { get; set; } = 2;
        public int PauseBetweenStartSeconds { get; set; } = 0;
        public int TimesToBeExecuted { get; set; } = 1;
        public int SecondsToBeExecuted { get; set; } = 60;
        public List<string> IgnoreUrlRequestsPatterns { get; set; }
        public List<string> TestScenariosNotToBeExecutedPatterns { get; set; }
        public List<string> TestScenariosToBeExecutedPatterns { get; set; }
        public Dictionary<string, int> TestScenariosWeights { get; set; }
    }
}
