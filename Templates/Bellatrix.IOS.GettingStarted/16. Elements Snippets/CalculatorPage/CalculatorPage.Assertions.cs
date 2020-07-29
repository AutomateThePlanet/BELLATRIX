namespace Bellatrix.Mobile.IOS.GettingStarted.Snippets
{
    public partial class CalculatorPage
    {
        public void AssertAnswer(int answer)
        {
            Answer.EnsureTextIs(answer.ToString());
        }
    }
}