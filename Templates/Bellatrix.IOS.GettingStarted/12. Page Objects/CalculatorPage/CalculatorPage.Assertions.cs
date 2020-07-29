using Bellatrix.Mobile.IOS;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    public partial class CalculatorPage
    {
        public void AssertAnswer(int answer)
        {
            Answer.EnsureTextIs(answer.ToString());
        }
    }
}