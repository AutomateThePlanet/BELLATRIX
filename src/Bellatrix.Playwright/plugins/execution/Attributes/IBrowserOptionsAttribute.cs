using System.Reflection;

namespace Bellatrix.Playwright.plugins.execution.Attributes;

public interface IBrowserOptionsAttribute
{
    public Dictionary<string, object> CreateOptions(MemberInfo memberInfo, Type testClassType);
}
