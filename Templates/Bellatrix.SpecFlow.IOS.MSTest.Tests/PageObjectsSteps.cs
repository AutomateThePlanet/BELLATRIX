using Bellatrix.SpecFlow.Mobile;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.IOS.MSTest.Tests
{
    [Binding]
    public class PageObjectsSteps : AndroidSteps
    {
        private CalculatorPage _calculatorPage;

        [When(@"I sum (.*) and (.*)")]
        public void WhenSumNumbers(int firstNumber, int secondNumber)
        {
            _calculatorPage = App.Create<CalculatorPage>();
            _calculatorPage.Sum(firstNumber, secondNumber);
        }

        [Then(@"I assert answer is (.*)")]
        public void AssertAnswer(int answer)
        {
            _calculatorPage.AssertAnswer(answer);
        }
    }
}
