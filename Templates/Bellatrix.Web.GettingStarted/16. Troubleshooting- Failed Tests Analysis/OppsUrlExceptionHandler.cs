using Bellatrix.Web.ExceptionAnalysation;

namespace Bellatrix.Web.GettingStarted._16._Troubleshooting__Exception_Analysation
{
    public class OppsUrlExceptionHandler : UrlExceptionHandler
    {
        protected override string TextToSearchInUrl => "oops";

        public override string DetailedIssueExplanation => "The test navigated to a page that was not present. Maybe someone deleted it?";
    }
}
