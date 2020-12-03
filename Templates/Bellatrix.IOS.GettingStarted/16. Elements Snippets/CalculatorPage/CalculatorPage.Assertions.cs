namespace Bellatrix.Mobile.IOS.GettingStarted.Snippets
{
    public partial class CalculatorPage
    {
        public void AssertAnswer(int answer)
        {
            Answer.ValidateTextIs(answer.ToString());
        }
    }
}