using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Bellatrix.TestWorkflowPlugins;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.MSTest
{
    public class MSTestExecutionContext : ExecutionContext
    {
        private static ThreadLocal<Exception> _thrownException;
        private TestContext _testContext;

        public MSTestExecutionContext(TestContext testContext)
        {
            _testContext = testContext;
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                if (eventArgs.Exception.Source != "System.Private.CoreLib")
                {
                    if (_thrownException == null)
                    {
                        _thrownException = new ThreadLocal<Exception>(() => eventArgs.Exception);
                    }
                    else
                    {
                        _thrownException.Value = eventArgs.Exception;
                    }
                }
            };
        }

        public override void AddTestAttachment(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            if (GetTestClassName() != null)
            {
                _testContext?.AddResultFile(filePath);
            }

            TestAttachments.Add(filePath);
        }

        protected override Exception GetException()
        {
            return _thrownException?.Value;
        }

        protected override string GetExceptionMessage()
        {
            var exceptionMessage = _thrownException?.Value?.Message;
            return exceptionMessage ?? string.Empty;
        }

        protected override string GetExceptionStackTrace()
        {
            var stackTrace = _thrownException?.Value?.StackTrace;
            return stackTrace ?? string.Empty;
        }

        protected override string GetTestClassName()
        {
            if (_testContext == null)
            {
                return null;
            }

            return _testContext.FullyQualifiedTestClassName;
        }

        protected override string GetTestFullName()
        {
            if (_testContext == null)
            {
                return null;
            }

            return $"{_testContext?.FullyQualifiedTestClassName}.{_testContext?.TestName}";
        }

        protected override string GetTestName()
        {
            if (_testContext == null)
            {
                return null;
            }

            return _testContext.TestName;
        }

        protected override TestOutcome GetTestOutcome()
        {
            if (_testContext == null)
            {
                return TestOutcome.NotSet;
            }
            else
            {
                return (TestOutcome)_testContext.CurrentTestOutcome;
            }
        }
    }
}
