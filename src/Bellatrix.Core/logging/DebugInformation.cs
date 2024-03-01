// <copyright file="DebugInformation.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellatrix.Core.Logging;

namespace Bellatrix;

public static class DebugInformation
{
    private static readonly object _lockObject = new object();

    public static void PrintStackTrace<TException>(this TException ex)
        where TException : Exception
    {
        if (ConfigurationService.GetSection<TroubleshootingSettings>().DebugInformationEnabled)
        {
            lock (_lockObject)
            {
                try
                {
                    Console.Error.WriteLine();
                    Console.Error.WriteLine(new string('*', 10));
                    Console.Error.WriteLine(ex.ToString());
                    Console.Error.WriteLine(new string('*', 10));
                    Console.Error.WriteLine();

                    Debug.WriteLine(string.Empty);
                    Debug.WriteLine(new string('*', 10));
                    Debug.WriteLine(ex.ToString());
                    Debug.WriteLine(new string('*', 10));
                    Debug.WriteLine(string.Empty);
                }
                catch
                {
                    // ignore
                }
            }
        }
    }
}
