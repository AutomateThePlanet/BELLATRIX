// <copyright file="MetricPreciseValidationBuilder.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellatrix.Assertions;
using Bellatrix.GoogleLighthouse;

namespace Bellatrix.Web.lighthouse;

public class MetricPreciseValidationBuilder
{
    public static event EventHandler<LighthouseReportEventArgs> AssertedLighthouseReportEventArgs;

    private dynamic _actualValue;
    private string _metricName;

    public MetricPreciseValidationBuilder(dynamic actualValue, string message)
    {
        _actualValue = actualValue;
        _metricName = message;
    }

    public FinishValidationBuilder Equal<T>(T expected)
    {
        return new FinishValidationBuilder(() => _actualValue == expected,
                () => BuildNotificationValidationMessage(ComparingOperators.EQUAL, expected),
                () => BuildFailedValidationMessage(ComparingOperators.EQUAL, expected));
    }

    public FinishValidationBuilder GreaterThan<T>(T expected)
    {
        return new FinishValidationBuilder(() => _actualValue > expected,
                () => BuildNotificationValidationMessage(ComparingOperators.GREATER_THAN, expected),
                () => BuildFailedValidationMessage(ComparingOperators.GREATER_THAN, expected));
    }

    public FinishValidationBuilder GreaterThanOrEqual<T>(T expected)
    {
        return new FinishValidationBuilder(() => _actualValue >= expected,
                () => BuildNotificationValidationMessage(ComparingOperators.GREATER_THAN_EQUAL, expected),
                () => BuildFailedValidationMessage(ComparingOperators.GREATER_THAN_EQUAL, expected));
    }

    public FinishValidationBuilder LessThan<T>(T expected)
    {
        return new FinishValidationBuilder(() => _actualValue < expected,
                () => BuildNotificationValidationMessage(ComparingOperators.LESS_THAN, expected),
                () => BuildFailedValidationMessage(ComparingOperators.LESS_THAN, expected));
    }

    public FinishValidationBuilder LessThanOrEqual<T>(T expected)
    {
        return new FinishValidationBuilder(() => _actualValue <= expected,
                () => BuildNotificationValidationMessage(ComparingOperators.LESS_THAN_EQAUL, expected),
                () => BuildFailedValidationMessage(ComparingOperators.LESS_THAN_EQAUL, expected));
    }

    private string BuildNotificationValidationMessage<T>(ComparingOperators comparingMessage, T expected)
    {
        // Get ENUM description
        return $"{_metricName} {comparingMessage} {expected}";
    }

    private string BuildFailedValidationMessage<T>(ComparingOperators comparingMessage, T expected)
    {
        // Get ENUM description
        return $"{_metricName} {comparingMessage} {expected}";
    }

    public void Perform()
    {
        // TODO: call exception message
        Assert.IsTrue(_actualValue > 0, _metricName);
        AssertedLighthouseReportEventArgs?.Invoke(this, new LighthouseReportEventArgs(_metricName));
    }
}
