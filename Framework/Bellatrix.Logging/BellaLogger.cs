// <copyright file="BellaLogger.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using Serilog;

namespace Bellatrix.Logging
{
    public class BellaLogger : IBellaLogger
    {
        private static readonly bool _isParallelRun;
        private static readonly ConcurrentDictionary<string, StringBuilder> _testOutputs;
        private readonly ILogger _logger;
        static BellaLogger()
        {
            _isParallelRun = ServicesCollection.Current.Resolve<bool>("isParallelRun");
            _testOutputs = new ConcurrentDictionary<string, StringBuilder>();
        }

        public BellaLogger(ILogger logger) => _logger = logger;

        public void LogInformation(string message, params object[] args)
        {
            try
            {
                _logger.Information(message, args);
                AddToTestOutputs(message, args);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void LogWarning(string message, params object[] args)
        {
            try
            {
                _logger.Warning(message, args);
                AddToTestOutputs(message, args);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void AddToTestOutputs(string message, params object[] args)
        {
            if (_isParallelRun)
            {
                var executionContext = ServicesCollection.Current.Resolve<ExecutionContext>();
                if (executionContext != null)
                {
                    if (!_testOutputs.ContainsKey(executionContext.TestFullName))
                    {
                        _testOutputs.TryAdd(executionContext.TestFullName, new StringBuilder());
                    }

                    if (!string.IsNullOrEmpty(message))
                    {
                        _testOutputs[executionContext.TestFullName].AppendLine(string.Format($"{message}{Environment.NewLine}", args));
                    }
                }
            }
        }
    }
}