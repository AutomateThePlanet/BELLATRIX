using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.Web;

public class LighthouseAnalysisRunAttribute : BrowserAttribute
{
    public LighthouseAnalysisRunAttribute(bool useHeadless = false)
        : base(useHeadless ? BrowserType.ChromeHeadless : BrowserType.Chrome, Lifecycle.RestartEveryTime)
    {
        IsLighthouseEnabled = true;
    }
}
