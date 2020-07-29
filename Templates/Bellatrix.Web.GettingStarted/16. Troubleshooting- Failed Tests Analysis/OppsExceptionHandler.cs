using Bellatrix.Web.ExceptionAnalysation;

namespace Bellatrix.Web.GettingStarted._16._Troubleshooting__Exception_Analysation
{
    public class OppsExceptionHandler : CustomHtmlExceptionHandler
    {
        public override string DetailedIssueExplanation => "The test navigated to a page that was not present. Maybe someone deleted it?";

        protected override string TextToSearchInSource => "Oops! That page can’t be found.";
    }
}
