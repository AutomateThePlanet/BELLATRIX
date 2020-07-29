// <copyright file="TestInfoProvider.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bellatrix.Trace
{
    public class TestInfoProvider
    {
        public int CountBellatrixTests(Assembly assembly, string testClassAttributeName, string testMethodAttributeName)
        {
            int bellatrixTestsCount = 0;

            foreach (var currentType in assembly.GetTypes())
            {
                if (currentType.GetCustomAttributesData().Any(x => x.ToString().Contains(testClassAttributeName)))
                {
                    foreach (var currentMethod in currentType.GetMethods())
                    {
                        if (currentMethod.GetCustomAttributes().Any(x => x.GetType().FullName.Equals(testMethodAttributeName)))
                        {
                            bellatrixTestsCount++;
                        }
                    }
                }
            }

            return bellatrixTestsCount;
        }
    }
}
