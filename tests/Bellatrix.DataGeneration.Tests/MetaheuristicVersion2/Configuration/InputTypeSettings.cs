using System.Collections.Generic;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Configuration;
public class InputTypeSettings
{
    public List<string> ValidEquivalenceClasses { get; set; } = new();
    public List<string> InvalidEquivalenceClasses { get; set; } = new();
}
