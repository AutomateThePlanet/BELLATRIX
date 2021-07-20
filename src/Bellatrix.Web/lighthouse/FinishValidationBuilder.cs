using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellatrix.Assertions;
using Bellatrix.GoogleLighthouse;

namespace Bellatrix.Web.lighthouse
{
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
}
