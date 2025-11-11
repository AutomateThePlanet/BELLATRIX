// <copyright file="EventHandlerUtilities.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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

using System.Diagnostics;
using System.Reflection;

namespace Bellatrix.Playwright.Utilities;
public static class EventHandlerUtilities
{
    public static void Detach(object obj, string eventName)
    {
        var caller = new StackTrace().GetFrame(1).GetMethod();
        var type = obj.GetType();
        foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
        {
            if (typeof(Delegate).IsAssignableFrom(field.FieldType))
            {
                var handler = (field.GetValue(obj) as Delegate)?.GetInvocationList().FirstOrDefault(m => m.Method.Equals(caller));
                if (handler != null)
                {
                    type.GetEvent(eventName).RemoveEventHandler(obj, handler);
                    return;
                }
            }
        }
    }
}
