// <copyright file="ITelemetry.cs" company="Automate The Planet Ltd.">
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
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Bellatrix.Trace
{
    public interface ITelemetry
    {
        TelemetryClient TelemetryClient { get; }
        void TrackExceptionAndFlush(Exception ex);
        void TrackException(Exception ex);
        void TrackEvent(EventTelemetry eventTelemetry);
        void TrackEvent(string eventName);
        void TrackTestExecution(string projectTrackInfo);
        void Flush();
    }
}
