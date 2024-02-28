// <copyright file="Constants.cs" company="Automate The Planet Ltd.">
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
// <author>Ventsislav Ivanov</author>
// <site>https://bellatrix.solutions/</site>
namespace Bellatrix.ImageRecognition.Utilities;

internal static class Constants
{
    public const string ResultPrefix = "SIKULI#: ";
    public const string InteractiveConsoleReadyMarker = "... use ctrl-d to end the session";
    public const string ErrorMarker = "[error]";
    public const string ExitCommand = "exit()";
    public const int SikuliReadyTimeoutSeconds = 30;
}
