using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Bellatrix.TestWorkflowPlugins;

namespace Bellatrix
{
    public abstract class ExecutionContext
    {
        public ExecutionContext()
        {
            SharedData = new Dictionary<string, object>();
            TestAttachments = new List<string>();
        }

        public static string SettingsFileContent { get; set; }
        public Dictionary<string, object> SharedData { get; }
        public List<string> TestAttachments { get; }

        public TestOutcome TestOutcome => GetTestOutcome() == TestOutcome.NotSet ? (TestOutcome)SharedData["TestOutcome"] : GetTestOutcome();
        public string TestName => GetTestName() ?? (string)SharedData["TestName"];
        public string TestFullName => GetTestFullName() ?? (string)SharedData["TestFullName"];
        public string TestClassName => GetTestClassName() ?? (string)SharedData["TestClassName"];
        public string ExceptionMessage => GetExceptionMessage() ?? (string)SharedData["ExceptionMessage"];
        public string ExceptionStackTrace => GetExceptionStackTrace() ?? (string)SharedData["ExceptionStackTrace"];
        public Exception Exception
        {
            get
            {
                if (GetException() != null)
                {
                    return GetException();
                }
                else if (SharedData.ContainsKey("Exception"))
                {
                    return (Exception)SharedData["Exception"];
                }
                else
                {
                    return null;
                }
            }
        }

        public abstract void AddTestAttachment(string filePath);
        protected abstract string GetTestName();
        protected abstract TestOutcome GetTestOutcome();
        protected abstract string GetTestFullName();
        protected abstract string GetTestClassName();
        protected abstract string GetExceptionMessage();
        protected abstract Exception GetException();
        protected abstract string GetExceptionStackTrace();

        public void AddOrUpdateSharedData(string key, object data)
        {
            if (SharedData.ContainsKey(key))
            {
                SharedData[key] = data;
            }
            else
            {
                SharedData.Add(key, data);
            }
        }

        public object RemovedSharedData(string key)
        {
            object result = default;
            if (SharedData.ContainsKey(key))
            {
                SharedData.Remove(key, out result);
            }

            return result;
        }
    }
}
