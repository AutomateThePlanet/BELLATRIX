using Bellatrix.Mobile.IOS;

namespace Bellatrix.Mobile.IOS.GettingStarted.LLM;

public partial class CalculatorPage
{
    public void AssertAnswer(int answer)
    {
        Answer.ValidateTextIs(answer.ToString());
    }
}