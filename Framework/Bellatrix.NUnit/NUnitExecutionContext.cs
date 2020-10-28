using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Bellatrix.TestWorkflowPlugins;
using NUnit.Framework;

namespace Bellatrix.NUnit
{
    public class NUnitExecutionContext : ExecutionContext
    {
        public override void AddTestAttachment(string filePath)
        {
            if (GetTestClassName() != null && !string.IsNullOrEmpty(filePath))
            {
                TestContext.AddTestAttachment(filePath);
            }

            TestAttachments.Add(filePath);
        }

        protected override Exception GetException()
        {
            if (!string.IsNullOrEmpty(TestContext.CurrentContext?.Result.Message))
            {
                return new Exception(TestContext.CurrentContext?.Result.Message);
            }

            return null;
        }

        protected override string GetExceptionMessage()
        {
            return TestContext.CurrentContext?.Result.Message ?? string.Empty;
        }

        protected override string GetExceptionStackTrace()
        {
            return TestContext.CurrentContext?.Result.StackTrace ?? string.Empty;
        }

        protected override string GetTestClassName()
        {
            if (TestContext.CurrentContext?.Test.ClassName == "NUnit.Framework.Internal.TestExecutionContext+AdhocContext")
            {
                return null;
            }

            return TestContext.CurrentContext?.Test.ClassName;
        }

        protected override string GetTestFullName()
        {
            if (TestContext.CurrentContext?.Test.ClassName == "NUnit.Framework.Internal.TestExecutionContext+AdhocContext")
            {
                return null;
            }

            return TestContext.CurrentContext?.Test.FullName;
        }

        protected override string GetTestName()
        {
            if (TestContext.CurrentContext?.Test.ClassName == "NUnit.Framework.Internal.TestExecutionContext+AdhocContext")
            {
                return null;
            }

            return TestContext.CurrentContext?.Test.Name;
        }

        protected override TestOutcome GetTestOutcome()
        {
            if (TestContext.CurrentContext == null || TestContext.CurrentContext?.Test.ClassName == "NUnit.Framework.Internal.TestExecutionContext+AdhocContext")
            {
                return TestOutcome.NotSet;
            }
            else
            {
                return (TestOutcome)TestContext.CurrentContext.Result.Outcome.Status;
            }
        }
    }
}
