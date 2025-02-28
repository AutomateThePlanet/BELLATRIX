using Bellatrix.Web.GettingStarted;
using System.Collections.Generic;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
public interface IInputParameter
{
    List<TestValue> TestValues { get; }
}