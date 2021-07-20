using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellatrix.Assertions;
using Bellatrix.GoogleLighthouse;

namespace Bellatrix.Web.lighthouse
{
    public class MetricPreciseValidationBuilder
    {
        public static event EventHandler<LighthouseReportEventArgs> AssertedLighthouseReportEventArgs;

        private dynamic _actualValue;
        private string _metricName;

        public MetricPreciseValidationBuilder(dynamic actualValue, string message)
        {
            _actualValue = actualValue;
            _metricName = message;
        }

        public FinishValidationBuilder Equal<T>(T expected)
        {
            return new FinishValidationBuilder(() => _actualValue == expected,
                    () => BuildNotificationValidationMessage(ComparingOperators.EQUAL, expected),
                    () => BuildFailedValidationMessage(ComparingOperators.EQUAL, expected));
        }

        public FinishValidationBuilder GreaterThan<T>(T expected)
        {
            return new FinishValidationBuilder(() => _actualValue > expected,
                    () => BuildNotificationValidationMessage(ComparingOperators.GREATER_THAN, expected),
                    () => BuildFailedValidationMessage(ComparingOperators.GREATER_THAN, expected));
        }

        public FinishValidationBuilder GreaterThanOrEqual<T>(T expected)
        {
            return new FinishValidationBuilder(() => _actualValue >= expected,
                    () => BuildNotificationValidationMessage(ComparingOperators.GREATER_THAN_EQUAL, expected),
                    () => BuildFailedValidationMessage(ComparingOperators.GREATER_THAN_EQUAL, expected));
        }

        public FinishValidationBuilder LessThan<T>(T expected)
        {
            return new FinishValidationBuilder(() => _actualValue < expected,
                    () => BuildNotificationValidationMessage(ComparingOperators.LESS_THAN, expected),
                    () => BuildFailedValidationMessage(ComparingOperators.LESS_THAN, expected));
        }

        public FinishValidationBuilder LessThanOrEqual<T>(T expected)
        {
            return new FinishValidationBuilder(() => _actualValue <= expected,
                    () => BuildNotificationValidationMessage(ComparingOperators.LESS_THAN_EQAUL, expected),
                    () => BuildFailedValidationMessage(ComparingOperators.LESS_THAN_EQAUL, expected));
        }

        private string BuildNotificationValidationMessage<T>(ComparingOperators comparingMessage, T expected)
        {
            // Get ENUM description
            return $"{_metricName} {comparingMessage} {expected}";
        }

        private string BuildFailedValidationMessage<T>(ComparingOperators comparingMessage, T expected)
        {
            // Get ENUM description
            return $"{_metricName} {comparingMessage} {expected}";
        }

        public void Perform()
        {
            // TODO: call exception message
            Assert.IsTrue(_actualValue > 0, _metricName);
            AssertedLighthouseReportEventArgs?.Invoke(this, new LighthouseReportEventArgs(_metricName));
        }
    }
}
