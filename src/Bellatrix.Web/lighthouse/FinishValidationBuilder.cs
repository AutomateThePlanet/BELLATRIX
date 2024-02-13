// <copyright file="FinishValidationBuilder.cs" company="Automate The Planet Ltd.">
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

public class FinishValidationBuilder
{
    public static event EventHandler<LighthouseReportEventArgs> AssertedLighthouseReportEventArgs;

    private Func<bool> _comparingFunction;
    private string _notificationMessage;
    private string _failedAssertionMessage;

    public FinishValidationBuilder(Func<bool> comparingFunction) => _comparingFunction = comparingFunction;

    public FinishValidationBuilder(Func<bool> comparingFunction, Func<string> notificationMessageFunction, Func<string> failedAssertionMessageFunction)
    {
        _comparingFunction = comparingFunction;
        _notificationMessage = notificationMessageFunction.Invoke();
        _failedAssertionMessage = failedAssertionMessageFunction.Invoke();
    }

    public void Perform()
    {
        Assert.IsTrue<LighthouseAssertFailedException>(_comparingFunction.Invoke(), _failedAssertionMessage);
        AssertedLighthouseReportEventArgs?.Invoke(this, new LighthouseReportEventArgs(_notificationMessage));
    }
}
